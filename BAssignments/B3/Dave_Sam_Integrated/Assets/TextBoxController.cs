﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TreeSharpPlus;

public class TextBoxController : MonoBehaviour {

    public Text Name;
    public Text Body;
    public Button button;
    public Canvas Box;

    private int counter;
    public bool dialogFinished;
    public bool gameOver;

	// Use this for initialization
	void Start () {
        counter = 0;
        gameOver = false;
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
        Name.enabled = false;
        Body.enabled = false;
        Box.enabled = false;
        button.enabled = false;

        if (gameOver)
        {
            Application.LoadLevel(0);
        }
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

    void buttonEnabled()
    {
        button.enabled = true;
    }

    private void Dialog1()
    {
        Name.text = "Sam:";
        Body.text = "So our death penalty date is approaching fast...";

        button.enabled = false;
        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        Invoke("buttonEnabled",2);
    }

    private void Dialog2()
    {
        Name.text = "Dave:";
        Body.text = "It does appear so... We've been here for so long. We've done our time and learned from our mistakes..."
            + " why don't they just let us go?";

        button.enabled = false;
        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        Invoke("buttonEnabled", 2);
    }

    private void Dialog3()
    {
        Name.text = "Sam:";
        Body.text = "Because that's the system here... Whoever comes in never leaves.";

        button.enabled = false;
        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        Invoke("buttonEnabled", 2);
    }

    private void Dialog4()
    {
        Name.text = "Dave:";
        Body.text = "In that case, we better make our escape or we're going to die.";

        button.enabled = false;
        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        Invoke("buttonEnabled", 2);
    }

    private void Dialog5()
    {
        Name.text = "Sam:";
        Body.text = "If that's the case, we better get moving. Let's see if we can find something to take out the guards with.";

        button.enabled = false;
        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        Invoke("buttonEnabled", 2);
    }

    public RunStatus TreasureDialog()
    {
        Name.text = "Sam:";
        Body.text = "Hmm... I wonder what's behind this Treasure Chest...";

        button.enabled = false;
        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        Invoke("buttonEnabled", 2);

        return RunStatus.Success;
    }

    public RunStatus TreasureDialogComplete()
    {
        Name.text = "Sam:";
        Body.text = "Sweet! A secret passageway!";

        button.enabled = false;
        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        Invoke("buttonEnabled", 2);

        return RunStatus.Success;
    }

    public RunStatus PosterDialog()
    {
        Name.text = "Sam:";
        Body.text = "Hmm... I wonder what's behind this Poster...";

        button.enabled = false;
        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        Invoke("buttonEnabled", 2);

        return RunStatus.Success;
    }

    public RunStatus PosterDialogComplete()
    {
        Name.text = "Sam:";
        Body.text = "Sweet! A sword! I can take out the guards with this!";

        button.enabled = false;
        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        Invoke("buttonEnabled", 2);

        return RunStatus.Success;
    }

    public RunStatus CaughtDialog()
    {
        Name.text = "Cop:";
        Body.text = "Stop right there! You're going back to your cell!";

        button.enabled = false;
        button.GetComponentInChildren<Text>().text = "Press to restart";
        gameOver = true;
        Name.enabled = true;
        Body.enabled = true;
        Box.enabled = true;
        Invoke("buttonEnabled", 2);

        return RunStatus.Success;
    }
}
