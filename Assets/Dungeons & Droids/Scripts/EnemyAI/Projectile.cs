using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public GameObject impactEffect;
    public float radius = 3;
    public int damageAmount = 5;
    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 10f;
        bulletRigidbody.velocity = transform.forward * speed;
        bulletRigidbody.AddForce(transform.forward * 30f, ForceMode.Impulse);
        bulletRigidbody.AddForce(transform.up * 7, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        //Destroy(impact, 2);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Player"))
            {
                StartCoroutine(FindObjectOfType<PlayerManager>().TakeDamage(damageAmount));
            }
        }
        this.enabled = false;
    }
}
