using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationSwitch : MonoBehaviour
{
    public Animator animator;
    private InputAction toggleAction;
    private string currentAnimation = "mixamo.com"; // Initial state name

    void Start()
    {
        // Assuming your Input Action Asset is called "ModeSwitchInput"
        var inputActionAsset = GetComponent<PlayerInput>().actions;
        toggleAction = inputActionAsset["ToggleMode"];
        
        // Subscribe to the action performed event
        toggleAction.performed += ctx => SwitchAnimation();
    }

    void OnEnable()
    {
        toggleAction.Enable();
    }

    void OnDisable()
    {
        toggleAction.Disable();
    }

    private void SwitchAnimation()
    {
        // Get the current animation state name
        var currentState = animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation);

        if (currentAnimation == "mixamo.com")
        {
            animator.Play("Hip Hop Dancing");
            currentAnimation = "Hip Hop Dancing";
        }
        else if (currentAnimation == "Hip Hop Dancing")
        {
            animator.Play("mixamo.com");
            currentAnimation = "mixamo.com";
        }
    }
}
