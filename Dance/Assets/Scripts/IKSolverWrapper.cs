using UnityEngine;
using RootMotion.FinalIK;  

public class IKSolverWrapper : MonoBehaviour
{
    public IKSolverVR ikSolverVR; // Main IK Solver
    public Transform leftHandTarget;
    public Transform rightHandTarget;

    public Transform leftFootTarget;
    public Transform rightFootTarget;


    void Start()
    {
        ikSolverVR = new IKSolverVR();
        ikSolverVR.leftArm = new IKSolverVR.Arm();  // Assuming constructor setups are done here or elsewhere as needed
        ikSolverVR.rightArm = new IKSolverVR.Arm();

        ikSolverVR.leftLeg = new IKSolverVR.Leg();
        ikSolverVR.rightLeg = new IKSolverVR.Leg();

        // Initialize targets - these are assigned in the Unity inspector or via script
        if (leftHandTarget != null && rightHandTarget != null)
        {
            ikSolverVR.leftArm.target = leftHandTarget;
            ikSolverVR.rightArm.target = rightHandTarget;
        }

        if (leftFootTarget != null && rightFootTarget != null)
        {
            ikSolverVR.leftLeg.target = leftFootTarget;
            ikSolverVR.rightLeg.target = rightFootTarget;
        }
    }

    void Update()
    {
        // Handle the boolean for left/right in your context
        ikSolverVR.leftArm.Solve(true);
        ikSolverVR.rightArm.Solve(false);

        ikSolverVR.leftLeg.Solve(true);
        ikSolverVR.rightLeg.Solve(false);

    }
}
