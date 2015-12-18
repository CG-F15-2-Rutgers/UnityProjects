using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TreeSharpPlus;

public class TextBoxController : MonoBehaviour {

    public Text Name;
    public Text Body;
    public Button button;
    public Canvas Box;

    private int counter;
    private bool dialogFinished;

	// Use this for initialization
	void Start () {
        counter = 0;
        dialogFinished = false;
        Box.enabled = false;
        Name.enabled = false;
        Body.enabled = false;
        button.enabled = false;
        button.onClick.AddListener(delegate { buttonPressed(); });
    }
	
	// Update is called once per frame
	void Update () {
        switch (counter)
        {
            case 1:
                Dialog1();
                break;
            case 2:
                Dialog2();
                break;
            case 3:
                Dialog3();
                break;
            case 4:
                Dialog4();
                break;
            case 5:
                Dialog5();
                break;
            default:
                Name.enabled = false;
                Body.enabled = false;
                Box.enabled = false;
                button.enabled = false;
                break;

        }
	}

    public RunStatus startCounter()
    {
        counter = 1;
        return RunStatus.Success;
    }

    private void buttonPressed()
    {
        counter = counter + 1;
        dialogFinished = true;
    }

    public bool isDialogFinished()
    {
        return dialogFinished;
    }

    public RunStatus resetDialogFinished()
    {
        dialogFinished = false;
        return RunStatus.Success;
    }

    private void Dialog1()
    {
        Name.text = "Sam:";
        Body.text = "So our death penalty date is approaching fast...";

        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        button.enabled = true;
    }

    private void Dialog2()
    {
        Name.text = "Dave:";
        Body.text = "It does appear so... We've been here for so long. We've done our time and learned from our mistakes..."
            + " why don't they just let us go?";

        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        button.enabled = true;
    }

    private void Dialog3()
    {
        Name.text = "Sam:";
        Body.text = "Because that's the system here... Whoever comes in never leaves.";

        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        button.enabled = true;
    }

    private void Dialog4()
    {
        Name.text = "Dave:";
        Body.text = "In that case, we better make our escape or we're going to die.";

        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        button.enabled = true;
    }

    private void Dialog5()
    {
        Name.text = "Sam:";
        Body.text = "If that's the case, we better get moving. Let's see if we can find something to take out the guards with.";

        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        button.enabled = true;
    }
}
