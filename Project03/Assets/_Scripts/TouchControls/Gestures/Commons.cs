using UnityEngine;

public class Commons : MonoBehaviour
{
    public const float DPI_TO_PCM = 0.393701f;

    public static void ScaleByDPI(ref Vector2 pixelDistance)
    {
        float dpi = Screen.dpi;
        if (dpi <= 0f)
            dpi = 50f;
        pixelDistance /= dpi * DPI_TO_PCM;
    }

    public static bool CheckVectorInRange(Vector2 vector, Vector2 reference, float angleTreshold)
    {
        return Vector2.Dot(reference.normalized, vector.normalized) >= Mathf.Cos(Mathf.Deg2Rad * angleTreshold);
    }
}
