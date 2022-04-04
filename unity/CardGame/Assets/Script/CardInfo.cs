//카드 정보를 담을 클래스 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class CardInfo : MonoBehaviour
{
    public string cardName = null;
    //public int cardStatus =0;
    

    private SpriteRenderer rend;
    [SerializeField] //private 변수를 inspector에서 접근가능하게 해줌
    public Sprite wordImage;

   void Awake()
    {

    }

   

}

