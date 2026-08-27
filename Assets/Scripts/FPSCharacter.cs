using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCharacter : MonoBehaviour {
    [SerializeField] private GameObject camera;

    private CharacterController controller;
    private Vector3 velocity; // In meters per second, not units per update!
    private Vector2 targetYawPitch;
    private Vector2 currentYawPitch;

    public float baseMovementSpeed = 3.0f;
    public float runMovementSpeed = 8.0f;
    public float gravity = 9.81f;
    public float groundedGravity = 1.0f;
    public float rotationSpeed = 1.0f;
    public float rotationLerpFactor = 0.3f;

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference interactAction;

    void Start() {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        var lookAmount = lookAction.action.ReadValue<Vector2>() * rotationSpeed / 60.0f;
        targetYawPitch.x += lookAmount.x;
        targetYawPitch.y -= lookAmount.y;
        targetYawPitch.y = Mathf.Clamp(targetYawPitch.y, -90.0f, 90.0f);
        currentYawPitch.x = Mathf.LerpAngle(currentYawPitch.x, targetYawPitch.x, rotationLerpFactor);
        currentYawPitch.y = Mathf.LerpAngle(currentYawPitch.y, targetYawPitch.y, rotationLerpFactor);

        transform.eulerAngles = new Vector3(0, currentYawPitch.x, 0);
        camera.transform.localEulerAngles = new Vector3(currentYawPitch.y, 0, 0);

        var moveDirection = moveAction.action.ReadValue<Vector2>();
        var movementSpeed = sprintAction.action.IsPressed() ? runMovementSpeed : baseMovementSpeed;
        var relativeMovement = moveDirection * movementSpeed;

        if (controller.isGrounded) {
            velocity.y = -groundedGravity;
        } else {
            velocity.y -= gravity * Time.deltaTime;
        }

        velocity = transform.right * relativeMovement.x
            + transform.forward * relativeMovement.y
            + Vector3.up * velocity.y;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnEnable() {
        interactAction.action.started += Interact;
    }

    void OnDisable() {
        interactAction.action.started -= Interact;
    }

    void Interact(InputAction.CallbackContext callback) {
        Debug.Log("Interact");
    }
}
