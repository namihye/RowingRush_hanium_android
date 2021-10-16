using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class account : MonoBehaviour
{
    public void Login()
    {
        SceneManager.LoadScene("login");
    }

    public void Signup()
    {
        SceneManager.LoadScene("signup");
    }
}
