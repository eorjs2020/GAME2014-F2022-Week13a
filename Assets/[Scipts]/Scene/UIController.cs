using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject miniMap;

    private void Awake()
    {
        miniMap = GameObject.Find("MiniMap");
    }
    public void OnRestartButton_Pressed()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnYButton_Pressed()
    {
        miniMap?.SetActive(!miniMap.activeInHierarchy);
    }
}
