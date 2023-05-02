using UnityEngine;

public class Pinch : MonoBehaviour
{
    public delegate void PinchDelegate(float offset);
    public static event PinchDelegate onPinch;
    void Update() => PinchGesture();
    private void PinchGesture()
    {
        // check if enough touches are there
        if (Input.touchCount != 2 ||
            (Input.touches[0].phase == TouchPhase.Stationary &&
            Input.touches[1].phase == TouchPhase.Stationary))
            return;

        // get points
        Vector2 touch0 = Input.touches[0].position;
        Vector2 touch1 = Input.touches[1].position;
        Vector2 prevTouch0 = touch0 - Input.touches[0].position;
        Vector2 prevTouch1 = touch1 - Input.touches[1].position;

        // calculate pinch
        float bias = Mathf.Min(Screen.width, Screen.height);

        float currentDistance = (touch1 - touch0).magnitude / bias;
        float prevDistance = (prevTouch1 - prevTouch0).magnitude / bias;

        float offset = currentDistance - prevDistance;

        // feed pinch event
        onPinch?.Invoke(offset);
    }
}
