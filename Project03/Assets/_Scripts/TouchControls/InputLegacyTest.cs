using System.Text;
using UnityEngine;

public class InputLegacyTest : MonoBehaviour
{
    [SerializeField, Min(1.0f)] private float guiScale = 3;
    [SerializeField, Range(0.0f, 1.0f)] private float smoothingValue = 0.16f;
    private Vector3 acceleration;
    private Vector3 rotation;
    void Start()
    {
        Input.gyro.enabled = true;

        acceleration = Input.acceleration;
        rotation = Input.gyro.rotationRate;
    }
    void Update()
    {
        acceleration = Vector3.Lerp(acceleration, Input.acceleration, smoothingValue);
        rotation = Vector3.Lerp(rotation, Input.gyro.rotationRate, smoothingValue);
    }
    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(
            Vector3.zero,
            Quaternion.identity,
            Vector3.one * guiScale
        );
        GUILayout.Space(200);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Input.touchCount; i++)
        {
            sb.AppendLine(string.Format(
                "[{0}/{1}] Position: {2}  Delta: {3} ==== {4}",
                i,
                Input.touches[i].fingerId,
                Input.touches[i].position,
                Input.touches[i].deltaPosition,
                Input.touches[i].phase
                )
            );
        }
        GUILayout.Box(sb.ToString());

        GUILayout.Box("Acceleration: " + Input.acceleration.ToString());
        GUILayout.Box("Acceleration (smooth): " + acceleration.ToString());

        GUILayout.Box("Gyro: " + Input.gyro.rotationRate.ToString());
        GUILayout.Box("Gyro (smooth): " + rotation.ToString());
    }
    public void Test()
    {
        if (!TouchScreenKeyboard.isSupported)
            return;
        TouchScreenKeyboard openKeyboard = TouchScreenKeyboard.Open(
            "Ciao", 
            TouchScreenKeyboardType.EmailAddress, 
            false, false, false, false, 
            "Inser text here...", 5);
    }
}
