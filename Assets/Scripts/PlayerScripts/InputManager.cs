using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerLook playerLook;

    private PlayerControls controls;
    private PlayerControls.GroundMovementActions groundMovement;

    private Vector2 horizontalInput;
    private Vector2 lookInput;

    private void Awake()
    {
        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;

        // groundMovement.[action].performed += context => do something
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        
        groundMovement.LookX.performed += ctx => lookInput.x = ctx.ReadValue<float>();
        groundMovement.LookY.performed += ctx => lookInput.y = ctx.ReadValue<float>();
    }

    private void Update()
    {
        playerMovement.ReceiveInput(horizontalInput);
        playerLook.ReceiveInput(lookInput);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
