using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuRecordsUI : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text localRecords;
    [SerializeField] TMP_Text onlineRecords;

    public void ShowRecords(int levelNumber)
    {
        title.enabled = true;
        localRecords.enabled = true;
        onlineRecords.enabled = true;

        localRecords.text = "Local Best:\n00:00:0" + levelNumber.ToString();
        onlineRecords.text = "Online Records:\n00:00:00";
    }

    public void HideRecords()
    {
        title.enabled = false;
        localRecords.enabled = false;
        onlineRecords.enabled = false;
    }
}
