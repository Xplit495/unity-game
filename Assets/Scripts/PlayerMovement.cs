using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;         // Vitesse de déplacement
    public float jumpForce = 10f;   // Force de saut

    [Header("Ground Check")]
    public Transform groundCheck;   // Position pour vérifier le sol
    public float groundCheckRadius = 0.2f; // Rayon pour la détection du sol
    public LayerMask groundLayer;   // Masque pour le sol

    private bool isGrounded;        // Indique si le joueur est au sol
    private Rigidbody2D rb;         // Référence au Rigidbody2D
    private Animator animator;      // Référence à l'Animator
    private bool facingRight = true; // Indique si le personnage regarde à droite

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Déplacement horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        }

        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (animator != null)
        {
            animator.SetBool("Ground", isGrounded);
        }
    }

    void Flip()
    {
        facingRight = !facingRight; 
        Vector3 scale = transform.localScale;
        scale.x *= -1;             
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
