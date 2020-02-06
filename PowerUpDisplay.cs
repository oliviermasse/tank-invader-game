using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpDisplay : MonoBehaviour
{
    Text powerUpText;
    Player player;

    void Start()
    {
        powerUpText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        powerUpText.text = player.GetPowerUp();
    }
}
