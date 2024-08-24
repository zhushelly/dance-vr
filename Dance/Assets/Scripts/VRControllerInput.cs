using UnityEngine;
using UnityEngine.InputSystem;
using RootMotion.FinalIK;

public class VRControllerInput : MonoBehaviour
{
    public VRIK ik; // Reference to the VRIK component on avatar
    public InputActionAsset inputActions; // Controller tracking Input Actions asset

    private InputAction leftHandPosition;
    private InputAction leftHandRotation;
    private InputAction rightHandPosition;
    private InputAction rightHandRotation;

    // Corrective rotations - start with an assumption and adjust based on testing
    private Quaternion leftHandCorrection = Quaternion.Euler(0, 0, 270);
    private Quaternion rightHandCorrection = Quaternion.Euler(0, 0, 270);

    void Awake()
    {
        var actionMap = inputActions.FindActionMap("Hand Tracking");
        leftHandPosition = actionMap.FindAction("Left Hand Position");
        leftHandRotation = actionMap.FindAction("Left Hand Rotation");
        rightHandPosition = actionMap.FindAction("Right Hand Position");
        rightHandRotation = actionMap.FindAction("Right Hand Rotation");
    }

    void OnEnable()
    {
        // Enable actions
        leftHandPosition.Enable();
        leftHandRotation.Enable();
        rightHandPosition.Enable();
        rightHandRotation.Enable();
    }

    void OnDisable()
    {
        // Disable actions
        leftHandPosition.Disable();
        leftHandRotation.Disable();
        rightHandPosition.Disable();
        rightHandRotation.Disable();
    }

    void Update()
    {
        // Apply hand positions and rotations to the VRIK targets
        if (ik.solver.leftArm.target != null)
        {
            ik.solver.leftArm.target.position = leftHandPosition.ReadValue<Vector3>();
            ik.solver.leftArm.target.rotation = leftHandRotation.ReadValue<Quaternion>() * leftHandCorrection;
        }
        if (ik.solver.rightArm.target != null)
        {
            ik.solver.rightArm.target.position = rightHandPosition.ReadValue<Vector3>();
            ik.solver.rightArm.target.rotation = rightHandRotation.ReadValue<Quaternion>() * rightHandCorrection;
        }
    }
}
