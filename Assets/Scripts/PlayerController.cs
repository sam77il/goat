using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Vector2 inputVector;

    public float moveSpeed = 5f;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // Bewegung im 3D Raum basierend auf Input (X = links/rechts, Z = vor/zurück)
        Vector3 move = new Vector3(inputVector.x, 0, inputVector.y);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}
