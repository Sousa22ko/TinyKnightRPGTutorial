using UnityEngine;

public abstract class GenericMovimentComponent : MonoBehaviour 
{

    protected Entity entity;

    protected virtual float speed => 5f;
    public bool facingRight = true;


    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
    }

    protected void FixedUpdate() 
    {
        handleMoviment();
    }

    // scripts
    protected abstract void handleMoviment();

    protected virtual void handleFlipMovimentAnimation()
    {

        if (entity.rigidBody.linearVelocityX < 0 && facingRight == true || entity.rigidBody.linearVelocityX > 0 && facingRight == false)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}