using UnityEngine;
using UnityEngine.InputSystem;

public class PlaybackSpeedController : MonoBehaviour
{
    public Animator animator;
    private PlaybackSpeed inputActions;
    // private float baseSpeed = 1f;
    private float maxSpeed = 3f;
    private float minSpeed = 0.1f;

    private void Awake()
    {
        inputActions = new PlaybackSpeed();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        Vector2 stickInput = inputActions.PlayerControls.ChangeSpeed.ReadValue<Vector2>();
        float speedFactor = stickInput.x; // Assuming you use the horizontal axis for speed control
        float newSpeed = Mathf.Lerp(minSpeed, maxSpeed, (speedFactor + 1) / 2); // Normalize input to 0-1 range and then scale
        animator.speed = newSpeed;
    }
}
