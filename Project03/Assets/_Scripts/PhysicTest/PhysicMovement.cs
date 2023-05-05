using UnityEditor;
using UnityEngine;

public class PhysicMovement : MonoBehaviour
{
    [SerializeField] Vector3 minRotationRange;
    [SerializeField] Vector3 maxRotationRange;
    [SerializeField, Range(1f, 200f)] float rotationSpeed = 10f;
    [SerializeField] float debug_vectorLength = 10f;
    [SerializeField] Vector3 labelPosition = Vector3.one;
    [SerializeField] float minForceY;
    [SerializeField] float maxForceY;
    Rigidbody rb;
    Vector3 rotation;
    Vector3 startingRotation;
    Vector3 move;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingRotation = transform.eulerAngles;
        rotation = transform.eulerAngles;
    }
    void FixedUpdate()
    {
        Rotate();
        Move();
    }
    void Update()
    {
        Debug.DrawRay(transform.position, rb.velocity * debug_vectorLength, Color.cyan);
    }
    void OnDrawGizmos()
    {
        if (rb != null)
            Handles.Label(transform.position + labelPosition, "Velocity: " + rb.velocity);
    }
    void Rotate()
    {
        // rotation up
        if (Input.GetKey(KeyCode.S))
            rotation.z -= rotationSpeed * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.W))
            rotation.z += rotationSpeed * Time.fixedDeltaTime;

        // rotation sides
        if (Input.GetKey(KeyCode.A))
        {
            rotation.x += rotationSpeed * Time.fixedDeltaTime;
            rotation.y -= rotationSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotation.x -= rotationSpeed * Time.fixedDeltaTime;
            rotation.y += rotationSpeed * Time.fixedDeltaTime;
        }
        rotation.x = Mathf.Clamp(rotation.x, startingRotation.x - minRotationRange.x, startingRotation.x + maxRotationRange.x);
        rotation.y = Mathf.Clamp(rotation.y, startingRotation.y - minRotationRange.y, startingRotation.y + maxRotationRange.y);
        rotation.z = Mathf.Clamp(rotation.z, startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z);
        transform.localRotation = Quaternion.Euler(rotation);
    }
    void Move()
    {
        move.y = Remap(minRotationRange.z, maxRotationRange.z, minForceY, maxForceY, rotation.z);
        rb.velocity = transform.TransformDirection(move);
    }

    public static float Remap(float a1, float a2, float b1, float b2, float value)
    {
        float t = Mathf.InverseLerp(a1, a2, value);
        return Mathf.Lerp(b1, b2, t);
    }
}
