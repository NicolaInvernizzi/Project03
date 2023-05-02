using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f; 
    public Transform limit1;
    public Transform limit2;
    public bool limitX = true;
    public bool limitY = true;
    public bool limitZ = true;
    public float scaleFactor = 2;

    float xPosition;
    float yPosition;
    float zPosition;
    Vector3 newPosition;

    private void Start()
    {
        newPosition = transform.position;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Scale(scaleFactor);
        MovePlayer();
    }
    private void MovePlayer()
    {
        xPosition = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        yPosition = Input.GetAxis("Depth") * speed * Time.deltaTime;
        zPosition = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        ClampPosition();

        transform.position = newPosition;
        Draw_Cube(limit1.position, limit2.position);
    }
    private void ClampPosition()
    {
        newPosition.x = Clamp(transform.position.x, xPosition, limit2.position.x, limit1.position.x, limitX);
        newPosition.y = Clamp(transform.position.y, yPosition, limit2.position.y, limit1.position.y, limitY);
        newPosition.z = Clamp(transform.position.z, zPosition, limit1.position.z, limit2.position.z, limitZ);
    }
    private float Clamp(float value, float movement, float limit1, float limit2, bool limit)
    {
        float newValue = value + movement;
        if (limit)
            newValue = Mathf.Clamp(newValue, Mathf.Min(limit1, limit2), Mathf.Max(limit1, limit2));
        return newValue;
    }
    private void Draw_Cube(Vector3 v1, Vector3 v2)
    {
        Draw(v1, v2);
        Draw(v2, v1);
    }
    private void Draw(Vector3 v1, Vector3 v2)
    {
        // draw vertex
        Debug.DrawLine(v1, new Vector3(v2.x, v1.y, v1.z));
        Debug.DrawLine(v1, new Vector3(v1.x, v2.y, v1.z));
        Debug.DrawLine(v1, new Vector3(v1.x, v1.y, v2.z));

        // draw last edges
        Debug.DrawLine(new Vector3(v1.x, v2.y, v1.z), new Vector3(v2.x, v2.y, v1.z));
        Debug.DrawLine(new Vector3(v2.x, v2.y, v1.z), new Vector3(v2.x, v1.y, v1.z));
        Debug.DrawLine(new Vector3(v2.x, v1.y, v1.z), new Vector3(v2.x, v1.y, v2.z));
    }    
    private void Scale(float scaleFactor)
    {
        Vector3 newLimit1Position = Vector3.LerpUnclamped(limit2.position, limit1.position, scaleFactor/2f);
        Vector3 newLimit2Position = Vector3.LerpUnclamped(limit1.position, limit2.position, scaleFactor/2f);
        limit1.position = newLimit1Position;
        limit2.position = newLimit2Position;
    }
}
