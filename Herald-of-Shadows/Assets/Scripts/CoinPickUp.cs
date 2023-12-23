using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSfx;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
            Destroy(gameObject);
        }    
    }
}
