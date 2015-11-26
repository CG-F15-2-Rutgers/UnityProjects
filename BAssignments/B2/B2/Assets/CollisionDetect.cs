using UnityEngine;
using System.Collections;

public class CollisionDetect : MonoBehaviour {

    // Use this for initialization
    private Rigidbody HandSphere;

    bool doorOpen;
    bool doorClose;
    bool doorIdle;
    bool ButtonPress;

    public GameObject MovingDoor;
    public GameObject MovingDoorButton;
    Animator DoorAnimator;
    Animator ButtonAnimator;

    void Start () {
        HandSphere = GetComponent<Rigidbody>();
        DoorAnimator = MovingDoor.GetComponent<Animator>();
        ButtonAnimator = MovingDoorButton.GetComponent<Animator>();
    }
	

    void Doors(string direction)
    {
        DoorAnimator.SetTrigger(direction);
    }

    void Button(string direction)
    {
        ButtonAnimator.SetTrigger(direction);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Button")
        {
            Button("ButtonPressed");
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
