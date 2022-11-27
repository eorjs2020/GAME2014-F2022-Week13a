using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Sencing Suite")]
    public LayerMask collisionLayerMask;    
    public bool playerDetected = false;
    public bool LOS = false;

    private Collider2D colliderName;
    private Transform playerTransform;
    private float playerDirection;
    private float enemyDirection;

    private Vector2 playerDirectionVector;

    // Start is called before the first frame update
    void Start()
    {
        playerDirectionVector = Vector2.zero;
        playerDirection = 0;        

        playerTransform = FindObjectOfType<PlayerBehaviour>().transform;
        playerDetected = false;
        LOS = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(playerDetected)
        {
            var hits = Physics2D.Linecast(transform.position, playerTransform.position, collisionLayerMask);
            colliderName = hits.collider;

            playerDirectionVector = (playerTransform.position - transform.position);
            playerDirection = (playerDirectionVector.x > 0) ? 1.0f : -1.0f;
            enemyDirection = GetComponentInParent<EnemyController>().direction.x;

            LOS = (hits.collider.gameObject.name == "Player") && (playerDirection == enemyDirection);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            playerDetected=true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerDetected = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color =(LOS) ? Color.green : Color.red;

        if(playerDetected)
        {
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }

        Gizmos.color = (playerDetected) ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, 15.0f);
    }
}
