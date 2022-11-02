using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] int _itemID;
    PlayerController _playerController;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

   public void ItemLogic()
    {
        switch (_itemID)
        {
            case 0:
                _playerController.PickedUpItem(0);
                break;

            case 1:
                _playerController.PickedUpItem(1);

                break;

            case 2:
                _playerController.PickedUpItem(2);

                break;

            case 3:
                _playerController.PickedUpItem(3);

                break;

            case 4:
                _playerController.PickedUpItem(4);

                break;

            case 5:
                break;
            default:
                Debug.Log("Nothing is activated"); break;

        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            switch(_itemID)
            {
                case 0:
                    UIManager.Instance.PickupItemPrompt(0);
                    break;
                case 1:
                    UIManager.Instance.PickupItemPrompt(1);
                    break;
                case 2:
                    UIManager.Instance.PickupItemPrompt(2);
                    break;
                        case 3:
                    UIManager.Instance.PickupItemPrompt(3);
                    break;
                case 4:
                    UIManager.Instance.PickupItemPrompt(4);
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.Instance.ClearPickupPrompt();
        }
    }
}
