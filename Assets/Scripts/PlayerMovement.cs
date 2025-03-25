using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    AudioSource audioSource;
    public float speed = 5.0f;
    private Vector2 movementInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Obtém a entrada do jogador
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Define a direção do movimento
        movementInput = new Vector2(moveHorizontal, moveVertical).normalized;
    }

    void FixedUpdate()
    {
        // Aplica a velocidade diretamente ao Rigidbody2D
        rb.linearVelocity = movementInput * speed;
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
