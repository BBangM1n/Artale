using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player.Dmg += 3;
    }
    void Update()
    {
        
        if(Input.inputString == (transform.parent.GetComponent<Slot>().num + 1).ToString()) //아이템 삭제
        {
            Debug.Log("아이템 사용 슬롯넘버 " + (transform.parent.GetComponent<Slot>().num + 1) );
            player.Dmg -= 3;
            Destroy(this.gameObject);
        }

    }
    
}
