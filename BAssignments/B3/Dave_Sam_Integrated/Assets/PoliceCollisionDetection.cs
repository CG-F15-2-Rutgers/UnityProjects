using UnityEngine;
using System.Collections;

public class PoliceCollisionDetection : MonoBehaviour {

    private bool prisonerCollision;

	// Use this for initialization
	void Start () {
        prisonerCollision = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Prisoner"))
        {
            Debug.Log("Prisoner collided!");
            prisonerCollision = true;
        }
    }

    public bool hasPrisonerCollided()
    {
        return prisonerCollision;
    }
}
