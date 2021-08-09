using Realms;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private PieceSpawner pieceSpawner = default;
    [SerializeField] private GameObject pieces = default;

    private Realm realm;
    private IQueryable<PieceEntity> pieceEntities;

    public void MovePiece(Piece movedPiece, Vector3 newPosition)
    {
        // Check if there is already a piece at the new position and if so, destroy it.
        var attackedPiece = FindPiece(newPosition);
        if (attackedPiece != null)
        {
            var attackedPieceEntity = FindPieceEntity(newPosition);
            realm.Write(() =>
            {
                realm.Remove(attackedPieceEntity);
            });
            Destroy(attackedPiece.gameObject);
        }

        // Update the movedPiece's RealmObject.
        var oldPosition = movedPiece.transform.position;
        var pieceEntity = FindPieceEntity(oldPosition);
        realm.Write(() =>
        {
            pieceEntity.Position = newPosition;
        });

        // Update the movedPiece's GameObject.
        movedPiece.transform.position = newPosition;
    }

    public void ResetGame()
    {
        // Destroy all GameObjects.
        foreach (var piece in pieces.GetComponentsInChildren<Piece>())
        {
            Destroy(piece.gameObject);
        }

        // Re-create all RealmObjects with their initial position.
        pieceEntities = Persistence.ResetDatabase(realm);

        // Recreate the GameObjects.
        CreateGameObjects();
    }

    private void Awake()
    {
        realm = Realm.GetInstance();
        pieceEntities = realm.All<PieceEntity>();

        // Check if we already have PieceEntity's (which means we resume a game).
        if (pieceEntities.Count() == 0)
        {
            // No game was saved, create the necessary RealmObjects.
            pieceEntities = Persistence.ResetDatabase(realm);
        }
        CreateGameObjects();
    }

    private void CreateGameObjects()
    {
        // Each RealmObject needs a corresponding GameObject to represent it.
        foreach (PieceEntity pieceEntity in pieceEntities)
        {
            PieceType type = pieceEntity.PieceType;
            Vector3 position = pieceEntity.Position;
            pieceSpawner.SpawnPiece(type, position, pieces);
        }
    }

    private Piece FindPiece(Vector3 position)
    {
        return pieces.GetComponentsInChildren<Piece>()
            .FirstOrDefault(piece => piece.transform.position == position);
    }

    private PieceEntity FindPieceEntity(Vector3 position)
    {
        return pieceEntities.FirstOrDefault(piece =>
                            piece.PositionX == position.x &&
                            piece.PositionY == position.y &&
                            piece.PositionZ == position.z);
    }
}
