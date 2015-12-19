using UnityEngine;
using System.Collections;

public class ScannerCollision : MonoBehaviour {

    private bool scannerCollision;

	// Use this for initialization
	void Start () {
        scannerCollision = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandCollider"))
        {
            Debug.Log("Scanner unlock!");
            scannerCollision = true;
        }
    }

    public bool hasScannerCollided()
    {
        return scannerCollision;
    }
}
