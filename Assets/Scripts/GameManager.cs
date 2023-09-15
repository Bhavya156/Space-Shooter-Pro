using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    public bool _isCoopMode = false;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _pausepanel;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is empty");
        }
    }
    void Update()
    {
        if (_isCoopMode == false)
        {
            if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
            {
                SceneManager.LoadScene(1); //Game scene
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pausepanel.SetActive(true); // Main Scene
                Time.timeScale = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
            {
                SceneManager.LoadScene(2); // loads co-op mode again
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0); //loads main menu
            }
        }
    }

    public void RestartGame()
    {
        _isGameOver = true;
    }
    public void ResumeGame()
    {
        _pausepanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
