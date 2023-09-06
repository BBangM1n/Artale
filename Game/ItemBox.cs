using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    Player player;
    public float MaxHp;
    public float NowHp;
    public GameObject hudDamageText;
    public Transform hudpos;
    public Sprite[] image;
    SpriteRenderer spriteimage;

    public bool broken;
    public GameObject[] item;
    public int i;

    // Start is called before the first frame update
    void Start()
    {
        spriteimage = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        MaxHp = 20;
        NowHp = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (NowHp >= 15)
            spriteimage.sprite = image[0];
        else if (NowHp >= 10)
            spriteimage.sprite = image[1];
        else if (NowHp >= 5)
            spriteimage.sprite = image[2];
        else if (NowHp >= 0)
            spriteimage.sprite = image[3];

        if(NowHp <= 0 && broken == false)
        {
            dropitem();
            broken = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "ATK")
        {
            OnDamaged();
        }
    }

    void OnDamaged() // 피격
    {
        // 피격 당한후
        NowHp -= 2;
        GameObject hudtext = Instantiate(hudDamageText);
        hudtext.transform.position = hudpos.position;
        hudtext.GetComponent<font>().damage = 2;
    }

    void dropitem()
    {
        i = Random.Range(0, 3);
        Instantiate(item[i], transform.position, transform.rotation);
    }
}
