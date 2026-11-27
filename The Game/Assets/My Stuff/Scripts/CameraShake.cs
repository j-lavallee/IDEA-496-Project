using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private CinemachineCamera cam;
    private CinemachineBasicMultiChannelPerlin perlinNoise;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntegriy;

    void Awake()
    {
        Instance = this;
        cam = GetComponent<CinemachineCamera>();
        perlinNoise = cam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(CameraShakeProfile profile)
    {
        if (profile == null)
        {
            return;
        }

        if (shakeTimer > 0 && profile.intensity < perlinNoise.AmplitudeGain)
        {
            return;
        }

        perlinNoise.AmplitudeGain = profile.intensity;
        perlinNoise.FrequencyGain = profile.frequency;
        startingIntegriy = profile.intensity;
        shakeTimerTotal = profile.duration;
        shakeTimer = profile.duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            perlinNoise.AmplitudeGain = Mathf.Lerp(startingIntegriy, 0f, 1 - (shakeTimer / shakeTimerTotal));

            if (shakeTimer <= 0f)
            {
                perlinNoise.FrequencyGain = 1f;
            }
        }
    }
}
