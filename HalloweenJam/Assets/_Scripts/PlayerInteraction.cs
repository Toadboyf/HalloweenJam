using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    bool interacting;
    public bool looking;
    public float interactDist;
    public Outline storedLookTargetOutline;
    public LayerMask targetMask;
    public TextHandler displayText;
    public Animator rightAnim;
    RuntimeAnimatorController rightCurrentController;
    public Animator leftAnim;
    RuntimeAnimatorController leftCurrentController;
    public List<GameObject> heldItems = new List<GameObject>();
    public IKTargetController IKController;
    AudioSource audioSource;
    public TextHandler noteController;
    PlayerScript playerController;
    MouseLook playerLook;
    Camera cam;
    void Awake()
    {
        cam = Camera.main;
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerScript>();
        playerLook = GetComponentInChildren<MouseLook>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!looking)
        {
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDist, targetMask))
            {
                //Grab the Description class
                //string hitText = hit.transform.GetComponent<Description>().description;
                //Show dialogue box
                /*
                if(!displayText.isShowing(hitText))
                {
                    displayText.ShowText(hitText);
                }
                */
                //Show outline while hovering over interactible objects
                storedLookTargetOutline = hit.transform.gameObject.GetComponent<Outline>();
                storedLookTargetOutline.enabled = true;
                if(Input.GetMouseButtonDown(0))
                {
                    Interact(hit.transform.gameObject);
                }
            }
            else
            {
                //Remove dialogue box
                /*
                if(displayText.isShowing())
                    displayText.ResetText();
                */

                //remove item outline
                if(storedLookTargetOutline != null)
                {
                    storedLookTargetOutline.enabled = false;
                    storedLookTargetOutline = null;
                }
            }
        }
        else if(looking)
        {
            if(audioSource.clip != null)
            {
                //Wait for note audio or note timer to end
                if(!audioSource.isPlaying)
                {
                    looking = false;
                    PlayerControlOnOff();
                    noteController.ResetText();
                    noteController.gameObject.SetActive(false);
                }
            }
            else
            {
                if(Input.GetMouseButtonDown(0))
                {
                    looking = false;
                    PlayerControlOnOff();
                    noteController.gameObject.SetActive(false);
                    noteController.ResetText();
                }
            }
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
                item.EnableOnPlayer();
                //move left hand target from down pos to up pos.
                IKController.MoveLeftUp();
                Destroy(target);
            }
            else
            {
                heldItems.Add(item.pickupPrefab);
                //pickup the item with animation
                item.EnableOnPlayer();
                //move right hand target from down pos to up pos.
                IKController.MoveRightUp();
                Destroy(target);
            }
        }
        else if(item.note)
        {
            looking = true;
            PlayerControlOnOff();
            noteController.gameObject.SetActive(true);
            noteController.ShowText(item.noteText);
            if(item.noteAudio != null)
            {
                audioSource.PlayOneShot(item.noteAudio);
            }
        }
    }

    void PlayerControlOnOff()
    {
        playerController.isDisabled = !playerController.isDisabled;
        playerLook.isDisabled = !playerLook.isDisabled;
    }
}
