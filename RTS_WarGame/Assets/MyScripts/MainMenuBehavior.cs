using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour {

    public Button StartButton;
    public Button OptionsButton;
    public Button CreditsButton;
    public Button QuitButton;

    public GameObject MainMenuBackGround;
    public GameObject OptionsBackGround;
    public GameObject CreditsBackGround;

    public Button[] Back;

    //public Scrollbar MusicBar;
    //public Scrollbar SFXBar;

    private AudioSource _AS;
    public AudioClip _ACButton;

    private XmlManagerScript Data;
    private float musicVolume;
    private float SFXvolume;

    void Start()
    {
        Data = GetComponent<XmlManagerScript>();
        StartButton.onClick.AddListener(playGame);
        OptionsButton.onClick.AddListener(OptionsScreen);
        CreditsButton.onClick.AddListener(CreditsScreen);
        QuitButton.onClick.AddListener(QuitGame);

        for (int i = 0; i < Back.Length; i++)
        {
            if(Back[i] != null)
            {
                Back[i].onClick.AddListener(BackToMain);
            }
        }

        MainMenuBackGround.SetActive(true);
        OptionsBackGround.SetActive(false);
        CreditsBackGround.SetActive(false);

        if(Data.LoadData())
        {
            musicVolume = Data.tempHold.MusicVolume;
            SFXvolume = Data.tempHold.SoundEffectsVolume;
            //MusicBar.value = musicVolume;
            //SFXBar.value = SFXvolume;
        }
        else
        {
            musicVolume = 0.5f;
            SFXvolume = 0.5f;
            //MusicBar.value = musicVolume;
            //SFXBar.value = SFXvolume;
        }
        _AS = GetComponent<AudioSource>();
        _AS.volume = musicVolume;
        _AS.Play();
    }

    void playGame()
    {
        Data.SaveData(musicVolume, SFXvolume);
        _AS.Stop();
        SceneManager.LoadScene("MainGame");
    }

    void OptionsScreen()
    {
        _AS.PlayOneShot(_ACButton, SFXvolume);
        MainMenuBackGround.SetActive(false);
        OptionsBackGround.SetActive(true);
        CreditsBackGround.SetActive(false);
    }

    void CreditsScreen()
    {
        _AS.PlayOneShot(_ACButton, SFXvolume);
        MainMenuBackGround.SetActive(false);
        OptionsBackGround.SetActive(false);
        CreditsBackGround.SetActive(true);
    }

    void QuitGame()
    {
        Data.SaveData(musicVolume, SFXvolume);
        _AS.Stop();
        Debug.Log("You Pressed QUIT!");
        Application.Quit();
    }

    void BackToMain()
    {
        _AS.PlayOneShot(_ACButton, SFXvolume);
        MainMenuBackGround.SetActive(true);
        OptionsBackGround.SetActive(false);
        CreditsBackGround.SetActive(false);
    }

    public void MusicValueChange(float value)
    {
        musicVolume = value;
        _AS.volume = value;
    }

    public void SFXValueChange(float value)
    {
        SFXvolume = value;
    }
}

public class OnScrollChange : DataValueHold
{
    
}

