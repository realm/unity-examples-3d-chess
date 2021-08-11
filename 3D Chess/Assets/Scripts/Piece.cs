using Realms;
using System.Linq;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private PieceType pieceType = default;

    private Events events = default;
    private readonly Color selectedColor = new Color(1, 0, 0, 1);
    private readonly Color deselectedColor = new Color(1, 1, 1, 1);

    private Realm realm;
    private PieceEntity pieceEntity;

    public void Select()
    {
        gameObject.GetComponent<Renderer>().material.color = selectedColor;
    }

    public void Deselect()
    {
        gameObject.GetComponent<Renderer>().material.color = deselectedColor;
    }

    public void RemoveFromBoard()
    {
        realm.Write(() =>
        {
            realm.Remove(pieceEntity);
        });
        Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        events.PieceClickedEvent.Invoke(this);
    }

    private void Awake()
    {
        events = FindObjectOfType<Events>();
        realm = Realm.GetInstance();
    }

    private void Start()
    {
        var pieceEntities = realm.All<PieceEntity>();
        if (pieceEntities.Count() > 0)
        {
            pieceEntity = pieceEntities.Where(pieceEntity =>
                            pieceEntity.PositionX == transform.position.x &&
                            pieceEntity.PositionY == transform.position.y &&
                            pieceEntity.PositionZ == transform.position.z).FirstOrDefault();
        }
        if (pieceEntity == null)
        {
            pieceEntity = new PieceEntity(pieceType, transform.position);
            realm.Write(() =>
            {
                realm.Add(pieceEntity);
            });
        }
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            if (pieceEntity != null)
            {
                realm.Write(() =>
                {
                    pieceEntity.Position = transform.position;
                });
            }
            transform.hasChanged = false;
        }
    }
}
