using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScneGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundManager>().PlayMusic(Sound.END_MUSIC);
    }

  
}
