using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardHit : MonoBehaviour
{
    string IDword = null;
    string PWword = null;

    int IDIndex = -1;
    int PWIndex = -1;

    bool isIDInput = false;
    bool isPWInput = false;

    string alphaID = null;
    string alphaID2 = null;

    string alphaPW = null;
    string alphaPW2 = null;

    public InputField IDtext;
    public InputField PWtext;

    public GameObject ToggleBtnShow;
    public GameObject ToggleBtnHide;

    char[] IDChar = new char[30];
    char[] PWChar = new char[30];

    public GameObject KeyboardLower;
    public GameObject KeyboardUpper;

    public void OnClickID()
    {
        //ID 입력 칸 누르면 키보드ui 활성화시키고 ID 입력 칸에만 입력 받기
        KeyboardLower.SetActive(true);
        KeyboardUpper.SetActive(false);
        isIDInput = true;
        isPWInput = false;
    }

    public void OnClickPW()
    {
        //PW 입력 칸 누르면 키보드ui 활성화시키고 PW 입력 칸에만 입력 받기
        KeyboardLower.SetActive(true);
        KeyboardUpper.SetActive(false);
        isPWInput = true;
        isIDInput = false;
    }

    public void OnClickEnter()
    {
        //enter 누르면 키보드ui 비활성화
        KeyboardLower.SetActive(false);
        KeyboardUpper.SetActive(false);
    }

    public void OnClickUpper()
    {
        //위쪽 화살표 버튼 누르면 대문자로 변환

        //소문자 키보드가 활성화되어 있으면 1, 대문자 키보드가 활성화되어 있으면 0을 반환
        bool isKeyboardType = KeyboardLower.activeSelf == true;

        //1이면 대문자 키보드 활성화, 0이면 소문자 키보드 활성화시키기
        KeyboardLower.SetActive(!isKeyboardType);
        KeyboardUpper.SetActive(isKeyboardType);

    }

    public void alphabetFunction(string alphabet)
    {
        alphabet = KeyboardLower.activeSelf ? alphabet : alphabet.ToUpper();

        if (isIDInput)
        {
            IDIndex++;
            char[] keepIDchar = alphabet.ToCharArray();
            IDChar [IDIndex] = keepIDchar[0];
            alphaID = IDChar [IDIndex].ToString();
            IDword = IDword + alphabet;
            IDtext.text = IDword;
        }

        if (isPWInput)
        {
            PWIndex++;
            char[] keepPWchar = alphabet.ToCharArray();
            PWChar[PWIndex] = keepPWchar[0];
            alphaPW = PWChar [PWIndex].ToString();
            PWword = PWword + alphabet;
            PWtext.text = PWword;
        }

    }

    public void backspaceFunction()
    {
        if (isIDInput)
        {
            if (IDIndex >= 0)
            {
                IDIndex--;

                alphaID2 = null;
                
                for(int i=0; i<IDIndex+1; i++)
                {
                    alphaID2 = alphaID2 + IDChar[i].ToString();
                }

                IDword = alphaID2;
                IDtext.text = IDword;
                
            }
        }

        if (isPWInput)
        {
            if (PWIndex >= 0)
            {
                PWIndex--;

                alphaPW2 = null;

                for (int i = 0; i < PWIndex + 1; i++)
                {
                    alphaPW2 = alphaPW2 + PWChar[i].ToString();
                }

                PWword = alphaPW2;
                PWtext.text = PWword;
            }
        }

    }

    public void TooglePasswordVisablilty()
   
    {
        bool isCurrentlyPassword = PWtext.contentType == InputField.ContentType.Password;

        if (isCurrentlyPassword)
        {
            ToggleBtnShow.SetActive(true);
            ToggleBtnHide.SetActive(false);
        }
        else
        {
            ToggleBtnShow.SetActive(false);
            ToggleBtnHide.SetActive(true);
        }

        PWtext.contentType = isCurrentlyPassword ? InputField.ContentType.Standard : InputField.ContentType.Password;

        PWtext.ForceLabelUpdate();
    }


}