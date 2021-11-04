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
        //arcade ��� �����ߴٸ� Mode�� 1, exercise ��� �����ߴٸ� Mode�� 0
        Mode = (PlayerPrefs.GetString("userMode")=="arcade");
    }

    void Update()
    {
        if (entered)
        {
            timer += Time.deltaTime;

            if(timer > nSecond)
            {
                //������ ����� �������� �Ѿ��
                if (Mode) SceneManager.LoadScene("Rowing_A");
                else SceneManager.LoadScene("Rowing_A");
            }
        }
        else
        {
            timer = 0;
        }
    }
    
}