using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject playerIdle;
    public GameObject playerHit;

    private float _playerHorizontalScale;
    private float _playerVerticalScale;
    private Vector2 _currentPosition;

    void Start()
    {
        var localScale = transform.localScale;
        _playerHorizontalScale = localScale.x;
        _playerVerticalScale = localScale.y;

        playerIdle.SetActive(true);
        playerHit.SetActive(false);
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
        player.transform.position = new Vector2(1.1f, _currentPosition.y);
        player.transform.localScale = new Vector2(-_playerHorizontalScale, _playerVerticalScale);
        Invoke("SetIdlePlayer", 0.25f);
    }

    void SetIdlePlayer()
    {
        playerIdle.SetActive(true);
        playerHit.SetActive(false);
    }
}