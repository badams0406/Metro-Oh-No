using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntitodeScore : MonoBehaviour
{
    public Text scoreText;
    public static int antitodesFound;

    private void Start()
    {
        antitodesFound = 0;
    }

    private void Update()
    {
        scoreText.text = "" + Mathf.Round(antitodesFound);
    }

    public void CollectPotion()
    {
        antitodesFound++;
    }
    public int GetScore()
    {
        return antitodesFound;
    }
}
