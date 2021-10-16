using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickButton : MonoBehaviour
{
    public void OnClickHome()
    {
        SceneManager.LoadScene("first");

    }

    public void OnClickDistance(int TargetDistance)
    {
        PlayerPrefs.SetInt("TargetDistance", TargetDistance);
        SceneManager.LoadScene("BeforeRowing");
    }
}
