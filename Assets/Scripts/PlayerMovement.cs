using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    private bool isGrounded;

    [Header("References")]
    private Rigidbody2D rb;
    private Animator animator;

    private bool facingRight = true; // Indique si le personnage regarde vers la droite

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Déplacement horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        // Mise à jour du paramètre "Speed"
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Inverser le sprite en fonction de la direction
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        // Gérer le saut
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        // Mise à jour des paramètres liés au sol et à la vitesse verticale
        animator.SetBool("Ground", isGrounded);
        animator.SetFloat("vSpeed", rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie si le joueur touche le sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Vérifie si le joueur quitte le sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Retourne le sprite horizontalement
    void Flip()
    {
        facingRight = !facingRight; // Change la direction actuelle
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Inverse l'échelle sur l'axe X
        transform.localScale = scale;
    }

    // Exemple pour déclencher une animation d'attaque
    public void Attack()
    {
        animator.SetBool("Attack", true);
    }

    // Exemple pour déclencher une animation de blessure
    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    // Exemple pour déclencher une animation de mort
    public void Die()
    {
        animator.SetBool("Dead", true);
    }
}
