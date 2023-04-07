using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject levelsCanvas;
    AudioSource audioSource;
    bool isMenuOpen = true;
    public LevelButton levelButton1;
    public LevelButton levelButton2;
    public LevelButton levelButton3;
    public LevelButton levelButton4;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenMenu()
    {
        mainMenuCanvas.SetActive(true);
        levelsCanvas.SetActive(false);
        audioSource.Play();
        isMenuOpen = true;
    }

    public void OpenLevels()
    {
        mainMenuCanvas.SetActive(false);
        levelsCanvas.SetActive(true);
        audioSource.Play();
        isMenuOpen = false;
    }

    public void QuitGame()
    {
#if !UNITY_WEBGL
        Application.Quit();
#endif
        print("QUIT");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            OpenMenu();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) && isMenuOpen) {
            QuitGame();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && isMenuOpen) {
            OpenLevels();
        }

        if (!isMenuOpen && Input.GetKeyDown(KeyCode.Joystick1Button0)) {
            levelButton1.LoadLevel();
        }
        if (!isMenuOpen && Input.GetKeyDown(KeyCode.Joystick1Button1)) {
            levelButton2.LoadLevel();
        }
        if (!isMenuOpen && Input.GetKeyDown(KeyCode.Joystick1Button2)) {
            levelButton3.LoadLevel();
        }
        if (!isMenuOpen && Input.GetKeyDown(KeyCode.Joystick1Button3)) {
            levelButton4.LoadLevel();
        }
    }
}
