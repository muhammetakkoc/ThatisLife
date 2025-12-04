using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private bool facingRight = true;
    public bool isChopping = false;

    private Animator animator;
    private Rigidbody2D rb;

    
    private AudioSource footstepAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        footstepAudio = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        
        if (isChopping)
        {
            moveInput = Vector2.zero;
            animator.Play("choppingwood");

            if (footstepAudio.isPlaying)
                footstepAudio.Stop();

            return;
        }

        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveInputX, moveInputY).normalized;

        
        if (moveInput.sqrMagnitude > 0.001f)
        {
            animator.Play("run");

            
            if (!footstepAudio.isPlaying)
                footstepAudio.Play();
        }
        else
        {
            animator.Play("FarmerIdle");

            
            if (footstepAudio.isPlaying)
                footstepAudio.Stop();
        }

        
        if (moveInputX > 0 && !facingRight)
            Flip();
        else if (moveInputX < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
