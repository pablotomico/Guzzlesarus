﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnOnClick : Clickable {
    public Image Screen, Screen1, Screen2, Screen3, Screen4;
    public SpriteRenderer spriteRenderer;
    bool isClick;
    bool playingAnimation;


    // Use this for initialization
    public override void OnStart() {
        Button btn = this.GetComponent<Button>();
        if (btn != null) {
            btn.onClick.AddListener(OnClick);
        }
        isClick = false;
    }

    // Update is called once per frame
    void Update() {
        
        if (isClick) {
            switch (this.name) {
                case "StartBtn":
                    ScreenFadeOut("GuzzWorldScreen");
                    break;
                case "Farm":
                    // AkSoundEngine.PostEvent("Chicken", gameObject);
                    ScreenFadeOut("FarmScreen");
                    break;
                case "Cave":
                    Data data = SaveNLoadTxt.Load();
                    //AkSoundEngine.PostEvent("Door_Sound", gameObject);
                    if (data.tutstate == 3)
                        data.tutstate++;
                    if (!playingAnimation) {
                        playingAnimation = true;
                        GetComponentInChildren<Animator>().Play("ws_caveTap");
                    }
                    ScreenFadeOut("CaveScreen");
                    SaveNLoadTxt.Save(data);
                    break;
                case "Mailbox":
                    //AkSoundEngine.PostEvent("Click_Postbox", gameObject);
                    ScreenFadeOut("MailBoxScreen");
                    //data.unread = false;
                    //if (data.tutstate == 1)
                    //    data.tutstate++;

                    
                    break;
                case "PosterExit":

                    ScreenFadeOut("GuzzWorldScreen");
                    break;
                case "Mini1":
                    if (Screen1.color.r >= 0) {
                        Screen1.color = new Color(Screen1.color.r - 0.02f, Screen1.color.g - 0.02f, Screen1.color.b - 0.02f, Screen1.color.a);
                    }
                    ScreenFadeOut("MixingDryIngredients");
                    break;

                case "Back":
                    ScreenFadeOut("GuzzWorldScreen");
                    break;
                case "ButtonPancake":
                    Screen.gameObject.SetActive(true);
                    //AkSoundEngine.PostEvent("Click_Positive", gameObject);
                    break;
                case "BackToCave":
                    ScreenFadeOut("CaveScreen");
                    //AkSoundEngine.PostEvent("Click_Negative", gameObject);
                    break;
                case "XNext":
                    ScreenFadeOut("GuzzWorldScreen");
                    break;
                case "Table":
                    JarBtnClick.isClick1 = true;
                    Debug.Log("1");
                    isClick = false;
                    break;
                case "Oven":
                    JarBtnClick.isClickk = true;

                    isClick = false;
                    break;
                default:
                    isClick = false;
                    break;
            }
        }
    }

    public override void OnClick() {
        isClick = true;
        switch (this.name) {
            case "StartBtn":
                Debug.Log("Start Tapped");
                GetComponentInChildren<Animator>().Play("ss_StartTap");
                GetComponent<ButtonSound>().PlaySound();
                AkSoundEngine.SetRTPCValue("Menu_Music", 0f, GameObject.FindGameObjectWithTag("MainCamera"), 150);
                AkSoundEngine.PostEvent("Stop_Menu_Music", gameObject);
                break;
            case "Farm":
                //AkSoundEngine.PostEvent("Chicken", gameObject);
                break;
            case "Cave":
                GetComponentInChildren<Animator>().Play("ws_caveTap");
                AkSoundEngine.PostEvent("Door_Sound", gameObject);
                break;
            case "Mailbox":
                AkSoundEngine.PostEvent("Click_Postbox", gameObject);
                Data data = SaveNLoadTxt.Load();
                if (data.unreadMail) {
                    GameObject mailbox = GameObject.FindGameObjectWithTag("MailboxOpen");
                    mailbox.GetComponent<Animator>().Play("ws_mbOpenTap");
                    data.unreadMail = false;
                    SaveNLoadTxt.Save(data);
                }
                break;
        }
    }

    void ScreenFadeOut(string scene) {
        FindObjectOfType<ScreenFadeIn>().enabled = false;
        if (Screen != null) {

            Screen.gameObject.SetActive(true);
            if (Screen.color.a <= 1.0f) {
                Screen.color = new Color(Screen.color.r, Screen.color.g, Screen.color.b, Screen.color.a + 0.02f);
            } else {
                isClick = false;
                //Screen.gameObject.SetActive(false);

                SceneManager.LoadScene(scene);
            }
        } else if (spriteRenderer != null) {
            spriteRenderer.gameObject.SetActive(true);
            if (spriteRenderer.color.a <= 1.0f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + 0.02f);
            } else {
                isClick = false;
                //Screen.gameObject.SetActive(false);
                Debug.Log(scene);
                SceneManager.LoadScene(scene);
            }
        }
    }
}
