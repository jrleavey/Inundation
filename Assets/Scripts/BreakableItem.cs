using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableItem : MonoBehaviour
{
    public int _breakableID;
    public int breakforce;
    public GameObject[] breakableVersionPrefab;

    private bool droppedanItem = false;

    private int[] itemDropChange =
    {
        20,
        15,
        8,
        5,
        7,
        45,
    };
    public GameObject[] items;
    void Start()
    {
        
    }

    public void Damage(int gundamage)
    {
        Break();
    }
    private IEnumerator DropItems()
    {
        if (droppedanItem == false)
        {
            droppedanItem = true;
            Debug.Log("CalledDropItems");
            int randomItem = randomTable();
            GameObject newItem = Instantiate(items[randomItem], this.transform.position, Quaternion.identity);
            newItem.transform.parent = null;
            yield return new WaitForSeconds(3f);
        }
    }
    int randomTable()
    {
        int rng = Random.Range(1, 101);
        for (int i = 0; i < itemDropChange.Length; i++)
        {
            if (rng <= itemDropChange[i])
            {
                return i;
            }
            else
            {
                rng -= itemDropChange[i];
            }
        }
        return 0;
    }
    public void Break()
    {
        GameObject frac = Instantiate(breakableVersionPrefab[_breakableID], transform.position, transform.rotation);

        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakforce;
            rb.AddForce(force);
        }
        StartCoroutine(DropItems());
        Destroy(this.gameObject);
    }
}
