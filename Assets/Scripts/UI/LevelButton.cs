using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string levelName;
    [SerializeField] int levelNumber;
    [SerializeField] MainMenuRecordsUI recordsUI;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        recordsUI.ShowRecords(levelNumber);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        recordsUI.HideRecords();
    }
}
