using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerManagement
{
    PlayerManagement playerManagement = new PlayerManagement();
    bool grounded = false;
    protected float groundRadius = 0.2f;
        
    public void PlayerMovement(Rigidbody2D playerBody, Collider2D playerCollider, LayerMask Ground, LayerMask whatIsGround, Transform groundCheck, float jumpForce) {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        float hDirection = Input.GetAxis("Horizontal");
        
        if (hDirection < 0) {
            playerBody.velocity = new Vector2(-speed, playerBody.velocity.y);
            playerFlip(-1, playerBody);
        }
        else if (hDirection > 0) {
            playerBody.velocity = new Vector2(speed, playerBody.velocity.y);
            playerFlip(1, playerBody);
        }
        else {
            playerBody.velocity = new Vector2(0, playerBody.velocity.y);
        }
        if (Input.GetButtonDown("Jump") && grounded) {
            grounded = false;
            Jump(playerBody, playerCollider, jumpForce);
        }
    }

    public void Jump(Rigidbody2D playerBody, Collider2D playerCollider, float jumpForce) {
        playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
    }

    private void playerFlip(int x, Rigidbody2D playerBody) => playerBody.transform.localScale = new Vector2(x, 1);
}
