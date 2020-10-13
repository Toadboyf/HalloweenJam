using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DitzelGames.FastIK;

public class AnimScriptHandler : MonoBehaviour
{
    public Image redScreen;
    public float fadeTime;
    Color screenAlpha;
    public FastIKFabric rightIK;
    public FastIKFabric leftIK;
    public void DisableIK_Right()
    {
        rightIK.enabled = false;
    }
    public void DisableIK_Left()
    {
        leftIK.enabled = false;
    }
    public void EnableIK_Right()
    {
        rightIK.enabled = true;
    }
    public void EnableIK_Left()
    {
        leftIK.enabled = true;
    }
    public void PainFlash()
    {
        screenAlpha = redScreen.color;
        screenAlpha.a = 0;
        redScreen.enabled = true;
    }   

    void Update()
    {
        if(redScreen.enabled)
        {
            redScreen.color = Color.Lerp(redScreen.color, screenAlpha, fadeTime * Time.deltaTime);
            if(redScreen.color.a < 0.1f)
            {
                redScreen.color = screenAlpha;
                redScreen.enabled = false;
            }
        }
    }   
}
