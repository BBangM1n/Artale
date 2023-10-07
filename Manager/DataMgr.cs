using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // ¿Œ«≤ æ∆øÙ«≤ 


public class PlayerData
{
    //∑π∫ß, ƒ⁄¿Œ, hp, mp, »˚, πÊæÓ
    public string name;
    public int level = 1;
    public int coin = 0;
    public float Maxhp = 100;
    public float Maxmp = 100;
    public int str = 5;
    public int dfc = 2;
    public float exp = 0;
    public float Maxexp = 10;
    public int statpoint = 0;

    public bool skill = false;
    public bool skill2 = false;
}
public class DataMgr : MonoBehaviour
{
    // ΩÃ±€≈Ê
    public static DataMgr instance;

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/"; // print∑Œ »Æ¿Œ∞°¥… 
    }
    // Start is called before the first frame update
    void Start()
    {
        print(path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + nowSlot.ToString(), data);  // ∞Ê∑Œ, ¿˙¿Â«“ µ•¿Ã≈Õ
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
