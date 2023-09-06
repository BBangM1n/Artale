using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceboll : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right, distance, layer);
        if (raycast.collider != null)
        {
            Destroy();
        }

        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }

        Invoke("Destroy", 3);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inenemy = collision.gameObject.GetComponent<Enemy01>();
        if (collision.gameObject.tag == "Enemy")
        {
            inenemy.speed -= 2;
            inenemy.skill = true;
        }
    }
}
