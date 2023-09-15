using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText, _bestScoreText;
    [SerializeField]
    private Image _liveImg;
    [SerializeField]
    private Sprite[] _liveSprite;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    private int _score, _bestScore;
    [SerializeField]
    private Text _ammoCount;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        BestSinglePlayerScore();
    }

    // Update is called once per frame
    void BestSinglePlayerScore()
    {
        if (_gameManager._isCoopMode == false)
        {
            _bestScore = PlayerPrefs.GetInt("HighScore", 0);
            _bestScoreText.text = "Best: " + _bestScore;
        }
    }
    void GameOverSequence()
    {
        _gameManager.RestartGame();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }
    public void AddScore()
    {
        _score += 10;
        _scoreText.text = "Score: " + _score;
    }
    public void BestScore()
    {
        if (_gameManager._isCoopMode == false)
        {
            if (_score > _bestScore)
            {
                _bestScore = _score;
                PlayerPrefs.SetInt("HighScore", _bestScore);
                _bestScoreText.text = "Best: " + _bestScore;
            }
        }
    }
    public void UpdateLives(int currentlives)
    {
        _liveImg.sprite = _liveSprite[currentlives];
        if (currentlives == 0)
        {
            GameOverSequence();
        }
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void ResumePlay()
    {
        _gameManager.ResumeGame();
    }
    public void MainMenu()
    {
        _gameManager.GoToMainMenu();
    }
    public void AmmoCount(int count) {
        _ammoCount.text = "" + count;
    }
}
