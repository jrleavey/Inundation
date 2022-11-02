using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFadeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(.9f);
        Destroy(this.gameObject);
    }
}
