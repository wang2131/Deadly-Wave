using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbars : MonoBehaviour
{
    public Image healthbars;
    public Image fakehealthbars;

    public FireCentipede fireCentipede;
    void Start()
    {
        
    }

    void Update()
    {
        if (fireCentipede != null)
        {
            healthbars.fillAmount = fireCentipede.Hp / fireCentipede.MaxHp;
            if (fakehealthbars.fillAmount > healthbars.fillAmount)
            {
                fakehealthbars.fillAmount -= 0.001f;
            }
            else
            {
                fakehealthbars.fillAmount = healthbars.fillAmount;
            }
        }
    }
}
