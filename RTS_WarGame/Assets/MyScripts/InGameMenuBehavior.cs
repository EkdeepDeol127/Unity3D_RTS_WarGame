using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenuBehavior : MonoBehaviour {

    public GameObject InGameMenu;
    public GameObject InGameUI;

    public Button MainMenuButton;
    public Button BackButton;

    private bool PausedActive;

    private AudioSource _AS;
    public AudioClip[] _AC;
    public AudioClip _ACButton;

    private int SongQueue;
    private XmlManagerScript Data;
    private float musicVolume;
    private float SFXvolume;

    void Start ()
    {
        Data = GetComponent<XmlManagerScript>();
        PausedActive = false;
        MainMenuButton.onClick.AddListener(LoadMainMenu);
        BackButton.onClick.AddListener(BackToGame);
        InGameMenu.SetActive(false);
        InGameUI.SetActive(true);
        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        if (Data.LoadData())
        {
            musicVolume = Data.tempHold.MusicVolume;
            SFXvolume = Data.tempHold.SoundEffectsVolume;
        }
        else
        {
            musicVolume = 0.5f;
            SFXvolume = 0.5f;
        }

        SongQueue = 0;
        _AS = GetComponent<AudioSource>();
        _AS.volume = musicVolume;
        _AS.PlayOneShot(_AC[SongQueue]);
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PausedActive == false)
            {
                PauseMenu();
            }
            else
            {
                BackToGame();
            }
        }
        if(!_AS.isPlaying)
        {
            SongQueue++;
            if(SongQueue < _AC.Length)//check
            {
                _AS.PlayOneShot(_AC[SongQueue]);
            }
            else
            {
                SongQueue = 0;
                _AS.PlayOneShot(_AC[SongQueue]);
            }
        }
        if(Input.GetKeyDown(KeyCode.N))//just if you want to see the win/lose screens
        {
            _AS.Stop();
            SceneManager.LoadScene("WinScreen");
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            _AS.Stop();
            SceneManager.LoadScene("LoseScreen");
        }
    }

    void PauseMenu()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            _AS.Stop();
        }
        InGameMenu.SetActive(true);
        InGameUI.SetActive(false);
        _AS.PlayOneShot(_ACButton, SFXvolume);
    }

    void BackToGame()
    {
        _AS.PlayOneShot(_ACButton, SFXvolume);
        InGameMenu.SetActive(false);
        InGameUI.SetActive(true);
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
            _AS.Play();
        }
    }

    void LoadMainMenu()
    {
        _AS.Stop();
        SceneManager.LoadScene("MainMenu");
    }
}
