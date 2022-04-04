using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    public GameObject Click;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change() // 버튼을 누르면 레벨별로 level변수 지정하고 씬 전환
    {   
        GameObject clickBtn = EventSystem.current.currentSelectedGameObject;
       // Debug.Log(clickBtn.GetComponentInChildren<Text>().text);
        switch (clickBtn.GetComponentInChildren<Text>().text) // 버튼의 text에 따라 level설정
        {
            case "Easy": level = 8; break;
            case "Normal": level = 12;break;
            case "Hard":level = 14;break;
            default:break;
        }
       // Debug.Log(level);
        SceneManager.LoadScene("EasyLevel"); //씬전환
        DontDestroyOnLoad(Click); // 해당 오브젝트를 다른 씬에서 접근하여 사용하기 위하여 오브젝트 파괴하지 않고 유지
    }
}
