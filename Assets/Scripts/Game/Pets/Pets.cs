using System.Collections.Generic;
using UnityEngine;

public class Pets : MonoBehaviour
{
    [Header("Pet Info")]
    public string petName;
    public int damage = 1;
    public List<string> powers = new List<string>();
    public int currentPowerIndex = 0;

    [Header("Shop")]
    public int priceCoins = 100;
    public int priceRubies = 5;
    public bool isElite = false;

    [Header("Orbit")]
    public float orbitRadius = 2f;
    public float orbitSpeed = 90f; // graus por segundo

    [Header("Attack")]
    public BulletPool bulletPool; // Referência ao BulletPool
    public GameObject shootPrefab;
    public float shootCooldown = 1f;
    public float bulletSpeed = 10f;
    public int shotsPerFire = 1; // Quantidade de tiros por ataque do pet
    public float spreadAngle = 20f; // Ângulo de espalhamento dos tiros

    public Transform player;
    private float orbitAngle;
    private float shootTimer;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (bulletPool == null)
        {
            bulletPool = FindFirstObjectByType<BulletPool>();
        }

        orbitAngle = Random.Range(0f, 360f);
        shootTimer = 0f;
    }

    void Update()
    {
        if (player == null) return;

        // Orbitando ao redor do player
        orbitAngle += orbitSpeed * Time.deltaTime;
        float rad = orbitAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * orbitRadius;
        transform.position = player.position + offset;

        // Atirar no inimigo mais próximo
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                ShootAt(nearestEnemy.transform.position);
                shootTimer = shootCooldown;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = float.MaxValue;
        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }
        return nearest;
    }

    void ShootAt(Vector3 targetPosition)
    {
        if (bulletPool == null) return;

        Vector3 direction = (targetPosition - transform.position).normalized;

        // Cálculo do spread
        float currentSpread = (shotsPerFire == 8) ? 180f / (shotsPerFire - 1) : spreadAngle;
        float initialAngle = -(shotsPerFire - 1) * currentSpread / 2;

        for (int i = 0; i < shotsPerFire; i++)
        {
            float angleOffset = initialAngle + (i * currentSpread);
            Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, angleOffset);

            GameObject bullet = bulletPool.GetBullet(transform.position, bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = bulletRotation * Vector2.up * bulletSpeed;
            }

            var bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
                bulletScript.damage = this.damage;
        }
    }

    public string GetCurrentPower()
    {
        if (powers != null && powers.Count > 0 && currentPowerIndex >= 0 && currentPowerIndex < powers.Count)
            return powers[currentPowerIndex];
        return "";
    }
}