using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTimerDecal : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(despawntimer());
    }
    IEnumerator despawntimer()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
