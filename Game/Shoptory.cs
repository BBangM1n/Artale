using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoptory : MonoBehaviour
{
    public List<ShopData> slots = new List<ShopData>();
    private int maxSlot = 4; //¸Æ½º ½½·Ô°¹¼ö
    public GameObject slotPrefab; // ½½·ÔÇÁ¸®ÆÕ °¡Á®¿À±â

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
            GameObject go = Instantiate(slotPrefab, slotPanel.transform.GetChild(0), false); // Ã£Àº ½½·ÔÇÁ¸®ÆÕ¿¡ ÀÚ½Ä»ý¼º
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
