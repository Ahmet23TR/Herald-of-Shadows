using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSfx;
    [SerializeField] int poitsForCoinPickups = 100;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(poitsForCoinPickups);
            AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
            Destroy(gameObject);
        }    
    }
}
