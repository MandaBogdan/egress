using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private float attackCooldown = 1;

    private Rigidbody2D body;
    private Collider2D attackCollider;
    private Collider2D attackDownCollider;
    private Collider2D attackUpCollider;

    private bool isReady = true;
    private bool attack;
    private float vertical;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        attackCollider = transform.Find("Attack").GetComponent<Collider2D>();
        attackDownCollider = transform.Find("AttackDown").GetComponent<Collider2D>();
        attackUpCollider = transform.Find("AttackUp").GetComponent<Collider2D>();
    }

    void Update()
    {
        attack = input.RetrieveAttackInput();
        vertical = input.RetrieveLookInput();

        if(isReady && attack)
        {
            if (body.velocity.y != 0)
            {
                if (vertical == 1)
                    StartCoroutine(AttackProccess(attackUpCollider));
                else if (vertical == -1)
                    StartCoroutine(AttackProccess(attackDownCollider));
                else
                    StartCoroutine(AttackProccess(attackCollider));
            }
            else
            {
                if (vertical == 1)
                    StartCoroutine(AttackProccess(attackUpCollider));
                else
                    StartCoroutine(AttackProccess(attackCollider));
            }
        }      
    }

    IEnumerator AttackProccess(Collider2D collider)
    {
        isReady = false;
        collider.enabled = true;
        body.velocity = new Vector2(0, body.velocity.y);
        yield return new WaitForSeconds(attackDuration);
        collider.enabled = false;
        yield return new WaitForSeconds(attackCooldown);
        isReady = true;
    }
}
