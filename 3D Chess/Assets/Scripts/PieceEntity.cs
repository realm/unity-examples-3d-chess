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
        get => PositionEntity.ToVector3();
        set => PositionEntity = new Vector3Entity(value);
    }

    private int Type { get; set; }
    private Vector3Entity PositionEntity { get; set; }

    public PieceEntity(PieceType type, Vector3 position)
    {
        PieceType = type;
        Position = position;
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        if (propertyName == nameof(PositionEntity))
        {
            RaisePropertyChanged(nameof(Position));
        }
    }

    private PieceEntity()
    {
    }
}
