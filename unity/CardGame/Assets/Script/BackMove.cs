using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMove : MonoBehaviour
{
    public GameObject backPanel;
    // Start is called before the first frame update
    void Start()
    {
        backPanel.SetActive(false);
        buildBack();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        backPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void NoQuit()
    {
        Time.timeScale = 1;
        backPanel.SetActive(false);
    }
    public void YesQuit()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    void buildBack()
    {
        //transform.SetParent(GameObject.Find("Canvas").transform);
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
        transform.position = new Vector3((-1f * width / 2) + 1f, (-1f * height / 2) + 0.5f);
    }


}