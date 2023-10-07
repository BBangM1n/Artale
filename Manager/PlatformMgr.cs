using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMgr : MonoBehaviour
{
    public bool playerCheck;
    PlatformEffector2D platform;

    void Start()
    {
        playerCheck = false;
        platform = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && playerCheck == true)
        {
            platform.rotationalOffset = 180f;
            Invoke("Reload",0.3f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            platform.rotationalOffset = 0f;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerCheck = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCheck = false;
    }

    void Reload()
    {
        platform.rotationalOffset = 0f;
    }
}
