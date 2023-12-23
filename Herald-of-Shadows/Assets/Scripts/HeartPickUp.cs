using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    [SerializeField] int HeartPickups = 1;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToLife(HeartPickups);
            Destroy(gameObject);
        }    
    }
}
