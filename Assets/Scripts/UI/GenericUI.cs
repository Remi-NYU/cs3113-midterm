using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericUI : MonoBehaviour
{
    [HideInInspector] public bool initDone = false;
    [HideInInspector] public bool uiEnabled = false;

    public virtual void Show() { uiEnabled = true; }
    public virtual void Hide() { uiEnabled = false; }
}
