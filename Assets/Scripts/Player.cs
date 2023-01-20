using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private int movementSpeed = 1;
    [SerializeField] private int damageOtherOnCollision = 1;
    [SerializeField] private float verticalBounds = 4.5f;

    private Rigidbody2D rb;
    private Health health;
    private Vector2 movementDirection;

    public static event Action OnPlayerDeath;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        health = GetComponent<Health>();
        health.OnHealthDepleted += Die;
    }

    private void OnEnable()
    {
        InputManager.Instance.PlayerControls.Player.Move.performed += ReadMovement;
        InputManager.Instance.PlayerControls.Player.Move.canceled += ReadMovement;
    }

    private void OnDisable()
    {
        InputManager.Instance.PlayerControls.Player.Move.performed -= ReadMovement;
        InputManager.Instance.PlayerControls.Player.Move.canceled -= ReadMovement;
        movementDirection = Vector2.zero;
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();
    }

    private void ReadMovement(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
        movementDirection.x = 0f;
    }

    private void FixedUpdate()
    {
        Vector3 nextPosition = rb.position + Time.fixedDeltaTime * movementSpeed * movementDirection;
        nextPosition.y = Mathf.Clamp(nextPosition.y, -verticalBounds, verticalBounds);
        rb.MovePosition(nextPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.DealDamage(damageOtherOnCollision);
        }
    }
}
