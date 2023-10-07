using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04 : MonoBehaviour
{

    /* 설명 
     * 공중 비행몬스터. o
     * 플레이어 감지 기능. o
     * 일정한 범위안에 플레이어가 있으면 날기 시작. o
     */

    public Transform playertransform;
    public float speed = 4f;
    public float range = 10f;
    public bool nodie = true;

    public float MaxHp;
    public float NowHp;
    public int monsterdmg;
    public int monsterdef;
    private bool godmode = false;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;
    CapsuleCollider2D capsul;
    Player player;
    PlayerStat pstat;
    public GameObject hudDamageText;
    public Transform hudpos;

    //스킬 관련
    public bool skill = false;
    public int count = 0;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        capsul = GetComponent<CapsuleCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        pstat = GameObject.Find("Player").GetComponent<PlayerStat>();
        playertransform = GameObject.Find("Player").GetComponent<Transform>();

        // 시작 모션
        Invoke("Think", 5);

        // 몬스터 기본 정보
        MaxHp = 15;
        NowHp = 15;
        monsterdef = 3;
        monsterdmg = 5;
    }

    void Update()
    {
        //플레이어 탐지
        float distance = Vector2.Distance(transform.position, playertransform.position);
        if (nodie == true)
        {
            if (distance <= range)
            {
                anim.SetBool("Flying", true);
                transform.position = Vector3.MoveTowards(transform.position, playertransform.position, speed * Time.deltaTime);
            }
            else
                anim.SetBool("Flying", false);
        }

        if (skill == true)
        {
            StartCoroutine(Clear());
            skill = false;
        }

        if(playertransform.position.x >= transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (godmode == false) // 몬스터 피격인식
            if (coll.gameObject.tag == "ATK")
            {
                Debug.Log("총알인식");
                OnDamaged();
            }
    }

    void OnDamaged() // 피격 당할시
    {
        // 피격 당한후
        NowHp -= (player.Dmg - monsterdef);
        GameObject hudtext = Instantiate(hudDamageText);
        hudtext.transform.position = hudpos.position;
        hudtext.GetComponent<font>().damage = (player.Dmg - monsterdef);
        if (NowHp <= 0) // 죽었을 시
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

    IEnumerator Atkmotion() // 피격
    {
        godmode = true;
        yield return new WaitForSeconds(0.8f);
        godmode = false;
        sprite.color = new Color(1, 1, 1, 1);

    }

    IEnumerator Diemotion() // 죽음
    {
        nodie = false;
        pstat.Exp += 3;
        DataMgr.instance.nowPlayer.exp += 3;
        sprite.color = new Color(1, 1, 1, 0.5f);
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        sprite.flipY = true;
        capsul.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    //플레이어에게 데미지 입히기 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.hit(monsterdmg);
        }
    }

    IEnumerator Clear() // 복구
    {
        yield return new WaitForSeconds(2);
        speed += 2;
    }

    public void dot() // 도트 스킬
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

}
