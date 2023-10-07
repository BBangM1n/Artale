using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string filed;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            move();
            position();
        }
    }

    private void move()
    {
        SceneManager.LoadScene(filed);
    }

    void position()
    {
        player.transform.position = vt;
    }

}
