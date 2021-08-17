using Realms;
using System;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private PieceSpawner pieceSpawner = default;
    [SerializeField] private GameObject pieces = default;

    private Realm realm;
    private IQueryable<PieceEntity> pieceEntities;
    private IDisposable notificationToken;

    public void MovePiece(Vector3 oldPosition, Vector3 newPosition)
    {
        realm.Write(() =>
        {
            // Check if there is already a piece at the new position and if so, remove it.
            var attackedPiece = FindPieceEntity(newPosition);

            // Now move the piece to it's new position.
            var movedPieceEntity = FindPieceEntity(oldPosition);
            if (attackedPiece != null)
            {
                realm.Remove(attackedPiece);
            }
            movedPieceEntity.Position = newPosition;
        });
    }

    public void ResetGame()
    {
        pieceSpawner.CreateNewBoard(realm);
    }

    private void Awake()
    {
        realm = Realm.GetInstance();
        pieceEntities = realm.All<PieceEntity>();
        notificationToken = pieceEntities.SubscribeForNotifications((sender, changes, error) =>
        {
            if (error != null)
            {
                Debug.Log(error.ToString());
                return;
            }

            // Initial notification
            // Happens when subscribing, which causes the query to actually being executed (but on a background thread).
            if (changes == null)
            {
                // Check if we actually have PieceEntity's (which means we resume a game).
                if (sender.Count > 0)
                {
                    // Each RealmObject needs a corresponding GameObject to represent it.
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
    }

    private void OnDestroy()
    {
        notificationToken.Dispose();
    }

    private PieceEntity FindPieceEntity(Vector3 position)
    {
        return pieceEntities
                 .Filter("PositionEntity.X == $0 && PositionEntity.Y == $1 && PositionEntity.Z == $2",
                         position.x, position.y, position.z)
                 .FirstOrDefault();
    }
}
