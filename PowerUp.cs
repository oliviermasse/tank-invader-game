using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float spin = 60f;
    public bool wallEnabled = false;
    public bool doubleDamageEnabled = false;


    // Start is called before the first frame update
    public void OnTriggerEnter2D()
    {
        int powerUpIndex = 0;  //Random.Range(0,2);
        Destroy(gameObject);


        if (powerUpIndex == 0)
        {
            Wall();
        }
        else if (powerUpIndex == 1)
        {
            DoubleDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spin * Time.deltaTime);
    }

    public void Wall()
    {
        wallEnabled = true;
    }

    public void DoubleDamage()
    {
        doubleDamageEnabled = true;
    }
}
