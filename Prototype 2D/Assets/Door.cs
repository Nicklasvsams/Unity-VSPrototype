using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    protected bool isOpen = false;
    protected GameObject door;
    [SerializeField] private string scene;
    [SerializeField] private Sprite openDoor;
    [SerializeField] private int backgroundLayer = 7;

    protected override void Start()
    {
        base.Start();
        door = transform.parent.gameObject;
    }

    protected override void OnCollide(Collider2D coll)
    { 
        if (coll.name == "Player")
        {
            if (isOpen)
            {
                // Change scene
                GameManager.instance.SaveState();
                SceneManager.LoadScene(scene);
            }
        }
    }

    public override void OnInteract()
    {
        base.OnInteract();

        if (!isOpen)
        {
            isOpen = true;
            door.GetComponent<SpriteRenderer>().sprite = openDoor;
            door.layer = backgroundLayer;
        }
    }
}
