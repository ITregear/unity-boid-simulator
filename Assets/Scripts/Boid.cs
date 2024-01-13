using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector2 velocity;
    public float maxSpeed = 5f;

    void Start()
    {
        velocity = new Vector2(Random.Range(-maxSpeed, maxSpeed),
        Random.Range(-maxSpeed, maxSpeed));
    }

    void Update()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
        WrapAround();
    }

    void WrapAround()
    {
        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (viewportPosition.x > 1 || viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x;
        }

        if (viewportPosition.y > 1 || viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y;
        }

        transform.position = newPosition;
    }

}
