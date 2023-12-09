using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePoint : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (GetComponent<CircleCollider2D>() == null)
        {
            gameObject.AddComponent<CircleCollider2D>();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _spriteRenderer.color = Color.blue;
    }
}
