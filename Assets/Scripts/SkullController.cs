using UnityEngine;

public class SkullController : MonoBehaviour
{
    public float vanishDelay = 10f; 

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke(nameof(TriggerVanish), vanishDelay);
    }

    void TriggerVanish()
    {
        if (animator != null)
            animator.SetBool("isVanishing", true);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}