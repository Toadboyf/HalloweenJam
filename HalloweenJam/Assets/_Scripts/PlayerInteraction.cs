using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    bool interacting;
    public float interactDist;
    public LayerMask targetMask;
    public TextHandler displayText;
    public Animator rightAnim;
    RuntimeAnimatorController rightCurrentController;
    public Animator leftAnim;
    RuntimeAnimatorController leftCurrentController;
    public List<GameObject> heldItems = new List<GameObject>();
    public IKTargetController IKController;
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
            if(item.heldName == "flashlight")
            {
                heldItems.Add(item.pickupPrefab);
                //leftAnim.runtimeAnimatorController = item.animatorController;
                //leftAnim.enabled = true;
                item.EnableOnPlayer();
                //move left hand target from down pos to up pos.
                IKController.MoveLeftUp();
                Destroy(target);
            }
            else
            {
                heldItems.Add(item.pickupPrefab);
                //pickup the item with animation
                //rightAnim.runtimeAnimatorController = item.animatorController;
                //rightAnim.enabled = true;
                item.EnableOnPlayer();
                //move right hand target from down pos to up pos.
                IKController.MoveRightUp();
                Destroy(target);
            }
        }
    }
}
