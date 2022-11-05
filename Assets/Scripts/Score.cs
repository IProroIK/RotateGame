using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private const int DoorPassScore = 10;
    private const int WinScore = 300;

    [SerializeField] private List<Door> _doors;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private TextEffect _textEffect;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int _score;
    [SerializeField] private Win _win;
    private int _poolCount = 16;
    private bool _autoExpand = true;
    private PoolMono<TextEffect> _pool;

    private void Awake()
    {
        _pool = new PoolMono<TextEffect>(_textEffect, _poolCount, _poolContainer);
        _pool.autoExapand = _autoExpand;
    }
    private void OnEnable()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].OnDoorPass += AddScoreAfterDoorPass;

        }
        _player.OnAngileFeet += SpawnText;
        _win.OnWin += AddWinScore;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].OnDoorPass -= AddScoreAfterDoorPass;
        }
        _player.OnAngileFeet -= SpawnText;
        _win.OnWin -= AddWinScore;
    }

    private void SpawnText()
    {
        var textEffect = _pool.GetFreeElement();
        float randomOffset = Random.Range(3.5f, 5.6f);
        textEffect.transform.position = new Vector3(transform.position.x + randomOffset, transform.position.y, transform.position.z + 2);
        UpdateScoreUI(1);
    }
    private void UpdateScoreUI(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score.ToString();
    }

    private void AddScoreAfterDoorPass()
    {
        UpdateScoreUI(DoorPassScore);
    }

    private void AddWinScore()
    {
        UpdateScoreUI(WinScore);
    }

}
