using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{

    // public variables 

    public InputActionAsset InputActions;
    public Rigidbody2D Rigidbody;
    public float speed = 5;
    public Animator animator;


    // private internal variables 
    private bool facingRight = true;
    private InputAction moveActions;


    // lifecycle 
    private void OnEnable() => InputActions.FindActionMap("Player").Enable();

    private void OnDisable() => InputActions.FindActionMap("Player").Disable();

    private void Awake() => moveActions = InputActions.FindActionMap("Player").FindAction("Move");

    void FixedUpdate()
    {
        handleMoviment();
    }


    // scripts
    private void handleMoviment() 
    {
        Rigidbody.linearVelocity = moveActions.ReadValue<Vector2>().normalized * speed;
        handleMovimentAnimation();
    }

    void handleMovimentAnimation() 
    { 
        animator.SetFloat("Horizontal", Mathf.Abs(Rigidbody.linearVelocityX));
        animator.SetFloat("Vertical", Mathf.Abs(Rigidbody.linearVelocityY));

        //vira o personagem na direção do movimento
        if (Rigidbody.linearVelocityX < 0 && facingRight || Rigidbody.linearVelocityX > 0 && !facingRight) {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    
    }
}
