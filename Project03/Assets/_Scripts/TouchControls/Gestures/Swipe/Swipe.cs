using UnityEngine;

public class Swipe : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public delegate void SwipeDelegate(Vector2 startCoordinates, Direction direction);
    public static event SwipeDelegate OnSwipe;

    [SerializeField, Min(0.0f)] float minSwipeDistanceCm;
    [SerializeField, Min(0.1f)] float maxSwipeTime;
    Vector2 startCoordinates;
    float startTime;
    bool swipe;

    void Update()
    {
        SwipeGesture();
    }

    private void SwipeGesture()
    {
        if (Input.touchCount == 1 &&
            Input.touches[0].phase == TouchPhase.Began)
            StartSwipe(Input.touches[0]);

        else if (Input.touchCount == 1 &&
          Input.touches[0].phase == TouchPhase.Ended)
            EndSwipe(Input.touches[0]);

        else if (swipe && Input.touchCount > 1)
            CancelSwipe();
    }
    private void StartSwipe(Touch touch)
    {
        startCoordinates = touch.position;
        startTime = Time.unscaledTime;
        swipe = true;
    }
    private void EndSwipe(Touch touch)
    {
        swipe = false;

        // Listeners gate
        if (OnSwipe == null)
            return;

        // Time gate
        if (Time.unscaledTime - startTime > maxSwipeTime)
            return;

        Vector2 swipeOffset = touch.position - startCoordinates;
        Commons.ScaleByDPI(ref swipeOffset);

        // Distance gate
        if (swipeOffset.sqrMagnitude < minSwipeDistanceCm * minSwipeDistanceCm)
            return;

        // Detection distance
        Direction direction;

        if (Commons.CheckVectorInRange(swipeOffset, Vector3.up, 45.0f))
            direction = Direction.Up;
        else if (Commons.CheckVectorInRange(swipeOffset, Vector3.right, 45.0f))
            direction = Direction.Right;
        else if (Commons.CheckVectorInRange(swipeOffset, Vector3.left, 45.0f))
            direction = Direction.Left;
        else
            direction = Direction.Down;

        //	Notify swipe
        OnSwipe.Invoke(startCoordinates, direction);
    }
    private void CancelSwipe()
    {
        swipe = false;
    }
}
