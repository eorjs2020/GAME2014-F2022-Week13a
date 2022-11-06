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

    private Rigidbody2D rigid2D;


    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;

        Move();
        Jump();
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");
        if (x != 0)
        {
            Flip(x);

            x = (x >0.0) ? 1.0f : -1.0f;
            rigid2D.AddForce(Vector2.right * x * horizontalForce * ((isGrounded) ? 1.0f : airFactor));

            //rigid2D.velocity = Vector2.ClampMagnitude(rigid2D.velocity, horizontalSpeed);

            var clampXVelocity = Mathf.Clamp(rigid2D.velocity.x, -horizontalSpeed, horizontalSpeed);

            rigid2D.velocity = new Vector2(clampXVelocity, rigid2D.velocity.y);
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
        var y = Input.GetAxis("Jump");

        if((isGrounded) && (y > 0.0f))
        {
            rigid2D.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }
}
