using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    float xSpeed;
    int xScale;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x;
        FlipSprite();
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(bulletSpeed * xSpeed, 0f);
    }
    void FlipSprite()
    {
        if (PlayerIsFacingLeft() > 0)
        {
            Debug.Log(PlayerIsFacingLeft());
            gameObject.transform.localScale = new Vector2(1, 1);
        }
        else if(PlayerIsFacingLeft() < 0)
        {
            Debug.Log(PlayerIsFacingLeft());
            gameObject.transform.localScale = new Vector2(-1, 1);
        }
    }

    int PlayerIsFacingLeft()
    {
        if(player.transform.localScale.x < 0f)
        {
            xScale = -1;
        }
        else 
        {
            xScale = 1;
        }
        return xScale;
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