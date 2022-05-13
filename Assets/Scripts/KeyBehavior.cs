using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBehavior : MonoBehaviour
{
    private Text thisText;
    private static int score;

    void Start()
    {
        thisText = GetComponent<Text>();

        // set score value to be zero
        score = 0;
    }

    void Update()
    {
        // update text of Text element
        thisText.text = "Keys (" + score + "/3)";
    }

    public static void AddScore()
    {
        // add 500 points to score
        score += 1;
    }

    public static int GetScore()
    {
        return score;
    }
}
