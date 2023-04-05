using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject levelsCanvas;
    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenMenu()
    {
        mainMenuCanvas.SetActive(true);
        levelsCanvas.SetActive(false);
        audioSource.Play();
    }

    public void OpenLevels()
    {
        mainMenuCanvas.SetActive(false);
        levelsCanvas.SetActive(true);
        audioSource.Play();
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }
}
