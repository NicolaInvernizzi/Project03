using UnityEngine;

public class VirtualController : MonoBehaviour
{
    public Transform controlledCube = null;
    MyInput myInput = null;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float mouseRotationSpeedDeg = 90.0f;
    [SerializeField] float padRoatationSpeedDegS = 90.0f;
    void Awake()
    {
        myInput = new MyInput();
    }
    void Start()
    {
        myInput.Map.Enable();
        myInput.Camera.Enable();
    }
    void Update()
    {
        if (controlledCube != null)
        {
            Vector2 move = myInput.Map.Move.ReadValue<Vector2>();
            controlledCube.Translate((Vector3.forward * move.y + Vector3.right * move.x) * Time.deltaTime * moveSpeed, Space.World);

            Vector2 lookRate = myInput.Camera.LookRate.ReadValue<Vector2>();
            lookRate *= padRoatationSpeedDegS * Time.deltaTime;

            Vector2 lookDelta = myInput.Camera.LookDelta.ReadValue<Vector2>();
            lookDelta /= Mathf.Min(Screen.width, Screen.height);
            lookDelta *= mouseRotationSpeedDeg;

            Vector2 look;
            if (lookRate.sqrMagnitude > lookDelta.sqrMagnitude)
                look = lookRate;
            else
                look = lookDelta;

            controlledCube.Rotate(Vector3.right, look.y, Space.World);
            controlledCube.Rotate(Vector3.up, look.x, Space.World);
        }
    }
}
