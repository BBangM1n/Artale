using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMgr : MonoBehaviour
{
    public int stageIndex;
    public GameObject[] Stages;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStage()
    {
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
        }
    }

    
}
