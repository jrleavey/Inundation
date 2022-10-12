using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static PickUp;

public class PickUp : MonoBehaviour
{
    public enum ItemLists
    {
        revolverAmmo,
        shotgunAmmo,
        SMGAmmo,
        rifleAmmo,
        medKit,
        journal
    }

    [SerializeField] ItemProperty[] itemLists;
    [SerializeField] int amount;
    PlayerController playerController;
    bool canPickup;
    ItemLists itemlists;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void InteractWithPickUps()
    {
       if(canPickup)
        {
            switch (itemlists)
            {
                case ItemLists.revolverAmmo:
                    // playerController.ammo+=amount;
                    break;

                case ItemLists.shotgunAmmo:
                    // playerController.ammo+=amount;
                    break;

                case ItemLists.SMGAmmo:
                    // playerController.ammo+=amount;
                    break;

                case ItemLists.rifleAmmo:
                    // playerController.ammo+=amount;
                    break;

                case ItemLists.medKit:
                    // playerController.health+=amount;
                    break;

                case ItemLists.journal:
                    // Display journal function
                    break;
                    default:
                    Debug.Log("Nothing is activated"); break;

            }
        }
        //Destroy the game object or disable it
    }
    [System.Serializable]
    public class ItemProperty
    {
        public ItemLists itemList;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canPickup = true;
        }
    }
}
