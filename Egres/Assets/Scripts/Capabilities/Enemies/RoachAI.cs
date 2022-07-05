using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoachAI : MonoBehaviour
{
    [SerializeField] private int direction;
    [SerializeField] private float speed;

    private Vector3 scale;

    private bool isReady = true;

    void Update()
    {
        transform.position = new Vector2(transform.position.x + direction * speed * Time.deltaTime, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isReady)
            StartCoroutine(FlipDelay());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.contactCount < 2)
        {
            StartCoroutine(FlipDelay());
            return;
        }

        if (isReady && (collision.GetContact(1).point.x - collision.GetContact(0).point.x) < 3.9)
            StartCoroutine(FlipDelay());

    }

    void Flip()
    {
        scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        direction *= -1;
    }

    IEnumerator FlipDelay()
    {
        isReady = false;
        Flip();
        yield return new WaitForSeconds(0.3f);
        isReady = true;
    }
}
