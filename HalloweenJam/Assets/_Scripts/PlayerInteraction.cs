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
    public Sway leftSway;
    public Sway rightSway;
    public Animator rightAnim;
    RuntimeAnimatorController rightCurrentController;
    public Animator leftAnim;
    RuntimeAnimatorController leftCurrentController;
    public List<GameObject> heldItems = new List<GameObject>();
    public IKTargetController IKController;
    AudioSource audioSource;
    public TextHandler noteController;
    //public ScreenFader screenFader;
    public Image blackScreenOverlay;
    PlayerScript playerController;
    MouseLook playerLook;
    Camera cam;
    void Awake()
    {
        cam = Camera.main;
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerScript>();
        playerLook = GetComponentInChildren<MouseLook>();
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
        else if(item.isDoor)
        {
            if(!item.locked)
            {
                PlayerControlOnOff();
                blackScreenOverlay.color = Color.black;
                transform.position = item.teleportPos.position;
                transform.rotation = item.teleportPos.rotation;
                StartCoroutine(FadeFromBlack());
            }
        }
        else if(item.animate)
        {
            //pause player while animation plays
            if(item.pauseDuring)
                PlayerControlOnOff();
            //Add corresponding anim controller to item anim
            item.animToStart.runtimeAnimatorController = item.animatorController;
            //Turn on animator
            item.animToStart.enabled = true;
            item.animToStart.Play(item.stateToRun);
        }
    }

    public IEnumerator FadeFromBlack()
    {
        for(float alpha = 1.0f; alpha > 0.0f; alpha -= Time.deltaTime)
        {
            blackScreenOverlay.color = new Color(blackScreenOverlay.color.r, blackScreenOverlay.color.g, blackScreenOverlay.color.b, alpha);

            yield return null;
        }
        PlayerControlOnOff();
    }

    public void PlayerControlOnOff()
    {
        playerController.isDisabled = !playerController.isDisabled;
        playerLook.isDisabled = !playerLook.isDisabled;
        leftSway.isDisabled = !leftSway.isDisabled;
        rightSway.isDisabled = !rightSway.isDisabled;
    }
}
