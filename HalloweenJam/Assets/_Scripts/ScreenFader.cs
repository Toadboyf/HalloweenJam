using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    Image blackScreen;
    public bool transparent = true;
    public PlayerInteraction playerScript;

    void Awake()
    {
        blackScreen = GetComponent<Image>();
        blackScreen.enabled = false;
    }
    public IEnumerator FadeToBlack(GameObject player, Transform target)
    {
        blackScreen.enabled = true;
        transparent = false;
        for(float alpha = 0; alpha < 1; alpha += Time.deltaTime)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);

            yield return null;
        }
        //Move the player
        player.transform.position = target.position;
        StartCoroutine(FadeFromBlack());
    }
    public IEnumerator FadeFromBlack()
    {
        transparent = true;
        for(float alpha = 1.0f; alpha < 0.0f; alpha -= Time.deltaTime)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);

            yield return null;
        }
        blackScreen.enabled = false;
        playerScript.PlayerControlOnOff();
    }
}
