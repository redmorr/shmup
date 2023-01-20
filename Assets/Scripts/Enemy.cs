using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int pointsForDestruction = 100;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private int damageOtherOnCollision = 1;

    private readonly Vector2 movementDirection = Vector2.left;
    private Rigidbody2D rb;
    private Health health;

    public static event Action<int> OnEnemyDeath;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.OnHealthDepleted += Die;
    }

    private void Die()
    {
        OnEnemyDeath?.Invoke(pointsForDestruction);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Time.fixedDeltaTime * movementSpeed * movementDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.DealDamage(damageOtherOnCollision);
        }
    }
}
