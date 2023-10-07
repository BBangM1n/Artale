using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Select : MonoBehaviour
{
    public GameObject creat;
    public Text[] slotText;
    public Text NewName;

    bool[] savefile = new bool[3];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataMgr.instance.path + $"{i}"))
            {
                savefile[i] = true;
                DataMgr.instance.nowSlot = i;
                DataMgr.instance.LoadData();
                slotText[i].text = DataMgr.instance.nowPlayer.name;
                
            }
            else
            {
                slotText[i].text = "비어있는 슬롯";
            }
        }
        DataMgr.instance.DataClear();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slot(int number)
    {
        DataMgr.instance.nowSlot = number;

        if(savefile[number])
        {
            DataMgr.instance.LoadData();
            StartGame();
        }
        else
        {
            Creat();
        } 
    }

    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        if(!savefile[DataMgr.instance.nowSlot])
        {
            DataMgr.instance.nowPlayer.name = NewName.text;
            DataMgr.instance.SaveData();
        }
        SceneManager.LoadScene("World");
    }
}
