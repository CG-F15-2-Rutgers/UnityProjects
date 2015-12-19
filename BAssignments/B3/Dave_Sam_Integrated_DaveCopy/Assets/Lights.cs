using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour {

    public GameObject FrontLights;
    public GameObject BackLights;
    // Use this for initialization
    void Start()
    {
        //TurnLightsOff();
        InvokeRepeating("TurnLightsOff", 1, 2);

    }

    void TurnLightsOff()
    {
        FrontLights.SetActive(false);
        BackLights.SetActive(false);
        Invoke("TurnLightsOn", 1);
    }

    void TurnLightsOn()
    {
        FrontLights.SetActive(true);
        BackLights.SetActive(true);
    }
}
