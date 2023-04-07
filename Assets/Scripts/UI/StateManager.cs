using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Moving, Paused, Victory }

[System.Serializable]
struct StateToUI
{
    public GameState state;
    public GenericUI ui;
}

public class StateManager : MonoBehaviour
{
    [SerializeField] StateToUI[] stateToUI;
    GameState currentState;
    bool initFinished;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // All UI components need to have finished initializing to be able to do things
        if (!initFinished && !AttemptInit())
        {
            return;
        }

        // Pause
        if (currentState == GameState.Moving && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)))
        {
            SwitchState(GameState.Paused);
        }
        // Unpause
        else if (currentState == GameState.Paused && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            SwitchState(GameState.Moving);
        }
    }

    // returns true if initialized successfully, false otherwise
    bool AttemptInit()
    {
        // Will not attempt to initialize if the UIs have yet to initialize
        for (int i = 0; i < stateToUI.Length; i++)
        {
            if (!stateToUI[i].ui.initDone)
            {
                stateToUI[i].ui.gameObject.SetActive(true);
                return false;
            }
            else
            {
                stateToUI[i].ui.Hide();
            }
        }

        currentState = GameState.Moving;
        SwitchState(GameState.Moving);

        initFinished = true;
        return true;
    }

    public GenericUI GetUI(GameState state)
    {
        for (int i = 0; i < stateToUI.Length; i++)
        {
            if (stateToUI[i].state == state)
            {
                return stateToUI[i].ui;
            }
        }
        return null;
    }

    public void SwitchState(GameState newState)
    {
        GetUI(currentState).Hide();
        GetUI(newState).Show();
        currentState = newState;
        audioSource.Play();
    }

    public void Unpause()
    {
        SwitchState(GameState.Moving);
    }
}
