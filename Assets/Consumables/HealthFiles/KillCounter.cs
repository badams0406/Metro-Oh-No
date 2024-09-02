using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    public Text scoreText;
    public static int scoreCount;

    private void Start()
    {
        scoreCount = 0;
    }


    private void Update()
    {
        scoreText.text = "" + Mathf.Round(scoreCount);
    }

    public void addKill()
    {
        scoreCount++;
    }

}
