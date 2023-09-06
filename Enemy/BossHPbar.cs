using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPbar : MonoBehaviour
{
    public Boss01 Boss;
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
        MaxHp = Boss.MaxHp;
        NowHp = Boss.NowHp;
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
}
