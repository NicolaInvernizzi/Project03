using UnityEngine;

public class SwipeTester : MonoBehaviour
{
    public Swipe swipeManager;
    Renderer rend;
    void Awake()
    {
        Swipe.OnSwipe += HandleSwipe;
        rend = gameObject.GetComponent<Renderer>();
    }
    private void HandleSwipe(Vector2 startCoordinates, Swipe.Direction direction)
    {
        Debug.Log($"Swipe from {startCoordinates} in direction {direction}");

        switch(direction)
        {
            case Swipe.Direction.Up:
                rend.material.color = Color.yellow;
                break;
            case Swipe.Direction.Down:
                rend.material.color = Color.blue;
                break;
            case Swipe.Direction.Left:
                rend.material.color = Color.cyan;
                break;
            case Swipe.Direction.Right:
                rend.material.color = Color.red;
                break;
        }
    }
}
