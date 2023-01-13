using UnityEngine;

public class Chest : Collectable
{
    [SerializeField] private GameObject chest;
    [SerializeField] private Sprite emptyChest;
    [SerializeField] private int currencyAmt = 10;
    private int textSize = 40;

    protected override void Start()
    {
        base.Start();
        chest = transform.parent.gameObject;
    }

    public override void OnInteract()
    {
        base.OnInteract();

        if (!isCollected)
        {
            isCollected = true;
            chest.GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.Log(currencyAmt + " currency obtained");
            GameManager.instance.currency += currencyAmt;
            GameManager.instance.ShowText(currencyAmt + " currency", textSize, Color.yellow, transform.position, Vector3.up * 100, 1.0f);
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        
    }
}