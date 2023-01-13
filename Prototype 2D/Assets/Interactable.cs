using UnityEngine;

public class Interactable : Collidable
{
    private int interactedLayer = 13;

    protected override void Start()
    {
        base.Start();
    }

    public virtual void OnInteract()
    {
        gameObject.layer = interactedLayer;
        Debug.Log("Interacted with object");
    }
}
