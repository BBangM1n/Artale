using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoptory : MonoBehaviour
{
    public List<ShopData> slots = new List<ShopData>();
    private int maxSlot = 4; //�ƽ� ���԰���
    public GameObject slotPrefab; // ���������� ��������

    void Start()
    {
        slotadd();
    }

    public void slotadd()
    {
        //slotreset();
        GameObject slotPanel = GameObject.Find("ShopPanel");
        for (int i = 0; i < maxSlot; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotPanel.transform.GetChild(0), false); // ã�� ���������տ� �ڽĻ���
            go.name = "ShopSlot_" + i;
            ShopData slot = new ShopData();
            slot.isEmpty = true;
            slot.slotObj = go;
            slots.Add(slot);
        }
    }

    public void slotreset()
    {
        slots = new List<ShopData>();
    }
}
