using UnityEngine;

public class Particles : MonoBehaviour
{
    //ParticleEventSystem shotgunffect
    //ParticleEventSystem smgEffect
    //ParticleEventSystem pistolEffect
    //ParticleEventSystem arEffect


    //Particle
    //float hiteffectAfterAfewSecond
    //
    void Start()
    {
        ParticleEventSystem.current.muzzleEvent += PlayMuzzleEvent;
        ParticleEventSystem.current.enemyHitEvent += PlayEnemyHitEffect;
    }

    void PlayMuzzleEvent()
    {
        //GameObject impact;
        //switch(weaponID)
        //{
        //case: 0
        //playmuzzleFlasheffect.Play();
        //impact=Instantiate(shoutgunHiteffect, hit.point, QuaternionLook(hit.normal));
        //Destroy(impact, 0.1f);
        //Play shotgun smoke effect

        //case: 1
        //playmuzzleFlasheffect.Play();
        //impact=Instantiate(pistolHitEffect, hit.point, QuaternionLook(hit.normal));
        //Destroy(impact, 0.1f);

        //Play pistol smoke effect



        //case: 2
        //playmuzzleFlasheffect.Play();
        //impact=Instantiate(smgHitEffect, hit.point, QuaternionLook(hit.normal));
        //Destroy(impact, 0.1f);
        //Play smg smoke effect



        //case: 3
        //playmuzzleFlasheffect.Play();
        //impact=Instantiate(ARHitEffect, hit.point, QuaternionLook(hit.normal));
        //Destroy(impact, 0.1f);
        //Play AR smoke effect


        //default: print(something's wrong);
        //}
    }

    void PlayEnemyHitEffect()
    {
        //StartCoroutine(hiteffectAfterAfewSecond)
    }

    //void IEnumerator(float hiteffectAfterAfewSecond)
    //new WaitForSecond(hiteffectAfterAfewSecond)
    //EnemyHitEffect.Play();


}
