using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMCameraShake : MonoBehaviour
{
    public static CMCameraShake Instance { get; private set; }
    private CinemachineVirtualCamera _cam;
    private float timer;
    private float timerTotal;
    private float startingStrength;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _cam = GetComponent<CinemachineVirtualCamera>();
    }
    public void ShakeCamera(float ShakeStrength, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachinePerlinChannel = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachinePerlinChannel.m_AmplitudeGain = ShakeStrength;

        startingStrength = ShakeStrength;
        timerTotal = time;
        timer = time;
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0f)
        {
            CinemachineBasicMultiChannelPerlin cinemachinePerlinChannel = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachinePerlinChannel.m_AmplitudeGain = 0;
        }
    }


}
