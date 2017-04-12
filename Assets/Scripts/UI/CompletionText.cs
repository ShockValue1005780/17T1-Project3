﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletionText : MonoBehaviour
{

    public string TextFail;
    public string TextPass;
    public string TextPerfect;

    public Text text;

    void Start()
    {
        //Sets the colour of the background image based on current score
        if (GameDirector.LevelManager.CurrentLevel.orbsUsed <= GameDirector.LevelManager.CurrentLevel.perfectScore)
        {
            text.text = TextPerfect;
        }
        else if (GameDirector.LevelManager.CurrentLevel.orbsUsed <= GameDirector.LevelManager.CurrentLevel.passScore)
        {
            text.text = TextPass;
        }
        else
        {
            text.text = TextFail;
        }
    }
}
