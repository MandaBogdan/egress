using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAI : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;

    private Vector3 scale;
    private bool isReady = true;

    void Update()
    {
        transform.position = new Vector2(transform.position.x + direction.x * speed * Time.deltaTime, transform.position.y + direction.y * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isReady && collision.GetContact(0).point.x != transform.position.x) 
            StartCoroutine(FlipDelay()); 
        else
            StartCoroutine(FlipDelayY());  
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isReady && collision.GetContact(0).point.x != transform.position.x)
            StartCoroutine(FlipDelay());
        else
            StartCoroutine(FlipDelayY());
    }

    void Flip()
    {
        scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        direction.x *= -1;
    }

    void FlipY()
    {
        direction.y *= -1;
    }

    IEnumerator FlipDelay()
    {
        isReady = false;
        Flip();
        yield return new WaitForSeconds(0.3f);
        isReady = true;
    }

    IEnumerator FlipDelayY()
    {
        isReady = false;
        FlipY();
        yield return new WaitForSeconds(0.3f);
        isReady = true;
    }
}

