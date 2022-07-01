using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public InputController input = null;

    private Animator animator;
    private Rigidbody2D body;

    private bool isRight = true;
    private float horizontal;
    private float vertical;
    private bool attack;
    private Vector3 scale;

    void Start()
    {
        animator = GetComponent<Animator>();
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

        if(attack == true)
        {
            if (body.velocity.y != 0)
            {
                if (vertical == 0)
                    animator.Play("attack-air");
                else if (vertical == 1)
                    animator.Play("attack-up-air");
                else
                    animator.Play("attack-down");
            }
            else
            {
                if (vertical == 1)
                    animator.Play("attack-up");
                else
                    animator.Play("attack");
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
}