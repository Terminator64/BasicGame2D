using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D arrowRigidbody;
    [SerializeField] float arrowSpeed;

    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        arrowRigidbody=GetComponent<Rigidbody2D>();
        player=FindAnyObjectByType<PlayerMovement>();
        xSpeed = player.transform.localScale.x*arrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        arrowRigidbody.velocity=new Vector2 (xSpeed, 0f);

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemySlime")
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
