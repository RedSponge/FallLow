using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string NextLevel;

    public GameObject player;
    public bool IsGameStarted;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        StopGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
        else
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Collectible");
            if (objects.Length == 0)
            {
                SceneManager.LoadScene(NextLevel);
            }
        }
    }

    void StopGame()
    {
        IsGameStarted = false;
        canvas.SetActive(true);
        player.GetComponent<PlaneControlComponent>().enabled = false;
    }

    void StartGame()
    {
        IsGameStarted = true;
        canvas.SetActive(false);
        player.GetComponent<PlaneControlComponent>().enabled = true;
    }
}
