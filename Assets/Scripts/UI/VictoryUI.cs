using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryUI : GenericUI
{
    Canvas uiCanvas;
    [SerializeField] int currentLevelNumber;
    [SerializeField] string nextLevelName;
    [SerializeField] Timer timer;
    [SerializeField] TMP_Text timerText;

    void Start()
    {
        uiCanvas = GetComponent<Canvas>();
        initDone = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2)) {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3)) {
            LoadMainMenu();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button7)) {
            QuitGame();
        }
    }

    public override void Show()
    {
        Time.timeScale = 0;
        timer.gameObject.SetActive(false);
        uiCanvas.enabled = true;
        timerText.text = timer.GetTimeString();
        Records.SetRecord(currentLevelNumber, timer.GetTimeFloat());
    }

    public override void Hide()
    {
        timer.Enable();
        uiCanvas.enabled = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
#if !UNITY_WEBGL
        Application.Quit();
#endif
        print("QUIT");
    }
}
