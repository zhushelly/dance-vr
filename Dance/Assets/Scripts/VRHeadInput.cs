using UnityEngine;
using UnityEngine.InputSystem;
using RootMotion.FinalIK;

public class VRHeadInput : MonoBehaviour
{
    public VRIK ik; // Reference to the VRIK component on your avatar
    public InputActionAsset inputActions; // Input Actions asset

    private InputAction headPosition;
    private InputAction headRotation;

    void Awake()
    {
        // Initialize actions from your input asset
        var actionMap = inputActions.FindActionMap("Head Tracking");
        headPosition = actionMap.FindAction("Head Position");
        headRotation = actionMap.FindAction("Head Rotation");
    }

    void OnEnable()
    {
        headPosition.Enable();
        headRotation.Enable();
    }

    void OnDisable()
    {
        headPosition.Disable();
        headRotation.Disable();
    }

    void Update()
    {
        // Apply head position and rotation to the VRIK target
        if (ik.solver.head.target != null)
        {
            ik.solver.head.target.position = headPosition.ReadValue<Vector3>();
            ik.solver.head.target.rotation = headRotation.ReadValue<Quaternion>();
        }
    }
}
