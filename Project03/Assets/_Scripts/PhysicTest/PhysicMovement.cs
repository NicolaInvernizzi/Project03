using UnityEditor;
using UnityEngine;

// Old system: adjust forward force in order to change velocity direction

public class PhysicMovement : MonoBehaviour
{
    [Header("---------------Rotation---------------"), Space(5)]
    [SerializeField] Vector3 minRotationRange = new Vector3(30, 30, 60);
    [SerializeField] Vector3 maxRotationRange = new Vector3(30, 30, 10);
    [SerializeField, Range(1f, 200f)] float rotationSpeedUpDown = 20f;
    [SerializeField, Range(1f, 200f)] float rotationSpeedLeftRight = 15f;

    [Space(10), Header("---------------Velocity---------------"), Space(5)]
    [SerializeField] Vector3 min_DirectionVelocity = new Vector3(6f, -1.7f, 0f);
    [SerializeField] Vector3 max_DirectionVelocity = new Vector3(0.5f, -1f, 0f);
    [SerializeField] Vector3 min_DirectionVelocity2 = new Vector3(6f, -1.7f, 0f);
    [SerializeField] Vector3 max_DirectionVelocity2 = new Vector3(0.5f, -1f, 0f);
    [SerializeField, Range(0f, 200f)] float min_MagnitudeVelocity = 5f;
    [SerializeField, Range(0f, 200f)] float max_MagnitudeVelocity = 10f;
    [SerializeField] Color min_MagnitudeVelocity_Color = Color.black;
    [SerializeField] Color max_MagnitudeVelocity_Color = Color.cyan;

    [Space(10), Header("---------------Debug---------------"), Space(5)]
    [SerializeField, Range(5f, 200f)] float lineLength = 10f;
    [SerializeField] Vector3 labelPosition = Vector3.one;
    [SerializeField] bool mouse_wasd;
    [SerializeField] bool stopPlayer;
    [SerializeField] Vector3 stopPosition;
    [SerializeField] bool showVelocity;
    [SerializeField] bool showUpDownLimits;
    [SerializeField] bool showLeftRightLimits;
    [SerializeField] bool upDown;
    [SerializeField] bool leftRight;
    [SerializeField] bool both;

    Rigidbody rb;
    Vector3 rotation;
    Vector3 startingRotation;
    Vector3 directionVelocity;
    Vector3 directionVelocity2;
    Color colorMagnitudeVelocity;

    // Old system: [SerializeField, Min(0f)] float minInclination_OverallSpeed = 2f;
    // Old system: [SerializeField, Min(0f)] float maxInclination_OverallSpeed = 10f;
    // Old system: [SerializeField, Min(0f)] float gravity = 1f;
    // Old system: [SerializeField, Min(0f)] float moveForward = 1f;
    // Old system: Vector3 move;

    float magnitudeVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingRotation = transform.eulerAngles;
        rotation = transform.eulerAngles;

        // Old system: move.x = -gravity;
        // Old system: move.y = moveForward;
    }
    void FixedUpdate()
    {
        Rotate();
        Move();
    }
    void Update()
    {
        if(showVelocity)
            Debug.DrawRay(transform.position, rb.velocity * lineLength, colorMagnitudeVelocity);

        if(showUpDownLimits)
        {
            Debug.DrawRay(transform.position, max_DirectionVelocity.normalized * max_MagnitudeVelocity * lineLength, Color.magenta);
            Debug.DrawRay(transform.position, min_DirectionVelocity.normalized * min_MagnitudeVelocity * lineLength, Color.gray);
        }

        if(showLeftRightLimits)
        {
            Debug.DrawRay(transform.position, max_DirectionVelocity2.normalized * max_MagnitudeVelocity * lineLength, Color.green);
            Debug.DrawRay(transform.position, min_DirectionVelocity2.normalized * min_MagnitudeVelocity * lineLength, Color.blue);
        }

        // Old system: Debug.DrawRay(transform.position, transform.up * move.y * debug_vectorLength, Color.green);
        // Old system: Debug.DrawRay(transform.position, Vector3.up * move.x * debug_vectorLength, Color.red);
        // Old system: Debug.DrawRay(transform.position, Vector3.forward * move.z * debug_vectorLength, Color.blue);
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

        // Old system: move.y = Remap(startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z,
        //                           maxInclination_OverallSpeed, minInclination_OverallSpeed, rotation.z);
    }
    void Move()
    {
        // magnitude
        magnitudeVelocity = Remap(startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z,
                                  max_MagnitudeVelocity, min_MagnitudeVelocity, rotation.z);
        if (stopPlayer)
            transform.position = stopPosition;

        // direction
        directionVelocity = RemapSlerp(startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z,
                                max_DirectionVelocity, min_DirectionVelocity, rotation.z);
        directionVelocity2 = RemapSlerp(startingRotation.x - minRotationRange.x, startingRotation.x + maxRotationRange.x,
                                      max_DirectionVelocity2, min_DirectionVelocity2, rotation.x);

        // set velocity
        if (leftRight)
            rb.velocity = directionVelocity2.normalized * magnitudeVelocity;

        if(upDown)
            rb.velocity = directionVelocity.normalized * magnitudeVelocity;
  
        if (both) // direction + direction2 -> circumference with y lerped ymin, ymax and x lerped xmin, xmax.
            rb.velocity = (directionVelocity+ directionVelocity2).normalized * magnitudeVelocity;

        // velocity color adjustment
        colorMagnitudeVelocity = RemapColor(min_MagnitudeVelocity, max_MagnitudeVelocity, 
                                            min_MagnitudeVelocity_Color, max_MagnitudeVelocity_Color, rb.velocity.magnitude);

        // Old system: rb.velocity = (transform.up * move.y + Vector3.up * move.x + Vector3.forward * move.z).normalized *
        //              Remap(startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z,
        //                     max_MagnitudeVelocity, min_MagnitudeVelocity, rotation.z);

    }
    public static float Remap(float min_A, float max_A, float min_B, float max_B, float A)
    {
        float t = Mathf.InverseLerp(min_A, max_A, A);
        return Mathf.Lerp(min_B, max_B, t);
    }
    public static Vector3 RemapSlerp(float min_A, float max_A, Vector3 min_B, Vector3 max_B, float A)
    {
        float t = Mathf.InverseLerp(min_A, max_A, A);
        return Vector3.Slerp(min_B, max_B, t);
    }
    public static Color RemapColor(float min_A, float max_A, Color min_B, Color max_B, float A)
    {
        float t = Mathf.InverseLerp(min_A, max_A, A);
        return Color.Lerp(min_B, max_B, t);
    }
}
