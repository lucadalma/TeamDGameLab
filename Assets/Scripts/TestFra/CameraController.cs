using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f; // Velocità di movimento della telecamera
    public float rotationSpeed = 100f; // Velocità di rotazione
    public float zoomSpeed = 10f; // Velocità dello zoom
    public float minZoom = 5f; // Zoom minimo
    public float maxZoom = 50f; // Zoom massimo

    private float currentZoom = 20f;

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        // Ottieni gli input di movimento
        float horizontal = Input.GetAxis("Horizontal"); // A e D
        float vertical = Input.GetAxis("Vertical"); // W e S

        // Calcola la direzione del movimento in base alla direzione della telecamera
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Ignora il movimento sull'asse Y per evitare "salti" verticali
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Movimento finale basato sugli input e la direzione della telecamera
        Vector3 move = (forward * vertical + right * horizontal) * moveSpeed * Time.deltaTime;

        // Applica il movimento
        transform.position += move;
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // Tasto destro del mouse
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Rotazione sull'asse Y (destra/sinistra) e sull'asse X (alto/basso)
            transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, -mouseY * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        Camera.main.fieldOfView = currentZoom;
    }
}