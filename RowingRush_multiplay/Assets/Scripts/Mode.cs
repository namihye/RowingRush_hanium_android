using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mode : MonoBehaviour
{
    public void SceneChange_arcade()
    {
        PlayerPrefs.SetString("userMode", "arcade");
        SceneManager.LoadScene("A_Start");
    }

    public void SceneChange_exercise()
    {
        SceneManager.LoadScene("E_Start");
        PlayerPrefs.SetString("userMode", "exercise");

    }

}
