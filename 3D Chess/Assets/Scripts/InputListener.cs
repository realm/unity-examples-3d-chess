using UnityEngine;

public class InputListener : MonoBehaviour
{
    [SerializeField] private Events events = default;
    [SerializeField] private GameState gameState = default;

    private Piece activePiece = default;

    private void OnEnable()
    {
        events.PieceClickedEvent.AddListener(PieceClickedListener);
        events.SquareClickedEvent.AddListener(SquareClickedListener);
    }

    private void OnDisable()
    {
        events.PieceClickedEvent.RemoveListener(PieceClickedListener);
        events.SquareClickedEvent.RemoveListener(SquareClickedListener);
    }

    private void PieceClickedListener(Piece piece)
    {
        if (activePiece != null)
        {
            activePiece.Deselect();
        }
        activePiece = piece;
        activePiece.Select();
    }

    private void SquareClickedListener(Vector3 position)
    {
        if (activePiece != null)
        {
            gameState.MovePiece(activePiece, position);
            activePiece.Deselect();
            activePiece = null;
        }
    }
}
