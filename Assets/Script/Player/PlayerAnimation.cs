using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : PlayerManagement 
{
    PlayerManagement playerManagement = new PlayerManagement();
    PlayerController playerController = new PlayerController();

    public void AnimationManager(string variableName, int enumIndex, Animator playerAnimation) {
        playerAnimation.SetInteger(variableName, enumIndex);
    }

    public int AnimationCorrection(Rigidbody2D playerBody, Collider2D playerCollider, LayerMask Ground) {

        if(!playerCollider.IsTouchingLayers(Ground) && playerBody.velocity.y > .1f) {
            state = State.jumping;
        }
        
        else if(state == PlayerManagement.State.jumping) {
            if (playerBody.velocity.y < .1f) {
                state = State.falling;
            } 
        }

        else if (state == PlayerManagement.State.falling) {
            if (playerCollider.IsTouchingLayers(Ground)) {
                state = State.idle;
            } 
        }

        else if (state == PlayerManagement.State.hurt) {
            if (Mathf.Abs(playerBody.velocity.x) < .1f) {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(playerBody.velocity.x) > .1f) {
            state = State.running;
        }

        else {
            state = State.idle;
        }

        return (int) state;
    }

    public void SetHurt() => state = State.hurt;

    public void HurtEffect(float enemy_position, float char_position, Rigidbody2D playerBody) {
        if (enemy_position > char_position) {
            //Damaged and pushed to the left
            playerBody.velocity = new Vector2(-hurtForce, playerBody.velocity.y);
        }
        else {
            //Damaged and pushed to the right
            playerBody.velocity = new Vector2(hurtForce, playerBody.velocity.y);
        }
    }

}
