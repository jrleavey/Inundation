using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempControl : MonoBehaviour
{
    float speed = 3f;
    float jumpPower = 5f;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDireciton;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDireciton = (transform.forward * horizontalInput) + (transform.right * -verticalInput);

        transform.Translate(moveDireciton * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
}
