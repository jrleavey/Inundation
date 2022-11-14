using UnityEngine;

public class GunDecals : MonoBehaviour
{
    [Header("Basic Raycast attributes")]
    [SerializeField] LayerMask NeutralObjects;
    [SerializeField] float gunEffectRange;

    [SerializeField] GameObject bulletDecalPrefab;
    [SerializeField] float decalExistingTime; 

    GameObject RaycastHolders; //the variable in PlayerController Script
    void GenerateGunDecal()
    {
        RaycastHit hit;

        if (Physics.Raycast(RaycastHolders.transform.position, RaycastHolders.transform.forward, out hit, gunEffectRange, NeutralObjects))
        {
            if (hit.collider.CompareTag("GroundORWall")) // or hit.collider
            {
                GameObject decal= Instantiate(bulletDecalPrefab, hit.point, Quaternion.LookRotation(hit.normal));

                decal.transform.position += decal.transform.forward *  0.0001f; //Prevent two objects(wall or ground, bullet marks) Z fighting

                Destroy(decal, decalExistingTime); //after N seconds decal disappears
            }

            if (hit.collider.CompareTag("WaterOR"))
            {
                //Hmm MaybeInstantiate object playing water splash animation and destroy it?

                //Instantiate GameObject

                //Play object's animation or make it as autoplay something so that we don't need to trigger manually

                //Destroy the object after N seconds so that it doesn't play again
            }

        }
    }
}
