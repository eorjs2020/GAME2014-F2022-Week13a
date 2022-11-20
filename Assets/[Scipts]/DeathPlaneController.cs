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
            
            ReSpawn(collision.gameObject);
        }
    }

    public void ReSpawn(GameObject go)
    {
        go.transform.position = playerSpawnPoints.position;
    }
}
