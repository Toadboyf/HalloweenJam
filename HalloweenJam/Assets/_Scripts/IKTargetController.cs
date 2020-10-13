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
    public bool movingLeftup;
    public bool movingRightup;
    public bool movingLeftdown;
    public bool movingRightdown;


    void Awake()
    {
        target_l.position = left_down.position;
        target_r.position = right_down.position;
    }

    void Update()
    {
        if(movingLeftup)
        {
            if(Vector3.Distance(target_l.position, left_up.position) > 0.1f)
            {
                target_l.position = Vector3.MoveTowards(target_l.position, left_up.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                movingLeftup = false;
                SwitchSway(target_l);
            }
        }
        else if(movingLeftdown)
        {
            if(Vector3.Distance(target_l.position, left_down.position) > 0.1f)
            {
                target_l.position = Vector3.MoveTowards(target_l.position, left_down.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                movingLeftdown = false;
                SwitchSway(target_l);
            }
        }
        if(movingRightup)
        {
            if(Vector3.Distance(target_r.position, right_up.position) > 0.1f)
            {
                target_r.position = Vector3.MoveTowards(target_r.position, right_up.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                movingRightup = false;
                SwitchSway(target_r);
            }
        }
        else if(movingRightdown)
        {
            if(Vector3.Distance(target_r.position, right_down.position) > 0.1f)
            {
                target_r.position = Vector3.MoveTowards(target_r.position, right_down.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                movingRightdown = false;
                SwitchSway(target_r);
            }
        }
    }
    public void MoveLeftUp()
    {
        SwitchSway(target_l);
        movingLeftup = true;
    }
    public void MoveLeftDown()
    {
        SwitchSway(target_l);
        movingLeftdown = true;
    }
    public void MoveRightUp()
    {
        SwitchSway(target_r);
        movingRightup = true;
    }
    public void MoveRightDown()
    {
        SwitchSway(target_r);
        movingRightdown = true;
    }

    public void SwitchSway(Transform target)
    {
        target.GetComponent<Sway>().enabled = !target.GetComponent<Sway>().enabled;
    }
}
