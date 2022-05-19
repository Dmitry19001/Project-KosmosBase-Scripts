using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePerSecond : MonoBehaviour
{
    public TMP_Text scoreText;
    public float scoreAmount = 0;
    public float pointsPerSeconds = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.lose)
        {
            scoreAmount += pointsPerSeconds * Time.deltaTime; 
            scoreText.text = $"Score: {(int)scoreAmount}";
        }
    }
}


public static class player 
{
    public static bool lose = false;
}