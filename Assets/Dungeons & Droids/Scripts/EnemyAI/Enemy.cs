using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHP = 100;
    public GameObject projectile;
    public Transform spawnBulletPosition;

    public Animator animator;

    PlayerMovement player;
    public void Shoot()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        Vector3 aimDir = (player.transform.position - spawnBulletPosition.position).normalized;
        GameObject bullet = Instantiate(projectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
        FindObjectOfType<AudioManager>().Play("Hoverbot_Attack");

        // Destroy bullets after 0,4 seconds
        Destroy(bullet, .4f);
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHP -= damageAmount;
        if (enemyHP <= 0)
        {
            animator.SetTrigger("death");
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(FindObjectOfType<PlayerManager>().EnemyCount(1));
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }    
}
