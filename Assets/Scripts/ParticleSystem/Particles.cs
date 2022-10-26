using System.Collections;
using UnityEngine;
using static PlayerController;

public class Particles : MonoBehaviour
{
    public enum WeaponType
    {
        Handgun,
        Shotgun,
        SMG,
        AR
    }

   
    [SerializeField] WeaponType weaponType;
    [SerializeField] ParticleSystem shotgunffect;
    [SerializeField] ParticleSystem smgEffect;
    [SerializeField] ParticleSystem handgunEffect;
    [SerializeField] ParticleSystem rifleEffect;
    [SerializeField] float hiteffectAfterAfewSecond;

    private void Update()
    {
        PlayMuzzleFlash(weaponType);
    }
    public void PlayMuzzleFlash(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Handgun:
                handgunEffect.Play();
                break;

            case WeaponType.Shotgun:
                shotgunffect.Play();
                break;

            case WeaponType.SMG:
                smgEffect.Play();
                break;

            case WeaponType.AR:
                rifleEffect.Play();
                break;

            default:
                Debug.Log("Error");
                break;
        }
    }

   
}
