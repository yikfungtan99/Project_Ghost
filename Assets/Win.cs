﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    
    public void Return()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);

    }
}