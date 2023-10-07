using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy06Bullet : MonoBehaviour
{
    public float distance;
    public LayerMask layer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.down , distance, layer);
        if (raycast.collider != null)
        {
            Destroy();
        }
  
        Invoke("Destroy", 3);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.layer == 3)
        {
            var coll = collision.gameObject.GetComponent<Player>();
            coll.hit(8);
            coll.OnDamage(transform.position);
        }
    }
}
