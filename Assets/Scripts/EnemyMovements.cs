using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    [SerializeField] float EnemyMoveSpeed = 1f;
    Rigidbody2D EnemyRigidbody;
    void Start()
    {
        EnemyRigidbody=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        EnemyRigidbody.velocity=new Vector2(1*EnemyMoveSpeed,0);
    }

    void OnTriggerExit2D(Collider2D other) {
        EnemyMoveSpeed=-EnemyMoveSpeed;
        FlipEnemyFacing(EnemyMoveSpeed);
        
    }



    void FlipEnemyFacing(float Speed)
    {
         transform.localScale = new Vector2 (Mathf.Sign(Speed),1f);
         Debug.Log("zmiana kierunku");
    }
}
