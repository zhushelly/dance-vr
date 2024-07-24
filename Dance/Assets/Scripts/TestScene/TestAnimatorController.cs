using UnityEngine;

public class TestAnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the game object.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 key pressed, switching to Noh-Suriashi animation.");
            animator.SetTrigger("SwitchToNoh");
            Debug.Log("Current State: " + animator.GetCurrentAnimatorStateInfo(0).IsName("Noh-Suriashi"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2 key pressed, switching to BA-Echappe animation.");
            animator.SetTrigger("SwitchToBA");
            Debug.Log("Current State: " + animator.GetCurrentAnimatorStateInfo(0).IsName("BA-Echappe"));
        }
    }
}
