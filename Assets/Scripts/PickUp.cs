using System.ComponentModel;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Items[] itemSlot;
    [SerializeField] public bool canPickup; //Get rid of serializeField if bool works correctly
    Items.ItemType itemType;

    void InteractWithPickUps()
    {
        //if Input key is pressed in playercontroller call this function
        {
            switch (itemType)
            {
                case Items.ItemType.revolverAmmo:
                    //Add ammo to the player +amount
                    break;

                case Items.ItemType.shotgunAmmo:
                    //Add ammo to the player
                    break;

                case Items.ItemType.SMGammo:
                    //Add ammo to the player
                    break;

                case Items.ItemType.rifleAmmo:
                    //Add ammo to the player
                    break;

                case Items.ItemType.medKit:
                    //Add health to the player
                    break;

                case Items.ItemType.journal:
                    //
                    break;
            }
        }
        //Destroy the game object or disable it
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canPickup = true;
        }
    }

    [System.Serializable]
    class Items
    {
        [SerializeField] ItemType itemType;
        [SerializeField] int amount;

        public enum ItemType
        {
            revolverAmmo,
            shotgunAmmo,
            SMGammo,
            rifleAmmo,
            medKit,
            journal
        }
    }
}
