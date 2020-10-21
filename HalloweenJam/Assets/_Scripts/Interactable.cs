using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //What purpose does the item serve?
    public bool pickup;
    public GameObject pickupPrefab;
    public string heldName;
    public RuntimeAnimatorController animatorController;
    public bool right;
    public bool left;
    public GameObject itemToEnable;

    public bool note;
    public string noteText;
    public AudioClip noteAudio;

    public bool isDoor;
    public bool locked;
    public Transform teleportPos;

    public bool animate;
    public Animator animToStart;
    public string stateToRun;
    public bool destroyAfter;
    public bool pauseDuring;
    public void EnableOnPlayer()
    {
        itemToEnable.SetActive(true);
    }
}
