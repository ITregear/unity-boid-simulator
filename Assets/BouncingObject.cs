using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    public Vector2 initialVelocity = new Vector2(5f, 5f);
    private Rigidbody2D rb;
    public float leftBound = -1f, rightBound = 1f, topBound = 1f, bottomBound = -1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = initialVelocity;
    }

    void Update()
    {
        // Check for bounds and reverse the velocity if necessary
        if (transform.position.x > rightBound && rb.velocity.x > 0 || transform.position.x < leftBound && rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
        
        if (transform.position.y > topBound && rb.velocity.y > 0 || transform.position.y < bottomBound && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
    }

    // Optional: Draw the bounding box using Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(leftBound, topBound), new Vector2(rightBound, topBound));
        Gizmos.DrawLine(new Vector2(rightBound, topBound), new Vector2(rightBound, bottomBound));
        Gizmos.DrawLine(new Vector2(rightBound, bottomBound), new Vector2(leftBound, bottomBound));
        Gizmos.DrawLine(new Vector2(leftBound, bottomBound), new Vector2(leftBound, topBound));
    }
}
