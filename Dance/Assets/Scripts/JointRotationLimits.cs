using RootMotion.FinalIK;
using UnityEngine;

public class ConfigureRotationLimits : MonoBehaviour
{
    public VRIK ik;
    
    void Start()
    {
        // Assuming ik.references.leftForearm and ik.references.rightForearm are the elbow joints
        AddRotationLimit(ik.references.leftForearm, new Vector3(1, 0, 0), 90); // Limit left elbow to 90 degrees on the X axis
        AddRotationLimit(ik.references.rightForearm, new Vector3(1, 0, 0), 90); // Limit right elbow to 90 degrees on the X axis

        // Assuming ik.references.leftHand and ik.references.rightHand are the wrist joints
        AddRotationLimit(ik.references.leftHand, new Vector3(1, 0, 0), 45); // Limit left wrist to 45 degrees on the X axis
        AddRotationLimit(ik.references.leftHand, new Vector3(0, 1, 0), 45); // Limit left wrist to 45 degrees on the Y axis
        AddRotationLimit(ik.references.leftHand, new Vector3(0, 0, 1), 45); // Limit left wrist to 45 degrees on the Z axis

        AddRotationLimit(ik.references.rightHand, new Vector3(1, 0, 0), 45); // Limit right wrist to 45 degrees on the X axis
        AddRotationLimit(ik.references.rightHand, new Vector3(0, 1, 0), 45); // Limit right wrist to 45 degrees on the Y axis
        AddRotationLimit(ik.references.rightHand, new Vector3(0, 0, 1), 45); // Limit right wrist to 45 degrees on the Z axis
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
