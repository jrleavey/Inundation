using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    void Start()
    {
        DoorEvent.current.OnDoorTriggered += DoorOpen;
    }

    void DoorOpen()
    {
        
    }
}
