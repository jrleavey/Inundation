using UnityEngine;
using static PlayerController;

public class Particles : MonoBehaviour
{
    [SerializeField] ParticleSystem shotgunffect;
    [SerializeField] ParticleSystem smgEffect;
    [SerializeField] ParticleSystem handgunEffect;
    [SerializeField] ParticleSystem rifleEffect;

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
}
