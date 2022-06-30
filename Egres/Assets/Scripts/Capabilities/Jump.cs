using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Jump : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0, 5f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0, 5f)] private float upwardMovementMultiplier = 3f;
    [SerializeField, Range(0, 0.3f)] private float coyoteTime = 0.2f;
    [SerializeField, Range(0, 0.3f)] private float jumpBufferTime = 0.2f;

    private Controller controller;
    private Rigidbody2D body;
    private Ground ground;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale;

    private float jumpSpeed, coyoteCounter, jumpBufferCounter;

    private bool desiredJump;
    private bool onGround;
    private bool isJumping;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        controller = GetComponent<Controller>();
        defaultGravityScale = 1f;
    }

    void Update()
    {
        desiredJump |= controller.input.RetrieveJumpInput();
    }

    private void FixedUpdate() {
        onGround = ground.GetOnGround();
        velocity = body.velocity;
        print(body.velocity.y);
        if(onGround &&  Mathf.Abs(body.velocity.y) < 0.0001) {
            jumpPhase = 0;
            coyoteCounter = coyoteTime;
            isJumping = false;
        } else {
            coyoteCounter -= Time.deltaTime;
        }

        if(desiredJump) {
            desiredJump = false;
            jumpBufferCounter = jumpBufferTime;
        } else if (!desiredJump && jumpBufferCounter > 0) {
            jumpBufferCounter -= Time.deltaTime;
        }

        if(jumpBufferCounter > 0) {
            JumpAction();
        }

        if(controller.input.RetrieveJumpHoldInput() && body.velocity.y > 0) {
            body.gravityScale = upwardMovementMultiplier;
            print("YES_up");
        } else if(!controller.input.RetrieveJumpHoldInput() || body.velocity.y < 0){
            body.gravityScale = downwardMovementMultiplier;
        } else if(Mathf.Abs(body.velocity.y) < 0.0001){
            body.gravityScale = defaultGravityScale;
        }
        body.velocity = velocity;
    }

    private void JumpAction() {
        print("YES");
        print(coyoteCounter);
        if(coyoteCounter > 0f || (jumpPhase < maxAirJumps && isJumping)) {
            print("YES2");
            if(isJumping) {
                jumpPhase += 1;
            }
 
            jumpBufferCounter = 0;
            coyoteCounter = 0;
            isJumping = true;
            jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            
            if (velocity.y > 0f) {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            } else if (velocity.y < 0f) {
                jumpSpeed += Mathf.Abs(body.velocity.y);
            }
            velocity.y += jumpSpeed;
        }
    }
}
