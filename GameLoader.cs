using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        gameSession.ResetScore();
    }
    void Update()
    {
        Level level = GetComponent<Level>();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            level.LoadGame();
        }
    }
}
