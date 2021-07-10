using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum gameState
{
    gameplay, gameover
}

public class GameController : MonoBehaviour
{
    TransitionController _transitionController;
    OptionsController _optionsController;

    //public int ammo;
    //public int weaponAmmo;
    //public Text ammoText;
    public Text carrotText;
    private int carrots = 0;

    [Header("Gameplay Config.")]
    public gameState currentState;

    [Header("Ground Config.")]
    public GameObject[] groundPrefab;
    public float groundSpeed;
    public float groundSize;
    public float destroyDistance;

    // Start is called before the first frame update
    void Start()
    {
        _transitionController = FindObjectOfType(typeof(TransitionController)) as TransitionController;
        _optionsController = FindObjectOfType(typeof(OptionsController)) as OptionsController;
        updateCarrotText();
    }

    public void setCarrot(int quantity)
    {
        carrots += quantity;
        updateCarrotText();
    }

    void updateCarrotText()
    {
        carrotText.text = carrots.ToString();
    }

    public void gameOver()
    {
        currentState = gameState.gameover;
        StartCoroutine("gameover");
    }

    IEnumerator gameover()
    {
        _optionsController.StartCoroutine(_optionsController.changeMusic(_optionsController.gameoverClip));
        yield return new WaitForSeconds(0.5f);
        _transitionController.startFade(4);
    }
}
