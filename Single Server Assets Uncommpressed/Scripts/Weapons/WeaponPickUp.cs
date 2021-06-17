using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public GameObject weaponToPickUp;
    public Transform weaponHolder;
    public List<GameObject> weaponsHeld = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(Transform child in weaponHolder)
            {
                child.gameObject.SetActive(false);
            }

            weaponToPickUp.transform.parent = weaponHolder;
            weaponToPickUp.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
