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
    private float lastFireTime;
    private bool fireContinuously;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            fireContinuously = true; 
        }
        if (fireContinuously)
        {
            if (Time.time - lastFireTime > timeBetweenShoots) 
            {
                lastFireTime = Time.time; 
                FireBullet(); 
            }
            fireContinuously = false;
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunOffset.position, gunOffset.rotation);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * bulletSpeed; 
    }
}
