using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempControl : MonoBehaviour
{
    float speed = 3f;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDireciton;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDireciton = transform.forward * verticalInput + transform.right * horizontalInput;

        transform.Translate(moveDireciton * speed * Time.deltaTime);
    }
}
