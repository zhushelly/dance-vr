using RootMotion.FinalIK;
using UnityEngine;

public class JointRotationLimits : MonoBehaviour
{
    public VRIK vrik;
    
    void Start()
    {
        // Assuming vrik.references.leftForearm and vrik.references.rightForearm are the elbow joints
        AddRotationLimit(vrik.references.leftForearm, new Vector3(1, 0, 0), 90); // Limit left elbow to 90 degrees on the X axis
        AddRotationLimit(vrik.references.rightForearm, new Vector3(1, 0, 0), 90); // Limit right elbow to 90 degrees on the X axis
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
