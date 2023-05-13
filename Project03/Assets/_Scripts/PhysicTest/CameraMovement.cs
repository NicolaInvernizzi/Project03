using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Vector3 distanceToPlayer = new Vector3(0f, -4f, 4f);
    [SerializeField] Transform player_transform;
    //[SerializeField] float rotationX; 
    void LateUpdate()
    {
        transform.position = player_transform.position - distanceToPlayer;
        //transform.eulerAngles = new Vector3(rotationX + player_transform.eulerAngles.x, 0f, 0f);
    }
}
