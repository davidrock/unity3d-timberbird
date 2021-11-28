using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBarController : MonoBehaviour
{
    private float _scaleBar;
    private bool _gameEnded;
    private bool _gameStarted;
    public GameObject gameEngine;

    private void Start()
    {
        _scaleBar = this.transform.localScale.x;
    }

    private void Update()
    {
        if (_gameStarted)
        {
            if (_scaleBar > 0.0f)
            {
                _scaleBar -= 0.15f * Time.deltaTime;
                this.transform.localScale = new Vector2(_scaleBar, 1f);
            }
            else if (!_gameEnded)
            {
                _gameEnded = true;
                gameEngine.SendMessage("GameOver");
            }
        }
    }

    void Started()
    {
        _gameStarted = true;
    }

    void InscreaseTime()
    {
        if (_scaleBar <= 1f)
        {
            _scaleBar += 0.035f;
        }
    }

    void Stop()
    {
        _gameEnded = true;
        _gameStarted = false;
    }
}