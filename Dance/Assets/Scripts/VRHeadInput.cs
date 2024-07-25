
using UnityEngine;
using UnityEngine.InputSystem;
using RootMotion.FinalIK;

public class VRHeadInput : MonoBehaviour
{
    public VRIK ik; // Reference to the VRIK component on your avatar
    public InputActionAsset inputActions; // Head tracking Input Actions asset

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
        // Enable actions
        headPosition.Enable();
        headRotation.Enable();
    }

    void OnDisable()
    {
        // Disable actions
        headPosition.Disable();
        headRotation.Disable();
    }

    void Update()
    {
        // Apply head position and rotation to the VRIK target
        if (ik.solver.spine.headTarget != null)
        {
            ik.solver.spine.headTarget.position = headPosition.ReadValue<Vector3>();
            ik.solver.spine.headTarget.rotation = headRotation.ReadValue<Quaternion>();
        }
    }
}
