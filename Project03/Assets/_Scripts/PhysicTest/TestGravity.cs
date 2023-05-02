using Unity.VisualScripting;
using UnityEngine;

public class TestGravity : MonoBehaviour
{
    [Header("-------------Falling-------------"), Space(5)]
    [Header("X axis")]
    [Tooltip("Default Speed X")]
    [SerializeField, Range(-5f, 5f)] float minSpeedX = 0.1f;

    [Space(5), Header("Y axis")]
    [Tooltip("Speed Y while you're not touching")]
    [SerializeField, Range(-5f, 5f)] float minSpeedY = 0.1f;

    [Tooltip("Speed Y while you're holding finger down")]
    [SerializeField, Range(-5f, 5f)] float maxSpeedY = 0.5f;

    [Tooltip("Speed from wingSuit closed to wingSuit opened")]
    [SerializeField, Range(-5f, 5f)] float closeSpeed = 0.5f;

    [Tooltip("Speed from wingSuit opened to wingSuit closed ")]
    [SerializeField, Range(-5f, 5f)] float openSpeed = 0.5f;

    [Space(10)]
    [Header("-------------Rotation-------------"), Space(5)]
    [Header("Y axis")]
    [SerializeField, Range(1f, 20f)] float rotationSpeedY = 1f;
    [SerializeField, Range(-5f, -90f)] float rotationMinY = -10f;
    [SerializeField, Range(5f, 90f)] float rotationMaxY = 10f;
    [Space(5), Header("Z axis")]
    [SerializeField, Range(1f, 20f)] float rotationSpeedZ = 1f;
    [SerializeField, Range(-5f, -90f)] float rotationMinZ = -10f;
    [SerializeField, Range(5f, 90f)] float rotationMaxZ = 10f;

    [Space(10)]
    [Header("-------------Debug-------------"), Space(5)]
    [Tooltip("Debug movement lines length")]
    [SerializeField, Range(5f, 50f)] float debugMultiplyer = 10f;

    // to modify
    float maxSpeedX = 0.5f;

    float currentY;
    float currentX;
    float speedY;
    float timer;
    Vector3 newRotation;
    Vector3 vectorX;
    Vector3 vectorY;
    bool slow;

    private void Awake()
    {
        currentY = minSpeedY;
        currentX = minSpeedX;
        newRotation = transform.rotation.eulerAngles;
    }
    private void Update()
    {
        Rotation();
        Falling();
    }
    private void ResetValues(bool value)
    {
        slow = value;
        timer = 0;
        speedY = currentY;
    }
    private void Rotation()
    {
        // right
        if (Input.GetKey(KeyCode.A))
            newRotation.y = Mathf.Clamp(newRotation.y - rotationSpeedY * Time.deltaTime, rotationMinY, transform.rotation.eulerAngles.y);
        // left
        if (Input.GetKey(KeyCode.D))
            newRotation.y = Mathf.Clamp(newRotation.y + rotationSpeedY * Time.deltaTime, transform.rotation.eulerAngles.y, rotationMaxY);
        // up
        if (Input.GetKey(KeyCode.W))
            newRotation.z = Mathf.Clamp(newRotation.z + rotationSpeedZ * Time.deltaTime, transform.rotation.eulerAngles.z, rotationMaxZ);
        // down
        if (Input.GetKey(KeyCode.S))
            newRotation.z = Mathf.Clamp(newRotation.z - rotationSpeedZ * Time.deltaTime, rotationMinZ, transform.rotation.eulerAngles.z);

        transform.rotation = Quaternion.Euler(newRotation);
    }
    private void Falling()
    {
        // Simulate touchScreen
        if (Input.GetKeyDown(KeyCode.Space))
            ResetValues(true);
        else if (Input.GetKeyUp(KeyCode.Space))
            ResetValues(false);

        // Modify speedY
        if (slow && currentY != maxSpeedY)
            currentY = Mathf.Lerp(speedY, maxSpeedY, timer += ((Time.deltaTime / Mathf.Abs(speedY - maxSpeedY))) * closeSpeed);
        else if (!slow && currentY != minSpeedY)
            currentY = Mathf.Lerp(speedY, minSpeedY, timer += ((Time.deltaTime / Mathf.Abs(speedY - minSpeedY))) * openSpeed);

        // Apply movement
        vectorX = transform.right * currentX * Time.deltaTime;
        vectorY = transform.up * currentY * Time.deltaTime;
        transform.Translate(vectorX + vectorY, Space.Self);

        // Debug
        Debug.DrawRay(transform.position, transform.right * currentX * debugMultiplyer, Color.red);
        Debug.DrawRay(transform.position, transform.up * currentY * debugMultiplyer, Color.green);
        Debug.DrawRay(transform.position, (transform.right * currentX + transform.up * currentY) * debugMultiplyer, Color.cyan);
    }
}
