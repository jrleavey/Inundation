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

    public event Action OnDoorTriggered;

    public void doorwayTrigger(object sender)
    {
        OnDoorTriggered?.Invoke();
    }
}
