using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy {
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private LayerMask ground;
    private bool facingLeft = true;

    private Collider2D enemyCollider;
    
    protected override void Start() {
        base.Start();
        enemyCollider = GetComponent<Collider2D>();
    }

    private void Update() {
        if (enemyAnimator.GetBool("Jumping")) {
            if (enemyBody.velocity.y < .1) {
                enemyAnimator.SetBool("Falling", true);
                enemyAnimator.SetBool("Jumping", false);
            }
        }

        if (enemyCollider.IsTouchingLayers(ground) && enemyAnimator.GetBool("Falling")) {
            enemyAnimator.SetBool("Falling", false);
        }
    }

    private void Move() {
        if (facingLeft) {
            if (transform.localScale.x != 1) {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (transform.position.x > leftCap) {
                if (enemyCollider.IsTouchingLayers(ground)) {
                    enemyBody.velocity = new Vector2(-jumpLength, jumpHeight);
                    enemyAnimator.SetBool("Jumping", true);
                }
            }

            else {
                facingLeft = false;    
            }
        }

        else {
            if (transform.localScale.x != -1) {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (transform.position.x < rightCap) {
                if (enemyCollider.IsTouchingLayers(ground)) {
                    enemyBody.velocity = new Vector2(jumpLength, jumpHeight);
                    enemyAnimator.SetBool("Jumping", true);
                }
            }

            else {
                facingLeft = true;    
            }
        }
    }

    

    
}
