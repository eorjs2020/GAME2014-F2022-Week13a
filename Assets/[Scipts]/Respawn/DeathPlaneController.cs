using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DeathPlaneController : MonoBehaviour
{
    public Transform playerSpawnPoints;

    public void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.name == "Player")
        {
            var player = collision.gameObject?.GetComponent<PlayerBehaviour>();
            if(player)
            {
                player.life.LoseLife();
                player.healthBarController.ResetHealth();
            }

            if(player.life.value > 0)
            {
                ReSpawn(collision.gameObject);

                // Play Death Sound
            }
          
        }
    }

    public void ReSpawn(GameObject go)
    {
        go.transform.position = playerSpawnPoints.position;
    }
}
