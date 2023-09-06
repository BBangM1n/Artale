using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class font : MonoBehaviour
{
    public float tspeed;
    public float ospeed;
    TextMeshPro text;
    Color one;


    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        one = text.color;
        Invoke("Destroyobj", 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, tspeed * Time.deltaTime,0));
        one.a = Mathf.Lerp(one.a, 0, Time.deltaTime * ospeed);
        text.color = one;
    }

    void Destroyobj()
    {
        Destroy(gameObject);
    }
}
