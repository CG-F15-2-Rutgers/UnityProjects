using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour
{

    // Use this for initialization
    private Rigidbody HandSphere;

    bool doorOpen;
    bool doorClose;
    bool doorIdle;
   // bool ButtonPress;

    public GameObject MovingDoor;
    public GameObject MovingDoorButton;
    public GameObject IndicateDoorCollision;

    Animator DoorAnimator;
   // Animator ButtonAnimator;

    void Start()
    {
        HandSphere = GetComponent<Rigidbody>();
        DoorAnimator = MovingDoor.GetComponent<Animator>();
       // ButtonAnimator = MovingDoorButton.GetComponent<Animator>();
    }


    void Doors(string direction)
    {
        DoorAnimator.SetTrigger(direction);
    }

    /*
    void Button(string direction)
    {
        ButtonAnimator.SetTrigger(direction);
    }
    */

    void IndicateCollision()
    {
        IndicateDoorCollision.SetActive(true);
        Invoke("IndicateCollisionFalse", 1F);

    }

    void IndicateCollisionFalse()
    {
        IndicateDoorCollision.SetActive(false);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Button")
        {
            IndicateCollision();
           // Button("ButtonPressed");
            if (doorOpen == false)
            {
                doorOpen = true;
                Doors("Open");
            }
            else if (doorOpen == true)
            {
                doorOpen = false;
                doorClose = true;
                Doors("Close");
            }


        }

    }
}
