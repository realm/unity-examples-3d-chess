using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private Piece prefabPawnWhite = default;
    [SerializeField] private Piece prefabRookWhite = default;
    [SerializeField] private Piece prefabKnightWhite = default;
    [SerializeField] private Piece prefabBishopWhite = default;
    [SerializeField] private Piece prefabQueenWhite = default;
    [SerializeField] private Piece prefabKingWhite = default;
    [SerializeField] private Piece prefabPawnBlack = default;
    [SerializeField] private Piece prefabRookBlack = default;
    [SerializeField] private Piece prefabKnightBlack = default;
    [SerializeField] private Piece prefabBishopBlack = default;
    [SerializeField] private Piece prefabQueenBlack = default;
    [SerializeField] private Piece prefabKingBlack = default;

    public void SpawnPiece(PieceType pieceType, Vector3 position, GameObject parent, PieceMovement pieceMovement)
    {
        Piece piecePrefab = default;

        switch (pieceType)
        {
            case PieceType.WhitePawn:
                piecePrefab = prefabPawnWhite;
                break;
            case PieceType.WhiteRook:
                piecePrefab = prefabRookWhite;
                break;
            case PieceType.WhiteKnight:
                piecePrefab = prefabKnightWhite;
                break;
            case PieceType.WhiteBishop:
                piecePrefab = prefabBishopWhite;
                break;
            case PieceType.WhiteQueen:
                piecePrefab = prefabQueenWhite;
                break;
            case PieceType.WhiteKing:
                piecePrefab = prefabKingWhite;
                break;
            case PieceType.BlackPawn:
                piecePrefab = prefabPawnBlack;
                break;
            case PieceType.BlackRook:
                piecePrefab = prefabRookBlack;
                break;
            case PieceType.BlackKnight:
                piecePrefab = prefabKnightBlack;
                break;
            case PieceType.BlackBishop:
                piecePrefab = prefabBishopBlack;
                break;
            case PieceType.BlackQueen:
                piecePrefab = prefabQueenBlack;
                break;
            case PieceType.BlackKing:
                piecePrefab = prefabKingBlack;
                break;
        }

        Piece pieceInstance = Instantiate(piecePrefab, position, Quaternion.identity, parent.transform);
        pieceInstance.pieceMovement = pieceMovement;
    }

    public void CreateGameObjects(GameObject parent, PieceMovement pieceMovement)
    {
        SpawnPiece(PieceType.WhiteRook, new Vector3(1, 0, 1), parent, pieceMovement);
        SpawnPiece(PieceType.WhiteKnight, new Vector3(2, 0, 1), parent, pieceMovement);
        SpawnPiece(PieceType.WhiteBishop, new Vector3(3, 0, 1), parent, pieceMovement);
        SpawnPiece(PieceType.WhiteQueen, new Vector3(4, 0, 1), parent, pieceMovement);
        SpawnPiece(PieceType.WhiteKing, new Vector3(5, 0, 1), parent, pieceMovement);
        SpawnPiece(PieceType.WhiteBishop, new Vector3(6, 0, 1), parent, pieceMovement);
        SpawnPiece(PieceType.WhiteKnight, new Vector3(7, 0, 1), parent, pieceMovement);
        SpawnPiece(PieceType.WhiteRook, new Vector3(8, 0, 1), parent, pieceMovement);

        SpawnPiece(PieceType.WhitePawn, new Vector3(1, 0, 2), parent, pieceMovement);
        SpawnPiece(PieceType.WhitePawn, new Vector3(2, 0, 2), parent, pieceMovement);
        SpawnPiece(PieceType.WhitePawn, new Vector3(3, 0, 2), parent, pieceMovement);
        SpawnPiece(PieceType.WhitePawn, new Vector3(4, 0, 2), parent, pieceMovement);
        SpawnPiece(PieceType.WhitePawn, new Vector3(5, 0, 2), parent, pieceMovement);
        SpawnPiece(PieceType.WhitePawn, new Vector3(6, 0, 2), parent, pieceMovement);
        SpawnPiece(PieceType.WhitePawn, new Vector3(7, 0, 2), parent, pieceMovement);
        SpawnPiece(PieceType.WhitePawn, new Vector3(8, 0, 2), parent, pieceMovement);

        SpawnPiece(PieceType.BlackPawn, new Vector3(1, 0, 7), parent, pieceMovement);
        SpawnPiece(PieceType.BlackPawn, new Vector3(2, 0, 7), parent, pieceMovement);
        SpawnPiece(PieceType.BlackPawn, new Vector3(3, 0, 7), parent, pieceMovement);
        SpawnPiece(PieceType.BlackPawn, new Vector3(4, 0, 7), parent, pieceMovement);
        SpawnPiece(PieceType.BlackPawn, new Vector3(5, 0, 7), parent, pieceMovement);
        SpawnPiece(PieceType.BlackPawn, new Vector3(6, 0, 7), parent, pieceMovement);
        SpawnPiece(PieceType.BlackPawn, new Vector3(7, 0, 7), parent, pieceMovement);
        SpawnPiece(PieceType.BlackPawn, new Vector3(8, 0, 7), parent, pieceMovement);

        SpawnPiece(PieceType.BlackRook, new Vector3(1, 0, 8), parent, pieceMovement);
        SpawnPiece(PieceType.BlackKnight, new Vector3(2, 0, 8), parent, pieceMovement);
        SpawnPiece(PieceType.BlackBishop, new Vector3(3, 0, 8), parent, pieceMovement);
        SpawnPiece(PieceType.BlackQueen, new Vector3(4, 0, 8), parent, pieceMovement);
        SpawnPiece(PieceType.BlackKing, new Vector3(5, 0, 8), parent, pieceMovement);
        SpawnPiece(PieceType.BlackBishop, new Vector3(6, 0, 8), parent, pieceMovement);
        SpawnPiece(PieceType.BlackKnight, new Vector3(7, 0, 8), parent, pieceMovement);
        SpawnPiece(PieceType.BlackRook, new Vector3(8, 0, 8), parent, pieceMovement);
    }
}
