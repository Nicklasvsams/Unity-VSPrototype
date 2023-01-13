using UnityEngine;

public class Katana : Weapon
{
    [SerializeField] private SpriteRenderer sprite;

    protected override void Start()
    {
        base.Start();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    // Upgrade
    public int weaponLevel = 0;
    public int weaponDamage = 0;

    // Swing
    public float cooldown = 0.5f;
    private float lastSwing;

    protected override void Update()
    {
        base.Update();
    }

    public override void Swing()
    {
        if (Time.time - lastSwing > cooldown)
        {
            base.Swing();
            lastSwing = Time.time;
        }
    }
}
