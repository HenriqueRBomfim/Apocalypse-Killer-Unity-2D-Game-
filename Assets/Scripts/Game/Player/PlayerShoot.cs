using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    
    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private Transform gunOffset;

    [SerializeField]
    private float timeBetweenShoots;

    [SerializeField]
    private AudioSource shootSound;

    private float lastFireTime;
    private bool fireContinuously;

    private int shotsPerFire = 1;
    private float elapsedTime = 0f;
    private const float upgradeInterval = 30f;
    private float spreadAngle = 10f;

    void Update()
    {
        elapsedTime += Time.deltaTime; 

        if (elapsedTime >= upgradeInterval)
        {
            shotsPerFire++;
            elapsedTime = 0f;
            Debug.Log("Novo nível de tiros: " + shotsPerFire);
        }

        if (Input.GetKey(KeyCode.Space) || Mouse.current.leftButton.isPressed)
        {
            fireContinuously = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Mouse.current.leftButton.wasReleasedThisFrame)
        {
            fireContinuously = false;
        }

        if (fireContinuously && Time.time - lastFireTime > timeBetweenShoots)
        {
            lastFireTime = Time.time;
            FireBullet();
        }
    }

    private void FireBullet()
    {
        float initialAngle = -(shotsPerFire - 1) * spreadAngle / 2;

        for (int i = 0; i < shotsPerFire; i++)
        {
            float angleOffset = initialAngle + (i * spreadAngle);
            Quaternion bulletRotation = gunOffset.rotation * Quaternion.Euler(0, 0, angleOffset);

            GameObject bullet = Instantiate(bulletPrefab, gunOffset.position, bulletRotation);
            
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = bulletRotation * Vector2.up * bulletSpeed;
        }

        if (shootSound != null)
        {
            shootSound.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource não está atribuído ao script PlayerShoot!");
        }
    }
}
