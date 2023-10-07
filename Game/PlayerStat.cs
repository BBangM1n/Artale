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
    private float Lexp;

    public float ST;
    public float DE;
    public float HP;
    public float MP;
    public Text STtext, DEtext, HPtext, MPtext;
    Player player;

    void Awake()
    {
        ST = 5;
        DE = 5;
        HP = 5;
        MP = 5;
    }

    private void Start()
    {
        player = GetComponent<Player>();
        Lv = DataMgr.instance.nowPlayer.level;
        Exp = DataMgr.instance.nowPlayer.exp;
        MaxExp = DataMgr.instance.nowPlayer.Maxexp;
        Statpoint = DataMgr.instance.nowPlayer.statpoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Exp += 3;
            DataMgr.instance.nowPlayer.exp += 3;
        }

        if(Exp >= MaxExp)
        {
            Lexp = Exp - MaxExp;
            LvUp();
            MaxExp *= 1.5f;
            DataMgr.instance.nowPlayer.Maxexp *= 1.5f;
            Exp = 0;
            DataMgr.instance.nowPlayer.exp = 0;
            Exp += Lexp;
            DataMgr.instance.nowPlayer.exp += Lexp;
        }

        STtext.text = ST.ToString();
        DEtext.text = DE.ToString();
        HPtext.text = HP.ToString();
        MPtext.text = MP.ToString();
    }

    void LvUp()
    {
        DataMgr.instance.nowPlayer.level += 1;
        Lv += 1;
        DataMgr.instance.nowPlayer.statpoint += 5;
        Statpoint += 5;
    }

    public void STUpBtn()
    {
        if(Statpoint > 0)
        {
            player.Dmg += 1;
            DataMgr.instance.nowPlayer.str += 1;
            ST += 1;
            Statpoint -= 1;
            DataMgr.instance.nowPlayer.statpoint -= 1;
        }
    }
    public void STDownBtn()
    {
        if (Lv > 1 && ST > 5)
        {
            player.Dmg -= 1;
            DataMgr.instance.nowPlayer.str -= 1;
            ST -= 1;
            Statpoint += 1;
            DataMgr.instance.nowPlayer.statpoint += 1;
        }
    }

    public void DEUpBtn()
    {
        if (Statpoint > 0)
        {
            player.Defence += 1;
            DataMgr.instance.nowPlayer.dfc += 1;
            DE += 1;
            Statpoint -= 1;
            DataMgr.instance.nowPlayer.statpoint -= 1;
        }
    }
    public void DEDownBtn()
    {
        if (Lv > 1 && DE > 5)
        {
            player.Defence -= 1;
            DataMgr.instance.nowPlayer.dfc -= 1;
            DE -= 1;
            Statpoint += 1;
            DataMgr.instance.nowPlayer.statpoint += 1;
        }
    }

    public void HPUpBtn()
    {
        if (Statpoint > 0)
        {
            player.playerMaxhp += 1;
            DataMgr.instance.nowPlayer.Maxhp += 1;
            HP += 1;
            Statpoint -= 1;
            DataMgr.instance.nowPlayer.statpoint -= 1;
        }
    }
    public void HPDownBtn()
    {
        if (Lv > 1 && HP > 5)
        {
            player.playerMaxhp -= 1;
            DataMgr.instance.nowPlayer.Maxhp -= 1;
            HP -= 1;
            Statpoint += 1;
            DataMgr.instance.nowPlayer.statpoint += 1;
        }
    }

    public void MPUpBtn()
    {
        if (Statpoint > 0)
        {
            player.playerMaxmp += 1;
            DataMgr.instance.nowPlayer.Maxmp += 1;
            MP += 1;
            Statpoint -= 1;
            DataMgr.instance.nowPlayer.statpoint -= 1;
        }
    }
    public void MPDownBtn()
    {
        if (Lv > 1 && MP > 5)
        {
            player.playerMaxmp -= 1;
            DataMgr.instance.nowPlayer.Maxmp -= 1;
            MP -= 1;
            Statpoint += 1;
            DataMgr.instance.nowPlayer.statpoint += 1;
        }
    }
}
