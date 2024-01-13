using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public Vector2 velocity;
    public float maxSpeed = 5f;
    public float neighbourRadius = 3f;

    public Vector2 separationVector;
    public Vector2 alignmentVector;
    public Vector2 cohesionVector;

    private List<Boid> neighbours;

    void Start()
    {
        velocity = new Vector2(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed));
        neighbours = new List<Boid>();
    }

    void Update()
    {
        FindNeighbours();
        ApplyRules();
        Move();
        WrapAround();
    }

    void ApplyRules(){
        separationVector = Separate();
        alignmentVector = Align();
        // cohesionVector = Cohere();

        velocity += separationVector + alignmentVector;
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
    }

    Vector2 Separate()
    {
        // Separation rule
        Vector2 steering = new Vector2();
        int count = 0;

        foreach (Boid neighbour in neighbours)
        {
            float distance = Vector2.Distance(transform.position, neighbour.transform.position);
            if (distance > 0 && distance < neighbourRadius)
            {
                Vector2 difference = (Vector2)(transform.position - neighbour.transform.position);
                difference = difference.normalized / distance;
                steering += difference;
                count ++;
            }
        }

        if (count > 0)
        {
            steering /= count;
        }

        return steering;
    }

    Vector2 Align()
    {
        // Alignment rule
        Vector2 averageVelocity = new Vector2();
        int count = 0;

        foreach (Boid neighbour in neighbours)
        {
            if (neighbour.velocity != Vector2.zero)
            {
                averageVelocity += neighbour.velocity;
                count++;
            }
        }

        if (count > 0)
        {
            averageVelocity /= count;
        }

        return averageVelocity;
    }

    Vector2 Cohere()
    {
        // Cohesion rule
        Vector2 averagePosition = new Vector2();
        int count = 0;

        foreach (Boid neighbour in neighbours)
        {
            averagePosition += (Vector2)neighbour.transform.position;
            count++;
        }

        if (count > 0)
        {
            averagePosition /= count;
        }
        
        return averagePosition;
    }

    void FindNeighbours()
    {
        neighbours.Clear();
        Boid[] boids = FindObjectsOfType<Boid>();

        foreach (Boid other in boids)
        {
            if (other == this) continue; // Skips self instance

            if (Vector2.Distance(this.transform.position, other.transform.position) <= neighbourRadius)
            {
                neighbours.Add(other);
            }
        }
    }

    void Move()
    {

        if (velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    void WrapAround()
    {
        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Define a margin to trigger the turn-around behavior
        float margin = 0.1f;

        if (viewportPosition.x > 1 - margin || viewportPosition.x < 0 + margin)
        {
            velocity.x = -velocity.x; // Reverse the horizontal velocity
        }

        if (viewportPosition.y > 1 - margin || viewportPosition.y < 0 + margin)
        {
            velocity.y = -velocity.y; // Reverse the vertical velocity
        }
    }



}
