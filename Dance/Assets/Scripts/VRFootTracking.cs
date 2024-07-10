using UnityEngine;

public class VRFootTracking : MonoBehaviour
{
    public Transform headset;
    public Transform leftFootTarget;
    public Transform rightFootTarget;
    public float stepLength = 0.3f;  // Adjust this value based on testing
    public float lateralWidth = 0.2f;  // Distance between left and right foot

    private Vector3 previousPosition;
    private Vector3 movementDirection;
    private float footOffsetSign = 1;  // This will alternate to simulate left/right foot stepping

    void Start()
    {
        if (headset == null) Debug.LogError("Headset not assigned in VRFootTracking script.");
        previousPosition = headset.position;
    }

    void Update()
    {
        // Calculate movement direction
        Vector3 currentPosition = headset.position;
        movementDirection = (currentPosition - previousPosition).normalized;
        float speed = (currentPosition - previousPosition).magnitude / Time.deltaTime;

        // Simple walking animation logic
        if (speed > 0.1f)  // Threshold speed to detect movement
        {
            // Switch foot side each time a step is detected based on step length
            footOffsetSign = -footOffsetSign;
            Vector3 footOffset = movementDirection * stepLength + new Vector3(lateralWidth * footOffsetSign, 0, 0);
            if (footOffsetSign > 0)
            {
                rightFootTarget.position = currentPosition + footOffset;
            }
            else
            {
                leftFootTarget.position = currentPosition + footOffset;
            }
        }

        previousPosition = currentPosition;  // Update the position for the next frame
    }
}
