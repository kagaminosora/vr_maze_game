using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameRecords : MonoBehaviour {
    private string path;

    private bool guide = false; //play the game with a guide - true; without a guide - false;
    private bool firstPersonView = false;  //first-person view - true; third-person view - false;

    private string gameDuration;
    private bool gameOver = false;

    private float startTime;
    private float lastRecordTime;
    private string positionRecords;
    private int count; //count the number of position records


    public Text winText;
    public Text timer;
    public GameObject gameOverCanvas;
    public GameObject player;

    void Start()
    {
        path = Directory.GetCurrentDirectory();
        path += @"\Records\records.txt";
        startTime = Time.time;
        timer.text = "Timer: 0s";
        winText.text = "";
        Debug.Log(path);
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

        while(Time.time - lastRecordTime > 2)
        {
            Vector3 currentPlayerPosition = player.transform.position;
            positionRecords += "("+ System.String.Format("{0:N2}", currentPlayerPosition.x) + ", " + System.String.Format("{0:N2}", currentPlayerPosition.y) + ") -> ";
            if (count % 10 == 0)
            {
                positionRecords += "\n";
            }
            count++;
            lastRecordTime = Time.time;
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
        gameOverCanvas.SetActive(true);

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

        using (StreamWriter sw = new StreamWriter(path, true))
        {
            sw.WriteLine(gameRecord);
            sw.WriteLine(positionRecords);
        }
    }
}
