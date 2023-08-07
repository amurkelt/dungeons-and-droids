using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public GameObject impactEffect;
    public float radius = 3;
    public int damageAmount = 5;

    private void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<AudioManager>().Play("Hoverbot_Attack");
        //GameObject impact  = Instantiate(impactEffect, transform.position, Quaternion.identity);
        //Destroy(impact, 2);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.tag == "Player")
            {
                StartCoroutine(FindObjectOfType<PlayerManager>().TakeDamage(damageAmount));
            }
        }
        this.enabled = false;
    }
}
