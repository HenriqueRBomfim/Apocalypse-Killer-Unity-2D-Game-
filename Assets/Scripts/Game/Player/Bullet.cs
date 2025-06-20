using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;
    private BulletPool bulletPool;

    [Header("Bullet Damage")]
    public int damage = 1; // Defina o dano da Bullet aqui

    public void SetPool(BulletPool pool)
    {
        bulletPool = pool;
    }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>() != null)
        {
            var enemyHealthController = collision.GetComponent<HealthController>();
            enemyHealthController.TakeDamage(damage); // Usa o valor de dano configurado
            ReturnToPoolOrDestroy();
        }
        else if (collision.CompareTag("Wall"))
        {
            ReturnToPoolOrDestroy();
        }
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
        if (screenPosition.x < 0 || screenPosition.x > _camera.pixelWidth || screenPosition.y < 0 || screenPosition.y > _camera.pixelHeight)
        {
            ReturnToPoolOrDestroy();
        }
    }

    private void ReturnToPoolOrDestroy()
    {
        if (bulletPool != null)
        {
            bulletPool.ReturnBullet(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}