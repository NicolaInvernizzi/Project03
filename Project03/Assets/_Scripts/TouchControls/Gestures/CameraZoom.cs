using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    float minFoV = 15.0f;
    float maxFoV = 90.0f;
    private Camera cam;
    void Awake()
    {
        cam = GetComponent<Camera>();
    }
    public void Zoom(float offset)
    {
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - offset * (maxFoV - minFoV), minFoV, maxFoV);
    }
}
