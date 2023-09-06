using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<SlotData> slots = new List<SlotData>();
    private int maxSlot = 4; //¸Æ½º ½½·Ô°¹¼ö
    public GameObject slotPrefab; // ½½·ÔÇÁ¸®ÆÕ °¡Á®¿À±â

    private void Start() 
    {
        GameObject slotPanel = GameObject.Find("Panel");

        for(int i = 0 ; i < maxSlot ; i ++)
        {
            GameObject go = Instantiate(slotPrefab, slotPanel.transform, false); // Ã£Àº ½½·ÔÇÁ¸®ÆÕ¿¡ ÀÚ½Ä»ý¼º
            go.name = "Slot_" + i;
            SlotData slot = new SlotData();
            slot.isEmpty = true;
            slot.slotObj = go;
            slots.Add(slot);
        }    
    }
}
