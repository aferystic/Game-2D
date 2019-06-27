using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    private AudioClip backgroundNormal;
    [SerializeField]
    private AudioClip backgroundFight;
    [SerializeField]
    private AudioClip backgroundCorka;
    [SerializeField]
    private AudioClip backgroundGameOver;
    private  AudioSource audioSource;
    // Start is called before the first frame update
    private static BackgroundMusic instance;

    public static BackgroundMusic MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BackgroundMusic>();
            }
            return instance;
        }

    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void setToFight()
    {
        audioSource.clip = backgroundFight;
        audioSource.volume = 0.168f;
        audioSource.Play();
    }
    public void setToNormal()
    {
        audioSource.clip = backgroundNormal;
        audioSource.volume = 0.168f;
        audioSource.Play();
    }
    public void setToCorka()
    {
        audioSource.clip = backgroundCorka;
        audioSource.volume = 0.168f;
        audioSource.Play();
    }
    public void setToGameOver()
    {
        audioSource.clip = backgroundGameOver;
        audioSource.volume = 0.468f;
        audioSource.Play();
    }
    public string getAudioClip()
    {
        return audioSource.clip.name;
    }
}
