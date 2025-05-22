using UnityEngine;

public abstract class GenericMovimentComponent : MonoBehaviour 
{

    protected Animator animator;
    protected Rigidbody2D Rigidbody;

    protected virtual float speed => 5f;
    protected virtual bool facingRight { get; set; } = true;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate() 
    {
        handleMoviment();
    }

    // scripts
    protected abstract void handleMoviment();

    protected virtual void handleFlipMovimentAnimation()
    {
        // animator.SetBool("IsFacingRight")

        if (Rigidbody.linearVelocityX < 0 && facingRight == true || Rigidbody.linearVelocityX > 0 && facingRight == false)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}