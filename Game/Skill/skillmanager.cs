using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class skillmanager : MonoBehaviour
{
    public int buttoncode;
    public string Sname = "";
    public string exam = "";
    public string mana = "";
    public string cooltime = "";
    private int Level;
    public string skillcode = "";
    public bool learn = false;
    public bool Qin, Win, Ein, Rin = false;

    PlayerStat playerstat;
    Skill skill;
    Text text;
    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        playerstat = GameObject.Find("Player").GetComponent<PlayerStat>();
        skill = GameObject.Find("GameManager").GetComponent<Skill>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Getbtn() // 배우기
    {
        GetButtonName();
        SkillDB();
        if (Level <= playerstat.Lv ) // 배우면 버튼 지우고 놓기 버튼으로 바꾸기 <
        {
            learn = true;
            Debug.Log("배움");
            GetButtonChange();
        }
        else
        {
            Debug.Log("못배움");
        }
    }

    public void Exambtn()
    {
        GetButtonName();
        SkillDB();
        GameObject SkillDscription = GameObject.Find("Skill");
        SkillDscription.transform.GetChild(2).gameObject.SetActive(true);
        SkillDscription.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = exam;
        SkillDscription.transform.GetChild(2).GetChild(4).GetComponent<Text>().text = "마나 소모 값 : " + mana + " 쿨타임 : " + cooltime + "초";
    }

    public void InBtn() // Q, W, E, R중 고르기
    {
        GetButtonName();
        SkillDB();
        //만약 Q(w, e, r)을 고를때 칸에 스킬이 이미 있나 없나 여부 확인 후 적용
        GameObject Skilln = GameObject.Find("Skill");// 버튼 판넬 활성화
        Skilln.transform.GetChild(4).gameObject.SetActive(true);
    }

    public void GetButtonName()
    {
        // 버튼 이름 가져오기
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;

        string code = ButtonName.Substring(6, 2);
        buttoncode = int.Parse(code);
    }

    public void GetButtonChange()
    {
        GameObject clickobject = EventSystem.current.currentSelectedGameObject;
        clickobject.SetActive(false);
    }

    public void SkillDB()
    {
       switch(buttoncode)
        {
            case 01: // 아이스 슬로우
                Debug.Log("아이스");
                Sname = "아이스";
                exam = "슬로우가 걸림";
                mana = "15";
                cooltime = "5";
                Level = 5;
                skillcode = "SK01";
                break;
            case 02: // 파이어 도트 초마다 도트딜
                Debug.Log("파이어");
                Sname = "파이어";
                exam = "도트 딜이 들어감";
                mana = "20";
                cooltime = "10";
                Level = 10;
                skillcode = "SK02";
                break;
        }
    }

    public void yesbtn()
    {
        skill.Skillcode = skillcode;
        if(Qin == true)
        {
            skill.Qcodein();
            Qin = false;
        }
        else if(Win == true)
        {
            skill.Wcodein();
            Win = false;
        }
        else if (Ein == true)
        {
            skill.Ecodein();
            Ein = false;
        }
        else if (Rin == true)
        {
            skill.Rcodein();
            Rin = false;
        }
        Debug.Log("예스");
    }

    public void nobtn()
    {
        Debug.Log("노");
    }

    public void Qskillin()
    {
        if (skill.Qskill == false)
        {
            Debug.Log(Sname + "이 적용 됐습니다");
            skill.Skillcode = skillcode;
            skill.Qcodein();
            skill.Qskill = true;
        }
        else if (skill.Qskill == true) //배운 다른 스킬 교체 여부 물어보기
        {
            Debug.Log("이미 적용된 칸입니다. 바꾸시겠습니까?"); // 예 아니오 버튼 누를 시 스킬 교체
            GameObject Skilln = GameObject.Find("Skill");// 버튼 판넬 활성화
            Skilln.transform.GetChild(3).gameObject.SetActive(true);
            Qin = true;
        }
    }

    public void Wskillin()
    {
        if (skill.Wskill == false)
        {
            Debug.Log(Sname + "이 적용 됐습니다");
            skill.Skillcode = skillcode;
            skill.Wcodein();
            skill.Wskill = true;
        }
        else if (skill.Wskill == true) //배운 다른 스킬 교체 여부 물어보기
        {
            Debug.Log("이미 적용된 칸입니다. 바꾸시겠습니까?"); // 예 아니오 버튼 누를 시 스킬 교체
            GameObject Skilln = GameObject.Find("Skill");// 버튼 판넬 활성화
            Skilln.transform.GetChild(3).gameObject.SetActive(true);
            Win = true;
        }
    }

}
