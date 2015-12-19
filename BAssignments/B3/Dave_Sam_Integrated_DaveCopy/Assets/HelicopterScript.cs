using UnityEngine;
using System.Collections;

public class HelicopterScript : MonoBehaviour {
    public GameObject Sam;
    public GameObject Dave;
    public GameObject Helicopter;
    public Camera camera1;
    public Camera camera2;

    private Rigidbody HeliCollision;

    Animator helicopteranimator;
    // Use this for initialization
	void Start () {
        helicopteranimator = Helicopter.GetComponent<Animator>();
        HeliCollision = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	
    void OnTriggerEnter(Collider Other)
    {
            Debug.Log("Collision Detected");
            if (Other.gameObject.name == "Sam")
            {
            camera1.enabled = true;
            //camera2.enabled = false;
            Sam.SetActive(false);
            Dave.SetActive(false);
            helicopteranimator.SetTrigger("HelicopterTakeOff");
            }
        }


    
}
