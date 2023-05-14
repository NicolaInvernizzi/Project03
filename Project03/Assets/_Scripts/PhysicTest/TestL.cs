using UnityEngine;

public class TestL : MonoBehaviour
{
    float timer = 0;
    Vector3 startPosition;
    public bool value;
    public float duration;
    float percentage;
    private void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        percentage = timer / duration;
        if (value)
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, percentage);
        else
            transform.position = Vector3.Lerp(startPosition, Vector3.zero, percentage);
        timer += Time.deltaTime;
    }
}
