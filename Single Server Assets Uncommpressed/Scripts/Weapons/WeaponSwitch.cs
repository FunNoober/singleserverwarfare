using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitch : MonoBehaviour
{
    public int currentWeapon = 0;
    public InputAction switchWeapon;

    void Update()
    {
        int oldWeapon = currentWeapon;

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (currentWeapon >= transform.childCount - 1)
                currentWeapon = 0;
            else
                currentWeapon++;
        }

        if (oldWeapon != currentWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == currentWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
