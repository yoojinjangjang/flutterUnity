using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGround : MonoBehaviour
{
    // Start is called before the first frame update

    public float height;
    public float width;

    public GameObject background;


    AudioSource backAudio;

    void Start()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Screen.width / Screen.height;
        backAudio = this.GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().name != "GameOver")
        {
            backAudio.loop = true;
        }
        
        backAudio.Play();
        buildBackGround();
    }

    private void buildBackGround()
    {
        background.transform.position = new Vector3(0, 0, 1);
        
        background.transform.localScale = new Vector3(1, 1, 1);
        Vector3 test = background.GetComponent<SpriteRenderer>().bounds.size;
        background.transform.localScale = new Vector3(width/ test.x, height/ test.y, 1);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
