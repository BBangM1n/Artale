using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public Text goldtext;
    public int totalgold;
    public int stagegold;

    public Text GtimeText;
    public float gtime = 3600;
    int min;
    float sec;

    GameObject shop;
    Shoptory shoptory;
    Transform slotp;
    public GameObject shopPrefab;


    void Start()
    {
        stagegold = 0;
        totalgold = 0;
        shoptory = GameObject.Find("Shop").GetComponent<Shoptory>();
    }

    void Update()
    {
        goldtext.text = string.Format("{0} Gold", stagegold);

        shoptime();

       if (gtime <= 0)
        {
            for (int i = 0; i < 4; i++)
            {
                slotp = GameObject.Find("ShopPanel").transform.GetChild(0).GetChild(i);
                Destroy(slotp.gameObject);
            }
            gtime = 3600;
            shoptory.slotreset();
            shoptory.slotadd();
        }
    }

    void shoptime()
    {
        gtime -= Time.deltaTime;
        min = (int)gtime / 60;
        sec = gtime % 60;

        GtimeText.text = "상점초기화 가능 까지 " + string.Format("{0:D2} : {1:D2}", min, (int)sec);
    }
   
}
