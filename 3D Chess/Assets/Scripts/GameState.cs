using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private PieceSpawner pieceSpawner = default;
    [SerializeField] private GameObject pieces = default;

    public void MovePiece(Piece movedPiece, Vector3 newPosition)
    {
        // Check if there is already a piece at the new position and if so, remove it.
        var attackedPiece = FindPiece(newPosition);
        if (attackedPiece != null)
        {
            attackedPiece.RemoveFromBoard();
        }

        // Now move the piece to it's new position.
        movedPiece.transform.position = newPosition;
    }

    public void ResetGame()
    {
        pieceSpawner.CreateNewBoard(pieces);
    }

    private void Awake()
    {
        pieceSpawner.LoadBoard(pieces);
    }

    private Piece FindPiece(Vector3 position)
    {
        return pieces.GetComponentsInChildren<Piece>()
            .FirstOrDefault(piece => piece.transform.position == position);
    }
}
