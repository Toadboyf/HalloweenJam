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
}
