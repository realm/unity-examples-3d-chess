using Realms;
using System.Linq;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private Piece prefabBlackBishop = default;
    [SerializeField] private Piece prefabBlackKing = default;
    [SerializeField] private Piece prefabBlackKnight = default;
    [SerializeField] private Piece prefabBlackPawn = default;
    [SerializeField] private Piece prefabBlackQueen = default;
    [SerializeField] private Piece prefabBlackRook = default;

    [SerializeField] private Piece prefabWhiteBishop = default;
    [SerializeField] private Piece prefabWhiteKing = default;
    [SerializeField] private Piece prefabWhiteKnight = default;
    [SerializeField] private Piece prefabWhitePawn = default;
    [SerializeField] private Piece prefabWhiteQueen = default;
    [SerializeField] private Piece prefabWhiteRook = default;

    public void LoadBoard(GameObject parent)
    {
        var realm = Realm.GetInstance();
        var pieceEntities = realm.All<PieceEntity>();

        // Check if we actually have PieceEntity's (which means we resume a game).
        if (pieceEntities.Count() > 0)
        {
            // Each RealmObject needs a corresponding GameObject to represent it.
            foreach (PieceEntity pieceEntity in pieceEntities)
            {
                PieceType type = pieceEntity.PieceType;
                Vector3 position = pieceEntity.Position;
                SpawnPiece(type, position, parent);
            }
        }
        else
        {
            // No game was saved, create a new board.
            CreateNewBoard(parent);
        }
    }

    public void CreateNewBoard(GameObject parent)
    {
        // First clear the board, then recreate it from scratch.
        foreach (var piece in parent.GetComponentsInChildren<Piece>())
        {
            Destroy(piece.gameObject);
        }

        SpawnPiece(PieceType.WhiteRook, new Vector3(1, 0, 1), parent);
        SpawnPiece(PieceType.WhiteKnight, new Vector3(2, 0, 1), parent);
        SpawnPiece(PieceType.WhiteBishop, new Vector3(3, 0, 1), parent);
        SpawnPiece(PieceType.WhiteQueen, new Vector3(4, 0, 1), parent);
        SpawnPiece(PieceType.WhiteKing, new Vector3(5, 0, 1), parent);
        SpawnPiece(PieceType.WhiteBishop, new Vector3(6, 0, 1), parent);
        SpawnPiece(PieceType.WhiteKnight, new Vector3(7, 0, 1), parent);
        SpawnPiece(PieceType.WhiteRook, new Vector3(8, 0, 1), parent);

        SpawnPiece(PieceType.WhitePawn, new Vector3(1, 0, 2), parent);
        SpawnPiece(PieceType.WhitePawn, new Vector3(2, 0, 2), parent);
        SpawnPiece(PieceType.WhitePawn, new Vector3(3, 0, 2), parent);
        SpawnPiece(PieceType.WhitePawn, new Vector3(4, 0, 2), parent);
        SpawnPiece(PieceType.WhitePawn, new Vector3(5, 0, 2), parent);
        SpawnPiece(PieceType.WhitePawn, new Vector3(6, 0, 2), parent);
        SpawnPiece(PieceType.WhitePawn, new Vector3(7, 0, 2), parent);
        SpawnPiece(PieceType.WhitePawn, new Vector3(8, 0, 2), parent);

        SpawnPiece(PieceType.BlackPawn, new Vector3(1, 0, 7), parent);
        SpawnPiece(PieceType.BlackPawn, new Vector3(2, 0, 7), parent);
        SpawnPiece(PieceType.BlackPawn, new Vector3(3, 0, 7), parent);
        SpawnPiece(PieceType.BlackPawn, new Vector3(4, 0, 7), parent);
        SpawnPiece(PieceType.BlackPawn, new Vector3(5, 0, 7), parent);
        SpawnPiece(PieceType.BlackPawn, new Vector3(6, 0, 7), parent);
        SpawnPiece(PieceType.BlackPawn, new Vector3(7, 0, 7), parent);
        SpawnPiece(PieceType.BlackPawn, new Vector3(8, 0, 7), parent);

        SpawnPiece(PieceType.BlackRook, new Vector3(1, 0, 8), parent);
        SpawnPiece(PieceType.BlackKnight, new Vector3(2, 0, 8), parent);
        SpawnPiece(PieceType.BlackBishop, new Vector3(3, 0, 8), parent);
        SpawnPiece(PieceType.BlackQueen, new Vector3(4, 0, 8), parent);
        SpawnPiece(PieceType.BlackKing, new Vector3(5, 0, 8), parent);
        SpawnPiece(PieceType.BlackBishop, new Vector3(6, 0, 8), parent);
        SpawnPiece(PieceType.BlackKnight, new Vector3(7, 0, 8), parent);
        SpawnPiece(PieceType.BlackRook, new Vector3(8, 0, 8), parent);
    }

    private void SpawnPiece(PieceType pieceType, Vector3 position, GameObject parent)
    {
        var piecePrefab = pieceType switch
        {
            PieceType.BlackBishop => prefabBlackBishop,
            PieceType.BlackKing => prefabBlackKing,
            PieceType.BlackKnight => prefabBlackKnight,
            PieceType.BlackPawn => prefabBlackPawn,
            PieceType.BlackQueen => prefabBlackQueen,
            PieceType.BlackRook => prefabBlackRook,
            PieceType.WhiteBishop => prefabWhiteBishop,
            PieceType.WhiteKing => prefabWhiteKing,
            PieceType.WhiteKnight => prefabWhiteKnight,
            PieceType.WhitePawn => prefabWhitePawn,
            PieceType.WhiteQueen => prefabWhiteQueen,
            PieceType.WhiteRook => prefabWhiteRook,
            _ => throw new System.Exception("Invalid piece type.")
        };

        Instantiate(piecePrefab, position, Quaternion.identity, parent.transform);
    }
}
