using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] float speed = 1f; 
    [SerializeField] float roationAmount = 90f;
    [SerializeField] float forwardDireciton = 0;
    [SerializeField] bool isItRotatingDoor = true;
    [SerializeField] bool isOpen = false;

    Vector3 StartRotation;
    Vector3 PlayerStandardForward;
    Coroutine animCoroutine;

    void Start()
    {
        DoorEvent.current.OnDoorTriggered += DoorOpen;
        StartRotation = transform.rotation.eulerAngles;
        PlayerStandardForward = transform.forward;
    }

    void DoorOpen(Vector3 playerPostion, int id)
    {
        if (this.id == id)//Swithch area id and door id must match to run the logic
        {
            if (!isOpen)
            {
                if (animCoroutine != null)
                {
                    StopCoroutine(animCoroutine);
                }
            }

            if (isItRotatingDoor)
            {
                float dot = Vector3.Dot(PlayerStandardForward, (playerPostion - transform.position).normalized);
                Debug.Log($"Dot:{dot.ToString("N3")}");
                animCoroutine = StartCoroutine(DoorOpenMovement(dot));
            }
        }
    }
    IEnumerator DoorOpenMovement(float dot)
    {
        Quaternion startRotation = transform.rotation; //RotationStart
        Quaternion endRotation; //RotationEnd

        if (dot >= forwardDireciton)
        {
            endRotation = Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y + roationAmount, StartRotation.z));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y - roationAmount, StartRotation.z));
        }

        isOpen = true;

        float time = 0;
        while (time < 1) //timer
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);//Rotate the door
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    private void OnDestroy() => DoorEvent.current.OnDoorTriggered -= DoorOpen;

}

