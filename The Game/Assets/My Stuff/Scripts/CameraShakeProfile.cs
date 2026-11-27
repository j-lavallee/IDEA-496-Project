using UnityEngine;

[CreateAssetMenu(fileName = "NewShakeProfile", menuName = "Combat/Camera Shake Profile")]
public class CameraShakeProfile : ScriptableObject
{
    [Header("Shake Settings")]
    public float intensity = 2f;
    public float duration = 0.2f;
    [Tooltip("How fast the camera vibrates. 1 is default.")]
    public float frequency = 1f;
}
