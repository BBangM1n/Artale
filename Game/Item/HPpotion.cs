using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPpotion : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {

        if (Input.inputString == (transform.parent.GetComponent<Slot>().num + 1).ToString()) //아이템 삭제
        {
            Debug.Log("아이템 사용 슬롯넘버 " + (transform.parent.GetComponent<Slot>().num + 1));
            player.playerhp += 50;
            if (player.playerMaxhp < player.playerhp)
                player.playerhp -= (player.playerhp - player.playerMaxhp);
            Destroy(this.gameObject);
        }

    }
}
