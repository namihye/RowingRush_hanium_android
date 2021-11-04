using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text;

[System.Serializable]
public class GoogleData
{
    public string order, result, msg, myranking1, myranking2, myranking3, curID; 
    public int distance, time, speed;
}


public class GSmanager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbxc5R_Y7zTad80LiUs_O-6wE889DjIquIuirMARMTgOFqGOdsJ3/exec";
    //const string URL2 = "https://script.google.com/macros/s/AKfycbzurOOI8hf1HuSgWm3tPcUUi2KjvqsoD2XUvCwulWpxD-YPUAvBqiHpsQJmru1u-Bcx/exec";
    public GoogleData GD;


    public Canvas canvas;
    public InputField IDInput, PassInput;
    public TextMeshProUGUI message, myID, myRanking1, myRanking2, myRanking3;
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

        myID.text = GD.curID;
        DontDestroyOnLoad(canvas);

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
               // print("1");

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

    IEnumerator Rank(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            print("rank1");
            //form.AddField("order", "Ranking");

            if (www.isDone)
            {


                print("rank2");

                Response2(www.downloadHandler.text);
                print("rank3");

               // if (GD.result == id)
                //{
                  //  print("rank4");

                    //myRanking.text = GD.msg;
                //}

                print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);

                myRanking1.text = GD.myranking1;
                myRanking2.text = GD.myranking2;
                myRanking3.text = GD.myranking3;
                myID.text = GD.curID;


                // myRanking.text = "R U Crazy?";
                // Debug.Log(myRanking.text);
                //myID.text = id;

            }
            else
            {
                print("웹의 응답이 없습니다.");
                message.text = "No response from the web.";
            }
        }
    }

    public void Ranking()
    {

        WWWForm form = new WWWForm();

        form.AddField("order", "Ranking");
        StartCoroutine(Rank(form));
 
    }

    void Response(string json)
    {

        if(string.IsNullOrEmpty(json))
        {
            return;
        }

        GD = JsonUtility.FromJson<GoogleData>(json);


        Debug.Log(GD.result);

        if(GD.result == "ERROR")
        {

            print(GD.order + "을 실행할 수 없습니다. 에러 메시지 : " + GD.msg);
            return;
        }
        print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);


    }

    void Response2(string json)
    {
        print("첫번째반응");
        if (string.IsNullOrEmpty(json))
        {
            print("두번째반응");
            return;
        }
        print("세번째반응");
        GD = JsonUtility.FromJson<GoogleData>(json);
        print("네번째반응");
        Debug.Log(GD.result);

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