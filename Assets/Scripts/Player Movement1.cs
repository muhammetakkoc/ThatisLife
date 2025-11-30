using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private bool facingRight = true;
    public bool isChopping=false;

    private Animator animator;

    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2 (moveInputX, moveInputY).normalized;

        //if (chopwood)
        //{
        //    animator.Play("choppingwood");
        //}

        if (moveInput.magnitude > 0.01f)
        {
            animator.Play("run"); 
        }
        else 
        {
            animator.Play("FarmerIdle");
        }


        if (moveInputX > 0 && !facingRight)
            Flip();
        else if (moveInputX < 0 && facingRight)
            Flip();

        if (isChopping)
        {
            animator.Play("choppingwood");
            rb.linearVelocity = Vector2.zero;
            return;
            Debug.Log("agac kesiyor ");
        }
    }

    private void FixedUpdate()
    {
        

        rb.linearVelocity = moveInput * moveSpeed;
        
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
