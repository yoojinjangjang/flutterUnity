using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public GameObject timerBar;
    public GameObject timerGage;
    public GameObject canvas;

    GameObject timer;

    public float height;
    public float width;
    
    

    void Start()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Screen.width / Screen.height;
        buildTimer();


    }

    // Update is called once per frame
    void Update()
    {

        if (timer.transform.localScale.y > 0 && Time.timeScale == 1)
        {
            timer.transform.localScale -= new Vector3(0, 0.00005f, 0); // x축 조절로 시간 조절
        }
        else if(timer.transform.localScale.y <= 0)
        {
            timer.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
    }

    void buildTimer()
    {
        timer = Instantiate(timerBar) as GameObject;
        timer.transform.SetParent(timerGage.transform);
        timerGage.SetActive(true);


        // 타이머 위치와 크기 해상도에 맞게 설정하는 부분 
        timerGage.transform.position = new Vector3((-1 * width / 2)+1f, (-1 * height / 2)+1.3f);
        float calWidth = width / 17;
        float calHeight = height / 1.2f;
        timerGage.transform.localScale = new Vector3(1, 1, 1);
        Vector3 test = timerGage.GetComponent<SpriteRenderer>().bounds.size;
        timerGage.transform.localScale = new Vector3(calWidth / test.x, calHeight / test.y,1);
        //----------------요기까지 -----------

        timer.transform.localPosition = new Vector3(0f, 0.2f,0);
        timer.transform.localScale = new Vector2(0.8f, 1f);
    }



}
