using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    [SerializeField] private float multiplyer;

    private Rigidbody2D body;

    private float knockbackCooldown = 0.1f;
    private bool isReady = true;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReady)
        {
            body.velocity += collision.GetComponent<Knockback>().knockback * multiplyer;
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
