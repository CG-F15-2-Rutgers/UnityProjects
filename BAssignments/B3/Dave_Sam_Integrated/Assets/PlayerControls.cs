using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class PlayerControls : MonoBehaviour {

    public GameObject Player;
    public bool ControlsEnabled;

	// Use this for initialization
	void Start () {
        ControlsEnabled = false;
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
    }

    public RunStatus EnableControls()
    {
        ControlsEnabled = true;
        return RunStatus.Success;
    }
}
