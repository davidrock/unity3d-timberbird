using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject playerIdle;
    public GameObject playerHit;
    public GameObject cask;
    public GameObject enemyRight;
    public GameObject enemyLeft;
    public TextMeshProUGUI txtPoints;

    private int _points;
    private List<GameObject> _caskList;
    private float _playerHorizontalScale;
    private float _playerVerticalScale;
    private Vector2 _currentPosition;
    private float _caskHeight;
    private bool _playerDirection;
    private bool _gameOver;

    void Start()
    {
        _gameOver = false;
        var localScale = transform.localScale;
        _playerHorizontalScale = localScale.x;
        _playerVerticalScale = localScale.y;
        _caskHeight = cask.transform.localScale.y;
        _points = 0;
        txtPoints.text = $"Pontos: {_points}";

        playerIdle.SetActive(true);
        playerHit.SetActive(false);

        _caskList = new List<GameObject>();
        GenerateInitialCasks();
    }

    void Update()
    {
        _currentPosition = player.transform.position;
        if (!Input.GetButtonDown("Fire1") || _gameOver) return;
        var clickAction = Input.mousePosition.x;

        if (clickAction > Convert.ToInt16(Screen.width / 2))
        {
            HitRight();
        }
        else
        {
            HitLeft();
        }

        _caskList.RemoveAt(0);
        RecreateCask();
        CheckPlay();
    }

    private void HitLeft()
    {
        _playerDirection = false;
        playerIdle.SetActive(false);
        playerHit.SetActive(true);
        player.transform.position = new Vector2(-1.1f, _currentPosition.y);
        player.transform.localScale = new Vector2(_playerHorizontalScale, _playerVerticalScale);
        Invoke(nameof(SetIdlePlayer), 0.25f);
        _caskList[0].SendMessage("AttackLeft");
    }

    private void HitRight()
    {
        _playerDirection = true;
        playerIdle.SetActive(false);
        playerHit.SetActive(true);
        player.transform.position = new Vector2(0.82f, _currentPosition.y);
        player.transform.localScale = new Vector2(-_playerHorizontalScale, _playerVerticalScale);
        Invoke(nameof(SetIdlePlayer), 0.25f);
        _caskList[0].SendMessage("AttackRight");
    }

    private void SetIdlePlayer()
    {
        playerIdle.SetActive(true);
        playerHit.SetActive(false);
    }

    private GameObject CreateNewCask(Vector2 position)
    {
        GameObject newCask;

        if (Random.value > 0.5f || _caskList.Count <= 2)
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
            _caskList.Add(nCask);
        }
    }

    private void RecreateCask()
    {
        GameObject nCask = CreateNewCask(new Vector2(-0.14f, -3.46f + (10 * 1.03f)));
        _caskList.Add(nCask);
        for (int i = 0; i < 10; i++)
        {
            Vector3 position = _caskList[i].transform.position;
            _caskList[i].transform.position = new Vector2(position.x, position.y - 1.03f);
        }
    }

    private void CheckPlay()
    {
        if (_caskList[0].gameObject.CompareTag("Enemy"))
        {
            if ((_caskList[0].name == "EnemyLeft(Clone)" && !_playerDirection) ||
                (_caskList[0].name == "EnemyRight(Clone)" && _playerDirection))
            {
                print("Acertou um inimigo");
                GameOver();
                return;
            }
        }

        AddScore();
    }

    private void AddScore()
    {
        if (!_gameOver)
            txtPoints.text = $"Pontos: {_points++}";
    }

    private void GameOver()
    {
        _gameOver = true;
        playerHit.GetComponent<SpriteRenderer>().color = new Color(1, 0.35f, 0.05f);
        playerIdle.GetComponent<SpriteRenderer>().color = new Color(1, 0.35f, 0.05f);
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(-4, 10);
        player.GetComponent<Rigidbody2D>().AddTorque(50f);
        Invoke(nameof(Reload), 2f);
    }

    void Reload()
    {
        SceneManager.LoadScene("SampleScene");
    }
}