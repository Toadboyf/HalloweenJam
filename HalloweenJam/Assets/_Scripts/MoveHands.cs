using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHands : MonoBehaviour
{
    Animator anim;
    public Transform moveTarget;
    public Transform target;
    MouseLook mouseLook;
    Vector3 targetStartPos;
    public Transform holdPos;
    public Transform idlePos;

    void Awake()
    {
        anim = GetComponent<Animator>();
        mouseLook = GetComponentInParent<MouseLook>();
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            //targetStartPos = holdPos.position;
            anim.enabled = false;
            //Cursor.lockState = CursorLockMode.Confined;
            //target.transform.position = holdPos.position;
            mouseLook.isDisabled = true;
        }
        if(Input.GetMouseButton(1))
        {
            target.position = Vector3.MoveTowards(target.position, holdPos.position, 0.1f);

            //Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //target.position = Input.mousePosition;

        }
        if(Input.GetMouseButtonUp(1))
        {
            anim.enabled = true;
            //Cursor.lockState = CursorLockMode.Locked;
            //target.position = targetStartPos;
            target.transform.position = idlePos.position;
            mouseLook.isDisabled = false;
        }
    }
}
