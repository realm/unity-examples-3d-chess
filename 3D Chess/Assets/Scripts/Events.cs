using UnityEngine;
using UnityEngine.Events;

public class PieceClickedEvent : UnityEvent<Piece> { }
public class SquareClickedEvent : UnityEvent<Vector3> { }

public class Events : MonoBehaviour
{
    public PieceClickedEvent PieceClickedEvent = new PieceClickedEvent();
    public SquareClickedEvent SquareClickedEvent = new SquareClickedEvent();
}
