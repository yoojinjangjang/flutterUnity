//카드 회전 담당 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rotation1 : MonoBehaviour
{
    private SpriteRenderer rend; //이미지를 표현하는 속성을 가진 객체
    CardInfo thisCard;

    [SerializeField] //private 변수를 inspector에서 접근가능하게 해줌
    private Sprite backSprite;
    [SerializeField]
    int touch_c;

    private bool coroutineAllowed, facedUp;
    GameObject director;





    void Start()
    {

        rend = GetComponent<SpriteRenderer>();
        thisCard = GetComponent<CardInfo>();
        director = GameObject.Find("GameDirector");


        rend.sprite = backSprite;
        coroutineAllowed = true;
        facedUp = false;

        StartCoroutine(start_rotation());


    }

    void Update()
    {
        touch_c = director.GetComponent<GameDirector>().touch_c;
    }



    public void react()
    {


        if (coroutineAllowed && GameDirector.state == GameDirector.STATE.IDLE && facedUp == false && !EventSystem.current.IsPointerOverGameObject()) //flip count 뒤집은 횟수가 2가 되면 클릭해도 뒤집기 금지 ( 패널터치가 아닐경우 ) 
                                                                                                   //if (coroutineAllowed)
        {
            director.GetComponent<GameDirector>().selected_Card(thisCard.wordImage);
            StartCoroutine(RotateCard_Front());
            director.GetComponent<GameDirector>().touch_count();
            GameDirector.state = GameDirector.STATE.HIT;

        }
    }

    private IEnumerator RotateCard_Front()
    {

        coroutineAllowed = false;
        for (float i = 0f; i <= 180f; i += 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {
                rend.sprite = thisCard.wordImage;
                rend.transform.localScale = new Vector2(0.6f, 0.8f); //640 *427 기준
                // 사진의 크기에 따라 다른 스케일이 적용되어야 할 것 같다. 
                // 휴대폰 촬영 사진의 사이즈를 미리 전처리후 입력되어야 할 듯 싶다. 

            }
            yield return new WaitForSeconds(0.01f);
        }
        coroutineAllowed = true;
        facedUp = !facedUp;

    }

    public IEnumerator RotateCard_back()
    {
        director.GetComponent<GameDirector>().Count_minus();
        coroutineAllowed = false;
        yield return new WaitForSeconds(0.7f);

        for (float i = 180f; i >= 0f; i -= 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {
                rend.sprite = backSprite;
                rend.transform.localScale = new Vector2(1f, 1f);
            }
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        facedUp = !facedUp;


    }

    public IEnumerator start_rotation()
    {
        coroutineAllowed = false;
        for (float i = 0f; i <= 180f; i += 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {
                rend.sprite = thisCard.wordImage;
                rend.transform.localScale = new Vector2(0.6f, 0.8f);
            }
            yield return new WaitForSeconds(0.01f);
        }
        coroutineAllowed = true;
        facedUp = !facedUp;

        yield return new WaitForSeconds(1.75f);

        coroutineAllowed = false;

        for (float i = 180f; i >= 0f; i -= 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {
                rend.sprite = backSprite;
                rend.transform.localScale = new Vector2(1f, 1f);

            }
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        facedUp = !facedUp;

    }

}