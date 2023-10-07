using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    bool pausestatus = false;

    //sound panel
    public float saveaudio;
    private bool stat = false;

    AudioSource Audio;
    Image Sbutton;
    public Image soundbar;
    public GameObject pausepanel;
    public GameObject soundpanel;
    public GameObject HYNpanel;

    // Start is called before the first frame update
    void Start()
    {

        GameObject sbu = GameObject.Find("UICanvas");
        Sbutton = sbu.transform.GetChild(12).GetChild(1).GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Audio = GameObject.Find("Sound").GetComponent<AudioSource>();
    }

    public void pausebtn()
    {
        if (pausestatus)
        {
            Time.timeScale = 1;
            pausestatus = false;
        }
        else
        {
            Time.timeScale = 0;
            pausestatus = true;
        }
    }

    public void mute()
    {
        if (stat == false)
        {
            saveaudio = Audio.volume;
            Audio.volume = 0;
            stat = true;
            Sbutton.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else if(stat == true)
        {
            Audio.volume = saveaudio;
            stat = false;
            Sbutton.color = new Color(1, 1, 1, 1);
        }

    }

    public void volumeup()
    {
        if(Audio.volume <= 0.2f)
            Audio.volume += 0.01f;
        soundbar.fillAmount += 0.05f;
    }

    public void volumedown()
    {
        Audio.volume -= 0.01f;
        soundbar.fillAmount -= 0.05f;
    }

    public void goback()
    {
        var panel = GameObject.Find("SoundPanel");
        panel.SetActive(false);
        pausepanel.SetActive(true);

    }

    public void openpausepanel()
    {
        if (pausepanel.activeSelf == false)
            pausepanel.SetActive(true);
        else
        {
            pausepanel.SetActive(false);
            soundpanel.SetActive(false);
        }
            
    }

    public void opensoundpanel()
    {
        soundpanel.SetActive(true);
    }

    public void savebtn()
    {
        //저장눌렀을시 저장됐다는 메세지 뜨게하기
    }

    public void homebtn()
    {
        HYNpanel.SetActive(true);
        openpausepanel();
    }

    public void homeyesbtn()
    {
        SceneManager.LoadScene("Save");
    }

    public void homenobtn()
    {
        HYNpanel.SetActive(false);
        openpausepanel();
    }
}
