using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private PieceSpawner pieceSpawner = default;
    [SerializeField] private GameObject pieces = default;
    [SerializeField] private GameObject loadingIndicator = default;

    private Realm realm;
    private IQueryable<PieceEntity> pieceEntities;
    private IDisposable notificationToken;

    public void MovePiece(Vector3 oldPosition, Vector3 newPosition)
    {
        realm.Write(() =>
        {
            // Check if there is already another piece at the new position and if so, remove it.
            var attackedPiece = FindPieceEntity(newPosition);
            if (attackedPiece != null)
            {
                realm.Remove(attackedPiece);
            }

            // Now move the active piece to it's new position.
            var movedPieceEntity = FindPieceEntity(oldPosition);
            movedPieceEntity.Position = newPosition;
        });
    }

    public void ResetGame()
    {
        pieceSpawner.CreateNewBoard(realm);
    }

    private async void Awake()
    {
        realm = await CreateRealmAsync();
        pieceEntities = realm.All<PieceEntity>();

        // We use notifications here to achieve multiple things:
        // 1. On subscribing we get an initial notification without any changes. This happens because the subscription actually
        //    leads to the query ('realm.All<>()') being executed (on a background thread).
        //    During the initial execution the changes will be `null` but we can use this to set up our board.
        // 2. Every time a `PieceEntity` is inserted into this collection (which happens when the `PieceSpawner` adds `PieceEntity`s
        //    to the Realm) we see a notification here as well and then set up the corresponding `Piece`.
        notificationToken = pieceEntities.SubscribeForNotifications((sender, changes, error) =>
        {
            if (error != null)
            {
                Debug.Log(error.ToString());
                return;
            }

            // Initial notification
            if (changes == null)
            {
                // Check if we actually have `PieceEntity` objects in our Realm (which means we resume a game).
                if (sender.Count > 0)
                {
                    // Each `RealmObject` needs a corresponding `GameObject` to represent it.
                    foreach (PieceEntity pieceEntity in sender)
                    {
                        pieceSpawner.SpawnPiece(pieceEntity, pieces);
                    }
                }
                else
                {
                    // No game was saved, create a new board.
                    pieceSpawner.CreateNewBoard(realm);
                }
                return;
            }

            foreach (var index in changes.InsertedIndices)
            {
                var pieceEntity = sender[index];
                pieceSpawner.SpawnPiece(pieceEntity, pieces);
            }
        });

        Destroy(loadingIndicator);
    }

    private void OnDestroy()
    {
        // Any notifications that we subsccribe to should also include unsubscribing when the `GameObject` is no longer active
        // or alive to avoid notifications being sent to invalid objects.
        notificationToken.Dispose();
    }

    private PieceEntity FindPieceEntity(Vector3 position)
    {
        return pieceEntities
                 .Filter("PositionEntity.X == $0 && PositionEntity.Y == $1 && PositionEntity.Z == $2",
                         position.x, position.y, position.z)
                 .FirstOrDefault();
    }

    private async Task<Realm> CreateRealmAsync()
    {
        var app = App.Create("3d_chess-sjdkk");

        // This example focuses on an introduction to Sync.
        // We will keep the registration simple for now (by just using the email as password).
        // In a different example we will focus on authentication methods, login dialogues, etc.
        var email = PlayerPrefs.GetString(Constants.PlayerPrefsKeys.Name);
        var password = "temporaryPassword";
        try
        {
            await app.EmailPasswordAuth.RegisterUserAsync(email, password);
        }
        catch (AppException appException)
        {
            // If the user is already registered we can just log in. Any other exception needs to be thrown.
            if (!appException.Message.Contains("AccountNameInUse"))
            {
                throw;
            }
        }

        var user = await app.LogInAsync(Credentials.EmailPassword(email, password));
        var partitionKey = PlayerPrefs.GetString(Constants.PlayerPrefsKeys.GameId);
        var syncConfiguration = new SyncConfiguration(partitionKey, user);

        return await Realm.GetInstanceAsync(syncConfiguration);
    }
}
