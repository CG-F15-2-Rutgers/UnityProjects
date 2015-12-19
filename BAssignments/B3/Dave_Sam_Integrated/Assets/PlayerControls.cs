﻿using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class PlayerControls : MonoBehaviour {

    public GameObject Player;
    public bool ControlsEnabled;

    private bool treasureClicked;
    private bool posterClicked;

	// Use this for initialization
	void Start () {
        ControlsEnabled = false;
        treasureClicked = false;
        posterClicked = false;
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 vector;

        if ((Input.GetButtonDown("Fire2") || Input.GetButton("Fire2")) && ControlsEnabled)
        {
            if (Physics.Raycast(ray, out hit) == true)
            {
                if (hit.transform.gameObject.CompareTag("Ground"))
                {
                    vector = hit.point;
                    Player.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                }
            }
        }

        if ((Input.GetButtonDown("Fire1") || Input.GetButton("Fire1")) && ControlsEnabled)
        {
            if (Physics.Raycast(ray, out hit) == true)
            {
                if (hit.transform.gameObject.CompareTag("Treasure"))
                {
                    Debug.Log("Treasure chest clicked!");
                    treasureClicked = true;
                }
                if (hit.transform.gameObject.CompareTag("Poster"))
                {
                    Debug.Log("Poster clicked!");
                    posterClicked = true;
                }
            }
        }

    }

    public RunStatus EnableControls()
    {
        ControlsEnabled = true;
        return RunStatus.Success;
    }

    public bool isTreasureClicked()
    {
        return treasureClicked;
    }

    public bool isPosterClicked()
    {
        return posterClicked;
    }
}