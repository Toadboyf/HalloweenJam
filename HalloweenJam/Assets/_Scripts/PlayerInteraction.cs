using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    bool interacting;
    public float interactDist;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Interact();
        }
    }

    void Interact()
    {
        if(!interacting)
        {
            interacting = true;
            RaycastHit hit;
            Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDist);
            
        }
    }
}
