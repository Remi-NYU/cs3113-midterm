using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] int levelNumber;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    public void Start() {
        TMP_Text recordTime = transform.GetChild(0).GetComponent<TMP_Text>();
        Records.Initialize();
        float time = Records.GetRecord(levelNumber);
        if (time == -1) {
            recordTime.text = "No record";
        } else {
            recordTime.text = string.Format("{0:00}:{1:00}.{2:00}", Mathf.Floor(time / 60), Mathf.Floor(time % 60), Mathf.Floor((time * 100) % 100));
        }
    }
}
