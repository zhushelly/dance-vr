using UnityEngine;
using UnityEngine.InputSystem;
using RootMotion.FinalIK;

public class VRLegControllerInput : MonoBehaviour
{
    public VRIK ik; // Reference to the VRIK component on avatar
    public InputActionAsset inputActions; // Controller tracking Input Actions asset

    private InputAction leftFootPosition;
    private InputAction leftFootRotation;
    private InputAction rightFootPosition;
    private InputAction rightFootRotation;

    void Awake()
    {
        var actionMap = inputActions.FindActionMap("Hand Tracking");
        leftFootPosition = actionMap.FindAction("Left Hand Position");
        leftFootRotation = actionMap.FindAction("Left Hand Rotation");
        rightFootPosition = actionMap.FindAction("Right Hand Position");
        rightFootRotation = actionMap.FindAction("Right Hand Rotation");
    }

    void OnEnable()
    {
        // Enable leg actions
        leftFootPosition.Enable();
        leftFootRotation.Enable();
        rightFootPosition.Enable();
        rightFootRotation.Enable();
    }

    void OnDisable()
    {
        // Disable leg actions
        leftFootPosition.Disable();
        leftFootRotation.Disable();
        rightFootPosition.Disable();
        rightFootRotation.Disable();
    }

    void Update()
    {
        // Apply foot positions and rotations to the VRIK targets
        if (ik.solver.leftLeg.target != null)
        {
            ik.solver.leftLeg.target.position = leftFootPosition.ReadValue<Vector3>();
            ik.solver.leftLeg.target.rotation = leftFootRotation.ReadValue<Quaternion>();
        }
        if (ik.solver.rightLeg.target != null)
        {
            ik.solver.rightLeg.target.position = rightFootPosition.ReadValue<Vector3>();
            ik.solver.rightLeg.target.rotation = rightFootRotation.ReadValue<Quaternion>();
        }
    }
}
