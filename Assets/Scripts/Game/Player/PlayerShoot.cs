using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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

    public int shotsPerFire = 1;
    public int maxShotsPerFire = 8; // Limite público de tiros por disparo
    private float spreadAngle = 20f;
    private Animator animator;

    [SerializeField]
    private BulletPool bulletPool;
    private Coroutine shootCoroutine;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
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
            if (shootCoroutine != null)
                StopCoroutine(shootCoroutine);
            shootCoroutine = StartCoroutine(ShootAnimationCoroutine());
            FireBullet();
        }
    }

    private void FireBullet()
    {
        float currentSpread = (shotsPerFire == 8) ? 180f / (shotsPerFire - 1) : spreadAngle;
        float initialAngle = -(shotsPerFire - 1) * currentSpread / 2;

        for (int i = 0; i < shotsPerFire; i++)
        {
            float angleOffset = initialAngle + (i * currentSpread);
            Quaternion bulletRotation = gunOffset.rotation * Quaternion.Euler(0, 0, angleOffset);

            GameObject bullet = bulletPool.GetBullet(gunOffset.position, bulletRotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = bulletRotation * Vector2.up * bulletSpeed;
        }

        if (shootSound != null)
        {
            shootSound.volume = 0.2f;
            shootSound.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource não está atribuído ao script PlayerShoot!");
        }
    }

    private IEnumerator ShootAnimationCoroutine()
    {
        animator.SetBool("Shoot", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Shoot", false);
    }

    // Método para tentar aumentar shotsPerFire respeitando o limite
    public bool TryUpgradeShotsPerFire()
    {
        if (shotsPerFire < maxShotsPerFire)
        {
            shotsPerFire++;
            return true;
        }
        return false;
    }
}