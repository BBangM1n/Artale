using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public string Skillcode = "";
    public string Qc, Wc, Ec, Rc = "";
    public GameObject[] skills;
    public Transform pos;
    Player player;

    public bool Qskill, Wskill, Eskill, Rskill = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (Qskill == false)
                Debug.Log("아직 스킬 적용 안됨");
            SkillDeployment(Qc);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Wskill == false)
                Debug.Log("아직 스킬 적용 안됨");
            SkillDeployment(Wc);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Eskill == false)
                Debug.Log("아직 스킬 적용 안됨");
            SkillDeployment(Ec);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Rskill == false)
                Debug.Log("아직 스킬 적용 안됨");
            SkillDeployment(Rc);
        }
    }

    void SkillDeployment(string skillcode)
    {
        switch (skillcode)
        {
            case "SK01":
                Instantiate(skills[0], pos.position, transform.rotation);
                player.playermp -= 15;
                break;
            case "SK02":
                Instantiate(skills[1], pos.position, transform.rotation);
                player.playermp -= 20;
                break;
        }

    }

    public void Qcodein()
    {
        Qc = Skillcode;
    }
    public void Wcodein()
    {
        Wc = Skillcode;
    }
    public void Ecodein()
    {
        Ec = Skillcode;
    }
    public void Rcodein()
    {
        Rc = Skillcode;
    }
}
