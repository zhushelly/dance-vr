using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Switch to BA-Echappe animation when pressing '1'
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetBool("SwitchToBA", true);
            animator.SetBool("SwitchToNoh", false);
        }

        // Switch to Noh-Suriashi animation when pressing '2'
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetBool("SwitchToNoh", true);
            animator.SetBool("SwitchToBA", false);
        }
    }
}
