using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
    [Header("Initial Settings")]
    [SerializeField] private float startHeight = 400f;
    [SerializeField] private float startAngle = 45f;
    [SerializeField] private Vector3 startLookAtPoint = Vector3.zero;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float incrasedMovementSpeed = 20f;
    [SerializeField] private Vector2 mapBoundsX = new Vector2(-50f, 50f);
    [SerializeField] private Vector2 mapBoundsZ = new Vector2(-50f, 50f);

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minFOV = 20f;  // Zoom molto stretto (ingrandimento)
    [SerializeField] private float maxFOV = 60f;  // Zoom molto ampio (visione più larga)
    [SerializeField] private float currentFOV;

    private Camera mainCamera;

    [Header("Rotation Settings")]
    [SerializeField] private float edgeRotationSpeed = 3f;
    [SerializeField] private float manualRotationSpeed = 5f;
    [SerializeField] private float minVerticalAngle = 15f;
    [SerializeField] private float maxVerticalAngle = 80f;
    [SerializeField] private float defaultVerticalAngle = 45f;
    [SerializeField] private float edgeRotationMargin = 50f;

    private Transform cameraTransform;
    private Vector3 aimPoint;
    private float currentZoomDistance;
    private float desiredZoomDistance;
    private bool isRotating = false;
    private Vector3 lastMousePosition;
    public Camera cameraUI;

    private void Awake()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        mainCamera = cameraTransform.GetComponent<Camera>();
        currentFOV = mainCamera.fieldOfView;

        // Imposta la posizione e rotazione iniziale
        SetupInitialCameraPosition();

        // Calcola il punto di mira iniziale
        CalculateAimPoint();

        // Imposta la distanza di zoom iniziale
        currentZoomDistance = Vector3.Distance(transform.position, aimPoint);
        desiredZoomDistance = currentZoomDistance;
    }

    private void SetupInitialCameraPosition()
    {
        // Imposta l'angolo di rotazione iniziale
        transform.rotation = Quaternion.Euler(startAngle, 0f, 0f);

        // Calcola la posizione iniziale in base all'altezza e all'angolo
        float distanceFromCenter = startHeight / Mathf.Tan(startAngle * Mathf.Deg2Rad);
        Vector3 startPosition = new Vector3(
            startLookAtPoint.x,
            startHeight,
            startLookAtPoint.z - distanceFromCenter);

        transform.position = startPosition;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleManualRotation();
        HandleEdgeRotation();
    }

    private void HandleMovement()
    {
        Vector3 inputDirection = Vector3.zero;

        // Movimento con WASD (opzionale, puoi rimuoverlo se non ti serve)
        if (Input.GetKey(KeyCode.W)) inputDirection += transform.forward;
        if (Input.GetKey(KeyCode.S)) inputDirection -= transform.forward;
        if (Input.GetKey(KeyCode.A)) inputDirection -= transform.right;
        if (Input.GetKey(KeyCode.D)) inputDirection += transform.right;

        if (inputDirection != Vector3.zero)
        {
            inputDirection.y = 0;
            inputDirection.Normalize();
            Vector3 newPosition;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                newPosition = transform.position + inputDirection * incrasedMovementSpeed * Time.deltaTime;
            }
            else 
            {
                newPosition = transform.position + inputDirection * movementSpeed * Time.deltaTime;
            }
            newPosition.x = Mathf.Clamp(newPosition.x, mapBoundsX.x, mapBoundsX.y);
            newPosition.z = Mathf.Clamp(newPosition.z, mapBoundsZ.x, mapBoundsZ.y);

            transform.position = newPosition;
            CalculateAimPoint();
        }
    }

    private void HandleEdgeRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        float rotationAmount = edgeRotationSpeed * Time.deltaTime;

        // Rotazione orizzontale (asse Y) quando il mouse è ai bordi laterali
        if (mousePos.x <= edgeRotationMargin)
        {
            transform.RotateAround(aimPoint, Vector3.up, -rotationAmount);
        }
        else if (mousePos.x >= Screen.width - edgeRotationMargin)
        {
            transform.RotateAround(aimPoint, Vector3.up, rotationAmount);
        }

        // Rotazione verticale (asse X) quando il mouse è ai bordi superiori/inferiori
        if (mousePos.y <= edgeRotationMargin)
        {
            float currentAngle = transform.rotation.eulerAngles.x;
            float newAngle = Mathf.Clamp(currentAngle + rotationAmount, minVerticalAngle, maxVerticalAngle);
            transform.RotateAround(aimPoint, transform.right, newAngle - currentAngle);
        }
        else if (mousePos.y >= Screen.height - edgeRotationMargin)
        {
            float currentAngle = transform.rotation.eulerAngles.x;
            float newAngle = Mathf.Clamp(currentAngle - rotationAmount, minVerticalAngle, maxVerticalAngle);
            transform.RotateAround(aimPoint, transform.right, newAngle - currentAngle);
        }
    }

    private void HandleManualRotation()
    {
        if (Input.GetMouseButtonDown(1) && !IsPointerOverUIElement())
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            // Rotazione orizzontale
            float yawInput = mouseDelta.x * manualRotationSpeed * 0.1f;
            transform.RotateAround(aimPoint, Vector3.up, yawInput);

            // Rotazione verticale
            float pitchInput = -mouseDelta.y * manualRotationSpeed * 0.1f;
            float currentAngle = transform.rotation.eulerAngles.x;
            float newAngle = Mathf.Clamp(currentAngle + pitchInput, minVerticalAngle, maxVerticalAngle);
            transform.RotateAround(aimPoint, transform.right, newAngle - currentAngle);

            lastMousePosition = Input.mousePosition;
        }
    }

    private void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0f)
        {
            // Riduci il FOV per zoommare, aumentalo per de-zoommare
            currentFOV -= scrollInput * zoomSpeed;
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
            mainCamera.fieldOfView = currentFOV;
            cameraUI.fieldOfView = currentFOV;
        }
    }

    private void CalculateAimPoint()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = cameraTransform.position + cameraTransform.forward * 50f;
        }
    }

    private bool IsPointerOverUIElement()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}