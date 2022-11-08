using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField]
    private bool amICriticalHurtbox;
    [SerializeField]
    private GameObject _rootEnemy;
 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Damage (int gundamage)
    {
        if (amICriticalHurtbox == false)
        {
            _rootEnemy.GetComponent<FishermanAI>().Damage(gundamage);

        }
        else
        {
            int criticaldamage = gundamage * 2;
            _rootEnemy.GetComponent<FishermanAI>().Damage(criticaldamage);

        }
    }
}
