using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPpotion : MonoBehaviour
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
            player.playermp += 50;
            if (player.playerMaxmp < player.playermp)
                player.playermp -= (player.playermp - player.playerMaxmp);
            Destroy(this.gameObject);
        }

    }
}
