using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Projectile projectilePrefab;

    private void Awake()
    {
        InputManager.Instance.PlayerControls.Player.Fire.performed += FireProjectile;
    }

    private void FireProjectile(InputAction.CallbackContext _)
    {
        Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
    }
}
