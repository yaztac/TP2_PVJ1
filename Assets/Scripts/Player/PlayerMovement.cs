using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movememnt Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationSpeed = 15f;
    private Rigidbody rb;
    private float moveHorizontal;
    private float moveVertical;

    [Header("Raycast Settings")]
    private bool isGrounded;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask groundLayer;

    //Referencia a la camara principal
    private Transform camTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(Camera.main != null)
        {
            camTransform = Camera.main.transform;
        }
    }

    
    void Update()
    {
        GatherInput();
        CheckGround();

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

    }
    private void FixedUpdate()
    {
        Move();

    }

    private void GatherInput()
    {
       moveHorizontal = Input.GetAxisRaw("Horizontal");
       moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (movement.magnitude >= 0.1f && camTransform != null)
        {
            // Obtenemos la dirección de la cámara pero aplanada en el eje Y (para que no mueva al jugador hacia arriba/abajo)
            Vector3 camForward = camTransform.forward;
            Vector3 camRight = camTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            // Combinamos el input del teclado con la orientación de la cámara
            Vector3 moveDirection = (camForward * movement.z + camRight * movement.x).normalized;

            rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
        }
        else
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }
    private void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    public void RotateCharacter(float currentCameraYaw)
    {
        Vector3 inputDir = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (inputDir.magnitude >= 0.1f && camTransform != null)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        //transform.rotation = Quaternion.Euler(0f, rotateY, 0f);
    }
    private void CheckGround()
    {
        Vector3 origin = transform.position;
        isGrounded = Physics.Raycast(origin, Vector3.down, rayDistance, groundLayer);
        
        Color ColorRay = isGrounded ? Color.green : Color.red;
        Debug.DrawRay(origin, Vector3.down * rayDistance, ColorRay);
    }

}
