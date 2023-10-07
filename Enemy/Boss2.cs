using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{

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

    public int Pattern = 0;
    public int Pton = 1;
    public bool up = true;
    public int move = 0;
    public GameObject Laser;

    public GameObject bullet;
    Transform bpos;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        capsul = GetComponent<CapsuleCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        pstat = GameObject.Find("Player").GetComponent<PlayerStat>();
        bpos = GameObject.Find("bpos").GetComponent<Transform>();

        MaxHp = 100;
        NowHp = 100;
        monsterdef = 2;
        monsterdmg = 5;
        speed = 0.01f;

        StartCoroutine(Bullet());
    }

    // Update is called once per frame
    void Update()
    {
        if (skill == true)
        {
            StartCoroutine(Clear());
            skill = false;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + speed, -1.5f);

        PT();

        if (Pattern == 1 && Pton == 1)
        {
            StartCoroutine(StartPattern());
        }
        else if(Pattern == 2 && Pton == 2)
        {
            StartCoroutine(StartPattern2());
        }

        if(Pattern < 2)
        {
            StopCoroutine(Bullet());
        }

    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.up * 5f, new Color(0, 1, 0)); // 위방향진행시
        Debug.DrawRay(transform.position, Vector3.down * 4.5f, new Color(0, 1, 0)); // 아래방향진행시

        RaycastHit2D rayhitup = Physics2D.Raycast(transform.position, Vector3.up, 5f, LayerMask.GetMask("ALLFloor"));
        RaycastHit2D rayhitdown = Physics2D.Raycast(transform.position, Vector3.down, 4.5f, LayerMask.GetMask("ALLFloor"));
        if (rayhitup.collider != null)
        {
            turn();
            up = false;
        }
        else if(rayhitdown.collider != null)
        {
            turn();
            up = true;
        }
    }

    void turn()
    {
        speed *= -1;
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

    IEnumerator Clear() // 복구
    {
        yield return new WaitForSeconds(2);
        speed += 2;
    }

    IEnumerator Diemotion() // 죽음
    {
        pstat.Exp += 30;
        DataMgr.instance.nowPlayer.exp += 30;
        sprite.color = new Color(1, 1, 1, 0.5f);
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        sprite.flipY = true;
        capsul.enabled = false;
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

    void PT()
    {
        if(NowHp <= 30)
        {
            StopCoroutine(Bullet());
            Pattern = 2;
        }
        else if(NowHp <= 50)
        {
            Pattern = 1;
        }
        else
        {
            Pattern = 0;
        }
    }

    IEnumerator StartPattern()
    {
        Pton += 1;
        yield return new WaitForSeconds(0.1f);
        if (up == true)
            speed += 0.02f;
        else if (up == false)
            speed -= 0.02f;
        monsterdmg += 5;
    }

    IEnumerator StartPattern2()
    {
        Pton += 1;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        move = Random.Range(0, 2);
        if(move == 0)
        {
            transform.position = new Vector2 (17, transform.position.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(move == 1)
        {
            transform.position = new Vector2(-10, transform.position.y);
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }

        Instantiate(Laser, transform.position, gameObject.transform.rotation);
        
        yield return new WaitForSeconds(5f);

        StartCoroutine(Moving());
    }

    IEnumerator Bullet()
    {
        Instantiate(bullet, new Vector2(bpos.position.x, bpos.position.y), gameObject.transform.rotation);
        if (Pattern == 0)
        {
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(Bullet());
        }
        else if(Pattern == 1)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(Bullet());
        }
    }
}
