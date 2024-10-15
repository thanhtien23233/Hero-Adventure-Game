using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    //private void Move()
    //{
    //    rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));
    //}
}
