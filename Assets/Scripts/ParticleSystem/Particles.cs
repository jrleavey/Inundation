using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] ParticleSystem shotgunffect;
    [SerializeField] ParticleSystem smgEffect;
    [SerializeField] ParticleSystem pistolEffect;
    [SerializeField] ParticleSystem arEffect;
    PlayerController playerController;
    float hiteffectAfterAfewSecond;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }


    public void PlayMuzzleFlash()
    {
        

    }
}
