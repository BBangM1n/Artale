using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    //플레이어 스탯 힘 방어 이동속도 공격속도 
    public int Lv;
    public float Exp;
    public float MaxExp;
    public int Statpoint;

    public float ST;
    public float DE;
    public float HP;
    public float MP;
    public Text STtext, DEtext, HPtext, MPtext;

    void Awake()
    {
        Lv = 1;
        Exp = 0;
        MaxExp = 10;
        Statpoint = 0;

        ST = 5;
        DE = 5;
        HP = 5;
        MP = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Exp += 3;
        }

        if(Exp >= MaxExp)
        {
            LvUp();
            MaxExp *= 1.5f;
            Exp = 0;
        }

        STtext.text = ST.ToString();
        DEtext.text = DE.ToString();
        HPtext.text = HP.ToString();
        MPtext.text = MP.ToString();
    }

    void LvUp()
    {
        Lv += 1;
        Statpoint += 5;
    }

    public void STUpBtn()
    {
        if(Statpoint > 0)
        {
            ST += 1;
            Statpoint -= 1;
        }
    }
    public void STDownBtn()
    {
        if (Lv > 1 && ST > 5)
        {
            ST -= 1;
            Statpoint += 1;
        }
    }

    public void DEUpBtn()
    {
        if (Statpoint > 0)
        {
            DE += 1;
            Statpoint -= 1;
        }
    }
    public void DEDownBtn()
    {
        if (Lv > 1 && DE > 5)
        {
            DE -= 1;
            Statpoint += 1;
        }
    }

    public void HPUpBtn()
    {
        if (Statpoint > 0)
        {
            HP += 1;
            Statpoint -= 1;
        }
    }
    public void HPDownBtn()
    {
        if (Lv > 1 && HP > 5)
        {
            HP -= 1;
            Statpoint += 1;
        }
    }

    public void MPUpBtn()
    {
        if (Statpoint > 0)
        {
            MP += 1;
            Statpoint -= 1;
        }
    }
    public void MPDownBtn()
    {
        if (Lv > 1 && MP > 5)
        {
            MP -= 1;
            Statpoint += 1;
        }
    }
}
