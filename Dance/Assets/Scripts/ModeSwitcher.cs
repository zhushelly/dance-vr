using UnityEngine;
using UnityEngine.InputSystem;

public class ModeSwitcher : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public GameObject leftHandController; // Reference to the Left Hand Controller
    public GameObject rightHandController; // Reference to the Right Hand Controller
    public GameObject headset;

    private bool isAnimationPlaying = true; // Track the current mode
    private ModeSwitchInput inputActions; // Input actions reference

    void Awake()
    {
        inputActions = new ModeSwitchInput(); // Initialize input actions
    }

    void OnEnable()
    {
        inputActions.PlayerControls.ToggleMode.performed += OnToggleMode; // Register callback
        inputActions.PlayerControls.Enable(); // Enable the action map
    }

    void OnDisable()
    {
        inputActions.PlayerControls.ToggleMode.performed -= OnToggleMode; // Unregister callback
        inputActions.PlayerControls.Disable(); // Disable the action map
    }

    void OnToggleMode(InputAction.CallbackContext context)
    {
        if (isAnimationPlaying)
        {
            SwitchToVRMode();
        }
        else
        {
            SwitchToAnimationMode();
        }
    }

    void SwitchToAnimationMode()
    {
        animator.enabled = true; // Enable the animator to play the animation
        leftHandController.SetActive(false); // Disable left hand controller input
        rightHandController.SetActive(false); // Disable right hand controller input
        headset.SetActive(false);
        isAnimationPlaying = true;
    }

    void SwitchToVRMode()
    {
        animator.enabled = false; // Disable the animator to stop the animation
        leftHandController.SetActive(true); // Enable left hand controller input
        rightHandController.SetActive(true); // Enable right hand controller input
        headset.SetActive(true);
        isAnimationPlaying = false;
    }
}