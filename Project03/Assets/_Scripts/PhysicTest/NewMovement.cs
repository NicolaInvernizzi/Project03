using System;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    [Header("---------------Rotation---------------"), Space(5)]
    [SerializeField, Range(0f, 360f)] float min_rotationRangeX = 10f;
    [SerializeField, Range(0f, 360f)] float max_rotationRangeX = 60f;
    [Space(5), SerializeField, Range(0f, 360f)] float min_rotationRangeY = 45f;
    [SerializeField, Range(0f, 360f)] float max_rotationRangeY = 45f;
    [Space(5), SerializeField, Range(0f, 360f)] float min_rotationRangeZ = 45f;
    [SerializeField, Range(0f, 360f)] float max_rotationRangeZ = 45f;

    [Space(10), Header("---------------Rotation speed---------------"), Space(5)]
    [SerializeField, Range(1f, 200f)] float min_rotationSpeed_Up = 30f;
    [SerializeField, Range(1f, 200f)] float max_rotationSpeed_Up = 90f;
    [SerializeField, Range(1f, 200f)] float min_rotationSpeed_Down = 30f;
    [SerializeField, Range(1f, 200f)] float max_rotationSpeed_Down = 90f;
    [Space(5), SerializeField, Range(1f, 200f)] float min_rotationSpeedY = 30;
    [SerializeField, Range(1f, 200f)] float max_rotationSpeedY = 90f;
    [Tooltip("Use rotationSpeedX as global rotation speed: auto calculate other rotation speeds\nDoesn't work runtime!")]
    [Space(5), SerializeField] bool globalRotationSpeed;

    [Space(10), Header("---------------Velocity---------------"), Space(5)]
    [SerializeField, Range(0f, 50f)] float min_moveSpeed = 5f;
    [SerializeField, Range(0f, 50f)] float max_moveSpeed = 20f;
    [SerializeField] AnimationCurve moveSpeedCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField, Range(0f, -90f)] float limitVelocityUp = -20f;
    [SerializeField, Range(0f, 90f)] float limitVelocityDown = 20f;

    [Space(10), Header("---------------Debug---------------"), Space(5)]
    [SerializeField, Range(0.1f, 50f)] float rayLength = 5f;
    [SerializeField] bool stopPlayer = true;

    float current_moveSpeed;
    float current_rotationSpeedZ;
    float current_rotationSpeedDown;
    float current_rotationSpeedUp;
    float current_rotationSpeedY;
    float current_rotationSpeed_Left;
    float current_rotationSpeed_Right;
    float current_rotationSpeed_Forward;
    float current_rotationSpeed_Back;

    Vector3 rotation;
    Vector3 startingRotation;
    Vector3 currentDirectionVelocity;
    Vector3 currentVelocity;
    Vector3 stopPosition;
    Vector3 directionVelocity_up = Vector3.one;
    Vector3 directionVelocity_down = Vector3.one;
    Vector3 normalDirectionsPlane;

    void Start()
    {
        startingRotation = transform.eulerAngles;
        rotation = transform.eulerAngles;
        current_moveSpeed = min_moveSpeed;
        stopPosition = transform.position;
        DirectionVelocityAdjustment();
    }
    void Update()
    {
        Rotate();
        Move();

        if (stopPlayer)
            transform.position = stopPosition;
        else
            stopPosition = transform.position;

        Debug.DrawRay(transform.position, currentVelocity * rayLength, Color.cyan);
        Debug.DrawRay(transform.position, directionVelocity_up.normalized * min_moveSpeed * rayLength, Color.green);
        Debug.DrawRay(transform.position, directionVelocity_down.normalized * max_moveSpeed * rayLength, Color.red);
        Debug.DrawRay(transform.position, normalDirectionsPlane.normalized * rayLength, Color.grey);
    }
    void Rotate()
    {
        current_rotationSpeedDown = Remap(min_moveSpeed, max_moveSpeed, min_rotationSpeed_Down, max_rotationSpeed_Down, current_moveSpeed);
        current_rotationSpeedUp = Remap(min_moveSpeed, max_moveSpeed, max_rotationSpeed_Up, min_rotationSpeed_Up, current_moveSpeed);
        current_rotationSpeed_Left = Remap(startingRotation.y + max_rotationRangeY, startingRotation.y - min_rotationRangeY,
                                          min_rotationSpeedY, max_rotationSpeedY, rotation.y);
        current_rotationSpeed_Right = Remap(startingRotation.y + max_rotationRangeY, startingRotation.y - min_rotationRangeY,
                                          max_rotationSpeedY, min_rotationSpeedY, rotation.y);
        current_rotationSpeed_Forward = (current_rotationSpeed_Left * (min_rotationRangeZ + max_rotationRangeZ)) / (min_rotationRangeY + max_rotationRangeY);
        current_rotationSpeed_Back = (current_rotationSpeed_Right * (min_rotationRangeZ + max_rotationRangeZ)) / (min_rotationRangeY + max_rotationRangeY);

        if (Input.GetKey(KeyCode.S))
            rotation.x -= current_rotationSpeedUp * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
            rotation.x += current_rotationSpeedDown * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            rotation.z += current_rotationSpeed_Forward * Time.deltaTime;
            rotation.y -= current_rotationSpeed_Left * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotation.z -= current_rotationSpeed_Back * Time.deltaTime;
            rotation.y += current_rotationSpeed_Right * Time.deltaTime;
        }

        rotation.x = Mathf.Clamp(rotation.x, startingRotation.x - min_rotationRangeX, startingRotation.x + max_rotationRangeX);
        rotation.y = Mathf.Clamp(rotation.y, startingRotation.y - min_rotationRangeY, startingRotation.y + max_rotationRangeY);
        rotation.z = Mathf.Clamp(rotation.z, startingRotation.z - min_rotationRangeZ, startingRotation.z + max_rotationRangeZ);

        transform.localRotation = Quaternion.Euler(rotation);
        DirectionVelocityAdjustment();
    }
    void Move()
    {
        current_moveSpeed = Remap(0, 1, min_moveSpeed, max_moveSpeed,
            moveSpeedCurve.Evaluate(Mathf.InverseLerp(startingRotation.x - min_rotationRangeX, startingRotation.x + max_rotationRangeX, rotation.x)));

        currentDirectionVelocity = RemapSlerp(startingRotation.x - min_rotationRangeX, startingRotation.x + max_rotationRangeX,
                     directionVelocity_up.normalized, directionVelocity_down.normalized, rotation.x);

        currentVelocity = currentDirectionVelocity * current_moveSpeed;

        transform.position += (currentVelocity) * Time.deltaTime;
    }
    void DirectionVelocityAdjustment()
    {
        normalDirectionsPlane = Vector3.Cross(directionVelocity_down, directionVelocity_up).normalized;
        directionVelocity_up = Quaternion.AngleAxis(limitVelocityUp, normalDirectionsPlane) * Vector3.forward;
        directionVelocity_down = Quaternion.AngleAxis(limitVelocityDown, normalDirectionsPlane) * (-Vector3.up);
        directionVelocity_up = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * directionVelocity_up;
        directionVelocity_down = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * directionVelocity_down;
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
    public static Vector3 RemapLerp(float min_A, float max_A, Vector3 min_B, Vector3 max_B, float A)
    {
        float t = Mathf.InverseLerp(min_A, max_A, A);
        return Vector3.Lerp(min_B, max_B, t);
    }
}
