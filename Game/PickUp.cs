using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject slotItem;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Player"))
        {
            Inventory inven = other.GetComponent<Inventory>();
            for(int i = 0; i < inven.slots.Count ; i++)
            {
                if(inven.slots[i].isEmpty) //빈공간에 넣기
                {
                    Instantiate(slotItem,inven.slots[i].slotObj.transform,false); //자식생성
                    inven.slots[i].isEmpty = false;
                    Destroy(this.gameObject);
                    break;
                }
            }
        }   
    }
}
