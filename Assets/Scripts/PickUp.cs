using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] int _itemID;
    PlayerController _playerController;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    void ItemLogic(int _itemID)
    {
        switch (_itemID)
        {
            case 0:
                //_playerController.PickedUpItem(0);
                break;

            case 1:
                //_playerController.PickedUpItem(1);

                break;

            case 2:
                //_playerController.PickedUpItem(2);

                break;

            case 3:
                //_playerController.PickedUpItem(3);

                break;

            case 4:
                //_playerController.PickedUpItem(4);

                break;

            case 5:
                //_playerController.PickedUpItem(5);

                break;
            default:
                Debug.Log("Nothing is activated"); break;

        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            ItemLogic(_itemID);
        }
    }
}
