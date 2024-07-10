using UnityEngine;

public class IKSolverWrapper : MonoBehaviour
{
    public IKSolverVR ikSolverVR;

    void Start()
    {
        ikSolverVR = new IKSolverVR();
        // Initialize your IK solver here
    }

    void Update()
    {
        ikSolverVR.UpdateSolver();  // Hypothetical method
    }
}
