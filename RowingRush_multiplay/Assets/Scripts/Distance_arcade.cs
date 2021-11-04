using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Distance_arcade : MonoBehaviour
{
    public AudioSource audioSource;

    private GSmanager scriptG;

    public GameObject paddle;
    public GameObject paddle1;

    public GameObject _Boat;
    public GameObject _Boat2;
    public GameObject _Boat3;
    public GameObject _Boat4;
    public GameObject _Boat5;
    
    public GameObject FinishMenu;
    public GameObject Ranking;
    public GameObject StartUI;
    public GameObject _Time;
    public GameObject Canvas;


    public TextMeshProUGUI countText;
    public TextMeshProUGUI curTimeText;
    public TextMeshProUGUI curDistanceText;
    public TextMeshProUGUI curSpeedText;
    public TextMeshProUGUI RecordText;
    public TextMeshProUGUI MainText;

    public TextMeshProUGUI BestRecord;
    public TextMeshProUGUI SecondRecord;
    public TextMeshProUGUI ThirdRecord;

    float curTime;
    float BoatDistance;
    float BoatDistance2;
    float BoatDistance3;
    float BoatDistance4;
    float BoatDistance5;
    float maxBoat;
    bool isMode;


    float speed;
    float avgSpeed;
    int TargetDistance;
    int target;

    Vector3 FirstDistance = new Vector3(0, 0, 0);
    Vector3 currentPosition;
    Vector3 oldPosition;

    bool isFinishMenu = true;

    IEnumerator StartCount()
    {
        StartUI.SetActive(true);
        //countdown 3���� ����
        countText.text = "3";
        countText.gameObject.SetActive(true);
        audioSource.Play();

        //1�� �� countdowm 2�� ����
        yield return new WaitForSecondsRealtime(1);
        
        countText.gameObject.SetActive(false);
        countText.text = "2";
        countText.gameObject.SetActive(true);

        //1�� �� countdowm 1�� ����
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);
        countText.text = "1";
        countText.gameObject.SetActive(true);

        //1�� �� countdown go�� ����
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);
        countText.text = "GO!";
        countText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);

        StartCoroutine("Timer");
        _Boat.GetComponent<BoatControl>().enabled = true;
        _Boat2.GetComponent<Boat2Control>().enabled = true;
        _Boat3.GetComponent<Boat2Control>().enabled = true;
        _Boat4.GetComponent<Boat2Control>().enabled = true;
        _Boat5.GetComponent<Boat2Control>().enabled = true;

    }

    IEnumerator Timer()
    {
        _Time.SetActive(true);

        while (true)
        {
            curTime += Time.deltaTime;
            curTimeText.text = string.Format("{0:00}:{1:00}:{2:00}",
              (int)curTime / 3600 % 3600, (int)curTime / 60 % 60, curTime % 60);

            yield return null;
        }
    }

    IEnumerator CalDistance()
    {
        curDistanceText.text = BoatDistance.ToString("F0") + "m";
        yield return null;
    }

    IEnumerator CalSpeed()
    {
        currentPosition = transform.position;
        float distance = Vector3.Distance(oldPosition, currentPosition);
        speed = distance / Time.deltaTime;
        yield return new WaitForSecondsRealtime(1);
        curSpeedText.text = speed.ToString("F0");
        yield return new WaitForSecondsRealtime(1);
        oldPosition = currentPosition;
    }

    IEnumerator curScore()
    {
        isFinishMenu = false;
        MainText.text = (TargetDistance / 1000).ToString() + "km RANKING TOP3";
        avgSpeed = BoatDistance / curTime;

        float[] alldistance = {BoatDistance, BoatDistance2, BoatDistance3, BoatDistance4, BoatDistance5};
        Array.Sort(alldistance);
        
        float BestSpeed = alldistance[4] / curTime;
        float SecondSpeed = alldistance[3] / curTime;
        float ThirdSpeed = alldistance[2] / curTime;

        RecordText.text = string.Format("distance {0}    speed {1}m/s",
            BoatDistance.ToString("F0"), avgSpeed.ToString("F1"));

        BestRecord.text = string.Format("distance {0}    speed {1}m/s",
            alldistance[4].ToString("F0"), BestSpeed.ToString("F1"));

        SecondRecord.text = string.Format("distance {0}    speed {1}m/s",
            alldistance[3].ToString("F0"), SecondSpeed.ToString("F1"));

        ThirdRecord.text = string.Format("distance {0}    speed {1}m/s",
            alldistance[2].ToString("F0"), ThirdSpeed.ToString("F1"));

        

        yield return new WaitForSecondsRealtime(3);
        FinishMenu.SetActive(false);
        Canvas.SetActive(false);
        Ranking.SetActive(true);

    }

    IEnumerator TimeRotation(GameObject target, float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            target.transform.Rotate(0f, 1f, 0f);
            yield return new WaitForSeconds(time);
            target.transform.Rotate(-0f, -1f, -0f);
            yield return new WaitForSeconds(time);
            target.transform.Rotate(-0f, -1f, -0f);
            yield return new WaitForSeconds(time);
            target.transform.Rotate(0f, 1f, 0f);

            if (speed <= 2)
            {
                yield break;
            }
        }

    }

    IEnumerator TimeRotation1(GameObject target, float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            target.transform.Rotate(-0f, -1f, -0f);
            yield return new WaitForSeconds(time);
            target.transform.Rotate(0f, 1f, 0f);
            yield return new WaitForSeconds(time);
            target.transform.Rotate(0f, 1f, 0f);
            yield return new WaitForSeconds(time);
            target.transform.Rotate(-0f, -1f, -0f);


            if (speed <= 2)
            {
                yield break;
            }
        }

    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isMode = (PlayerPrefs.GetString("userMode")=="exercise");
        if(isMode){
            _Boat2.SetActive(false);
            _Boat3.SetActive(false);
            _Boat4.SetActive(false);
            _Boat5.SetActive(false);
        };
        TargetDistance = PlayerPrefs.GetInt("TargetDistance");
        StartCoroutine("StartCount");
        oldPosition = transform.position;
        FirstDistance = _Boat.transform.position;
    }


    public void FixedUpdate()
    {
        if (_Time.activeSelf == true)
        {
            BoatDistance = Vector3.Distance(FirstDistance, _Boat.transform.position)*2;
            BoatDistance2 = Vector3.Distance(FirstDistance, _Boat2.transform.position)*2;
            BoatDistance3 = Vector3.Distance(FirstDistance, _Boat3.transform.position)*2;
            BoatDistance4 = Vector3.Distance(FirstDistance, _Boat4.transform.position)*2;
            BoatDistance5 = Vector3.Distance(FirstDistance, _Boat5.transform.position)*2;

            maxBoat = Mathf.Max(BoatDistance, BoatDistance2, BoatDistance3, BoatDistance4, BoatDistance5);

            StartCoroutine("CalDistance");
            StartCoroutine("CalSpeed");


            if (speed > 0.2)
            {
                StartCoroutine(TimeRotation(paddle, 1f));
                StartCoroutine(TimeRotation1(paddle1, 1f));

            }
        }


    }

    void LateUpdate()
    {
        //�ÿ� �������δ� TargetDistance/10 m ���� �� ���ߴ� �ɷ� �����س��� - ���� ���� �ʿ�
        
        if (maxBoat > TargetDistance/10 && isFinishMenu == true)
        {
            _Boat.GetComponent<BoatControl>().enabled = false;
            _Boat2.GetComponent<Boat2Control>().enabled = false;
            _Boat3.GetComponent<Boat2Control>().enabled = false;
            _Boat4.GetComponent<Boat2Control>().enabled = false;
            _Boat5.GetComponent<Boat2Control>().enabled = false;

            StopCoroutine("Timer");
            StopCoroutine("CalDistance");
            StopCoroutine("CalSpeed");
            FinishMenu.SetActive(true);
            StartCoroutine("curScore");
            
            scriptG = GameObject.Find("GSmanager").GetComponent<GSmanager>();
            scriptG.SetValue((TargetDistance / 1000).ToString(), curTimeText.text, avgSpeed.ToString("F0"));
        }

    }

}
