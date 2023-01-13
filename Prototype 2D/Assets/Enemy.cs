using System.Collections;
using UnityEngine;

public class Enemy : Collidable
{
    [SerializeField] private Animator animator;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private int health = 10;

    // TODO: Implement Collidable

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            animator.SetTrigger("DeathTrigger");
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    private IEnumerator FlashRed()
    {
        for (int i = 0; i < 3; i++)
        {
            renderer.color = Color.red;
            yield return new WaitForSeconds(0.03f);
            renderer.color = Color.white;
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
