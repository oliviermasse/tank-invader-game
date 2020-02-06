using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreDisplay : MonoBehaviour
{
    Text bestScoreText;
    GameSession gameSession;

    void Start()
    {
        bestScoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        bestScoreText.text = gameSession.GetBestScore().ToString();
    }
}
