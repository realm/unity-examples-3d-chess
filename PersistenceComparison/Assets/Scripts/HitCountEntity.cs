using Realms;

public class HitCountEntity : RealmObject
{
    [PrimaryKey]
    public int PrimaryKey { get; set; }
    public int Value { get; set; }

    private HitCountEntity() { }

    public HitCountEntity(int primaryKey)
    {
        PrimaryKey = primaryKey;
    }
}