using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100f;
    [SerializeField] int scoreValue = 100;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] float volume = 0.1f;

    [Header("Drop")]
    [SerializeField] float dropProb = 0.1f;
    [SerializeField] GameObject cratePrefab;
    [SerializeField] float spin = 60f;
    GameObject powerUp;

    [Header("Projectile")]
    [SerializeField] float shotCounter;
    [SerializeField] float minFireRate = 0.2f;
    [SerializeField] float maxFireRate = 1f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] AudioClip shootSFX;


    private void Start()
    {
        shotCounter = Random.Range(minFireRate, maxFireRate);
    }

    private void Update()
    {
        CountDownAndShoot();
    }
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minFireRate, maxFireRate);
        }
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, volume);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddtoScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);

        if (deathSFX != null)
        {
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, volume);
        }

        float dropBool = Random.value;
        if (dropBool <= dropProb) // compares random number from 0 to 1 to drop_probability
        {
            CreatePowerUp();
        }
    }

    void CreatePowerUp()
    {
        powerUp = Instantiate(cratePrefab, transform.position, Quaternion.identity);
    }
}

