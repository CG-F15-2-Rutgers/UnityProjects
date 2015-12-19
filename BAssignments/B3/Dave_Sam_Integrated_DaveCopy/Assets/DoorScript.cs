using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
    bool doorOpen;

    public GameObject door;
    public GameObject scanner;
    public GameObject GreenIndicator;
    public GameObject RedIndicator;

    private Rigidbody ObjectPassingThrough;

    Animator doorAnimator;

	void Start () {
        ObjectPassingThrough = GetComponent<Rigidbody>();
        doorAnimator = door.GetComponent<Animator>();
	}
	
    void OnTriggerEnter(Collider Other)
    {
        Debug.Log("Collision Detected");
        if (Other.gameObject.name == "MainButton")
        {
            GreenIndicator.SetActive(true);
            RedIndicator.SetActive(false);
            doorOpen = true;
            doorAnimator.SetTrigger("DoorOpen");
        }
    }

    
}
