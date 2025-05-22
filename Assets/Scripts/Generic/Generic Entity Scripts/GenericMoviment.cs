using UnityEngine;

public abstract class GenericMovimentComponent : MonoBehaviour 
{

    protected Animator animator;
    protected Rigidbody2D Rigidbody;

    protected virtual float speed => 5f;
    protected virtual bool facingRight { get; set; } = true;

    protected void FixedUpdate() 
    {
        handleMoviment();
        handleMovimentAnimation();
    }

    // scripts
    protected abstract void handleMoviment();

    protected virtual void handleMovimentAnimation()
    {
        animator.SetFloat("Horizontal", Mathf.Abs(Rigidbody.linearVelocityX));
        animator.SetFloat("Vertical", Mathf.Abs(Rigidbody.linearVelocityY));

        //vira o personagem na direção do movimento
        if (Rigidbody.linearVelocityX < 0 && facingRight || Rigidbody.linearVelocityX > 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

    }
}