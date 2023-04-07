using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : GenericUI
{
    Canvas uiCanvas;
    [SerializeField] Timer timer;

    void Start()
    {
        uiCanvas = GetComponent<Canvas>();
        initDone = true;
    }

        void Update() {
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
        timer.Disable();
        uiCanvas.enabled = true;
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
