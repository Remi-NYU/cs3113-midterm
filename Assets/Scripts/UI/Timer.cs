using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    float time = 0;
    bool timerRunning = true;

    // Update is called once per frame
    void Update()
    {
        if (!timerRunning) return;

        time += Time.unscaledDeltaTime;
        timerText.text = GetTimeString();
    }

    public void Enable()
    {
        timerRunning = true;
    }

    public void Disable()
    {
        timerRunning = false;
    }

    public string GetTimeString()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 100) % 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public float GetTimeFloat() {
        return time;
    }
}
