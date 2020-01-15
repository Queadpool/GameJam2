using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool _gameIsPaused = false;
    public GameObject PauseMenuUI;

    [SerializeField] private float _floatTimer = 3.0f;
    [SerializeField] private int _intTimer = 3;
    [SerializeField] private bool _countdown = false;
    [SerializeField] private Text _countdownTitle;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (_countdown)
        {
            _floatTimer -= Time.deltaTime;

            if(_floatTimer <= 0)
            {
                _intTimer = 0;
                _countdownTitle.text = _intTimer.ToString();
                _countdown = false;
                _countdownTitle.gameObject.SetActive(false);
                _floatTimer = 3.0f;
                _intTimer = 3;
            }
            else if(_floatTimer <= 1)
            {
                _intTimer = 1;
                _countdownTitle.text = _intTimer.ToString();
            }
            else if(_floatTimer <= 2)
            {
                _intTimer = 2;
                _countdownTitle.text = _intTimer.ToString();
            }
            else if(_floatTimer == 3)
            {
                _countdownTitle.text = _intTimer.ToString();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        _gameIsPaused = false;
        _countdown = true;
        _countdownTitle.gameObject.SetActive(true);
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        _gameIsPaused = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
