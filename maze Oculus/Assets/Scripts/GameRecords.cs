using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameRecords : MonoBehaviour {
    private bool guide = false; //play the game with a guide - true; without a guide - false;
    private bool firstPersonView = false;  //first-person view - true; third-person view - false;

    private string gameDuration;
    private bool gameOver = false;

    private float startTime;
    public Text winText;
    public Text timer;

    void Start()
    {
        startTime = Time.time;
        timer.text = "Timer: 0s";
        winText.text = "";
    }

    void FixedUpdate()
    {
        if (winText.text.Equals(""))
        {
            gameDuration = System.String.Format("{0:N2}", (Time.time - startTime));
            timer.text = "Timer: " + gameDuration + "s";
        }
        else
        {
            if (!gameOver)
            {
                GameOver();
                gameOver = true;
            }
        }
    }

    //when the guide sends a message, set guide true
    private void ThereIsGuide()
    {
        guide = true;
    }

    //when the local avatar sends a message, set firstPersonView true
    private void IsFirstPersonView()
    {
        firstPersonView = true;
    }

    private void GameOver()
    {
        string gameRecord = "";
        if (firstPersonView)
        {
            gameRecord += "View: First-person, ";
        }
        else
        {
            gameRecord += "View: Third-person, ";
        }

        if (guide)
        {
            gameRecord += "Guide: On, ";
        }
        else
        {
            gameRecord += "Guide: Off, ";
        }

        gameRecord += "Time: "+ gameDuration+"s;";

        using (StreamWriter sw = new StreamWriter(@"D:\maze\first-person\Records\records.txt", true))
        {
            sw.WriteLine(gameRecord);
        }

    }
}
