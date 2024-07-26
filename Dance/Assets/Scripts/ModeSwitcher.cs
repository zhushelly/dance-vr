using UnityEngine;
using UnityEngine.InputSystem;

public class ModeSwitcher : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public GameObject leftHandController; // Reference to the Left Hand Controller
    public GameObject rightHandController; // Reference to the Right Hand Controller
    public GameObject headset;
    public VRControllerInput vrControllerInput; // Reference to VRControllerInput script
    public VRHeadInput vrHeadInput; // Reference to VRHeadInput script

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
        headset.SetActive(false); // Disable headset input
        vrControllerInput.enabled = false; // Disable VRControllerInput script
        vrHeadInput.enabled = false; // Disable VRHeadInput script
        isAnimationPlaying = true;
    }

    void SwitchToVRMode()
    {
        animator.enabled = false; // Disable the animator to stop the animation
        leftHandController.SetActive(true); // Enable left hand controller input
        rightHandController.SetActive(true); // Enable right hand controller input
        headset.SetActive(true); // Enable headset input
        vrControllerInput.enabled = true; // Enable VRControllerInput script
        vrHeadInput.enabled = true; // Enable VRHeadInput script
        isAnimationPlaying = false;

        // Reinitialize VR inputs to ensure they are correctly reset
        vrControllerInput.Reinitialize();
        vrHeadInput.Reinitialize();
    }
}
