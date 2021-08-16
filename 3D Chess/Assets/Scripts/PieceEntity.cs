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
        get => positionEntity.ToVector3();
        set => positionEntity = new Vector3Entity(value);
    }

    private int Type { get; set; }
    private Vector3Entity positionEntity { get; set; }

    public PieceEntity(PieceType type, Vector3 position)
    {
        PieceType = type;
        Position = position;
    }

    private PieceEntity()
    {

    }
}

public class Vector3Entity : EmbeddedObject
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    private Vector3Entity()
    {

    }

    public Vector3Entity(Vector3 vector)
    {
        X = vector.x;
        Y = vector.y;
        Z = vector.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(X, Y, Z);
    }
}
