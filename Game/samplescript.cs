using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class samplescript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("È°¼ºÈ­");
        }
    }
}
