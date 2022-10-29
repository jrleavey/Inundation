using System;
using UnityEngine;
using static PlayerController;

public class Particles : MonoBehaviour
{
    [SerializeField] ParticleSystem shotgunffect;
    [SerializeField] ParticleSystem smgEffect;
    [SerializeField] ParticleSystem handgunEffect;
    [SerializeField] ParticleSystem rifleEffect;
    [SerializeField] GameObject hitEffect;
    [SerializeField] LayerMask enemyLayerMask;

    public void PlayMuzzleFlash(ActiveWeapon weaponType)
    {
        switch (weaponType)
        {
            case ActiveWeapon.Handgun:
                handgunEffect.Play();
                break;

            case ActiveWeapon.Shotgun:
                shotgunffect.Play();
                break;

            case ActiveWeapon.SMG:
                smgEffect.Play();
                break;

            case ActiveWeapon.Rifle:
                rifleEffect.Play();
                break;

            default:
                Debug.Log("Error");
                break;
        }
    }

    public void HitEffectOnEnemies()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 100f, enemyLayerMask))
        {
            CreateHitImpact(hit);
        }
    }

    public void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact =Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.5f);
    }
}
