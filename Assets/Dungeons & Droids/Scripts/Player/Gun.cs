using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform fpsCam;
    public float range = 20;
    public float impactForce = 150;
    public int damageAmount = 20;

    public int fireRate = 10;
    private float nextTimeToFire = 0;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public int currentAmmo;
    public int maxAmmo = 10;
    public int magazineAmmo = 30;

    public float reloadTime = 2f;
    public bool isReloading;

    public Animator animator;

    InputAction shoot;

    void Start()
    {
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        shoot.AddBinding("<Gamepad>/x");

        shoot.Enable();

        currentAmmo = maxAmmo;
    }
    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitching weaponSwitching = FindObjectOfType<WeaponSwitching>();
        if (weaponSwitching.isSwitching == true)
        {
            StartCoroutine(Wait());
            return;
        }

        if (currentAmmo == 0 && magazineAmmo == 0)
        {
            animator.SetBool("isShooting", false);
            return;
        }

        if (isReloading)
            return;

        bool isShooting = shoot.ReadValue<float>() == 1;
        animator.SetBool("isShooting", isShooting);

        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }

        if (currentAmmo <= 0 && magazineAmmo > 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        AudioManager.instance.Play("Shoot");

        muzzleFlash.Play();

        currentAmmo--;
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.position + fpsCam.forward, fpsCam.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Enemy e = hit.transform.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damageAmount);
                return;
            }

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damageAmount);
                return;
            }

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
            impact.transform.parent = hit.transform;

            // Remove impact effect after 3 seconds
            Destroy(impact, 3);
        }
    }

    IEnumerator Reload()
    {
            isReloading = true;
            AudioManager.instance.Play("Reload");

            animator.SetBool("isReloading", true);

            yield return new WaitForSeconds(reloadTime - .25f);
            animator.SetBool("isReloading", false);
            yield return new WaitForSeconds(.25f);

            if (magazineAmmo >= maxAmmo)
            {
                currentAmmo = maxAmmo;
                magazineAmmo -= maxAmmo;
            }
            else
            {
                currentAmmo = magazineAmmo;
                magazineAmmo = 0;
            }
            isReloading = false;
    }

    IEnumerator Wait()
    {
        WeaponSwitching weaponSwitching = FindObjectOfType<WeaponSwitching>();
        yield return new WaitForSeconds(0.25f);
        weaponSwitching.isSwitching = false;
    }
}