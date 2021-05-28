using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    //public int ammo;
    //public int weaponAmmo;

    //public Text ammoText;
    public Text carrotText;
    private int carrots = 0;

    [Header("Ground Config.")]
    public GameObject[] groundPrefab;
    public float groundSpeed;
    public float groundSize;
    public float destroyDistance;

    // Start is called before the first frame update
    void Start()
    {
        updateCarrotText();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene("gameover");
    }
}
