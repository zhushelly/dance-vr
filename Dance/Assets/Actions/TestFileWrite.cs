//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Actions/TestFileWrite.inputactions
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

public partial class @TestFileWrite: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @TestFileWrite()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TestFileWrite"",
    ""maps"": [
        {
            ""name"": ""Control"",
            ""id"": ""b91929a1-ffd1-4ff0-81cc-75a2a3cfe401"",
            ""actions"": [
                {
                    ""name"": ""Write"",
                    ""type"": ""Button"",
                    ""id"": ""30f6c8c4-80e6-4d2f-b777-f74a4bd4c1bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""203bb528-87f6-42b7-bb4d-75716d1d17af"",
                    ""path"": ""<OculusTouchController>/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Write"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Control
        m_Control = asset.FindActionMap("Control", throwIfNotFound: true);
        m_Control_Write = m_Control.FindAction("Write", throwIfNotFound: true);
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

    // Control
    private readonly InputActionMap m_Control;
    private List<IControlActions> m_ControlActionsCallbackInterfaces = new List<IControlActions>();
    private readonly InputAction m_Control_Write;
    public struct ControlActions
    {
        private @TestFileWrite m_Wrapper;
        public ControlActions(@TestFileWrite wrapper) { m_Wrapper = wrapper; }
        public InputAction @Write => m_Wrapper.m_Control_Write;
        public InputActionMap Get() { return m_Wrapper.m_Control; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlActions set) { return set.Get(); }
        public void AddCallbacks(IControlActions instance)
        {
            if (instance == null || m_Wrapper.m_ControlActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ControlActionsCallbackInterfaces.Add(instance);
            @Write.started += instance.OnWrite;
            @Write.performed += instance.OnWrite;
            @Write.canceled += instance.OnWrite;
        }

        private void UnregisterCallbacks(IControlActions instance)
        {
            @Write.started -= instance.OnWrite;
            @Write.performed -= instance.OnWrite;
            @Write.canceled -= instance.OnWrite;
        }

        public void RemoveCallbacks(IControlActions instance)
        {
            if (m_Wrapper.m_ControlActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IControlActions instance)
        {
            foreach (var item in m_Wrapper.m_ControlActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ControlActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ControlActions @Control => new ControlActions(this);
    public interface IControlActions
    {
        void OnWrite(InputAction.CallbackContext context);
    }
}