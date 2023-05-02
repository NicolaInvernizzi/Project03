using UnityEngine;

public class PinchTester : MonoBehaviour
{
    public Pinch pinchManager;
    Renderer rend;
    float offset; 
    void Awake()
    {
        Pinch.onPinch += HandlePinch;
        rend = gameObject.GetComponent<Renderer>();
    }
    private void HandlePinch(float offset)
    {
        this.offset = offset;

        Debug.Log($"Pinch offset {offset}");

        if(offset >= 0 && offset < 0.3)
            rend.material.color = Color.grey;
        if (offset >= 0.3 && offset < 0.6)
            rend.material.color = new Color(0.1f, 0f, 0.5f, 1f);
        if (offset >= 0.6 && offset < 0.9)
            rend.material.color = new Color(0.3f, 0.5f, 0f, 1f);
        if (offset >= 0.9 && offset < 1.2)
            rend.material.color = new Color(0f, 0f, 0f, 1f);
    }
    void OnGUI()
    {
        GUILayout.Space(200);
        GUILayout.Box("offset: " + offset.ToString(), GUILayout.Width(300), GUILayout.Height(100));
        GUILayout.Space(200);
    }
}
