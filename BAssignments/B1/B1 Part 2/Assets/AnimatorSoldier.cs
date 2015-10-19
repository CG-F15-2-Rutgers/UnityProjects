using UnityEngine;
using System.Collections;

public class AnimatorSoldier : MonoBehaviour {

    Animator anim;
    private int jumphash = Animator.StringToHash("jump");
    private float jump;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    float getSprint()
    {
        float sprint = 0;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("test");
            sprint = 1.0f;
        }
        return sprint;
    }
    float getJump() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = 1.0f;
        }
        else jump = 0.0f;
        return jump;
    }
	// Update is called once per frame
	void Update () {
        float ud_move = Input.GetAxis("Vertical");
        float lr_move = Input.GetAxis("Horizontal");
        float sprint = getSprint();
        float jump = getJump();
        anim.SetFloat("vertical_speed", ud_move);
        anim.SetFloat("Turn", lr_move);
        anim.SetFloat("Sprint", sprint);
        anim.SetFloat("Jump", jump);
	}
}
