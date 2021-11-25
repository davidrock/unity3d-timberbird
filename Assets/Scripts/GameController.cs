using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject playerIdle;
    public GameObject playerHit;

    public GameObject cask;
    public GameObject enemyRight;
    public GameObject enemyLeft;

    private float _playerHorizontalScale;
    private float _playerVerticalScale;
    private Vector2 _currentPosition;
    private float _caskHeight;

    void Start()
    {
        var localScale = transform.localScale;
        _playerHorizontalScale = localScale.x;
        _playerVerticalScale = localScale.y;
        _caskHeight = cask.transform.localScale.y;

        playerIdle.SetActive(true);
        playerHit.SetActive(false);
        
        GenerateInitialCasks();
    }

    void Update()
    {
        _currentPosition = player.transform.position;
        if (!Input.GetButtonDown("Fire1")) return;
        var clickAction = Input.mousePosition.x;

        if (clickAction > Convert.ToInt16(Screen.width / 2))
        {
            HitRight();
        }
        else
        {
            HitLeft();
        }
    }

    private void HitLeft()
    {
        playerIdle.SetActive(false);
        playerHit.SetActive(true);
        player.transform.position = new Vector2(-1.1f, _currentPosition.y);
        player.transform.localScale = new Vector2(_playerHorizontalScale, _playerVerticalScale);
        Invoke("SetIdlePlayer", 0.25f);
    }

    private void HitRight()
    {
        playerIdle.SetActive(false);
        playerHit.SetActive(true);
        player.transform.position = new Vector2(0.82f, _currentPosition.y);
        player.transform.localScale = new Vector2(-_playerHorizontalScale, _playerVerticalScale);
        Invoke("SetIdlePlayer", 0.25f);
    }

    void SetIdlePlayer()
    {
        playerIdle.SetActive(true);
        playerHit.SetActive(false);
    }

    public GameObject CreateNewCask(Vector2 position)
    {
        GameObject newCask;

        if (Random.value > 0.5f)
        {
            newCask = Instantiate(cask);
        }
        else
        {
            newCask = Instantiate(Random.value > 0.5f ? enemyRight : enemyLeft);
        }

        newCask.transform.position = position;
        return newCask;
    }

    private void GenerateInitialCasks()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject nCask = CreateNewCask(new Vector2(-0.14f, -3.46f + (i * 1.03f)));
        }
    }
}