using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    public static DoorEvent current;

    void Start()
    {
        current = this;
    }

    public event Action<Vector3, int> OnDoorTriggered;

    public void doorwayTrigger(int id, Vector3 playerLocation)
    {
        OnDoorTriggered?.Invoke(playerLocation, id);
    }
}
