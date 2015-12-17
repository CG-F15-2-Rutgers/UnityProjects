using UnityEngine;
using System.Collections;

public class RButtonPressed : MonoBehaviour {

    public bool rPressed = false;
    private int count;

	// Use this for initialization
	void Start () {
        rPressed = false;
        count = 0;
	}

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.R))
        {
            rPressed = true;
        }
    }
}
