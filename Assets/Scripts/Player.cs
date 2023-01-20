using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private int movementSpeed = 1;
    [SerializeField] private int damageOtherOnCollision = 1;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private Health health;

    public static event Action OnPlayerDeath;

    private void Awake()
    {
        InputManager.Instance.PlayerControls.Player.Move.performed += ReadMovement;
        InputManager.Instance.PlayerControls.Player.Move.canceled += ReadMovement;
        
        rb = GetComponent<Rigidbody2D>();

        health = GetComponent<Health>();
        health.OnHealthDepleted += Die;
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();
    }

    private void ReadMovement(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
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
