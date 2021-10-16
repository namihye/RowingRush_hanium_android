using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class GoogleData
{
    public string order, result, msg, distance, time, speed;
}


public class GSmanager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbxc5R_Y7zTad80LiUs_O-6wE889DjIquIuirMARMTgOFqGOdsJ3/exec";
    public GoogleData GD;



    public InputField IDInput, PassInput;
    public TextMeshProUGUI message, myID;
    string id, pass;

    string jsondata;





    bool SetIDPass()
    {
        id = IDInput.text.Trim();
        pass = PassInput.text.Trim();

        if (id == "" || pass == "") return false;
        else return true;
    }
    public void Register()
    {
        if (!SetIDPass())
        {
            print("아이디 또는 비밀번호가 비어있습니다");
            message.text = "ID or password is blank";
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }

    public void Login()
    {
        if (!SetIDPass())
        {
            print("아이디 또는 비밀번호가 비어있습니다");
            message.text = "ID or password is blank";
            return;
        }

        WWWForm form = new WWWForm();

        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));

    }

    void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "logout");

        StartCoroutine(Post(form));
    }
    public void SetValue(string a, string b, string c)
    {

        WWWForm form = new WWWForm();
        form.AddField("order", "setValue");
        form.AddField("distance", a);
        form.AddField("time", b);
        form.AddField("speed", c);

        StartCoroutine(Post(form));
    }



    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                Response(www.downloadHandler.text);

                string[] data = www.downloadHandler.text.Split(new char[] { '"' });
                message.text = data[data.Length - 2];
                string S1 = www.downloadHandler.text;
                string S2 = "log in succeed";

                if (S1.Contains(S2))
                {
                    SceneManager.LoadScene("third");

                }
            }
            else
            {
                print("웹의 응답이 없습니다.");
                message.text = "No response from the web.";
            }
        }
    }

    void Response(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return;
        }

        GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            print(GD.order + "을 실행할 수 없습니다. 에러 메시지 : " + GD.msg);
            return;
        }

        print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);


    }


    public void SC_login()
    {
        SceneManager.LoadScene("login");

    }
    public void SC_signup()
    {
        SceneManager.LoadScene("signup");

    }

}