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

        if (Input.inputString == (transform.parent.GetComponent<Slot>().num + 1).ToString()) //������ ����
        {
            Debug.Log("������ ��� ���Գѹ� " + (transform.parent.GetComponent<Slot>().num + 1));
            player.playerhp += 50;
            if (player.playerMaxhp < player.playerhp)
                player.playerhp -= (player.playerhp - player.playerMaxhp);
            Destroy(this.gameObject);
        }

    }
}
