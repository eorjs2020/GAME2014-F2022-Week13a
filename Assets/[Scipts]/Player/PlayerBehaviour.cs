using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement Properties")]
    public float horizontalForce;
    public float horizontalSpeed;

    public float verticalForce;
    public float airFactor;
    public Transform groundPoint; // the origin of the circle
    public float groundRadius; // the size of ths circle
    public LayerMask groundLayerMask; // the stuff we can collide with
    public bool isGrounded;

    [Header("Animator")]
    public Animator animator;
    public PlayerAnimationState state;

    [Header("Health System")]
    public HealthBarController healthBarController;
    public LifeCounterController life;
    public DeathPlaneController deathPlane;

    [Header("Controls")]
    public Joystick LeftStick;
    [Range(0.1f, 1.0f)]
    public float verticalTrheshhold;

    private Rigidbody2D rigid2D;


    // Start is called before the first frame update
    void Start()
    {
        healthBarController = FindObjectOfType<PlayerHealth>()?.GetComponent<HealthBarController>();
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        life = FindObjectOfType<LifeCounterController>();
        deathPlane = FindObjectOfType<DeathPlaneController>();
        LeftStick = (Application.isMobilePlatform) ? GameObject.Find("LeftStick").GetComponent<Joystick>() : null;
    }

    private void Update()
    {
        if (healthBarController.value <= 0)
        {
            life.LoseLife();

            if(life.value > 0)
            {
                healthBarController.ResetHealth();
                deathPlane.ReSpawn(gameObject);
            }           
        }

        // TODO : if live  < 0 load the End Scene
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;

        Move();
        Jump();
        AirCheck();
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal") + ((Application.isMobilePlatform) ? LeftStick.Horizontal : 0.0f);
        if (x != 0)
        {
            Flip(x);

            x = (x >0.0) ? 1.0f : -1.0f;
            rigid2D.AddForce(Vector2.right * x * horizontalForce * ((isGrounded) ? 1.0f : airFactor));

            //rigid2D.velocity = Vector2.ClampMagnitude(rigid2D.velocity, horizontalSpeed);

            var clampXVelocity = Mathf.Clamp(rigid2D.velocity.x, -horizontalSpeed, horizontalSpeed);

            rigid2D.velocity = new Vector2(clampXVelocity, rigid2D.velocity.y);

            ChangeAnimation(PlayerAnimationState.RUN);
        }

        if((isGrounded) && (x == 0))
        {
            ChangeAnimation(PlayerAnimationState.IDLE);
        }
    }

    public void Flip(float x)
    {
        if( x != 0.0f)
        {
            transform.localScale = new Vector3((x > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
        }
    }

    private void Jump()       
    {
        var y = Input.GetAxis("Jump") + ((Application.isMobilePlatform) ? LeftStick.Vertical : 0.0f);

        if((isGrounded) && (y > verticalTrheshhold))
        {
            rigid2D.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }

    private void AirCheck()
    {
        if(!isGrounded)
        {
            ChangeAnimation(PlayerAnimationState.JUMP);
        }
    }

    private void ChangeAnimation(PlayerAnimationState playerAnimationState)
    {
        state = playerAnimationState;
        animator.SetInteger("AnimationState", (int)state);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthBarController.TakeDamage(20);

            // TODO : if Live > 0 -> play the "Hurt" sound
        }
    }
}
