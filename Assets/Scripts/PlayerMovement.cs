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
        // Vérifier si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Déplacement horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        // Mettre à jour l'animation de vitesse
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        }

        // Inverser la direction du sprite si nécessaire
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        // Sauter si au sol et bouton de saut pressé
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Mettre à jour l'état "Grounded" dans l'Animator
        if (animator != null)
        {
            animator.SetBool("Ground", isGrounded);
        }
    }

    // Retourner le sprite horizontalement
    void Flip()
    {
        facingRight = !facingRight; // Change la direction actuelle
        Vector3 scale = transform.localScale;
        scale.x *= -1;             // Inverse l'échelle sur l'axe X
        transform.localScale = scale;
    }

    // Afficher la zone de détection dans la scène pour débogage
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
