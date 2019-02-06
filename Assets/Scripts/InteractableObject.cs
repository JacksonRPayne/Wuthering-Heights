using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public string quote;

    public void Highlight()
    {
        Debug.Log("highlighted");
    }

    public void UnHighlight()
    {
        Debug.Log("un-highlighted");
    }

    public void DisplayQuote()
    {
        Manager.Instance.ActivateTextBox(quote);
        Debug.Log(quote);
    }

}
