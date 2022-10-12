//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Input/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Controller"",
            ""id"": ""5a478e87-d645-4b4d-9123-f721d21216c4"",
            ""actions"": [
                {
                    ""name"": ""Left Stick Movement"",
                    ""type"": ""Value"",
                    ""id"": ""da370eb5-1234-4b3a-bb11-b28b1b202bc9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Right Stick Look"",
                    ""type"": ""Value"",
                    ""id"": ""16a69c1b-9d2a-4804-b158-97d5d121c455"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Left Stick Click"",
                    ""type"": ""Button"",
                    ""id"": ""37f02de3-4da5-4f59-8b47-47f8b3711648"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right Stick Click"",
                    ""type"": ""Button"",
                    ""id"": ""e44e1178-3451-4b08-a155-1ffe0f9f181f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d199a995-7bcf-4612-bf05-6f4c8b71194c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""755a1e44-0a15-4ec0-8634-6e40788c9330"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f0b02b7a-8ce0-48d7-9b26-57ae2d5f8279"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Right Stick Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""919eeca8-10db-4e48-a8f8-df3eebf60f0c"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right Stick Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6887d864-1e6f-4284-bffd-9f18ed89ae94"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Left Stick Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""bcbca5c8-76b8-4077-9de7-b467b00ddcc2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0db96847-f19c-4014-a627-da1b5288122d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Left Stick Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a12ad029-6802-40e0-b7d5-d8eb3ced7a3f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Left Stick Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0d3e07f7-ca3b-4f09-978a-44274b100af0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Left Stick Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""774ced74-3657-47c5-b5ed-2570f8477d61"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Left Stick Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3db8640a-f68b-436f-a4bf-850d007d3e55"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad41a781-8a9f-41fa-b050-3fb5f26f4ef3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43889a26-3605-4408-8d9d-4b4aa49f5886"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47046470-0db9-43af-a132-335f155e236e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f88eb4ff-016a-4e7d-9249-7ee625a4e2f5"",
                    ""path"": ""<XInputController>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1855765a-19e0-4e13-82df-93613a875336"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21d04bf2-7b1b-4fa7-91d9-ed7f6fcd1d01"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right Stick Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6076271-61b2-4751-96b7-c0db25685532"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right Stick Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Controller
        m_Controller = asset.FindActionMap("Controller", throwIfNotFound: true);
        m_Controller_LeftStickMovement = m_Controller.FindAction("Left Stick Movement", throwIfNotFound: true);
        m_Controller_RightStickLook = m_Controller.FindAction("Right Stick Look", throwIfNotFound: true);
        m_Controller_LeftStickClick = m_Controller.FindAction("Left Stick Click", throwIfNotFound: true);
        m_Controller_RightStickClick = m_Controller.FindAction("Right Stick Click", throwIfNotFound: true);
        m_Controller_Interact = m_Controller.FindAction("Interact", throwIfNotFound: true);
        m_Controller_Menu = m_Controller.FindAction("Menu", throwIfNotFound: true);
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

    // Controller
    private readonly InputActionMap m_Controller;
    private IControllerActions m_ControllerActionsCallbackInterface;
    private readonly InputAction m_Controller_LeftStickMovement;
    private readonly InputAction m_Controller_RightStickLook;
    private readonly InputAction m_Controller_LeftStickClick;
    private readonly InputAction m_Controller_RightStickClick;
    private readonly InputAction m_Controller_Interact;
    private readonly InputAction m_Controller_Menu;
    public struct ControllerActions
    {
        private @PlayerControls m_Wrapper;
        public ControllerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftStickMovement => m_Wrapper.m_Controller_LeftStickMovement;
        public InputAction @RightStickLook => m_Wrapper.m_Controller_RightStickLook;
        public InputAction @LeftStickClick => m_Wrapper.m_Controller_LeftStickClick;
        public InputAction @RightStickClick => m_Wrapper.m_Controller_RightStickClick;
        public InputAction @Interact => m_Wrapper.m_Controller_Interact;
        public InputAction @Menu => m_Wrapper.m_Controller_Menu;
        public InputActionMap Get() { return m_Wrapper.m_Controller; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllerActions set) { return set.Get(); }
        public void SetCallbacks(IControllerActions instance)
        {
            if (m_Wrapper.m_ControllerActionsCallbackInterface != null)
            {
                @LeftStickMovement.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnLeftStickMovement;
                @LeftStickMovement.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnLeftStickMovement;
                @LeftStickMovement.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnLeftStickMovement;
                @RightStickLook.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnRightStickLook;
                @RightStickLook.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnRightStickLook;
                @RightStickLook.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnRightStickLook;
                @LeftStickClick.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnLeftStickClick;
                @LeftStickClick.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnLeftStickClick;
                @LeftStickClick.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnLeftStickClick;
                @RightStickClick.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnRightStickClick;
                @RightStickClick.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnRightStickClick;
                @RightStickClick.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnRightStickClick;
                @Interact.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteract;
                @Menu.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMenu;
            }
            m_Wrapper.m_ControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftStickMovement.started += instance.OnLeftStickMovement;
                @LeftStickMovement.performed += instance.OnLeftStickMovement;
                @LeftStickMovement.canceled += instance.OnLeftStickMovement;
                @RightStickLook.started += instance.OnRightStickLook;
                @RightStickLook.performed += instance.OnRightStickLook;
                @RightStickLook.canceled += instance.OnRightStickLook;
                @LeftStickClick.started += instance.OnLeftStickClick;
                @LeftStickClick.performed += instance.OnLeftStickClick;
                @LeftStickClick.canceled += instance.OnLeftStickClick;
                @RightStickClick.started += instance.OnRightStickClick;
                @RightStickClick.performed += instance.OnRightStickClick;
                @RightStickClick.canceled += instance.OnRightStickClick;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
            }
        }
    }
    public ControllerActions @Controller => new ControllerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IControllerActions
    {
        void OnLeftStickMovement(InputAction.CallbackContext context);
        void OnRightStickLook(InputAction.CallbackContext context);
        void OnLeftStickClick(InputAction.CallbackContext context);
        void OnRightStickClick(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
}
