using UnityEngine;
using UnityEngine.InputSystem;

public class ModeSwitcher : MonoBehaviour
{
    public Animator animator;
    private ModeSwitchInput inputActions;
    private bool isAnimationPlaying = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        // Initialize input actions
        inputActions = new ModeSwitchInput();
    }

    private void OnEnable()
    {
        // Register callback for the toggle mode action
        inputActions.PlayerControls.ToggleMode.performed += OnToggleMode;
        inputActions.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        // Unregister callback for the toggle mode action
        inputActions.PlayerControls.ToggleMode.performed -= OnToggleMode;
        inputActions.PlayerControls.Disable();
    }

    private void OnToggleMode(InputAction.CallbackContext context)
    {
        if (isAnimationPlaying)
        {
            SwitchToVRMode();
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
        else
        {
            SwitchToAnimationMode();
        }
    }

    void SwitchToAnimationMode()
    {
        animator.enabled = true; // Enable the animator to play the animation
        isAnimationPlaying = true;
    }

    void SwitchToVRMode()
    {
        animator.enabled = false; // Disable the animator to stop the animation
        isAnimationPlaying = false;
    }
}
