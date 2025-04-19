using System.Security.Cryptography;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    private Rigidbody2D rigidBody;
    private PlayerAwarenessController playerAwarenessController;
    private Vector2 targetDirection; 
    private float changeDirectionCooldown; 
    private Animator animator;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        targetDirection = transform.up;
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
        animator.SetFloat("linearVelocity", rigidBody.linearVelocity.magnitude);
    }

    private void UpdateTargetDirection()
    {
        HandlePlayerTargeting();
        HandleRandomDirectionChange();

    }

    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rigidBody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        rigidBody.linearVelocity = transform.up * speed;
    }

    private void HandleRandomDirectionChange()
    {
        changeDirectionCooldown -= Time.deltaTime;
        if (changeDirectionCooldown <= 0)
        {
            float randomAngle = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(randomAngle, transform.forward);
            targetDirection = rotation * targetDirection;

            changeDirectionCooldown = Random.Range(1f, 2.5f); 
        }
    }

    private void HandlePlayerTargeting()
    {
        if (playerAwarenessController.AwareOfPlayer)
        {
            targetDirection = playerAwarenessController.DirectionToPlayer;
        }
    }
}

