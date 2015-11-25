using UnityEngine;
using System.Collections;

public class DoorMotion : MonoBehaviour {

    bool doorOpen;
    bool doorClose;
    bool doorIdle;
    public GameObject MovingDoor;
    public GameObject buttonDoor1Border;
    public CharacterMecanim cm;
    GameObject COP;
    NavMeshAgent agent;
    Animator animator;

    // Use this for initialization
    void Start () {
        doorOpen = false;
        animator = MovingDoor.GetComponent<Animator>();

        GameObject[] cops = GameObject.FindGameObjectsWithTag("Cop");

        foreach (GameObject cop in cops)
        {
            COP = cop;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 60, Color.cyan);


        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(ray, out hit) == true)
            {
                Vector3 characterPosition = hit.transform.gameObject.transform.position;
                Vector3 copPosition = COP.transform.position;
                if (hit.transform.gameObject.CompareTag("ButtonDoor1") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 3)
                {

                    buttonDoor1Border.SetActive(true);
                    Invoke("DontShowButton", 1);
                    string name = hit.collider.gameObject.name;
                    /*
                    if (doorOpen == false)
                    {
                        Debug.Log("Door Opened");
                        doorOpen = true;
                        Doors("Open");
                    }
                    else if (doorOpen == true)
                    {
                        Debug.Log("Door Closed");
                        doorOpen = false;
                        doorClose = true;
                        Doors("Close");
                    }
                    */

                }
            }
        }
    }

    void Doors(string direction)
    {
        animator.SetTrigger(direction);
    }

    void DontShowButton()
    {
        buttonDoor1Border.SetActive(false);
    }
}
