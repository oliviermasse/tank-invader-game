
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float padding = 0.05f;
    [SerializeField] int health = 1000;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] float volume = 0.5f;

    [Header("Shooting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireBulletDelay = 0.2f;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] float bombSpeed;
    [SerializeField] float fireBombDelay = 2f;
    [SerializeField] AudioClip bulletSFX;
    [SerializeField] AudioClip bombSFX;
    Coroutine firingBulletCoroutine;
    Coroutine firingBombCoroutine;
    float xmin;
    float xmax;
    float ymin;
    float ymax;

    [Header("PowerUps")]
    [SerializeField] bool wallEnabled = false;
    [SerializeField] bool doubleDamageEnabled = false;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] bool doubleSpeedEnabled = false;
    [SerializeField] bool bigBombEnabled = false;
    [SerializeField] bool maxHealthEnabled = false;
    [SerializeField] GameObject bigBombPrefab;
    Coroutine firingTwoBulletCoroutine;
    Coroutine firingBigBombCoroutine;

    void Start()
    {
        SetUpMoveBoundaries();
        wallEnabled = false;
        doubleDamageEnabled = false;
        doubleSpeedEnabled = false;
        bigBombEnabled = false;
    }

    private void SetUpMoveBoundaries()  //clamp player movement to screen
    {
        Camera gameCamera = Camera.main;
        xmin = gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).x + padding; 
        xmax = gameCamera.ViewportToWorldPoint(new Vector2(1, 0)).x - padding;
        ymin = gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).y + padding;
        ymax = gameCamera.ViewportToWorldPoint(new Vector2(0, 1)).y - padding;
    }

    public void Update()
    {
        Move();
        if (bigBombEnabled == true) { StartCoroutine(FireBigBomb()); }
        else { FireBomb(); }

        if (doubleDamageEnabled == true) { FireTwoBullets(); }
        else { FireBullet(); }

        if (wallEnabled==true) { shieldPrefab.transform.position = transform.position; }

        if (doubleSpeedEnabled == true) { playerSpeed = 4f; }
        else { playerSpeed = 2f; } 
    }

    public void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xmin, xmax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, ymin, ymax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void FireBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingBulletCoroutine = StartCoroutine(FireBulletContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingBulletCoroutine);
        }
    }
    IEnumerator FireBulletContinuously()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);// as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
            AudioSource.PlayClipAtPoint(bulletSFX, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(fireBulletDelay);
        }
    }

    public void FireTwoBullets()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingTwoBulletCoroutine = StartCoroutine(FireTwoBulletContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingTwoBulletCoroutine);
        }
    }
    IEnumerator FireTwoBulletContinuously()
    {
        while (true)
        {
            Vector2 bulletPos1 = new Vector2(transform.position.x + 0.06f, transform.position.y);
            Vector2 bulletPos2 = new Vector2(transform.position.x - 0.06f, transform.position.y);

            GameObject bullet1 = Instantiate(bulletPrefab, bulletPos1, Quaternion.identity) as GameObject;
            GameObject bullet2 = Instantiate(bulletPrefab, bulletPos2, Quaternion.identity) as GameObject;

            bullet1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
            bullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);

            AudioSource.PlayClipAtPoint(bulletSFX, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(fireBulletDelay);
        }
    }

    private void FireBomb()
    {

        if (Input.GetButtonDown("Fire2"))
        {
            firingBombCoroutine = StartCoroutine(FireBombContinuously());
        }
        if (Input.GetButtonUp("Fire2"))
        {
            StopCoroutine(firingBombCoroutine);
        }
    }
    IEnumerator FireBombContinuously()
    {
        if (firingBombCoroutine != null) { StopCoroutine(firingBombCoroutine); }

        while (true)
        {
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity) as GameObject;
            bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bombSpeed);
            AudioSource.PlayClipAtPoint(bombSFX, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(fireBombDelay);
        }
    }

    IEnumerator FireBigBomb()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            firingBigBombCoroutine = StartCoroutine(FireBigBombContinuously());
        }
        if (Input.GetButtonUp("Fire2"))
        {
            StopCoroutine(firingBigBombCoroutine);
        }
        yield return new WaitForSeconds(10f);
    }

    IEnumerator FireBigBombContinuously()
    {
        while (true)
        {
            GameObject bigBomb = Instantiate(bigBombPrefab, transform.position, Quaternion.identity) as GameObject;
            bigBomb.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bombSpeed);
            AudioSource.PlayClipAtPoint(bombSFX, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(fireBombDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();

        if (powerUp)
        {
            int powerUpIndex = (int)Random.Range(0, 5);
            if (powerUpIndex == 0) { StartCoroutine(Wall()); }
            else if (powerUpIndex == 1) { StartCoroutine(DoubleDamage()); }
            else if (powerUpIndex == 2) { StartCoroutine(MaxHealth()); }
            else if (powerUpIndex == 3) { StartCoroutine(BigBomb()); }
            else { StartCoroutine(DoubleSpeed()); }
        }
        else if (damageDealer) { ProcessHit(damageDealer); }
    }

    IEnumerator MaxHealth()
    {
        health = 100;
        maxHealthEnabled = true;
        yield return new WaitForSeconds(3f);
        maxHealthEnabled = false;
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

    IEnumerator Wall()
    {
        if (wallEnabled != true)
        {
            wallEnabled = true;
            yield return new WaitForSeconds(10f);
            shieldPrefab.transform.position = new Vector2(0f, -7f);
            wallEnabled = false;
        }
    }

    IEnumerator DoubleDamage()
    {
        StopCoroutine(firingBulletCoroutine);
        if (doubleDamageEnabled != true)
        {
            doubleDamageEnabled = true;
            yield return new WaitForSeconds(5f);
            doubleDamageEnabled = false;
            StopCoroutine(firingTwoBulletCoroutine);
        }
    }

    IEnumerator BigBomb()
    {
        if (bigBombEnabled != true)
        {
            bigBombEnabled = true;
            yield return new WaitForSeconds(10f);
            bigBombEnabled = false;
            StopCoroutine(firingBigBombCoroutine);
        }
    }

    IEnumerator DoubleSpeed()
    {
        if (doubleSpeedEnabled != true)
        {
            doubleSpeedEnabled = true;
            yield return new WaitForSeconds(10f);
            doubleSpeedEnabled = false;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(shieldPrefab);
    }

    public int GetHealth()
    {
        return health;
    }

    public string GetPowerUp()
    {
        if (doubleDamageEnabled) { return ("Double Fire"); }
        if (maxHealthEnabled) { return ("Max Health"); }
        if (bigBombEnabled) { return ("Instant Kill"); }
        if (doubleSpeedEnabled) { return ("Double Speed"); }
        if (wallEnabled) { return ("Shield"); }
        else { return (" "); }
    }
}