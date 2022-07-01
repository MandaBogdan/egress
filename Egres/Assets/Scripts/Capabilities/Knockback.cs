using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField]private float multiplyer;

    public Vector2 knockback;

    private Rigidbody2D body;

    private float knockbackCooldown = 0.1f;
    private bool isReady = true;


    private void Start()
    {
        body = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            if (isReady)
            {
                body.velocity -= knockback * multiplyer;
                StartCoroutine(KnockbackCooldown());
            }    
    }

    IEnumerator KnockbackCooldown()
    {
        isReady = false;
        yield return new WaitForSeconds(knockbackCooldown);
        isReady = true;
    }
}
