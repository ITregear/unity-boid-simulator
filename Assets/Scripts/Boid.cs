using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public Vector2 velocity;
    public float maxSpeed = 40f;
    public float minSpeed = 2f;
    public float neighbourRadius = 3f;
    public float viewAngle = 120f;

    public Vector2 separationVector;
    public Vector2 alignmentVector;
    public Vector2 cohesionVector;
    public float separationWeight = 3.5f;
    public float alignmentWeight = 1.0f;
    public float cohesionWeight = 1.0f;


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
        AvoidObstacles();
        Move();
    }

    void ApplyRules(){
        separationVector = Separate() * separationWeight;
        alignmentVector = Align() * alignmentWeight;
        cohesionVector = Cohere() * cohesionWeight;

        velocity += separationVector + alignmentVector + cohesionVector;
        velocity = (velocity.magnitude > maxSpeed) ? velocity.normalized * maxSpeed : velocity; // Clamp to maxSpeed
        velocity = (velocity.magnitude < minSpeed) ? velocity.normalized * minSpeed : velocity; // Clamp to minSpeed
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
        Vector2 averagePosition = Vector2.zero;
        int count = 0;

        foreach (Boid neighbor in neighbours)
        {
            averagePosition += (Vector2)neighbor.transform.position;
            count++;
        }

        if (count > 0)
        {
            averagePosition /= count;
            Vector2 cohesionForce = averagePosition - (Vector2)transform.position;
            return cohesionForce;
        }

        return Vector2.zero;
}


    void FindNeighbours()
    {
        neighbours.Clear();
        Boid[] boids = FindObjectsOfType<Boid>();

        foreach (Boid other in boids)
        {
            if (other == this) continue;

            Vector2 directionToOther = other.transform.position - transform.position;

            if (Vector2.Angle(velocity, directionToOther) < viewAngle / 2)
            {
                float distance = Vector2.Distance(transform.position, other.transform.position);

                if (distance <= neighbourRadius)
                {
                    neighbours.Add(other);
                }
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

    void AvoidObstacles()
    {
        float raycastLength = 4.0f;
        Vector2 raycastDirection = velocity.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastLength);

        Debug.DrawLine(transform.position, transform.position + (Vector3)raycastDirection * raycastLength, Color.red);

        if (hit.collider != null)
        {
            Vector2 avoidDirection = Vector2.Reflect(velocity, hit.normal);
            velocity = Vector2.Lerp(velocity, avoidDirection, Time.deltaTime * 50);
        }
    }



}
