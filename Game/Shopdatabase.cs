using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopdatabase : MonoBehaviour
{
    public Text item_name;
    public Text item_money;
    public Sprite[] chage_img;

    int icode;
    
    GameMgr Gmgr;
    Player player;
    Image thisImg;
    PlayerStat stat;
    Image BufImg;

    string iname = "";
    int money = 0;
    string exam = "";
    int image_num = 0;


    // Start is called before the first frame update
    void Start()
    {
        Gmgr = GameObject.Find("GameManager").GetComponent<GameMgr>();
        player = GameObject.Find("Player").GetComponent<Player>();
        stat = GameObject.Find("Player").GetComponent<PlayerStat>();

        List();
        item_name.text = iname;
        item_money.text = money + " G";
        thisImg = transform.GetChild(0).GetComponent<Image>();
        ChangeImage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Buybtn()
    {
        
        if ( Gmgr != null)
        {
            if (money >= 0 && Gmgr.stagegold >= money)
            {
                Gmgr.stagegold = Gmgr.stagegold - money;
                Buf();
            }
            else
                Debug.Log("돈이 부족합니다");
        }

        Debug.Log(icode);
    }

    public void Exambtn()
    {
        GameObject ItemDscription = GameObject.Find("ShopPanel");
        ItemDscription.transform.GetChild(2).gameObject.SetActive(true);
        ItemDscription.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = exam;
    }

    void ChangeImage()
    {
        thisImg.sprite = chage_img[image_num];
    }

    void List()
    {
        switch(Random.Range(0,4))
        {
            case 0:
                iname = "HP버프";
                money = 50;
                exam = "HP2배";
                image_num = 0;
                icode = 0;
                break;
            case 1:
                iname = "MP버프";
                money = 60;
                exam = "MP2배";
                image_num = 1;
                icode = 1;
                break;
            case 2:
                iname = "공격력버프";
                money = 70;
                exam = "공격력2배";
                image_num = 2;
                icode = 2;
                break;
            case 3:
                iname = "방어력버프";
                money = 80;
                exam = "방어력2배";
                image_num = 3;
                icode = 3;
                break;
            case 4:
                iname = "랜덤버프";
                money = 90;
                exam = "방어력2배";
                image_num = 4;
                icode = 4;
                break;
        }
    }


    void Buf()
    {
        
        switch (icode)
        {
            case 0:
                BufImg = GameObject.Find("HP").GetComponent<Image>();
                Debug.Log("hp2배");
                player.playerMaxhp += 100;
                player.playerhp += 100;
                BufImg.color = new Color(1, 1, 1, 1);
                break;
            case 1:
                BufImg = GameObject.Find("MP").GetComponent<Image>();
                player.playerMaxmp += 100;
                player.playermp += 100;
                BufImg.color = new Color(1, 1, 1, 1);
                break;
            case 2:
                BufImg = GameObject.Find("ST").GetComponent<Image>();
                player.Dmg *= 2;
                BufImg.color = new Color(1, 1, 1, 1);
                break;
            case 3:
                BufImg = GameObject.Find("DE").GetComponent<Image>();
                player.Defence *= 2;
                BufImg.color = new Color(1, 1, 1, 1);
                break;
            case 4:
                BufImg = GameObject.Find("RD").GetComponent<Image>();
                BufImg.color = new Color(1, 1, 1, 1);
                break;
        }
    }
}
