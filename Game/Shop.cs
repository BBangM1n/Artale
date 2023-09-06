using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject shop = GameObject.Find("ShopPanel");
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("상점을 열었습니다.");
                shop.transform.GetChild(0).gameObject.SetActive(true);
                shop.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}
