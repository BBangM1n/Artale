using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPbar : MonoBehaviour
{
    public Enemy01 enemy01;
    public Enemy02 enemy02;
    public Enemy03 enemy03;
    public Enemy03mini enemy03mini;

    public GameObject objet;
    public float Maxhp;
    public float Nowhp;
    public Image hpimage;
    public Vector3 Offset;
    void Start()
    {

    }

    void Update()
    {
        enemycode();
        objet.gameObject.SetActive(Nowhp < Maxhp);
        HandleHP();

        objet.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }

    private void HandleHP()
    {
        hpimage.fillAmount = Mathf.Lerp(hpimage.fillAmount, (float)Nowhp / (float)Maxhp, Time.deltaTime * 10);
    }

    void enemycode()
    {
        if (enemy01 != null)
        {
            Maxhp = enemy01.MaxHp;
            Nowhp = enemy01.NowHp;
        }
        else if(enemy02 != null)
        {
            Maxhp = enemy02.MaxHp;
            Nowhp = enemy02.NowHp;
        }
        else if (enemy03 != null)
        {
            Maxhp = enemy03.MaxHp;
            Nowhp = enemy03.NowHp;
        }
        else if (enemy03mini != null)
        {
            Maxhp = enemy03mini.MaxHp;
            Nowhp = enemy03mini.NowHp;
        }
    }
}
