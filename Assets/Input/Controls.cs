// GENERATED AUTOMATICALLY FROM 'Assets/Input/Controls.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Controls : IInputActionCollection
{
    private InputActionAsset asset;
    public Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""9c80c0ca-06cd-467f-9a38-4818824455ba"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""14991b38-505c-494f-908f-8f174119ba28"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability One"",
                    ""type"": ""Button"",
                    ""id"": ""df65880c-e352-43cb-a236-f9dfc8288fb3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability Two"",
                    ""type"": ""Button"",
                    ""id"": ""95b1e40c-22d2-4b82-bcf4-767ccadbb71e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability Three"",
                    ""type"": ""Button"",
                    ""id"": ""1b4be438-17c8-4153-aaf1-207d20fb4bb5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""31467b42-e25c-46ef-a502-6e8c9f707867"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""2055fc4a-35db-4123-b978-107fc4cc361a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start Button"",
                    ""type"": ""Button"",
                    ""id"": ""8f0842ba-f4c1-429a-9b0a-f611e56e5d47"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a7dc311f-d92c-43de-aab6-077de8f6c901"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c93306dc-189e-40a7-9f6c-4b7bbb02a470"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db767148-3ee9-420a-948a-ae37a9fd17b1"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Ability One"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c87b0891-a7cd-4123-9803-9510e026118d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Ability Two"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a7e4a14-42d4-4017-9aa4-f931731452d4"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Ability Three"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a0d672a-cf39-4921-93cc-c390765404db"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da514998-c4c8-46ce-bc25-72ceb1087f38"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Start Button"",
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
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_AbilityOne = m_Player.FindAction("Ability One", throwIfNotFound: true);
        m_Player_AbilityTwo = m_Player.FindAction("Ability Two", throwIfNotFound: true);
        m_Player_AbilityThree = m_Player.FindAction("Ability Three", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Block = m_Player.FindAction("Block", throwIfNotFound: true);
        m_Player_StartButton = m_Player.FindAction("Start Button", throwIfNotFound: true);
    }

    ~Controls()
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_AbilityOne;
    private readonly InputAction m_Player_AbilityTwo;
    private readonly InputAction m_Player_AbilityThree;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Block;
    private readonly InputAction m_Player_StartButton;
    public struct PlayerActions
    {
        private Controls m_Wrapper;
        public PlayerActions(Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @AbilityOne => m_Wrapper.m_Player_AbilityOne;
        public InputAction @AbilityTwo => m_Wrapper.m_Player_AbilityTwo;
        public InputAction @AbilityThree => m_Wrapper.m_Player_AbilityThree;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Block => m_Wrapper.m_Player_Block;
        public InputAction @StartButton => m_Wrapper.m_Player_StartButton;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                AbilityOne.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbilityOne;
                AbilityOne.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbilityOne;
                AbilityOne.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbilityOne;
                AbilityTwo.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbilityTwo;
                AbilityTwo.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbilityTwo;
                AbilityTwo.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbilityTwo;
                AbilityThree.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbilityThree;
                AbilityThree.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbilityThree;
                AbilityThree.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbilityThree;
                Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Block.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                Block.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                Block.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                StartButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartButton;
                StartButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartButton;
                StartButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartButton;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                AbilityOne.started += instance.OnAbilityOne;
                AbilityOne.performed += instance.OnAbilityOne;
                AbilityOne.canceled += instance.OnAbilityOne;
                AbilityTwo.started += instance.OnAbilityTwo;
                AbilityTwo.performed += instance.OnAbilityTwo;
                AbilityTwo.canceled += instance.OnAbilityTwo;
                AbilityThree.started += instance.OnAbilityThree;
                AbilityThree.performed += instance.OnAbilityThree;
                AbilityThree.canceled += instance.OnAbilityThree;
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.canceled += instance.OnMovement;
                Block.started += instance.OnBlock;
                Block.performed += instance.OnBlock;
                Block.canceled += instance.OnBlock;
                StartButton.started += instance.OnStartButton;
                StartButton.performed += instance.OnStartButton;
                StartButton.canceled += instance.OnStartButton;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnAbilityOne(InputAction.CallbackContext context);
        void OnAbilityTwo(InputAction.CallbackContext context);
        void OnAbilityThree(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnStartButton(InputAction.CallbackContext context);
    }
}
