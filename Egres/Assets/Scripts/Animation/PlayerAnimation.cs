using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField] private float attackCooldown = 1;

    private Animator animator;
    private Animator attackAnimator;
    private Animator attackUpAnimator;
    private Animator attackDownAnimator;
    private Rigidbody2D body;

    private bool isRight = true;
    private bool isReady = true;
    private float horizontal;
    private float vertical;
    private bool attack;
    private Vector3 scale;

    void Start()
    {
        animator = GetComponent<Animator>();
        attackAnimator = transform.Find("Attack").GetComponent<Animator>();
        attackUpAnimator = transform.Find("AttackUp").GetComponent<Animator>();
        attackDownAnimator = transform.Find("AttackDown").GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = input.RetrieveMoveInput();
        vertical = input.RetrieveLookInput();
        attack = input.RetrieveAttackInput();

        if (horizontal > 0 && !isRight)
        {
            Flip();
        }
        else if(horizontal < 0 && isRight)
        {
            Flip();
        }

        if(attack && isReady)
        {
            StartCoroutine(AttackCooldown());
            if (body.velocity.y != 0)
            {
                if (vertical == 0)
                {
                    animator.Play("attack-air");
                    attackAnimator.Play("attack");
                }
                else if (vertical == 1)
                {
                    animator.Play("attack-up-air");
                    attackUpAnimator.Play("attack");
                }                 
                else
                {
                    animator.Play("attack-down");
                    attackDownAnimator.Play("attack");
                }            
            }
            else
            {
                if (vertical == 1)
                {
                    animator.Play("attack-up");
                    attackUpAnimator.Play("attack");
                }
                else
                {
                    animator.Play("attack");
                    attackAnimator.Play("attack");
                }
            }
        }

        if (body.velocity.y > 0)
            animator.SetBool("isJumping", true);
        else
            animator.SetBool("isJumping", false);

        if (body.velocity.y < 0)
            animator.SetBool("isFalling", true);
        else
            animator.SetBool("isFalling", false);

        if (body.velocity.x != 0)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);
            

        if (vertical > 0)
            animator.SetInteger("vertical", 1);
        else if (vertical < 0)
            animator.SetInteger("vertical", -1);
        else
            animator.SetInteger("vertical", 0);
    }

    void Flip()
    {
        scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isRight = !isRight;
    }

    IEnumerator AttackCooldown()
    {
        isReady = false;
        yield return new WaitForSeconds(attackCooldown);
        isReady = true;
    }
}