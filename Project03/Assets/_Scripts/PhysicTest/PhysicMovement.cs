using UnityEditor;
using UnityEngine;

public class PhysicMovement : MonoBehaviour
{
    [SerializeField] Vector3 minRotationRange = new Vector3(30, 30, 60);
    [SerializeField] Vector3 maxRotationRange = new Vector3(30, 30, 10);
    [SerializeField, Range(1f, 200f)] float rotationSpeedUpDown = 20f;
    [SerializeField, Range(1f, 200f)] float rotationSpeedLeftRight = 15f;
    [SerializeField, Range(5f, 200f)] float debug_vectorLength = 10f;
    [SerializeField] Vector3 labelPosition = Vector3.one;
    [SerializeField, Min(0f)] float minInclination_OverallSpeed = 2f;
    [SerializeField, Min(0f)] float maxInclination_OverallSpeed = 10f;
    [SerializeField, Min(0f)] float minOverallSpeed = 5f;
    [SerializeField, Min(0f)] float maxOverallSpeed = 20f;
    [SerializeField, Min(0f)] float gravity = 1f;
    [SerializeField, Min(0f)] float moveForward = 1f;
    [SerializeField] bool mouse_wasd;
    Rigidbody rb;
    Vector3 rotation;
    Vector3 startingRotation;
    Vector3 move;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingRotation = transform.eulerAngles;
        rotation = transform.eulerAngles;
        move.x = -gravity;
        move.y = moveForward;
    }
    void FixedUpdate()
    {
        Rotate();
        Move();
    }
    void Update()
    {
        Debug.DrawRay(transform.position, rb.velocity * debug_vectorLength, Color.cyan);
        Debug.DrawRay(transform.position, transform.up * move.y * debug_vectorLength, Color.green);
        Debug.DrawRay(transform.position, Vector3.up * move.x * debug_vectorLength, Color.red);
        Debug.DrawRay(transform.position, Vector3.forward * move.z * debug_vectorLength, Color.blue);
    }
    void OnDrawGizmos()
    {
        if (rb != null)
            Handles.Label(transform.position + labelPosition, "Velocity: " + rb.velocity);
    }
    void Rotate()
    {
        if(mouse_wasd)
        {
            // rotation up
            rotation.z += Input.GetAxis("Mouse Y") * rotationSpeedUpDown * Time.fixedDeltaTime;
            rotation.x -= Input.GetAxis("Mouse X") * rotationSpeedUpDown * Time.fixedDeltaTime;


            // rotation sides
            rotation.y += Input.GetAxis("Mouse X") * rotationSpeedLeftRight * Time.fixedDeltaTime;
        }
        else
        {
            // rotation up
            if (Input.GetKey(KeyCode.S))
                rotation.z -= rotationSpeedUpDown * Time.fixedDeltaTime;
            if (Input.GetKey(KeyCode.W))
                rotation.z += rotationSpeedUpDown * Time.fixedDeltaTime;

            // rotation sides
            if (Input.GetKey(KeyCode.A))
            {
                rotation.x += rotationSpeedLeftRight * Time.fixedDeltaTime;
                rotation.y -= rotationSpeedLeftRight * Time.fixedDeltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotation.x -= rotationSpeedLeftRight * Time.fixedDeltaTime;
                rotation.y += rotationSpeedLeftRight * Time.fixedDeltaTime;
            }
        }

        rotation.x = Mathf.Clamp(rotation.x, startingRotation.x - minRotationRange.x, startingRotation.x + maxRotationRange.x);
        rotation.y = Mathf.Clamp(rotation.y, startingRotation.y - minRotationRange.y, startingRotation.y + maxRotationRange.y);
        rotation.z = Mathf.Clamp(rotation.z, startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z);
        transform.localRotation = Quaternion.Euler(rotation);

        move.y = Remap(startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z,
                          maxInclination_OverallSpeed, minInclination_OverallSpeed,
                          rotation.z);
    }
    void Move()
    {
        rb.velocity = (transform.up * move.y + Vector3.up * move.x + Vector3.forward * move.z).normalized *
                      Remap(startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z,
                             maxOverallSpeed, minOverallSpeed, 
                             rotation.z);
    }

    public static float Remap(float a1, float a2, float b1, float b2, float value)
    {
        float t = Mathf.InverseLerp(a1, a2, value);
        return Mathf.Lerp(b1, b2, t);
    }
}
