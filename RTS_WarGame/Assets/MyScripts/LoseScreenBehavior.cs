using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScreenBehavior : MonoBehaviour {

    public Button MainMenuButton;
    private AudioSource _AS;
    private XmlManagerScript Data;
    private float musicVolume;
    private float SFXvolume;

    void Start()
    {
        Data = GetComponent<XmlManagerScript>();
        MainMenuButton.onClick.AddListener(MainMenuLoad);
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
        _AS = GetComponent<AudioSource>();
        _AS.volume = musicVolume;
        _AS.Play();
    }

    void MainMenuLoad()
    {
        _AS.Stop();
        SceneManager.LoadScene("MainMenu");
    }
}
