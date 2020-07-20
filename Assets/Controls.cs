// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Kyo"",
            ""id"": ""a66a6388-068c-4a9d-8c08-4a4ba9fb1471"",
            ""actions"": [
                {
                    ""name"": ""Attack1"",
                    ""type"": ""Button"",
                    ""id"": ""1e19e594-c7b9-41d3-8645-49c75caedc22"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack2"",
                    ""type"": ""Button"",
                    ""id"": ""f6e112f9-9e48-43da-b32e-3837aaef995c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f562278a-0f1d-4c8e-97cd-282e65683206"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""13a3bd43-a2fe-4a89-b768-18f105fb62f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""6ea49146-3022-4374-aafd-fa3eb3e74f20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""16ad744b-45fd-4ddc-81e9-821b2592c4a5"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9815c79-c903-4b24-9908-e4118fc9d8ca"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6cf62674-a490-4364-aee6-303a39c9ae3e"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fc5a054-9543-4dda-bd4a-1ea2008d5bfa"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38b3bcae-14a7-4542-b177-b23a1cdc5457"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""a8498c37-1d6f-4976-8f2e-3c747d93ef1c"",
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
                    ""id"": ""2c035b2e-06b7-45f4-b3de-06eeaa10af36"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b50fbee5-eb36-411a-afbe-5363e3b13939"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9628f42a-c1ec-4476-a099-4415aac35030"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8611eada-4f1d-4bfe-9cff-12cd9fa175cf"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""77fbd93f-95d8-4067-af89-0e878e3524d4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4993b799-2bfa-4636-878e-bab2459f65c5"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75118546-db22-4285-8095-6b2585a47481"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f065a55f-2a8d-4657-90c1-eab27905e679"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": []
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // Kyo
        m_Kyo = asset.FindActionMap("Kyo", throwIfNotFound: true);
        m_Kyo_Attack1 = m_Kyo.FindAction("Attack1", throwIfNotFound: true);
        m_Kyo_Attack2 = m_Kyo.FindAction("Attack2", throwIfNotFound: true);
        m_Kyo_Move = m_Kyo.FindAction("Move", throwIfNotFound: true);
        m_Kyo_Jump = m_Kyo.FindAction("Jump", throwIfNotFound: true);
        m_Kyo_Dash = m_Kyo.FindAction("Dash", throwIfNotFound: true);
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

    // Kyo
    private readonly InputActionMap m_Kyo;
    private IKyoActions m_KyoActionsCallbackInterface;
    private readonly InputAction m_Kyo_Attack1;
    private readonly InputAction m_Kyo_Attack2;
    private readonly InputAction m_Kyo_Move;
    private readonly InputAction m_Kyo_Jump;
    private readonly InputAction m_Kyo_Dash;
    public struct KyoActions
    {
        private @Controls m_Wrapper;
        public KyoActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack1 => m_Wrapper.m_Kyo_Attack1;
        public InputAction @Attack2 => m_Wrapper.m_Kyo_Attack2;
        public InputAction @Move => m_Wrapper.m_Kyo_Move;
        public InputAction @Jump => m_Wrapper.m_Kyo_Jump;
        public InputAction @Dash => m_Wrapper.m_Kyo_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Kyo; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KyoActions set) { return set.Get(); }
        public void SetCallbacks(IKyoActions instance)
        {
            if (m_Wrapper.m_KyoActionsCallbackInterface != null)
            {
                @Attack1.started -= m_Wrapper.m_KyoActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_KyoActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_KyoActionsCallbackInterface.OnAttack1;
                @Attack2.started -= m_Wrapper.m_KyoActionsCallbackInterface.OnAttack2;
                @Attack2.performed -= m_Wrapper.m_KyoActionsCallbackInterface.OnAttack2;
                @Attack2.canceled -= m_Wrapper.m_KyoActionsCallbackInterface.OnAttack2;
                @Move.started -= m_Wrapper.m_KyoActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_KyoActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_KyoActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_KyoActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KyoActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KyoActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_KyoActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_KyoActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_KyoActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_KyoActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Attack2.started += instance.OnAttack2;
                @Attack2.performed += instance.OnAttack2;
                @Attack2.canceled += instance.OnAttack2;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public KyoActions @Kyo => new KyoActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IKyoActions
    {
        void OnAttack1(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
}
