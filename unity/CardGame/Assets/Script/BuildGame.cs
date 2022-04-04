//게임 시작시 프리팹을 사용한 게임 빌드 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class BuildGame : MonoBehaviour
{
    public static Sprite[] Spr;
    //public static List<Sprite> Spr2 = new List<Sprite>();
    public GameObject canvas;
    GameObject click;

    public GameObject cardPrefab;

    public int level = 8; // 레벨 설정

    // Start is called before the first frame update  
    void Start()
    {
        canvas = GameObject.Find("Canvas");

        click = GameObject.Find("Click"); //MainMenu의 level변수를 가진 오브젝트 가져옴
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

            list[i] = Random.Range(0, x);
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

        Spr = Resources.LoadAll<Sprite>("audi_resize"); //이미지 폴더 내 파일 로드
        
        
        //------------------------------Streaming Asset 접근---------------------------------------
        
/*        BetterStreamingAssets.Initialize(); // 라이브러리 초기화 


        *//*string galleryPath = string.Empty;
        string PersistentDataPath = Application.persistentDataPath;
        galleryPath = PersistentDataPath.Substring(0, PersistentDataPath.IndexOf("Android")) + string.Format("{0}/", "Pictures");
        Debug.Log(galleryPath);*//*

        string[] paths = Directory.GetFiles(Application.streamingAssetsPath, "*.jpg");
        Debug.Log(paths[0]);
        //string[] paths = BetterStreamingAssets.GetFiles("\\" ,"*.jpg", SearchOption.AllDirectories);
        for(int i = 0; i < paths.Length; i++)
        {
            // string path = System.IO.Path.Combine(Application.dataPath,paths[i]);
            // Debug.Log(path);
            byte[] byteTexture = File.ReadAllBytes(paths[i]);
             Debug.Log(paths[i]);
             if (byteTexture.Length > 0)
                {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);
                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Sprite spr = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                spr.name = paths[i];
                Spr2.Add(spr);
            
             }
        }
        */
        //----------------------------------------------------------------------

        




        int[] rand_image = randomCard(Spr.Length, level / 2);
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
            Prefabs[rand_number[i]].transform.GetChild(0).GetComponent<CardInfo>().wordImage = Spr[rand_image[j]];
            Prefabs[rand_number[i + 1]].transform.GetChild(0).GetComponent<CardInfo>().wordImage = Spr[rand_image[j]];
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



}




