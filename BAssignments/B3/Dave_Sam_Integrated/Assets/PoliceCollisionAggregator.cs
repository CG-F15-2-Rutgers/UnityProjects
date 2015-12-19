using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoliceCollisionAggregator : MonoBehaviour {

    private List<PoliceCollisionDetection> PCDArray;
    public PoliceCollisionDetection Cop1;
    public PoliceCollisionDetection Cop2;
    public PoliceCollisionDetection Cop3;
    public PoliceCollisionDetection Cop4;
    public PoliceCollisionDetection Cop5;
    public PoliceCollisionDetection Cop6;

    private bool prisonerCollided;
    // Use this for initialization
    void Start () {
        PCDArray = new List<PoliceCollisionDetection>();
        PCDArray.Add(Cop1);
        PCDArray.Add(Cop2);
        PCDArray.Add(Cop3);
        PCDArray.Add(Cop4);
        PCDArray.Add(Cop5);
        PCDArray.Add(Cop6);
        prisonerCollided = false;
    }
	
	// Update is called once per frame
	void Update () {
	    for(int i = 0;i < PCDArray.Count; i++)
        {
            if(PCDArray[i].hasPrisonerCollided() == true)
            {
                Debug.Log("Prisoner collision detected in aggregator!");
                prisonerCollided = true;
            }
        }
	}

    public bool hasPrisonerCollided()
    {
        return prisonerCollided;
    }
}
