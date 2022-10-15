using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PickUp1 : MonoBehaviour
{
    private PlayerController _playerController;

    [SerializeField]
    private int _itemID;
    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void ItemLogic(int _itemID)
    {
        switch (_itemID)
        {
            case 0: // Handgun Bullets
                _playerController.PickedUpItem(0);
                break;
            case 1: // Shotgun Bullets
                _playerController.PickedUpItem(1);
                break;
            case 2: // SMG Bullets
                _playerController.PickedUpItem(2);
                break;
            case 3: // Rifle Bullets
                _playerController.PickedUpItem(3);
                break;
            case 4: // Health
                _playerController.PickedUpItem(4);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ItemLogic(_itemID);
        }
    }
}

