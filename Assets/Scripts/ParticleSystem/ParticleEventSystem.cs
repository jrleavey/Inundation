using UnityEngine;
using System;

public class ParticleEventSystem : MonoBehaviour
{
    public static ParticleEventSystem current;
    void Start()
    {
        current = this;
    }

    public event Action muzzleEvent;
    public event Action enemyHitEvent;

    public void MuzzleShotEffect()
    {
        if (muzzleEvent != null) { MuzzleShotEffect(); }
    }

    public void EnemyHitEffect()
    {
        if (enemyHitEvent != null) { EnemyHitEffect(); }
    }
}
