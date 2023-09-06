using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATK : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;

    Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        ATK();
    }

    void ATK()
    {
        if(Input.GetKeyUp(KeyCode.Z))
        {
            Instantiate(bullet, pos.position, transform.rotation);
        }
    }
}
