using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movememnt Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody rb;
    private float moveHorizontal;
    private float moveVertical;


    [Header("Raycast Settings")]
    private bool isGrounded;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        Vector3 movement = new Vector3(moveHorizontal * -1, 0.0f, moveVertical * -1).normalized;
        rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, movement.z * speed);
    }
    private void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0));
    }

    public void RotateCharacter(float rotateY)
    {
        transform.rotation = Quaternion.Euler(0f, rotateY, 0f);
    }
    private void CheckGround()
    {
        Vector3 origin = transform.position;
        isGrounded = Physics.Raycast(origin, Vector3.down, rayDistance, groundLayer);
        
        Color ColorRay = isGrounded ? Color.green : Color.red;
        Debug.DrawRay(origin, Vector3.down * rayDistance, ColorRay);
    }

}
