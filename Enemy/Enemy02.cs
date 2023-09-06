using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02 : MonoBehaviour
{
    public float MaxHp;
    public float NowHp;
    public int monsterdmg;
    public int monsterdef;
    private bool godmode = false;
    public float speed;

    Rigidbody2D rigid;
    Animator anim;
    public SpriteRenderer sprite;
    CapsuleCollider2D capsul;
    Player player;
    public GameObject hudDamageText;
    public Transform hudpos;


    public bool skill = false;
    public int count = 0;

    public GameObject bullet;
    public Transform pos;

    public float cooltime;
    private float curtime;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        capsul = GetComponent<CapsuleCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();

        // ���� �⺻ ����
        MaxHp = 20;
        NowHp = 20;
        monsterdef = 1;
        monsterdmg = 5;
        speed = 3;

        Cooltime();
    }

    // Update is called once per frame
    void Update()
    {
        if (skill == true)
        {
            StartCoroutine(Clear());
            skill = false;
        }

        sensor();
        Reload();
        
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (godmode == false) // ���� �ǰ��ν�
            if (coll.gameObject.tag == "ATK")
            {
                OnDamaged();
                Debug.Log("�Ѿ��ν�");
            }
    }

    void OnDamaged() // �ǰ� ���ҽ�
    {
        // �ǰ� ������
        NowHp -= (player.Dmg - monsterdef);
        GameObject hudtext = Instantiate(hudDamageText);
        hudtext.transform.position = hudpos.position;
        hudtext.GetComponent<font>().damage = (player.Dmg - monsterdef);
        if (NowHp <= 0) // �׾��� ��
        {
            StartCoroutine(Diemotion());
        }
        else
        {
            sprite.color = new Color(1, 1, 1, 0.5f);
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            StartCoroutine(Atkmotion());
        }
    }

    IEnumerator Atkmotion() // �ǰ�
    {
        godmode = true;
        yield return new WaitForSeconds(0.8f);
        godmode = false;
        sprite.color = new Color(1, 1, 1, 1);

    }

    IEnumerator Diemotion() // ����
    {
        sprite.color = new Color(1, 1, 1, 0.5f);
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        sprite.flipY = true;
        capsul.enabled = false;
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    //�÷��̾�� ������ ������ 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.hit(monsterdmg);
        }
    }

    IEnumerator Clear() // ����
    {
        yield return new WaitForSeconds(2);
        speed += 2;
    }

    public void dot() // ��Ʈ ��ų
    {
        StartCoroutine(dots());

        IEnumerator dots()
        {
            NowHp -= 2;
            count += 1;
            yield return new WaitForSeconds(1);

            if (count <= 10)
            {
                StartCoroutine(dots());
            }
            else
            {
                count = 0;
                StopCoroutine(dots());
            }
        }
    }

    void ATK()
    {
         Instantiate(bullet, pos.position, transform.rotation);
    }

    void Reload()
    {
        curtime += Time.deltaTime;
    }

    void Cooltime()
    {
        cooltime = 1;
    }

    void sensor()
    {
        Debug.DrawRay(rigid.position, Vector3.left * 8, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.left, 8, LayerMask.GetMask("Player"));
        RaycastHit2D rayHit2 = Physics2D.Raycast(rigid.position, Vector3.right, 8, LayerMask.GetMask("Player"));

        if (rayHit.collider != null) //���ʰ��� 
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (curtime >= cooltime)
            {
                ATK();
                curtime = 0;
                anim.SetBool("Atk",true);
            }
        }
        else if (rayHit2.collider != null) // �����ʰ���
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (curtime >= cooltime)
            {
                ATK();
                curtime = 0;
                anim.SetBool("Atk", true);
            }
        }
        else
        {
            anim.SetBool("Atk", false);
        }
    }
}
