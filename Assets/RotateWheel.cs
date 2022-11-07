using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public float _speed;
    public bool _reverseRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_reverseRotation == false)
        {
            this.transform.Rotate(Vector3.right * _speed);

        }
        else
        {
            this.transform.Rotate(Vector3.left * _speed);

        }
    }
}
