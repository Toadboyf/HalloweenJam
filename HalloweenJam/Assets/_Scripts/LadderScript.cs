using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    //When player enters the zone next to the ladder, set gravity to zero, when leaves set gravity back to normal
    //Determine zone with a box collider?

    public PlayerScript playerController;
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            CharacterController controller = playerController.GetComponent<CharacterController>();
            controller.slopeLimit = 190.0f;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            CharacterController controller = playerController.GetComponent<CharacterController>();
            controller.slopeLimit = 45.0f;
        }
    }
}
