using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class TreasureBoxMovement : MonoBehaviour {

    public GameObject TreasureBox;

    private bool timeToMove;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (timeToMove)
        {
            TreasureBox.transform.position += Vector3.right * 3.86f;
            timeToMove = false;
        }
	}

    public RunStatus makeTreasureMove()
    {
        timeToMove = true;
        return RunStatus.Success;
    }
}
