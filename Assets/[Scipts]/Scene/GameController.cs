using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject onScreenContols;

    // Start is called before the first frame update
    void Awake()
    {
        onScreenContols = GameObject.Find("OnScreenContols");

        onScreenContols.SetActive(Application.isMobilePlatform);

        //FindObjectType<SoundManager>().PlayMusic(Sound.MAIN_MUSIC);
    }


}
