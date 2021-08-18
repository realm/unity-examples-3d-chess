using Realms;
using UnityEngine;

public class Vector3Entity : EmbeddedObject
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }

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
