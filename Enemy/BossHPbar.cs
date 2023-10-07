using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPbar : MonoBehaviour
{
    public Boss01 Boss1;
    public Boss2 Boss2;
    public Image hpimage;
    public Text hptext;
    public float MaxHp;
    public float NowHp;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        enemycode();
        HandleHP();
        hptext.text = string.Format("HP {0}/{1}", NowHp, MaxHp);

        if (NowHp <= 0)
        {
            hptext.text = string.Format("HP {0}/{1}", 0, MaxHp);
            Invoke("Clear", 2);
        }
    }

    private void HandleHP()
    {
        hpimage.fillAmount = Mathf.Lerp(hpimage.fillAmount, (float)NowHp / (float)MaxHp, Time.deltaTime * 10);
    }

    void Clear()
    {
        var ser = GameObject.Find("BossHPbar");
        ser.SetActive(false);
    }

    void enemycode()
    {
        if (Boss1 != null)
        {
            MaxHp = Boss1.MaxHp;
            NowHp = Boss1.NowHp;
        }
        else if (Boss2 != null)
        {
            MaxHp = Boss2.MaxHp;
            NowHp = Boss2.NowHp;
        }
    }
}
