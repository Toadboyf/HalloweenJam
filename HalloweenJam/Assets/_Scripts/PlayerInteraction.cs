using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    bool interacting;
    public float interactDist;
    public LayerMask targetMask;
    public TextHandler displayText;
    public Animator anim;
    RuntimeAnimatorController currentController;
    public GameObject heldItem;
    Camera cam;
    void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDist, targetMask))
        {
            //Grab the Description class
            string hitText = hit.transform.GetComponent<Description>().description;
            //Show dialogue box
            if(!displayText.isShowing(hitText))
            {
                displayText.ShowText(hitText);
            }
            if(Input.GetMouseButtonDown(0))
            {
                Interact(hit.transform.gameObject);
            }
        }
        else
        {
            //Remove dialogue box
            if(displayText.isShowing())
                displayText.ResetText();
        }
    }

    void Interact(GameObject target)
    {
        Interactable item = target.GetComponent<Interactable>();
        if(item.pickup)
        {
            heldItem = item.pickupPrefab;
            //pickup the item with animation
            anim.runtimeAnimatorController = item.animatorController;
            anim.enabled = true;
            Destroy(target);
        }
        else 
        {

        }
    }
}
