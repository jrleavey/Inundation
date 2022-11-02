using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private GameObject _player;
    public GameObject _parent;
    void Start()
    {

        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player.GetComponent<PlayerController>().TakeDamage();
        }
        if (other.tag == "Ground")
        {
            Destroy(_parent.gameObject);
        }
    }
}
