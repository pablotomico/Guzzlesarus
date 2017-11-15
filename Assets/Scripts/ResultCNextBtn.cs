﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCNextBtn : MonoBehaviour {
    public GameObject Background;
    public Image screen;
    public Transform Star, Coin, XP;

    bool isClick,i;
    float startTime, journeyLength;
    Vector2 startPos, endPos;
    // Use this for initialization
    void Start () {
        isClick = false;
        i = false;
        
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
        if (isClick)
        {
            if (!i)
            {
                Debug.Log(1);
                startTime = Time.time;
                journeyLength = Vector2.Distance(XP.position, Coin.position);
                i = true;
                startPos = new Vector2(Coin.position.x - journeyLength, Coin.position.y);
                endPos = new Vector2(startPos.x - journeyLength, startPos.y);
            }
            else
            {
                Debug.Log(2);
                float distCovered = (Time.time - startTime) * 100.0f;
                float fracJourney = distCovered / journeyLength;
                Background.GetComponent<Transform>().position = Vector2.Lerp(startPos, endPos, fracJourney);
            }
            if ((Vector2)Background.GetComponent<Transform>().position == endPos)
            {
                Debug.Log(3);
                isClick = false;
            }
        }
        
	}

    private void OnClick()
    {
        isClick = true;
    }

    private void Move(float distance)
    {
        
    }
}