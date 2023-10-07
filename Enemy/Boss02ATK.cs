using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02ATK : MonoBehaviour
{
    Transform Laserpos;
    Player player;

    public bool hiton = false;
    // Start is called before the first frame update
    void Start()
    {
        Laserpos = GameObject.Find("Laserpos").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.y <= 1)
        {
            transform.localScale += new Vector3(0, 0.005f, 0);
        }

        else if(transform.localScale.y >= 1)
        {
            if(hiton == true)
            {
                hiton = false;
                player.hit(10);
                player.OnDamage(transform.position);
            }
            Invoke("Destroy", 1.5f);
        }

        transform.position = Laserpos.position; 
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hiton = true;
            Debug.Log("트리거발동");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hiton = false;
    }
}
