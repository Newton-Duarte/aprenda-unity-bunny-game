using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class OptionsController : MonoBehaviour
{
    [Header("Slider Config.")]
    [SerializeField] Slider musicVolumeSlider;

    [Header("Audio Config.")]
    [SerializeField] internal AudioSource musicSource;
    [SerializeField] internal AudioSource fxSource;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] internal AudioClip titleClip;
    [SerializeField] internal AudioClip startClip;
    [SerializeField] internal AudioClip gameplayClip;
    [SerializeField] internal AudioClip gameoverClip;

    [Header("HUD Config.")]
    [SerializeField] Text musicSourceValueText;
    [SerializeField] Text fxSourceValueText;

    // Start is called before the first frame update
    void Start()
    {
        initializePlayerPrefs();
        playTitleMusic();
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            toggleOptions();
        }
    }

    private void toggleOptions()
    {
        optionsPanel.SetActive(!optionsPanel.activeInHierarchy);
    }

    private void playTitleMusic()
    {
        musicSource.clip = titleClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    void initializePlayerPrefs()
    {
        if (PlayerPrefs.GetInt("FirstPlaythrough") == 0)
        {
            PlayerPrefs.SetFloat("musicVolume", 1f);
            PlayerPrefs.SetInt("FirstPlaythrough", 1);
        }

        float musicVolume = PlayerPrefs.GetFloat("musicVolume");
        musicSource.volume = musicVolume;
        musicVolumeSlider.value = musicVolume;
        musicSourceValueText.text = Mathf.Round(musicVolume * 100).ToString();
    }

    public IEnumerator changeMusic(AudioClip clip)
    {
        float currentVolume = musicSource.volume;

        for (float v = currentVolume; v > 0; v -= 0.01f)
        {
            musicSource.volume = v;
            yield return new WaitForEndOfFrame();
        }

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();

        for (float v = 0; v < currentVolume; v += 0.01f)
        {
            musicSource.volume = v;
            yield return new WaitForEndOfFrame();
        }
    }

    public void OnMusicVolumeChange()
    {
        float volume = musicVolumeSlider.value;

        musicSourceValueText.text = Mathf.Round(volume * 100).ToString();
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
}
