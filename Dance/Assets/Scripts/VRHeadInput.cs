using UnityEngine;
using UnityEngine.InputSystem;
using RootMotion.FinalIK;

public class VRHeadInput : MonoBehaviour
{
    public VRIK ik; // Reference to the VRIK component on your avatar
    public Transform headTarget; // Direct reference to the Head Target Transform
    public InputActionAsset inputActions; // Head tracking Input Actions asset

    private InputAction headPosition;
    private InputAction headRotation;

    void Awake()
    {
        var actionMap = inputActions.FindActionMap("Head Tracking");
        headPosition = actionMap.FindAction("Head Position");
        headRotation = actionMap.FindAction("Head Rotation");

        // Ensure the headTarget is assigned
        if (headTarget == null && ik != null)
            headTarget = ik.references.head; // HeadTarget assigned in inspector
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
        // Directly update the HeadTarget's Transform
        if (headTarget != null)
        {
            headTarget.position = headPosition.ReadValue<Vector3>();
            headTarget.rotation = headRotation.ReadValue<Quaternion>();
        }
    }
}
