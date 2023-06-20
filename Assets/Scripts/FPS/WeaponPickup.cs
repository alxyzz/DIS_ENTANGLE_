using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerScript p = other.gameObject.GetComponent<PlayerScript>();
            if (p != null)
            {
                p.PickWeaponUp();
                Destroy(gameObject);
            }
        }
    }
}
