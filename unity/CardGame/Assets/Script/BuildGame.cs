//게임 시작시 프리팹을 사용한 게임 빌드 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;

public class BuildGame : MonoBehaviour
{
    public static Sprite[] Spr;
    List<Sprite> userSpr = new List<Sprite>();
    //public static List<Sprite> Spr2 = new List<Sprite>();
    public GameObject canvas;
    GameObject click;
    
    
    
    GameObject game;
    MySqlConnection conn = null;
    private string sqlDBip = "spring-abeec.ciqmuaoilajl.us-east-1.rds.amazonaws.com";
    private string sqlDBname = "abeec";
    private string sqlDBid = "yoojinjangjang";
    private string sqlDBpw = "aledma123!";





    public GameObject cardPrefab;

    public int level = 8; // 레벨 설정

    // Start is called before the first frame update  
    void Start()
    {
        canvas = GameObject.Find("Canvas");

        click = GameObject.Find("Click"); //MainMenu의 level변수를 가진 오브젝트 가져옴
        game = GameObject.Find("Game"); // MainMenu의 id변수를 가진 오브젝트 가져옴 
        level = click.GetComponent<SceneChange>().level; // 해당 level로 현재 level 설정 
        Destroy(click);
        Debug.Log(level);
        buildgame(level); //게임 난이도에 따라 매개변수 넘기는 값 변경할 예정
    }

    // Update is called once per frame
    void Update()
    {

    }


    private int[] randomCard(int x, int y)// x개 중에 무작위로 y개 뽑는 함수
    {
        int[] list = new int[y];

        for (int i = 0; i < y; i++)
        {

            list[i] = UnityEngine.Random.Range(0, x);
            for (int j = 0; j < i; j++)
            {
                if (list[i] == list[j])
                {
                    i -= 1;
                    break;
                }
            }
        }
        return list;
    }

    private List<GameObject> prefab_generator(int level)
    {
        List<GameObject> Prefabs = new List<GameObject>();

       // Spr = Resources.LoadAll<Sprite>("audi_resize"); //이미지 폴더 내 파일 로드


        userSpr = dbConnect(game.GetComponent<Game>().id);
        
        for(int i = 0; i < userSpr.Count; i++)
        {
            Debug.Log(userSpr[i].name);
        }

        int[] rand_image = randomCard(userSpr.Count, level / 2);
        int[] rand_number = randomCard(level, level);

        for (int i = 0; i < level; i++) //카드 프리팹 생성
        {
            GameObject card = Instantiate(cardPrefab) as GameObject;
            card.transform.SetParent(canvas.transform);
            Prefabs.Add(card);

        }

        for (int i = 0; i < level; i++) // 각 카드 프리팹.name에 랜덤 번호 부여
        {
            Prefabs[i].name = rand_number[i].ToString();
        }


        int j = 0;
        for (int i = 0; i < level; i += 2)
        {
            Prefabs[rand_number[i]].transform.GetChild(0).GetComponent<CardInfo>().wordImage = userSpr[rand_image[j]];
            Prefabs[rand_number[i + 1]].transform.GetChild(0).GetComponent<CardInfo>().wordImage = userSpr[rand_image[j]];
            j++;
        }


        return Prefabs;
    }

    private void buildgame(int level) //프리팹 생성하고, 배치하는 함수
    {
        GameDirector.state = GameDirector.STATE.WAIT;
        List<GameObject> Prefabs = prefab_generator(level);
        if (level == 8)
        {
            StartCoroutine(buildgameEasy(Prefabs));
        }
        else if (level == 12)
        {
            StartCoroutine(buildGameNormal(Prefabs));
        }
        else if (level == 14)
        {
            StartCoroutine(buildGameHard(Prefabs));
        }




    }
    public IEnumerator buildgameEasy(List<GameObject> Prefabs)
    {
        int number = 3;
        float px = -2.0f; //프리팹 놓을 좌표 x
        float py = 2.5f;  //프리팹 놓을 좌표 y

        float px1 = px;
        int p = 0;
        for (int j = 0; j < 2; j++)
        {
            float py1 = py;

            for (int i = 0; i < number; i++)
            {
                //GameObject card = Instantiate(cardPrefab) as GameObject; //프리팹 생성
                Prefabs[p].transform.position = new Vector3(px1, py1, 0); //프리팹 배치하기

                py1 -= 2.5f;
                p += 1;
            }
            px1 += 4.2f;
        }

        float py2 = 1.16f;
        float px2 = px + 2.1f;
        for (int i = 0; i < number - 1; i++)
        {
            //GameObject card = Instantiate(cardPrefab) as GameObject;
            Prefabs[p].transform.position = new Vector3(px2, py2, 0);


            py2 -= 2.5f;
            p += 1;
        }
        yield return new WaitForSeconds(2.0f);
        GameDirector.state = GameDirector.STATE.IDLE;
    }

    public IEnumerator buildGameNormal(List<GameObject> Prefabs)
    {
        int number = 3;
        float px = -2.0f; //프리팹 놓을 좌표 x
        float py = 2.5f;  //프리팹 놓을 좌표 y

        float px1 = px;
        int p = 0;
        for (int j = 0; j < 2; j++)
        {
            float py1 = py;

            for (int i = 0; i < number; i++)
            {
                //GameObject card = Instantiate(cardPrefab) as GameObject; //프리팹 생성
                Prefabs[p].transform.position = new Vector3(px1, py1, 0); //프리팹 배치하기



                py1 -= 2.5f;
                p += 1;
            }


            px1 += 4.6f;
        }

        float px2 = px - 2.3f;
        for (int j = 0; j < 3; j++)
        {
            float py2 = 1.16f;

            for (int i = 0; i < number - 1; i++)
            {
                //GameObject card = Instantiate(cardPrefab) as GameObject;
                Prefabs[p].transform.position = new Vector3(px2, py2, 0);


                py2 -= 2.5f;
                p += 1;

            }
            px2 += 4.6f;


        }
        yield return new WaitForSeconds(2.0f);
        GameDirector.state = GameDirector.STATE.IDLE;
    }

    public IEnumerator buildGameHard(List<GameObject> Prefabs)
    {
        int number = 3;
        float px = -2.0f; //프리팹 놓을 좌표 x
        float py = 2.5f;  //프리팹 놓을 좌표 y

        float px1 = px;
        int p = 0;
        for (int j = 0; j < 2; j++)
        {
            float py1 = py;

            for (int i = 0; i < number; i++)
            {
                //GameObject card = Instantiate(cardPrefab) as GameObject; //프리팹 생성
                Prefabs[p].transform.position = new Vector3(px1, py1, 0); //프리팹 배치하기



                py1 -= 2.5f;
                p += 1;
            }


            px1 += 4.6f;
        }

        float px2 = px - 2.3f;
        for (int j = 0; j < 2; j++)
        {
            float py2 = 1.16f;

            for (int i = 0; i < number - 1; i++)
            {
                //GameObject card = Instantiate(cardPrefab) as GameObject;
                Prefabs[p].transform.position = new Vector3(px2, py2, 0);


                py2 -= 2.5f;
                p += 1;

            }
            px2 += 9.2f;


        }

        float px3 = px + 2.3f;
        float py3 = 1.16f + 2.5f;

        for (int j = 0; j<4; j++)
        {
            Prefabs[p].transform.position = new Vector3(px3, py3, 0);
            py3 -= 2.5f;
            p += 1;
        }
        yield return new WaitForSeconds(2.0f);
        GameDirector.state = GameDirector.STATE.IDLE;
    }

    

    // db 연결하여 학습한 단어이미지 받아오는 부분 
    public List<Sprite> dbConnect(string id)
    {
        List<Sprite> spr = new List<Sprite>();

        string sqlConn = string.Format("server={0};uid={1};pwd={2};database={3};charset=utf8 ;",
            sqlDBip, sqlDBid, sqlDBpw, sqlDBname);
        //접속 확인하기
        try
        {
            conn = new MySqlConnection(sqlConn);
            conn.Open();
            Debug.Log("SQL의 접속 상태 : " + conn.State); //접속이 되면 OPEN이라고 나타남
            
        }
        catch (Exception msg)
        {
            Debug.Log("error");
            Debug.Log(msg); //기타다른오류가 나타나면 오류에 대한 내용이 나타남
        }



        // 아래 코드 이미지 구성시 삭제
        List<string> quarys = new List<string>();

        // 학습한 영단어 리스트 이미지 구성시 사용
        //List<string> englishs = new List<string>();

        id = "yoojinjangjang";
        string quary = string.Format("select english from my_voca where user_id = '{0}';", id);
        MySqlCommand command = new MySqlCommand(quary, conn);
        MySqlDataReader rdr = command.ExecuteReader();
        while (rdr.Read())
        {
            for(int i = 0; i < rdr.FieldCount; i++)
            {
                
                Debug.Log(rdr[i]);
                // 아래 코드 이미지 구성시 삭제 
                string quary2 = string.Format("select id from voca where english = '{0}';", rdr[i]);
                quarys.Add(quary2);

               // englishs.Add((string)rdr[i]); // 이미지 구성시 사용
            }
        }
        rdr.Close();


      /*    이미지 모두 구성완료한 후 실행할 코드 
       
         for (int i = 0; i < englishs.Count; i++)
        {   
            string name = string.Format("images/{0}",englishs[i]);
            spr.Add(Resources.Load<Sprite>(name));  // english 이름으로 image 가져오기
        }
*/

        // 테스트를 위한 코드 이미지 구성시 삭제 함 
        for (int i = 0; i < quarys.Count; i++)
        {
            command.CommandText = quarys[i];

            rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                Debug.Log((int)rdr[0]);
                if ((int)rdr[0] > 901 && (int)rdr[0] < 960)
                {
                    string name = string.Format("images/abeec ({0})", (int)rdr[0]-900);
                    spr.Add(Resources.Load<Sprite>(name));
                }

                

            }
            rdr.Close();
            
        }




        conn.Close();
        Debug.Log("SQL의 접속 상태 : " + conn.State);
        
        return spr;
    }
}




