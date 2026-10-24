using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    [SerializeField] private GameObject target_p;
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);

    [SerializeField] private float sensitivityX = 4.0f;
    [SerializeField] private float sensitivityY = 2.0f;
    [SerializeField] private float minYAngle = -20.0f;
    [SerializeField] private float maxYAngle = 60.0f;

    private float currentX = 0.0f;
    private float currentY = 20.0f;

    void Start()
    {
        // Bloquear el cursor al centro de la pantalla para mover la cámara con el mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (target_p != null)
        {
            currentX = target_p.transform.eulerAngles.y;
        }
    }

    void Update()
    {
 
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY -= Input.GetAxis("Mouse Y") * sensitivityY;

        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
    }

    void LateUpdate()
    {
        if (target_p == null) return;

        
        Quaternion camRotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 positionOffset = camRotation * new Vector3(0, 0, -distance);

        transform.position = target_p.transform.position + offset + positionOffset;
        transform.LookAt(target_p.transform.position + offset);

        
        PlayerMovement playerMovement = target_p.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.RotateCharacter(currentX);
        }
    }
}

