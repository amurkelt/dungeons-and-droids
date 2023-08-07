using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public GameObject impactEffect;

    public void TakeDamage (float amount) 
    { 
        health -= amount;
        if (health < 0f)
        {
            Die();
        }
    }

    void Die()
    {
        FindObjectOfType<AudioManager>().Play("Explosion");
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);
        Destroy(gameObject);
    }
}
