using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    protected Animator enemyAnimator;
    protected Rigidbody2D enemyBody;
    protected AudioSource enemyDeath;

    protected virtual void Start() {
        enemyAnimator = GetComponent<Animator>();  
        enemyBody = GetComponent<Rigidbody2D>();
        enemyDeath = GetComponent<AudioSource>();
    }

    public void JumpedOn() {
        enemyAnimator.SetTrigger("Death");
        enemyDeath.Play();
        enemyBody.velocity = new Vector2(0, 0);
    }

    private void Death() {
        Destroy(this.gameObject);
    }
}
