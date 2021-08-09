using Realms;
using UnityEngine;

public class PieceEntity : RealmObject
{
    public PieceType PieceType
    {
        get => (PieceType)Type;
        set => Type = (int)value;
    }

    public Vector3 Position
    {
        get => new Vector3(PositionX, PositionY, PositionZ);
        set
        {
            PositionX = value.x;
            PositionY = value.y;
            PositionZ = value.z;
        }
    }

    public float PositionX { get; private set; }
    public float PositionY { get; private set; }
    public float PositionZ { get; private set; }

    private int Type { get; set; }

    public PieceEntity(PieceType type, Vector3 position)
    {
        PieceType = type;
        Position = position;
    }

    private PieceEntity()
    {

    }
}
