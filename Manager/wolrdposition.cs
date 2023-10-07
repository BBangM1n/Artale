using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolrdposition : MonoBehaviour
{
    public Vector2 ct;
    public Vector2 sz;
    public CameraController camara;
    void Start()
    {
        camara = GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        camara.center = ct;
        camara.size = sz;
    }

}
