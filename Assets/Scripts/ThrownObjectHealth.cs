using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjectHealth : MonoBehaviour
{
    public int health;
    public int _speed;
    private void Start()
    {
        health = 1;
    }
    public void Damage(int gundamage)
    {
        health -= gundamage;
    }
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
