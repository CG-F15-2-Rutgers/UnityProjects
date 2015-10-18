using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject soldier;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 soldier_pos = soldier.transform.position;
        Vector3 camerapos = new Vector3(soldier_pos.x + (float)1.0, soldier_pos.y + (float)2.0, soldier_pos.z + (float)-3.0);
        this.transform.position = camerapos;
	}
}
