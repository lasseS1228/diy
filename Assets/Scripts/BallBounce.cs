using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float bounceForce = 5f;  // The force applied when bouncing
    public float gravityScale = 1f; // Gravity affecting the ball
    public Rigidbody2D rb;          // Rigidbody2D of the ball
    public float ballSpeed = 10f;   // Speed at which the ball moves

    private bool isBouncing = true;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        // Set the initial velocity of the ball (moving it along the X-axis)
        rb.linearVelocity = Vector2.right * ballSpeed;
        rb.gravityScale = gravityScale; // Apply gravity scale to the ball
    }

    void Update()
    {
        if (isBouncing)
        {
            // No need to manually apply gravity here as it's handled by Rigidbody2D's gravityScale
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ring"))
        {
            RingDraw ring = collision.gameObject.GetComponent<RingDraw>();

            // Check if the ball has passed through the gap
            if (IsBallThroughGap(collision))
            {
                DestroyRing(collision.gameObject);  // Destroy the ring when the ball passes through the gap
            }
            else
            {
                // Apply bounce force when hitting the ring
                Vector2 bounceDirection = Vector2.Reflect(rb.linearVelocity, collision.contacts[0].normal);
                rb.linearVelocity = bounceDirection * bounceForce;  // Bounce back with the given force
            }
        }
    }

    bool IsBallThroughGap(Collision2D collision)
    {
        // Check if the ball's position is within the gap of the ring (i.e., no collision)
        return Mathf.Abs(transform.position.x) > collision.gameObject.transform.position.x - 0.5f &&
               Mathf.Abs(transform.position.x) < collision.gameObject.transform.position.x + 0.5f;
    }

    void DestroyRing(GameObject ring)
    {
        // Trigger explosion particles
        ParticleSystem explosion = ring.GetComponentInChildren<ParticleSystem>();
        if (explosion != null)
        {
            explosion.Play();  // Play the explosion particles
        }

        // Destroy the ring
        Destroy(ring);
    }
}
