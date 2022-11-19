using UnityEngine;

public class SwitchArea : MonoBehaviour
{
    [SerializeField] int id;
    GameObject playerLocation;

    void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerStay(Collider other) //This part can be tweaked depending on how you want to activate the door
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DoorEvent.current.doorwayTrigger(id, playerLocation.transform.position);
            }
        }
    }
}
