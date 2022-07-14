using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLocation : MonoBehaviour
{
    public Vector2 groundPosition;

    private Rigidbody2D body;
    private Ground ground;

    void Start()
    {
        ground = GetComponent<Ground>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (ground.GetOnGround())
            groundPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trap")
            GoToGround();
    }

    public void GoToGround()
    {
        transform.position = groundPosition;
        body.velocity = new Vector2(0, 0);
    }
}
