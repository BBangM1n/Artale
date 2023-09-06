using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public Player player;
    public PlayerStat playerstat;
    public float MaxHp;
    public float NowHp;
    public Image hpimage;
    public Text hptext;

    public float MaxMp;
    public float NowMp;
    public Image mpimage;
    public Text mptext;

    public float MaxExp;
    public float NowExp;
    public Image expimage;
    public Text exptext;

    public Text Lvtext;
    public Text Lvpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MaxHp = player.playerMaxhp;
        NowHp = player.playerhp;
        HandleHP();
        hptext.text = string.Format("HP {0}/{1}", NowHp, MaxHp);

        MaxMp = player.playerMaxmp;
        NowMp = player.playermp;
        HandleMP();
        mptext.text = string.Format("MP {0}/{1}", NowMp, MaxMp);

        NowExp = playerstat.Exp;
        HandleEXP();
        exptext.text = string.Format("EXP {0}/{1}", NowExp,playerstat.MaxExp);


        Lvtext.text = "Lv : " + playerstat.Lv.ToString();
        Lvpoint.text = playerstat.Statpoint.ToString();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Transform StatP = GameObject.Find("Stat").transform.GetChild(0);
            StatP.gameObject.SetActive(true);
            Transform StatBtn = GameObject.Find("Stat").transform.GetChild(1);
            StatBtn.gameObject.SetActive(true);
            Transform Statpoint = GameObject.Find("Stat").transform.GetChild(2);
            Statpoint.gameObject.SetActive(true);
            
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Transform SkillP = GameObject.Find("Skill").transform.GetChild(0);
            SkillP.gameObject.SetActive(true);
            Transform SkillBtn = GameObject.Find("Skill").transform.GetChild(1);
            SkillBtn.gameObject.SetActive(true);
        }


    }

    private void HandleHP()
    {
        hpimage.fillAmount = Mathf.Lerp(hpimage.fillAmount, (float)NowHp / (float)MaxHp, Time.deltaTime * 10);
    }

    private void HandleMP()
    {
        mpimage.fillAmount = Mathf.Lerp(mpimage.fillAmount, (float)NowMp / (float)MaxMp, Time.deltaTime * 10);
    }

    private void HandleEXP()
    {
        expimage.fillAmount = Mathf.Lerp(expimage.fillAmount, (float)NowExp / (float)playerstat.MaxExp, Time.deltaTime * 5);
    }

}
