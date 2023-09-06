using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private MeshRenderer render;

    public float speed;
    private float offset;

    public float BackSpeed = 5.0f; //화면이 따라오는 속도

    Player player; //플레이어 찾는용도

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;
    
    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
        render = GetComponent<MeshRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(center, size);
    }
    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(offset , 0);


        //플레이어 따라 이동되는 배경
        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * BackSpeed * Time.deltaTime, dir.y * BackSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }

    private void LateUpdate() {

        float lx = size.x  * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y  * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, 0f);
    }
}
