using UnityEngine;
using UnityEngine.InputSystem;
using RootMotion.FinalIK;

public class VRControllerInput : MonoBehaviour
{
    public VRIK ik; // Reference to the VRIK component on your avatar
    public InputActionAsset inputActions; // Your Input Actions asset

    private InputAction handPosition;
    private InputAction handRotation;

    void Awake()
    {
        var actionMap = inputActions.FindActionMap("Hand Tracking");
        handPosition = actionMap.FindAction("Hand Position");
        handRotation = actionMap.FindAction("Hand Rotation");
    }

    void OnEnable()
    {
        handPosition.Enable();
        handRotation.Enable();
    }

    void OnDisable()
    {
        handPosition.Disable();
        handRotation.Disable();
    }

    void Update()
    {
        ik.solver.leftArm.target.position = handPosition.ReadValue<Vector3>();
        ik.solver.leftArm.target.rotation = handRotation.ReadValue<Quaternion>();
        // Repeat for the right arm or other parts as needed
    }
}
