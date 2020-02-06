using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] bool isBomb = false;
    [SerializeField] GameObject explosionVFX;
    float explosionDuration = 0.5f;

    public int GetDamage()
    {
        return damage;
    }
    public void Hit()
    {
        Destroy(gameObject);
        if (isBomb)
        {
            GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);
        }
    }

}
