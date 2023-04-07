using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityIndicatorHUD : MonoBehaviour
{
    Image image;
    TMP_Text text;
    [SerializeField] Sprite rollSprite;
    [SerializeField] Sprite jumpSprite;
    [SerializeField] Sprite fallSprite;
    [SerializeField] Sprite glideSprite;
    ControlWrapper controlWrapper = new ControlWrapper();

    void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        text = transform.GetChild(1).GetComponent<TMP_Text>();
        SetRoll();
    }

    void Update() {
        if (controlWrapper.Mode_Move())
            SetRoll();
        if (controlWrapper.Mode_Jump())
            SetJump();
        if (controlWrapper.Mode_Fall())
            SetFall();
        if (controlWrapper.Mode_Glide())
            SetGlide();
    }

    public void SetRoll() {
        image.sprite = rollSprite;
        text.text = "Roll";
    }

    public void SetJump() {
        image.sprite = jumpSprite;
        text.text = "Jump";
    }

    public void SetFall() {
        image.sprite = fallSprite;
        text.text = "Fall";
    }

    public void SetGlide() {
        image.sprite = glideSprite;
        text.text = "Glide";
    }
}
