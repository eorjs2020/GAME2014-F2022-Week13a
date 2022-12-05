using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class SoundManager : MonoBehaviour
{
    public List<AudioSource> channels;
    private List<AudioClip> audioClips;

    // Start is called before the first frame update
    void Start()
    {
        channels = GetComponents<AudioSource>().ToList();
        audioClips = new List<AudioClip>();
        InitializeSoundFX();
    }

    // Update is called once per frame
    private void InitializeSoundFX()
    {
        audioClips.Add(Resources.Load<AudioClip>("Audio/jump-sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/hurt-sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/death-sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/main-soundtrack"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/end-soundtrack"));
    }

    public void PlaySoundFX(Sound sound, Chanel channel)
    {
        channels[(int)channel].clip = audioClips[(int)sound];
        channels[(int)channel].Play();
    }

    public void PlayMusic(Sound sound)
    {
        channels[(int)Chanel.MUSIC].clip = audioClips[(int)sound];
        channels[(int)Chanel.MUSIC].volume = 0.25f;
        channels[(int)Chanel.MUSIC].loop = true;
        channels[(int)Chanel.MUSIC].Play();        
    }
}
