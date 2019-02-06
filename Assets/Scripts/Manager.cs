using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public static Manager Instance;

    public Animator anim;
    public bool textBoxActive = false;

    public Text textBoxText;
    public Text buttonText;

    public Button button;

    public string defaultButtonText = "Show Quote";
    public string altButtonText = "Back";

    public bool buttonState = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Hover()
    {
        button.interactable = true;
    }

    public void UnHover()
    {
        button.interactable = false;
    }

    public void ActivateTextBox(string newText)
    {
        textBoxText.text = newText;
        textBoxActive = true;
        anim.SetBool("isActive", true);
    }

    public void DisableTextBox()
    {
        textBoxActive = false;
        UnHover();
        anim.SetBool("isActive", false);
    }

    public void ToggleButtonState()
    {
        buttonState = !buttonState;

        if (buttonState)
            buttonText.text = altButtonText;
        else
            buttonText.text = defaultButtonText;

    }

}
