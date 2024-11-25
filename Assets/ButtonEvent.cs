using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField]private GameObject gameOverUI;
    [SerializeField]private GameObject gameWinUI;
    [SerializeField]private VoidParameterEventChannel gameWinEventChannel;
    

    private void Awake()
    {
        
    }
}
