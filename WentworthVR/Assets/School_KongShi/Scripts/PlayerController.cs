using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    private readonly float speed = 700f;
    private readonly float maxSpeed = 3f;
    
    private Rigidbody rb;

    private bool canMove;
    private float hInput;
    private float vInput;

    private void Start()
    {
        DialogueManager.instance.StartTalking += StopMoving;
        DialogueManager.instance.StopTalking += StartMoving;
        rb = GetComponent<Rigidbody>();
        canMove = true;
    }

    void Update()
    {
        if (canMove) 
        {
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");
            SpeedControl();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveDir = orientation.forward * vInput + orientation.right * hInput;
        rb.AddForce(speed * Time.fixedDeltaTime * moveDir.normalized);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new(limitedVel.x, rb.velocity.y, limitedVel.z);
            rb.drag = 2f;
        }
        else if (rb.velocity == Vector3.zero) rb.drag = 1f;
    }

    private void StopMoving()
    {
        canMove = false;
        
    }

    private void StartMoving()
    {
        canMove = true;
        
    }

    private void OnDisable()
    {
        DialogueManager.instance.StartTalking -= StopMoving;
        DialogueManager.instance.StopTalking -= StartMoving;
    }
}