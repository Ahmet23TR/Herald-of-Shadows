using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    float xSpeed;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x;
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(bulletSpeed * xSpeed, 0f);
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
    
}
