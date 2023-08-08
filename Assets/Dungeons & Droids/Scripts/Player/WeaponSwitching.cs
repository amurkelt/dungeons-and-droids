using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class WeaponSwitching : MonoBehaviour
{
    InputAction switching;
    public int selectedWeapon = 0;
    public TextMeshProUGUI ammoInfoText;

    void Start()
    {
        // First option of Weapon switching by mouse scroll

        //switching = new InputAction("Scroll", binding: "<Mouse>/scroll");
        //switching.Enable();

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

        //float scrollValue = switching.ReadValue<Vector2>().y;

        int previousSelected = selectedWeapon;

        //if (scrollValue > 0)
        //{
        //    selectedWeapon++;
        //    if (selectedWeapon == transform.childCount)
        //        selectedWeapon = 0;
        //}
        //else if (scrollValue < 0)
        //{
        //    selectedWeapon--;
        //    if (selectedWeapon == -1)
        //        selectedWeapon = transform.childCount - 1;
        //}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            StartCoroutine(FindObjectOfType<PlayerManager>().SelectedWeapon(1));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 2;
            StartCoroutine(FindObjectOfType<PlayerManager>().SelectedWeapon(2));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 1;
            StartCoroutine(FindObjectOfType<PlayerManager>().SelectedWeapon(3));
        }

        if (previousSelected != selectedWeapon)
            SelectWeapon();

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
