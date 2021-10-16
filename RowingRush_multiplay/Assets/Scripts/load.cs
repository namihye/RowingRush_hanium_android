using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load : MonoBehaviour
{
   public void SceneChange()
    {
        SceneManager.LoadScene("account");    
    }

    public void Delay()
    {
        Invoke("SceneChange", 1);
    }
}

