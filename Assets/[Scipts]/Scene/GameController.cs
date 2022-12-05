using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject onScreenContols;

    public GameObject miniMap;

    // Start is called before the first frame update
    void Awake()
    {
        onScreenContols = GameObject.Find("OnScreenContols");

        onScreenContols.SetActive(Application.isMobilePlatform);

        //FindObjectType<SoundManager>().PlayMusic(Sound.MAIN_MUSIC);

        miniMap = GameObject.Find("MiniMap");
        
    }
    private void Start()
    {
        miniMap?.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            miniMap?.SetActive(!miniMap.activeInHierarchy);           
        }
    }

}
