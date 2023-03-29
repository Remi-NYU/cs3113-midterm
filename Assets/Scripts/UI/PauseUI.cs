using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : GenericUI
{
    Canvas uiCanvas;

    void Start()
    {
        uiCanvas = GetComponent<Canvas>();
        initDone = true;
    }

    public override void Show()
    {
        uiCanvas.enabled = true;
    }

    public override void Hide()
    {
        uiCanvas.enabled = false;
    }
}