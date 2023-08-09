using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scope : MonoBehaviour
{
    public Animator animator;
    private bool isScoped = false;
    public Camera fpsCam;
    InputAction scope;

    void Start()
    {
        scope = new InputAction("Scope", binding: "<mouse>/rightButton");

        scope.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitching weaponSwitching = FindObjectOfType<WeaponSwitching>();
        if (weaponSwitching.isSwitching == true)
        {
            OnUnscoped();
            weaponSwitching.isSwitching = false;
        }

        Gun gun = FindObjectOfType<Gun>();
        if (gun.isReloading || gun.currentAmmo == 0)
        {
            OnUnscoped();
        }
        else
        {
            // Scope on Button Down and Up
            //if (Input.GetMouseButtonDown(1))
            //{
            //    StartCoroutine(OnScoped());
            //}

            //else if (Input.GetMouseButtonUp(1))
            //{
            //    OnUnscoped();
            //}

            // Scope is a toggle as in CS:GO
            if (scope.triggered)
            {
                isScoped = !isScoped;

                if (isScoped)
                {
                    StartCoroutine(OnScoped());
                }
                else
                {
                    OnUnscoped();
                }
            }
        }
    }
    IEnumerator OnScoped()
    {
        animator.SetBool("isScoped", true);
        yield return new WaitForSeconds(0.25f);
        fpsCam.fieldOfView = 45;
    }
    void OnUnscoped()
    {
        animator.SetBool("isScoped", false);
        fpsCam.fieldOfView = 60;
    }
}
