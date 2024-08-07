using UnityEngine;
using UnityEngine.InputSystem;
public class ModeSwitcher : MonoBehaviour
{
    public Animator animator;
    public GameObject leftController;
    public GameObject rightController;
    public GameObject headset;
    private ModeSwitchInput inputActions;
    private bool isAnimationPlaying = true;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private void Awake()
    {
        // Save the original position and rotation of the character
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
            // Restore original position and rotation in VR mode
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
        leftController.SetActive(false);
        rightController.SetActive(false); 
        headset.SetActive(false);
        isAnimationPlaying = true;
    }
    void SwitchToVRMode()
    {
        animator.enabled = false; // Disable the animator to stop the animation
        leftController.SetActive(true); // Enable left hand controller input
        rightController.SetActive(true); // Enable right hand controller input
        headset.SetActive(true);
        isAnimationPlaying = false;
    }
}

