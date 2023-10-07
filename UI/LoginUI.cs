using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoginUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartBtn()
    {
        SceneManager.LoadScene("Save");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void DescriptionBtn()
    {
        GameObject description = GameObject.Find("Canvas"); 
        description.transform.GetChild(6).gameObject.SetActive(true);
    }
}
