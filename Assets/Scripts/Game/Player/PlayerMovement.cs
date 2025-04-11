using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    AudioSource audioSource;
    [SerializeField]
    public float speed = 5.0f;
    [SerializeField]
    private float rotationSpeed;
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(moveHorizontal, moveVertical).normalized;
    }

    void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
    }

    private void SetPlayerVelocity()
    {
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput,
        movementInput, ref movementInputSmoothVelocity, 0.1f);
        rb.linearVelocity = smoothedMovementInput * speed;
    }

    public void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private void RotateInDirectionOfInput()
    {
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            rb.MoveRotation(rotation);
        }
        else
        {
            rb.angularVelocity = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coletável")
        {
            Destroy(other.gameObject);
            audioSource.Play();
        }
    }
}
