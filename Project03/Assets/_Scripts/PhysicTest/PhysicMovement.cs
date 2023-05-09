using UnityEditor;
using UnityEngine;

public class PhysicMovement : MonoBehaviour
{
#region Variables
    [Header("---------------Rotation---------------"), Space(5)]
    [SerializeField] Vector3 minRotationRange = new Vector3(30, 30, 60);
    [SerializeField] Vector3 maxRotationRange = new Vector3(30, 30, 10);
    [SerializeField, Range(1f, 200f)] float rotationSpeedUpDown = 20f;
    [SerializeField, Range(1f, 200f)] float rotationSpeedLeftRight = 15f;

    [Space(10), Header("---------------Velocity---------------"), Space(5)]
    [SerializeField] Vector3 directionVelocity_up = new Vector3(6f, -1.7f, 0f);
    [SerializeField] Vector3 directionVelocity_down = new Vector3(0.5f, -1f, 0f);
    [SerializeField] Vector3 directionVelocity_left = new Vector3(6f, -4f, -2f);
    [SerializeField] Vector3 directionVelocity_right = new Vector3(6f, -4f, 2f);
    [SerializeField, Range(0f, 200f)] float min_MagnitudeVelocity = 5f;
    [SerializeField, Range(0f, 200f)] float max_MagnitudeVelocity = 20f;
    [SerializeField] AnimationCurve magnitudeCurve;
    [SerializeField] bool slerp;

    [Space(10), Header("---------------Debug---------------"), Space(5)]
    [SerializeField] Vector3 labelPosition = Vector3.one;
    [SerializeField] bool mouse_wasd;
    [SerializeField] bool stopPlayer;
    [SerializeField] Vector3 stopPosition;

    [Space(10), Header("---------------Draw---------------"), Space(5)]
    [SerializeField, Range(5f, 200f)] float lineLength = 10f;
    [SerializeField] bool draw_Velocity = true;
    [SerializeField] bool draw_DirectionUpDown;
    [SerializeField] bool draw_DirectionLeftRight;
    [SerializeField] bool draw_MovemenZone = true;
    [SerializeField, Range(0.01f, 1f)] float frequencyLine = 0.01f;
    /*
      [SerializeField] bool draw_UpDown;
      [SerializeField] bool draw_LeftRight;
    */

    Rigidbody rb;
    Vector3 rotation;
    Vector3 startingRotation;
    Vector3 directionVelocityUpDown;
    Vector3 directionVelocityLeftRight;
    float magnitudeVelocity;
    #endregion

#region Flow
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingRotation = transform.eulerAngles;
        rotation = transform.eulerAngles;
        stopPosition = transform.position;
    }
    void Update()
    {
        if (draw_Velocity)
            Debug.DrawRay(transform.position, rb.velocity * lineLength, Color.black);

        if (draw_DirectionUpDown)
        {
            Debug.DrawRay(transform.position, directionVelocity_down.normalized * max_MagnitudeVelocity * lineLength, Color.magenta);
            Debug.DrawRay(transform.position, directionVelocity_up.normalized * min_MagnitudeVelocity * lineLength, Color.gray);
        }

        if (draw_DirectionLeftRight)
        {
            Debug.DrawRay(transform.position, directionVelocity_left.normalized * rb.velocity.magnitude * lineLength, Color.green);
            Debug.DrawRay(transform.position, directionVelocity_right.normalized * rb.velocity.magnitude * lineLength, Color.blue);
        }

        if(draw_MovemenZone)
            DrawSimulation();
    }
    void FixedUpdate()
    {
        if(!stopPlayer)
            stopPosition = transform.position;
        Rotate();
        Move();
    }
#endregion

#region Methods
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
    }
    void Move()
    {
        // magnitude
        magnitudeVelocity = Remap(0, 1, max_MagnitudeVelocity, min_MagnitudeVelocity, 
                                  magnitudeCurve.Evaluate(Mathf.InverseLerp(startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z, rotation.z)));
        if (stopPlayer)
            transform.position = stopPosition;

        // direction
        if(slerp)
        {
            directionVelocityUpDown = RemapSlerp(startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z,
                        directionVelocity_down, directionVelocity_up, rotation.z);
            directionVelocityLeftRight = RemapSlerp(startingRotation.x - minRotationRange.x, startingRotation.x + maxRotationRange.x,
                                          directionVelocity_left, directionVelocity_right, rotation.x);
        }
        else
        {
            directionVelocityUpDown = RemapLerp(startingRotation.z - minRotationRange.z, startingRotation.z + maxRotationRange.z,
                        directionVelocity_down, directionVelocity_up, rotation.z);
            directionVelocityLeftRight = RemapLerp(startingRotation.x - minRotationRange.x, startingRotation.x + maxRotationRange.x,
                                          directionVelocity_left, directionVelocity_right, rotation.x);
        }
        rb.velocity = (directionVelocityUpDown + directionVelocityLeftRight).normalized * magnitudeVelocity;
    }
    void DrawSimulation()
    {
        /*
           if (draw_UpDown)
            DrawComplex(false, false, false, true, true, false, false);
           if (draw_LeftRight)
            DrawComplex(true, true, false, false, false, false, false);
        */

        DrawComplex(true, false, true, true, true, false, false, slerp);
        DrawComplex(true, false, false, true, true, false, false, slerp);

        DrawComplex(true, true, false, true, false, true, false, slerp);
        DrawComplex(true, true, false, true, false, false, false, slerp);

        DrawComplex(true, true, false, true, true, false, false, slerp);
        DrawComplex(true, true, false, true, true, false, true, slerp);

        Debug.DrawLine(transform.position, transform.position + (directionVelocity_down + directionVelocity_right).normalized * max_MagnitudeVelocity * lineLength);
        Debug.DrawLine(transform.position, transform.position + (directionVelocity_down + directionVelocity_left).normalized * max_MagnitudeVelocity * lineLength);
        Debug.DrawLine(transform.position, transform.position + (directionVelocity_up + directionVelocity_right).normalized * min_MagnitudeVelocity * lineLength);
        Debug.DrawLine(transform.position, transform.position + (directionVelocity_up + directionVelocity_left).normalized * min_MagnitudeVelocity * lineLength);
    }
    void DrawComplex(bool drawLR, bool moveLR, bool maxLR, bool drawUD, bool moveUD, bool maxUD, bool invert, bool slerp)
    {
        Vector3 direction1 = Vector3.zero;
        Vector3 new_direction1 = Vector3.zero;
        Vector3 direction2 = Vector3.zero;
        Vector3 new_direction2 = Vector3.zero;
        float magnitude = 0f;
        float new_magnitude = 0f;
        float k1 = 0f;
        float k2 = 0f;
        float h1 = 0f;
        float h2 = 0f;

        for (float i = 0; i < 1; i += frequencyLine)
        {
            if (drawLR)
            {
                // down -> up
                if (moveLR)
                {
                    h1 = i;
                    h2 = frequencyLine;
                }
                else
                {
                    if (maxLR)
                        h1 = 1;
                }

                if (invert)
                {
                    if(slerp)
                    {
                        direction2 = RemapSlerp(1, 0, directionVelocity_left, directionVelocity_right, h1);
                        new_direction2 = RemapSlerp(1, 0, directionVelocity_left, directionVelocity_right, h1 + h2);
                    }
                    else
                    {
                        direction2 = RemapLerp(1, 0, directionVelocity_left, directionVelocity_right, h1);
                        new_direction2 = RemapLerp(1, 0, directionVelocity_left, directionVelocity_right, h1 + h2);
                    }
                }
                else
                {
                    if(slerp)
                    {
                        direction2 = RemapSlerp(0, 1, directionVelocity_left, directionVelocity_right, h1);
                        new_direction2 = RemapSlerp(0, 1, directionVelocity_left, directionVelocity_right, h1 + h2);
                    }
                    else
                    {
                        direction2 = RemapLerp(0, 1, directionVelocity_left, directionVelocity_right, h1);
                        new_direction2 = RemapLerp(0, 1, directionVelocity_left, directionVelocity_right, h1 + h2);
                    }

                }
                magnitude = rb.velocity.magnitude;
                new_magnitude = rb.velocity.magnitude;
            }

            if (drawUD)
            {
                // left -> right
                if (moveUD)
                {
                    k1 = i;
                    k2 = frequencyLine;
                }
                else
                {
                    if (maxUD)
                        k1 = 1;
                }

                if(slerp)
                {
                    direction1 = RemapSlerp(0, 1, directionVelocity_down, directionVelocity_up, k1);
                    new_direction1 = RemapSlerp(0, 1, directionVelocity_down, directionVelocity_up, k1 + k2);
                }
                else
                {
                    direction1 = RemapLerp(0, 1, directionVelocity_down, directionVelocity_up, k1);
                    new_direction1 = RemapLerp(0, 1, directionVelocity_down, directionVelocity_up, k1 + k2);
                }

                magnitude = Remap(0, 1, max_MagnitudeVelocity, min_MagnitudeVelocity, magnitudeCurve.Evaluate(k1));
                new_magnitude = Remap(0, 1, max_MagnitudeVelocity, min_MagnitudeVelocity, magnitudeCurve.Evaluate(k1 + k2));
            }
            Debug.DrawLine(transform.position + (direction1 + direction2).normalized * magnitude * lineLength,
               transform.position + (new_direction1 + new_direction2).normalized * new_magnitude * lineLength);
        }
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
#endregion
}
