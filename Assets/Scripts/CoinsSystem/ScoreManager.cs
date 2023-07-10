using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int _score = 0;
    public static ScoreManager Instance;
    [SerializeField] private Text _scoreCount;

    void Start()
    {
        _scoreCount.enabled = true;
        Invoke("HideScoreBar", 3f);
        Instance = this;
    }

    public void AddScore(int _count)
    {
        _score += _count;
        _scoreCount.text = "Score: " + (_score).ToString();

        _scoreCount.enabled = true;
        Invoke("HideScoreBar", 3f);
    }

    private void HideScoreBar()
    {
        _scoreCount.enabled = false;
    }
}