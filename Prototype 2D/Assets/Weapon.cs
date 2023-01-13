using UnityEngine;

public class Weapon : Collidable
{
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            Debug.Log(coll.name);
        }
    }

    public virtual void Swing()
    {
        Debug.Log("Swing");
    }
}
