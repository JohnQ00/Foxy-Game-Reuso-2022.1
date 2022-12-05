using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManagement : MonoBehaviour {    

    //DEFINITIONS
    int falling = 3;
    int hurting = 4;

    //Calling the component
    private Animator playerAnimator;
    protected Rigidbody2D playerBody;
    protected Collider2D playerCollider;

    //Imported classes
    PlayerController controller;
    PlayerAnimation animation;
    PlayerSounds sounds;

    //Animation variables/FSM
    protected enum State { idle, running, jumping, falling, hurt, crouching }
    protected State state = State.idle;
    private int animatorEnumIndex;

    //Inspector variables
    [SerializeField] protected LayerMask Ground;
    [SerializeField] protected float speed = 7f;
    [SerializeField] protected float jumpForce = 30f;
    [SerializeField] protected float hurtForce = 10f;
    [SerializeField] protected int cherries = 0;
    [SerializeField] protected TextMeshProUGUI healthText;
    [SerializeField] protected int health = 3;
    [SerializeField] protected TextMeshProUGUI cherryText;
    [SerializeField] protected AudioSource cherryPickSound;
    [SerializeField] protected AudioSource playerFootstep;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask whatIsGround;

    protected void Start() {
        playerBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>();
        playerFootstep = GetComponent<AudioSource>();
        controller = new PlayerController();
        animation = new PlayerAnimation();
        sounds = new PlayerSounds();
    }

    private void Update() {
        if (animatorEnumIndex != (int) State.hurt) {
            controller.PlayerMovement(playerBody, playerCollider, Ground, whatIsGround, groundCheck, jumpForce);
        }
        animatorEnumIndex = animation.AnimationCorrection(playerBody, playerCollider, Ground);
        animation.AnimationManager("state", animatorEnumIndex, playerAnimator);
    }

    public void OnCollisionEnter2D(Collision2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (other.gameObject.tag == "Enemy") {
            if (animatorEnumIndex == falling) {
                enemy.JumpedOn();
                controller.Jump(playerBody, playerCollider, jumpForce);
            }
            else {
                animatorEnumIndex = hurting;
                animation.SetHurt();
                handleHealth();
                animation.HurtEffect(other.gameObject.transform.position.x, transform.position.x, playerBody);
            }
        }
        else if (other.gameObject.tag == "Spikes") {
            animation.SetHurt();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Collectable") {
            cherryPickSound.Play();
            Destroy(collider.gameObject);
            cherries++;
            cherryText.text = cherries.ToString();
        }
        else if(collider.tag == "PowerUps") {
            Destroy(collider.gameObject);
            jumpForce = 45f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower());
        }
    }

    private void GetFootstep() {
        sounds.Footstep(playerFootstep);
    }

    private IEnumerator ResetPower() {
        yield return new WaitForSeconds(10);
        jumpForce = 30f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void handleHealth() {
        health -= 1;
        healthText.text = health.ToString();
        if (health <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
