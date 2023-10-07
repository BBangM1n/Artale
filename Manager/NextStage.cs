using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    public DungeonMgr Manager;
    Player player;
    public Vector2 vt;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Manager.NextStage();
            Playerposition();
        }
    }

    public void Playerposition()
    {
        player.transform.position = vt;
    }
}
