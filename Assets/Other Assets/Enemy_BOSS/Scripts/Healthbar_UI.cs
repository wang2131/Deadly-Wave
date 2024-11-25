using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_UI : MonoBehaviour
{
    public Image Border;
    public Image healthbars;
    public Image fakehealthbars;

    PlayerController Player;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        life();
    }
    public void life() 
    {
        healthbars.fillAmount = (float)Player.nowHp / (float)Player.maxHp;
        if (fakehealthbars.fillAmount > healthbars.fillAmount)
        {
            fakehealthbars.fillAmount -= 0.004f;
            
        }
        else
        {
            fakehealthbars.fillAmount = healthbars.fillAmount;
        }
    } 
}
