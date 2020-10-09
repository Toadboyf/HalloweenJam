using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    public Text text;

    void Awake()
    {
        
    }

    public void ShowText(string t)
    {
        text.gameObject.SetActive(true);
        text.text = t;
    }

    public void ResetText()
    {
        text.text = "";
        text.gameObject.SetActive(false);
    }
    public bool isShowing()
    {
        if(text.isActiveAndEnabled)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isShowing(string t)
    {
        if(text.text == t)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
