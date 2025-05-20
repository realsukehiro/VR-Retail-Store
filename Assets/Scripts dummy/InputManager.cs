using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float crouchHeight = 1f;
    public float defaultHeight = 2f;
    public float crouchSpeed = 3f;

    [Header("References")]
    public Camera playerCamera;
    public Transform interactorSource;
    public float interactRange = 10f;

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX = 0;
    private bool _canMove = true;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        if (interactorSource == null)
            interactorSource = Camera.main.transform;

        // Initially lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        // Subscribe to events that might affect input
        GameEvents.OnGamePaused += () => _canMove = false;
        GameEvents.OnGameResumed += () => _canMove = true;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        GameEvents.OnGamePaused -= () => _canMove = false;
        GameEvents.OnGameResumed -= () => _canMove = true;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleInteractionInput();
        HandleUIInput();
    }

    private void HandleMovementInput()
    {
        if (!_canMove) return;

        // Movement logic
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);

        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
            _characterController.height = crouchHeight;
        }
        else
        {
            _characterController.height = defaultHeight;
        }

        float curSpeedX = _canMove ? currentSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = _canMove ? currentSpeed * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Jump
        if (Input.GetButton("Jump") && _canMove && _characterController.isGrounded)
            _moveDirection.y = jumpPower;
        else
            _moveDirection.y = movementDirectionY;

        // Apply gravity
        if (!_characterController.isGrounded)
            _moveDirection.y -= gravity * Time.deltaTime;

        // Move character
        _characterController.Move(_moveDirection * Time.deltaTime);

        // Camera rotation
        _rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void HandleInteractionInput()
    {
        if (!_canMove && GameManager.Instance.GetCurrentState() != GameManager.GameState.ProductInteraction) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // If in product detail UI, close it
            if (GameManager.Instance.GetCurrentState() == GameManager.GameState.ProductInteraction)
            {
                GameEvents.CloseProductUI();
                return;
            }

            // Otherwise, check for product interaction
            Ray ray = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
            {
                ProductInteractable productInteractable = hitInfo.collider.GetComponent<ProductInteractable>();
                if (productInteractable != null)
                {
                    GameEvents.SelectProduct(productInteractable.ProductData);
                }
            }
        }
    }

    private void HandleUIInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameEvents.ToggleCartUI();
        }

        // Add any other UI input handling here
    }
}
