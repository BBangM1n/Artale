using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helppotal : MonoBehaviour
{
    
    public Vector2 vt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.position = vt;
        }

    }
}
