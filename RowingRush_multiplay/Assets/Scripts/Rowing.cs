using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rowing : MonoBehaviour
{

    const float nSecond = 2f;
    bool Mode;

    float timer = 0;
    bool entered = false;

    public void PointerEnter()
    {
        entered = true;
    }

    public void PointerExit()
    {
        entered = false;
    }

    void Start()
    {
        //arcade 모드 선택했다면 Mode는 1, exercise 모드 선택했다면 Mode는 0
        Mode = (PlayerPrefs.GetString("userMode")=="arcade");
        Debug.Log(PlayerPrefs.GetString("userMode"));
    }

    void Update()
    {
        if (entered)
        {
            timer += Time.deltaTime;

            if(timer > nSecond)
            {
                //선택한 모드의 주행으로 넘어가기
                if (Mode) SceneManager.LoadScene("Rowing_A");
                else SceneManager.LoadScene("Rowing_E");
            }
        }
        else
        {
            timer = 0;
        }
    }
    
}