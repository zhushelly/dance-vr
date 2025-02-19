using RootMotion.FinalIK;
using UnityEngine;

public class JointRotationLimits : MonoBehaviour
{
    public VRIK ik;
    
    void Start()
    {
        // Assuming ik.references.leftForearm and ik.references.rightForearm are the elbow joints
        AddRotationLimit(ik.references.leftForearm, new Vector3(1, 0, 0), 180); // Limit left elbow to 180 degrees on the X axis
        AddRotationLimit(ik.references.rightForearm, new Vector3(1, 0, 0), 180); // Limit right elbow to 180 degrees on the X axis

        // Assuming ik.references.leftHand and ik.references.rightHand are the wrist joints
        AddRotationLimit(ik.references.leftHand, new Vector3(1, 0, 0), 90); // Limit left wrist to 90 degrees on the X axis
        AddRotationLimit(ik.references.leftHand, new Vector3(0, 1, 0), 90); // Limit left wrist to 90 degrees on the Y axis
        AddRotationLimit(ik.references.leftHand, new Vector3(0, 0, 1), 90); // Limit left wrist to 90 degrees on the Z axis

        AddRotationLimit(ik.references.rightHand, new Vector3(1, 0, 0), 90); // Limit right wrist to 90 degrees on the X axis
        AddRotationLimit(ik.references.rightHand, new Vector3(0, 1, 0), 90); // Limit right wrist to 90 degrees on the Y axis
        AddRotationLimit(ik.references.rightHand, new Vector3(0, 0, 1), 90); // Limit right wrist to 90 degrees on the Z axis

        // Assuming ik.references.leftUpperArm and ik.references.rightUpperArm are the shoulder joints
        // Set appropriate limits for shoulder joint rotations
        AddRotationLimit(ik.references.leftUpperArm, new Vector3(1, 0, 0), 90); // Limit left shoulder to 90 degrees on the X axis
        AddRotationLimit(ik.references.leftUpperArm, new Vector3(0, 1, 0), 90); // Limit left shoulder to 90 degrees on the Y axis
        AddRotationLimit(ik.references.leftUpperArm, new Vector3(0, 0, 1), 45); // Limit left shoulder to 45 degrees on the Z axis

        AddRotationLimit(ik.references.rightUpperArm, new Vector3(1, 0, 0), 90); // Limit right shoulder to 90 degrees on the X axis
        AddRotationLimit(ik.references.rightUpperArm, new Vector3(0, 1, 0), 90); // Limit right shoulder to 90 degrees on the Y axis
        AddRotationLimit(ik.references.rightUpperArm, new Vector3(0, 0, 1), 45); // Limit right shoulder to 45 degrees on the Z axis
    }

    void AddRotationLimit(Transform joint, Vector3 axis, float limit)
    {
        // Add RotationLimitAngle component if not already present
        RotationLimitAngle rotationLimit = joint.GetComponent<RotationLimitAngle>();
        if (rotationLimit == null)
        {
            rotationLimit = joint.gameObject.AddComponent<RotationLimitAngle>();
        }

        // Configure the rotation limit
        rotationLimit.axis = axis; // Set the axis
        rotationLimit.limit = limit; // Set the limit
    }
}
