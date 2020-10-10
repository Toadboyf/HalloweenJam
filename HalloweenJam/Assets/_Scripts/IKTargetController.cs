using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetController : MonoBehaviour
{
    public Transform left_down;
    public Transform left_up;
    public Transform right_down;
    public Transform right_up;
    public float moveSpeed;
    public Transform target_l;
    public Transform target_r;
    public bool movingLeft;
    public bool movingRight;

    void Awake()
    {
        target_l.position = left_down.position;
        target_r.position = right_down.position;
    }

    void Update()
    {
        if(movingLeft)
        {
            //StartCoroutine(UpSpeed(target_l, left_up, "left"));
            if(Vector3.Distance(target_l.position, left_up.position) > 0.01f)
            {
                //Vector3.Lerp(target_l.position, left_up.position, Time.deltaTime * moveSpeed);
                target_l.position = left_up.position;
            }
            else
            {
                movingLeft = false;
                SwitchSway(target_l);
            }
        }
        if(movingRight)
        {
            //StartCoroutine(UpSpeed(target_r, right_up, "right"));
            if(Vector3.Distance(target_r.position, right_up.position) > 0.01f)
            {
                //Vector3.Lerp(target_r.position, right_up.position, Time.deltaTime * moveSpeed);
                target_r.position = right_up.position;
            }
            else
            {
                movingRight = false;
                SwitchSway(target_r);
            }
        }
    }
    public void MoveLeftUp()
    {
        SwitchSway(target_l);
        movingLeft = true;
    }

    public void MoveRightUp()
    {
        SwitchSway(target_r);
        movingRight = true;
    }

    IEnumerator UpSpeed(Transform hand, Transform target, string side)
    {
        if(Vector3.Distance(hand.position, target.position) > 0.01f)
        {
            Vector3.Lerp(hand.position, target.position, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }
        else
        {
            if(side == "left")
            {
                movingLeft = false;
                SwitchSway(target_l);
            }
            else
            {
                movingRight = false;
                SwitchSway(target_r);
            }
            yield return null;
        }
    }

    public void SwitchSway(Transform target)
    {
        target.GetComponent<Sway>().enabled = !target.GetComponent<Sway>().enabled;
    }
}
