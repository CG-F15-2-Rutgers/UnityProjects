using UnityEngine;
using System.Collections;

public class LightsScript : MonoBehaviour {
    public GameObject FrontLights;
    public GameObject BackLights;
	// Use this for initialization
	void Start () {
        //TurnLightsOff();
      InvokeRepeating("TurnLightsOff", 1,2);

	}
	
	// Update is called once per frame
	void Update () {

        
       
    }
    

    void TurnLightsOff()
    {
        Debug.Log("LIGHTS OFF ");
        FrontLights.SetActive(false);
        BackLights.SetActive(false);
        Invoke("TurnLightsOn", 1);
    }

    void TurnLightsOn()
    {
        Debug.Log("LIGHTS ON");
        FrontLights.SetActive(true);
        BackLights.SetActive(true);
    }

}
