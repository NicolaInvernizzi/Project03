//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Controls/MyInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MyInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MyInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyInput"",
    ""maps"": [
        {
            ""name"": ""Map"",
            ""id"": ""722c59eb-d2f6-414b-b5c3-81d0e74a34f0"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b06fa70a-27a6-4af5-a3b7-49802e8e1979"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b03dac5f-c20c-487d-ac4f-ae997b30525c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Reset"",
                    ""type"": ""Button"",
                    ""id"": ""24f23d6f-da1a-4faf-a6f0-3493ca44b828"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=7,pressPoint=1)"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6ab6df9c-3db1-413e-8438-12d14b10cc6d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50a6bb0c-59f1-4c2c-b6a4-0e2575002025"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2a2bd173-a71c-4b7e-aa0c-2e07f4dc0b9c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""04e8c60d-bcf9-4185-af12-0bfdd9296e7e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a2dfd5b7-8c23-48a4-a4d1-a0a4a2a6072a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""aee3dfd2-c829-48ca-b29b-7847acd1a479"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""291a36eb-f2d0-46e2-819f-85dbdb404acf"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bccb0bcb-8c1d-49b6-a57a-b3bf0776e43c"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a0683c9-6374-462b-abd6-e35cfc1f40a3"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""db2aa0dc-f618-4b01-8da6-5c9fed33bfbb"",
            ""actions"": [
                {
                    ""name"": ""LookRate"",
                    ""type"": ""Value"",
                    ""id"": ""66fff732-7f19-4848-a593-333dfa55a9cf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LookRate1"",
                    ""type"": ""Value"",
                    ""id"": ""47b749db-711b-4142-be3d-2b9ba662201c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LookDelta"",
                    ""type"": ""Value"",
                    ""id"": ""2623c196-b4bb-4226-88bb-720924e32445"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""112fec69-0098-4883-a0e8-28e11701fcd8"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookRate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""885bf22d-186b-468e-afdc-5ca664caabc9"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80c9ca70-c0c1-446c-9b2c-ab3a9777bc91"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookRate1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Map
        m_Map = asset.FindActionMap("Map", throwIfNotFound: true);
        m_Map_Jump = m_Map.FindAction("Jump", throwIfNotFound: true);
        m_Map_Move = m_Map.FindAction("Move", throwIfNotFound: true);
        m_Map_Reset = m_Map.FindAction("Reset", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_LookRate = m_Camera.FindAction("LookRate", throwIfNotFound: true);
        m_Camera_LookRate1 = m_Camera.FindAction("LookRate1", throwIfNotFound: true);
        m_Camera_LookDelta = m_Camera.FindAction("LookDelta", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Map
    private readonly InputActionMap m_Map;
    private IMapActions m_MapActionsCallbackInterface;
    private readonly InputAction m_Map_Jump;
    private readonly InputAction m_Map_Move;
    private readonly InputAction m_Map_Reset;
    public struct MapActions
    {
        private @MyInput m_Wrapper;
        public MapActions(@MyInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Map_Jump;
        public InputAction @Move => m_Wrapper.m_Map_Move;
        public InputAction @Reset => m_Wrapper.m_Map_Reset;
        public InputActionMap Get() { return m_Wrapper.m_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapActions set) { return set.Get(); }
        public void SetCallbacks(IMapActions instance)
        {
            if (m_Wrapper.m_MapActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_MapActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_MapActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnMove;
                @Reset.started -= m_Wrapper.m_MapActionsCallbackInterface.OnReset;
                @Reset.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnReset;
                @Reset.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnReset;
            }
            m_Wrapper.m_MapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Reset.started += instance.OnReset;
                @Reset.performed += instance.OnReset;
                @Reset.canceled += instance.OnReset;
            }
        }
    }
    public MapActions @Map => new MapActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_LookRate;
    private readonly InputAction m_Camera_LookRate1;
    private readonly InputAction m_Camera_LookDelta;
    public struct CameraActions
    {
        private @MyInput m_Wrapper;
        public CameraActions(@MyInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @LookRate => m_Wrapper.m_Camera_LookRate;
        public InputAction @LookRate1 => m_Wrapper.m_Camera_LookRate1;
        public InputAction @LookDelta => m_Wrapper.m_Camera_LookDelta;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @LookRate.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookRate;
                @LookRate.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookRate;
                @LookRate.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookRate;
                @LookRate1.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookRate1;
                @LookRate1.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookRate1;
                @LookRate1.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookRate1;
                @LookDelta.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookDelta;
                @LookDelta.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookDelta;
                @LookDelta.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookDelta;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LookRate.started += instance.OnLookRate;
                @LookRate.performed += instance.OnLookRate;
                @LookRate.canceled += instance.OnLookRate;
                @LookRate1.started += instance.OnLookRate1;
                @LookRate1.performed += instance.OnLookRate1;
                @LookRate1.canceled += instance.OnLookRate1;
                @LookDelta.started += instance.OnLookDelta;
                @LookDelta.performed += instance.OnLookDelta;
                @LookDelta.canceled += instance.OnLookDelta;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    public interface IMapActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnLookRate(InputAction.CallbackContext context);
        void OnLookRate1(InputAction.CallbackContext context);
        void OnLookDelta(InputAction.CallbackContext context);
    }
}
