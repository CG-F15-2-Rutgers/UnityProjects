using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class PlayerControls : MonoBehaviour {

    public GameObject Player;
    public bool ControlsEnabled;

    private bool treasureClicked;
    private bool posterClicked;
    private bool isCopClicked;
    private GameObject copClicked;

	// Use this for initialization
	void Start () {
        copClicked = Player;
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
                    Debug.Log("Raycast hit ground!");
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
                if (hit.transform.gameObject.CompareTag("Cop"))
                {
                    Debug.Log(hit.transform.gameObject.name+" clicked!");
                    copClicked = hit.transform.gameObject;
                    this.isCopClicked = true;
                }
            }
        }

    }

    public RunStatus EnableControls()
    {
        ControlsEnabled = true;
        return RunStatus.Success;
    }

    public RunStatus DisableControls()
    {
        ControlsEnabled = false;
        Player.GetComponent<NavMeshAgent>().Stop();
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

    public GameObject whichCopClicked()
    {
        return copClicked;
    }

    public bool isCopClickedFunc()
    {
        return isCopClicked;
    }

    public RunStatus resetCopClicked()
    {
        isCopClicked = false;
        return RunStatus.Success;
    }
}
