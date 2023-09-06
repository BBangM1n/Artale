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

    public void Getbtn() // ����
    {
        GetButtonName();
        SkillDB();
        if (Level <= playerstat.Lv ) // ���� ��ư ����� ���� ��ư���� �ٲٱ� <
        {
            learn = true;
            Debug.Log("���");
            GetButtonChange();
        }
        else
        {
            Debug.Log("�����");
        }
    }

    public void Exambtn()
    {
        GetButtonName();
        SkillDB();
        GameObject SkillDscription = GameObject.Find("Skill");
        SkillDscription.transform.GetChild(2).gameObject.SetActive(true);
        SkillDscription.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = exam;
        SkillDscription.transform.GetChild(2).GetChild(4).GetComponent<Text>().text = "���� �Ҹ� �� : " + mana + " ��Ÿ�� : " + cooltime + "��";
    }

    public void InBtn() // Q, W, E, R�� ����
    {
        GetButtonName();
        SkillDB();
        //���� Q(w, e, r)�� ���� ĭ�� ��ų�� �̹� �ֳ� ���� ���� Ȯ�� �� ����
        GameObject Skilln = GameObject.Find("Skill");// ��ư �ǳ� Ȱ��ȭ
        Skilln.transform.GetChild(4).gameObject.SetActive(true);
    }

    public void GetButtonName()
    {
        // ��ư �̸� ��������
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
            case 01: // ���̽� ���ο�
                Debug.Log("���̽�");
                Sname = "���̽�";
                exam = "���ο찡 �ɸ�";
                mana = "15";
                cooltime = "5";
                Level = 5;
                skillcode = "SK01";
                break;
            case 02: // ���̾� ��Ʈ �ʸ��� ��Ʈ��
                Debug.Log("���̾�");
                Sname = "���̾�";
                exam = "��Ʈ ���� ��";
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
        Debug.Log("����");
    }

    public void nobtn()
    {
        Debug.Log("��");
    }

    public void Qskillin()
    {
        if (skill.Qskill == false)
        {
            Debug.Log(Sname + "�� ���� �ƽ��ϴ�");
            skill.Skillcode = skillcode;
            skill.Qcodein();
            skill.Qskill = true;
        }
        else if (skill.Qskill == true) //��� �ٸ� ��ų ��ü ���� �����
        {
            Debug.Log("�̹� ����� ĭ�Դϴ�. �ٲٽðڽ��ϱ�?"); // �� �ƴϿ� ��ư ���� �� ��ų ��ü
            GameObject Skilln = GameObject.Find("Skill");// ��ư �ǳ� Ȱ��ȭ
            Skilln.transform.GetChild(3).gameObject.SetActive(true);
            Qin = true;
        }
    }

    public void Wskillin()
    {
        if (skill.Wskill == false)
        {
            Debug.Log(Sname + "�� ���� �ƽ��ϴ�");
            skill.Skillcode = skillcode;
            skill.Wcodein();
            skill.Wskill = true;
        }
        else if (skill.Wskill == true) //��� �ٸ� ��ų ��ü ���� �����
        {
            Debug.Log("�̹� ����� ĭ�Դϴ�. �ٲٽðڽ��ϱ�?"); // �� �ƴϿ� ��ư ���� �� ��ų ��ü
            GameObject Skilln = GameObject.Find("Skill");// ��ư �ǳ� Ȱ��ȭ
            Skilln.transform.GetChild(3).gameObject.SetActive(true);
            Win = true;
        }
    }

}
