using UnityEngine;

public class Piece : MonoBehaviour
{
    private Events events = default;

    private readonly Color selectedColor = new Color(1, 0, 0, 1);
    private readonly Color deselectedColor = new Color(1, 1, 1, 1);

    public PieceEntity Entity { get; set; }

    public void Select()
    {
        gameObject.GetComponent<Renderer>().material.color = selectedColor;
    }

    public void Deselect()
    {
        gameObject.GetComponent<Renderer>().material.color = deselectedColor;
    }

    private void OnMouseDown()
    {
        events.PieceClickedEvent.Invoke(this);
    }

    private void Awake()
    {
        events = FindObjectOfType<Events>();
    }

    private void Start()
    {
        Entity.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Entity.IsValid):
                if (!Entity.IsValid)
                {
                    Destroy(gameObject);
                }
                break;
            case nameof(Entity.Position):
                gameObject.transform.position = Entity.Position;
                break;
        }
    }
}
