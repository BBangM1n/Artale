using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : MonoBehaviour
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
    CapsuleCollider2D capsul;
    Player player;
    PlayerStat pstat;
    public GameObject hudDamageText;
    public Transform hudpos;


    public bool skill = false;
    public int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        capsul = GetComponent<CapsuleCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        pstat = GameObject.Find("Player").GetComponent<PlayerStat>();

        // 시작 모션
        Invoke("Think", 5);

        // 몬스터 기본 정보
        MaxHp = 10;
        NowHp = 10;
        monsterdef = 2;
        monsterdmg = 5;
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (skill == true)
        {
            StartCoroutine(Clear());
            skill = false;
        }
    }

    void FixedUpdate()
    {
        //움직임
        rigid.velocity = new Vector2(movemotion * speed, rigid.velocity.y);

            //앞 플랫폼 체크
        Vector2 frontVec = new Vector2(rigid.position.x + movemotion * 0.5f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("ALLFloor"));

        if (rayhit.collider == null)
        {
                turn();
        }
    }

    void turn() // 몬스터 방향 전환
    {
        movemotion *= -1;
        sprite.flipX = movemotion == 1;

        CancelInvoke();
        Invoke("Think", 5);
    }

    void Think() // 몬스터 패턴
    {
        //방향 선택
        movemotion = Random.Range(-1, 2);

        float nextTime = Random.Range(2f, 5f);
        Invoke("Think", nextTime);

        //걷는 모션
        if (movemotion != 0)
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);
        //방향 전환
        if (movemotion != 0)
            sprite.flipX = movemotion == 1;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(godmode == false) // 몬스터 피격인식
            if (coll.gameObject.tag == "ATK")
            {
                OnDamaged();
                Debug.Log("총알인식");
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
            movemotion = 0;
            anim.SetBool("Walk", false);
            StartCoroutine(Atkmotion());
        }
    }

    IEnumerator Atkmotion() // 피격
    {
        godmode = true;
        yield return new WaitForSeconds(0.5f);
        movemotion = Random.Range(-1, 2);
        if (movemotion == 0)
            movemotion = Random.Range(-1, 2);
        anim.SetBool("Walk", true);
        if (movemotion != 0)
            sprite.flipX = movemotion == 1;
        yield return new WaitForSeconds(0.8f);
        godmode = false;
        sprite.color = new Color(1, 1, 1, 1);

    }

    IEnumerator Diemotion() // 죽음
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
