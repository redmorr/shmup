using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private PlayerControls playerControls;
    private Rigidbody2D rb;

    private Vector2 movementDirection;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Move.performed += ReadMovement;
        playerControls.Player.Move.canceled += ReadMovement;
        playerControls.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    private void ReadMovement(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
        Debug.Log(movementDirection);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Time.fixedDeltaTime * movementSpeed * movementDirection);
    }

}
