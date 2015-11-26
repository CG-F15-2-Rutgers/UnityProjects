using UnityEngine;
using System.Collections;

public class Win_Script : MonoBehaviour {

    private bool win = false;
    private CharacterMecanim cm;
    public Canvas winMsg;

	// Use this for initialization
	void Start () {
        cm = GetComponent<CharacterMecanim>();
        winMsg.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(cm.win_count >= 7)
        {
            Invoke("StopProgram", 5F);
        }
        
	}

    void StopProgram()
    {
        Time.timeScale = 0.0f;
        winMsg.enabled = true;
    }
}
