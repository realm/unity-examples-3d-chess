using UnityEngine;
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

public class RealmExample : MonoBehaviour
{
    // Resources:
    // https://github.com/realm/realm-dotnet

    [SerializeField] private int hitCount = 0;

    private Realm realm;
    private HitCountEntity hitCountEntity;

    private void Start()
    {
        // Open a database connection.
        realm = Realm.GetInstance();

        // Read the hit count data from the database.
        hitCountEntity = realm.Find<HitCountEntity>(1);
        if (hitCountEntity != null)
        {
            hitCount = hitCountEntity.Value;
        }
    }

    private void OnApplicationQuit()
    {
        realm.Write(() =>
        {
            // In case the database was empty, create a new `HitCountEntity`.
            if (hitCountEntity == null)
            {
                hitCountEntity = new HitCountEntity(1);
                realm.Add(hitCountEntity);
            }

            // Update the hit count in the database.
            hitCountEntity.Value = hitCount;
        });

        realm.Dispose();
    }

    private void OnMouseDown()
    {
        hitCount++;
    }
}
