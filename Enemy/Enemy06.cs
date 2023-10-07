using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy06 : MonoBehaviour
{
    public int movemotion;
    public float MaxHp;
    public float NowHp;
    public int monsterdmg;
    public int monsterdef;
    private bool godmode = false;
    public float speed;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;
    CircleCollider2D capsul;
    Player player;
    PlayerStat pstat;
    public GameObject hudDamageText;
    public Transform hudpos;


    public bool skill = false;
    public int count = 0;

    public float cooltime = 1;
    private bool On;

    public GameObject bullet;
    public Transform pos;

    public float bulletcooltime;
    private float bulletcurtime;



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        capsul = GetComponent<CircleCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        pstat = GameObject.Find("Player").GetComponent<PlayerStat>();

        // ���� ���
        Invoke("Think", 5);

        // ���� �⺻ ����
        MaxHp = 20;
        NowHp = 20;
        monsterdef = 3;
        monsterdmg = 5;
        speed = 1.5f;

        On = true;

        StartCoroutine(bulletspawn());

    }

    // Update is called once per frame
    void Update()
    {
        if (skill == true)
        {
            StartCoroutine(Clear());
            skill = false;
        }

        if(cooltime >= 0)
            cooltime -= Time.deltaTime;
        OnandOff();
    }

    void FixedUpdate()
    {
        //������
        rigid.velocity = new Vector2(movemotion * speed, rigid.velocity.y);


        if(On == true)
            transform.Translate(new Vector2(0, -1f * Time.deltaTime * 0.5f));
        else if(On == false)
            transform.Translate(new Vector2(0, 1f * Time.deltaTime * 0.5f));

        Vector2 frontVec = new Vector2(rigid.position.x + movemotion * 0.5f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.left * 2f, new Color(0, 1, 0));

        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.left, 2, LayerMask.GetMask("ALLFloor"));

        if (rayhit.collider != null)
        {
            turn();
        }

    }

    void turn() // ���� ���� ��ȯ
    {
        movemotion *= -1;
        sprite.flipX = movemotion == -1;

        CancelInvoke();
        Invoke("Think", 5);
    }



    void Think() // ���� ����
    {
        //���� ����
        movemotion = Random.Range(-1, 2);

        float nextTime = Random.Range(2f, 5f);
        Invoke("Think", nextTime);

        //�ȴ� ���
        if (movemotion != 0)
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);
        //���� ��ȯ
        if (movemotion != 0)
            sprite.flipX = movemotion == -1;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (godmode == false) // ���� �ǰ��ν�
            if (coll.gameObject.tag == "ATK")
            {
                if (player.Dmg >= monsterdef)
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
            movemotion = 0;
            anim.SetBool("Walk", false);
            StartCoroutine(Atkmotion());
        }
    }

    IEnumerator Atkmotion() // �ǰ�
    {
        godmode = true;
        yield return new WaitForSeconds(0.5f);
        movemotion = Random.Range(-1, 2);
        if (movemotion == 0)
            movemotion = Random.Range(-1, 2);
        anim.SetBool("Walk", true);
        if (movemotion != 0)
            sprite.flipX = movemotion == -1;
        yield return new WaitForSeconds(0.8f);
        godmode = false;
        sprite.color = new Color(1, 1, 1, 1);

    }

    IEnumerator Diemotion() // ����
    {
        pstat.Exp += 3;
        DataMgr.instance.nowPlayer.exp += 3;
        sprite.color = new Color(1, 1, 1, 0.5f);
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        sprite.flipY = true;
        capsul.enabled = false;
        movemotion = 0;
        anim.SetBool("Walk", false);
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

    void OnandOff()
    {
        if(cooltime <= 0)
        {
            if (On == true)
            {
                On = false;
                cooltime = 1;
            }
            else if (On == false)
            {
                On = true;
                cooltime = 1;
            }
        }
    }

    void ATK()
    {
        Instantiate(bullet, pos.position, transform.rotation);
    }

    IEnumerator bulletspawn()
    {
        ATK();
        yield return new WaitForSecondsRealtime(3.0f);
        StartCoroutine(bulletspawn());
    }
}
