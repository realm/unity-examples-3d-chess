using Realms;
using UnityEngine;

public class PieceEntity : RealmObject
{
    public PieceType PieceType
    {
        get { return (PieceType)Type; }
        set { Type = (int)value; }
    }

    public Vector3 Position
    {
        get { return new Vector3(PositionX, PositionY, PositionZ); }
        set
        {
            PositionX = value.x;
            PositionY = value.y;
            PositionZ = value.z;
        }
    }

    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }

    private int Type { get; set; }

    public PieceEntity(PieceType type, Vector3 position)
    {
        Type = (int)type;
        PositionX = position.x;
        PositionY = position.y;
        PositionZ = position.z;
    }

    private PieceEntity()
    {

    }
}
