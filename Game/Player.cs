using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    Image BufImg;
    SpriteRenderer sprite;
    CapsuleCollider2D capsule;

    public float maxSpeed; //이동속도
    public int jumpPower; //점프

    //2단 점프 방지
    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGrounded;

    public GameMgr gamemgr;

    public float playerMaxhp; // 플레이어 HP 최대치
    public float playerhp; // 플레이어 HP

    public float playerMaxmp; // 플레이어 MP 최대치
    public float playermp; // 플레이어 MP
    private float mpcooltime = 3; // mp자동회복
    public bool mpbool = false;

    public float Dmg; // 플레이어 데미지 & 무기
    public int Defence; // 플레이어 방어력

    public bool godmode = false; // 갓모드 여부

    public GameObject hudDamageText; //피격 텍스트
    public Transform hudpos; //피격 텍스트

    public GameObject hudDiemsgText; // 사망 메시지
    public bool diemsg = false;



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CapsuleCollider2D>();

        maxSpeed = 5;
        jumpPower = 10;
        playerMaxhp = 100;
        playerhp = playerMaxhp;
        playerMaxmp = 100;
        playermp = playerMaxmp;
        anim.SetBool("Death", false);

        Dmg = 5;
        Defence = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //이동
        if(Input.GetButtonUp("Horizontal")) {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0001f, rigid.velocity.y);
            }

        //점프
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.67f, 0.2f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            anim.SetBool("Jump", true);  // 점프 애니메이션
        }
        if (isGrounded && rigid.velocity.y == 0)
            anim.SetBool("Jump",false); // 땅일시 점프 풀림
       
        //플레이어 강제로 피 닳게하기
        if(Input.GetKeyDown(KeyCode.R))
        {
            playerhp -= 10;
            playermp -= 5;
            mpbool = true;
        }

        if(playerhp <= 0)
        {
            Die();
            BufImg = GameObject.Find("HP").GetComponent<Image>();
            BufImg.color = new Color32(142, 142, 142, 255);
        }

        if (Mathf.Abs(rigid.velocity.x) < 0.3)  // 달리는 애니메이션
            anim.SetBool("Run", false);
        else
            anim.SetBool("Run", true);

        if (Input.GetKeyDown(KeyCode.Z)) //어택 애니메이션
        {
            //anim.SetTrigger("Attack");
        }


        if(mpbool == true)
            mpcooltime -= Time.deltaTime;
        if (mpcooltime <= 0 && playermp < playerMaxmp)
        {
            mpup();
        }

    }

    private void FixedUpdate()
    {
        float hor = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * hor, ForceMode2D.Impulse);

        // 이동속도 최대값
        if(rigid.velocity.x > maxSpeed)
           rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        else if(rigid.velocity.x < maxSpeed*(-1))
           rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);

        
        if(hor > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if (hor < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    void OnTriggerEnter2D(Collider2D coll) 
    {
        //코인 구분
        if(coll.gameObject.tag == "Gold"){
            gamemgr.stagegold += 100;
            Debug.Log(gamemgr.stagegold);
            coll.gameObject.SetActive(false);
        }

        if(coll.gameObject.tag == "Silber"){
            gamemgr.stagegold += 50;
            Debug.Log(gamemgr.stagegold);
            coll.gameObject.SetActive(false);
        }

        if(coll.gameObject.tag == "Bronze"){
            gamemgr.stagegold += 10;
            Debug.Log(gamemgr.stagegold);
            coll.gameObject.SetActive(false);
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            OnDamage(collision.transform.position);
        }
    }

    void Die()
    {
        hudDiemsgText.SetActive(true);
        Time.timeScale = 0;
    }

    void mpup()
    {
        playermp += 3;
        if (playermp > playerMaxmp)
        {
            playermp -= (playermp - playerMaxmp);
            mpbool = false;
        }
        mpcooltime = 3;
    }

    public void OnDamage(Vector2 targetpos)
    {
        godmode = true;
        gameObject.layer = 8;
        sprite.color = new Color(1, 1, 1, 0.4f);
        int hit = transform.position.x - targetpos.x > 0 ? 1 : -1;

        rigid.AddForce(new Vector2(hit, 1)*7, ForceMode2D.Impulse);

        Invoke("OffDamage", 2);
        Debug.Log("닿음");
    }

    public void OffDamage()
    {
        godmode = false;
        gameObject.layer = 3;
        sprite.color = new Color(1, 1, 1, 1);
    }

    public void hit(float damage)
    {
        playerhp -= (damage - Defence);
        GameObject hudtext = Instantiate(hudDamageText);
        hudtext.transform.position = hudpos.position;
        hudtext.GetComponent<font>().damage = (damage - Defence);
    }
}
