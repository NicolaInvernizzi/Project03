using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class NewInputSystemTest : MonoBehaviour
{
    private MyInput myInput = null;
    void Awake()
    {
        myInput = new MyInput();
    }
    void Start()
    {
        InputSystem.onAnyButtonPress.Call(HandleControl);
        myInput.Map.Enable();
    }
    private void HandleControl(InputControl control)
    {
        Debug.Log(control.device);
    }
    void Update()
    {
//        //	Direct input access ????????????????
//        if (
//#if ENABLE_INPUT_SYSTEM
//        Keyboard.current.escapeKey.wasPressedThisFrame
//#elif ENABLE_LEGACY_INPUT_MANAGER
//		Input.GetKeyDown(KeyCode.Escape)
//#endif
//        )
//            Debug.Log("AAA");

        //	Actions mapping input access
        if (myInput.Map.Jump.WasPressedThisFrame())
            Debug.Log("Jump Pressed");
        else if (myInput.Map.Jump.WasReleasedThisFrame())
            Debug.Log("Jump Released");
        if (myInput.Map.Reset.WasPerformedThisFrame())
            Debug.Log("Reset!");
        if (myInput.Map.Reset.WasPressedThisFrame())
            Debug.Log("Reset Pressed");
        else if (myInput.Map.Reset.WasReleasedThisFrame()) 
            Debug.Log("Reset Released");

        Debug.Log(myInput.Map.Move.ReadValue<Vector2>());
    }
}
