using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class WeaponSwitching : MonoBehaviour
{
    InputAction switching;
    public bool isSwitching;
    public int selectedWeapon = 0;
    public TextMeshProUGUI ammoInfoText;

    void Start()
    {
        switching = new InputAction("Scroll", binding: "<Mouse>/scroll");
        switching.Enable();

        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        Gun gun = FindObjectOfType<Gun>();

        if (gun.currentAmmo <= 0)
        {
            ammoInfoText.text = "0 / " + gun.magazineAmmo;
        }
        else
        {
            ammoInfoText.text = gun.currentAmmo + " / " + gun.magazineAmmo;
        }

        // Switching by Mouse Scroll
        float scrollValue = switching.ReadValue<Vector2>().y;

        int previousSelected = selectedWeapon;

        if (scrollValue > 0)
        {
            selectedWeapon++;
            isSwitching = true;
            if (selectedWeapon == transform.childCount)
                selectedWeapon = 0;
        }
        else if (scrollValue < 0)
        {
            selectedWeapon--;
            isSwitching = true;
            if (selectedWeapon == -1)
                selectedWeapon = transform.childCount - 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            isSwitching = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            selectedWeapon = 1;
            isSwitching = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
            isSwitching = true;
        }

        if (previousSelected != selectedWeapon)
            SelectWeapon();

        // Show WeaponIcon in GUI
        switch (selectedWeapon)
        {
            case 0:
                StartCoroutine(FindObjectOfType<PlayerManager>().SelectedWeapon(1));
                break;
            case 1:
                StartCoroutine(FindObjectOfType<PlayerManager>().SelectedWeapon(2));
                break;
            case 2:
                StartCoroutine(FindObjectOfType<PlayerManager>().SelectedWeapon(3));
                break;
        }

    }

    private void SelectWeapon()
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
        transform.GetChild(selectedWeapon).gameObject.SetActive(true);
    }
}
