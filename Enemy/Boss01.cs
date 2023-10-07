using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01 : MonoBehaviour
{
    public int movemotion;
    public int move;
    public float MaxHp;
    public float NowHp;
    public int monsterdmg;
    public int monsterdef;
    private bool godmode = false;
    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    CapsuleCollider2D capsul;
    Player player;
    PlayerStat pstat;
    public GameObject hudDamageText;
    public Transform hudpos;


    public bool skill = false;
    public int count = 0;
    public GameObject LastPotal;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        capsul = GetComponent<CapsuleCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        pstat = GameObject.Find("Player").GetComponent<PlayerStat>();

        // 시작 모션
        movemotion = -1;

        // 몬스터 기본 정보
        MaxHp = 100;
        NowHp = 100;
        monsterdef = 2;
        monsterdmg = 5;
        speed = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (skill == true)
        {
            StartCoroutine(Clear());
            skill = false;
        }

        Debug.DrawRay(new Vector2(rigid.position.x, -5), Vector3.left * 50, new Color(0, 1, 1));
        Debug.DrawRay(new Vector2(rigid.position.x, -6), Vector3.left * 50, new Color(0, 1, 1));
        Debug.DrawRay(new Vector2(rigid.position.x, -7), Vector3.left * 50, new Color(0, 1, 1));
        RaycastHit2D rayhitL = Physics2D.Raycast(new Vector2(rigid.position.x, -5), Vector3.left, 50, LayerMask.GetMask("Player"));
        RaycastHit2D rayhitL2= Physics2D.Raycast(new Vector2(rigid.position.x, -6), Vector3.left, 50, LayerMask.GetMask("Player"));
        RaycastHit2D rayhitL3= Physics2D.Raycast(new Vector2(rigid.position.x, -7), Vector3.left, 50, LayerMask.GetMask("Player"));
        RaycastHit2D rayhitR = Physics2D.Raycast(new Vector2(rigid.position.x, -5), Vector3.right, 50, LayerMask.GetMask("Player"));
        RaycastHit2D rayhitR2= Physics2D.Raycast(new Vector2(rigid.position.x, -6), Vector3.right, 50, LayerMask.GetMask("Player"));
        RaycastHit2D rayhitR3= Physics2D.Raycast(new Vector2(rigid.position.x, -7), Vector3.right, 50, LayerMask.GetMask("Player"));

        if (rayhitL.collider != null || rayhitL2.collider != null || rayhitL3.collider != null)
        {
            if (godmode == true)
            {
                movemotion = -1;
                sprite.flipX = movemotion == 1;
            }
        }
        else if (rayhitR.collider != null || rayhitR2.collider != null || rayhitR3.collider != null)
        {
            if (godmode == true)
            {
                movemotion = 1;
                sprite.flipX = movemotion == 1;
            }
        }
    }

    void FixedUpdate()
    {
        //움직임
        rigid.velocity = new Vector2(movemotion * speed, rigid.velocity.y);

        //앞 플랫폼 체크
        Vector2 frontVec = new Vector2(rigid.position.x + movemotion * 4, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down * 4.5f, new Color(0, 1, 0));

        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 4.5f, LayerMask.GetMask("ALLFloor"));

        if (rayhit.collider == null)
        {
            turn();
        }
    }

    void turn() // 몬스터 방향 전환
    {
        movemotion *= -1;
        sprite.flipX = movemotion == 1;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (godmode == false) // 몬스터 피격인식
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
            StartCoroutine(Atkmotion());
        }
    }

    IEnumerator Atkmotion() // 피격
    {
        godmode = true;
        yield return new WaitForSeconds(0.5f);
        movemotion = Random.Range(-1, 2);
        if (movemotion == 0)
            move = Random.Range(-1, 1);
        if (move == -1)
            movemotion = -1;
        else
            movemotion = 1;
        if (movemotion != 0)
            sprite.flipX = movemotion == 1;
        yield return new WaitForSeconds(0.8f);
        godmode = false;
        sprite.color = new Color(1, 1, 1, 1);
    }

    IEnumerator Diemotion() // 죽음
    {
        pstat.Exp += 30;
        DataMgr.instance.nowPlayer.exp += 30;
        sprite.color = new Color(1, 1, 1, 0.5f);
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        sprite.flipY = true;
        capsul.enabled = false;
        movemotion = 0;
        yield return new WaitForSeconds(3);
        LastPotal.SetActive(true);
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
