using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTime : MonoBehaviour
{
    AudioClip BGM1;
    AudioClip BGM2;
    AudioClip Boss1;
    AudioClip Boss2;

    public AudioSource BGM;

    public GameObject leftAirWall;
    public GameObject rightAirWall;

    public GameObject BossHealthBars;

    public bool isBossTiem;
    public FireCentipede fireCentipede;
    void Awake()
    {
        BGM1 = Resources.Load<AudioClip>("Sounds/BGM1");
        Boss1 = Resources.Load<AudioClip>("Sounds/Boss1");
    }

    void Start()
    {
        isBossTiem = false;
    }


    void Update()
    {
        if (fireCentipede != null)
        {
            if (fireCentipede.isDead)
            {
                leftAirWall.SetActive(false);
                rightAirWall.SetActive(false);
                BossHealthBars.SetActive(false);
                Destroy(gameObject);
                BGM.clip = BGM1;
                BGM.Play();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            isBossTiem = true;
            leftAirWall.SetActive(true);
            rightAirWall.SetActive(true);
            BossHealthBars.SetActive(true);
            if (fireCentipede != null)
            {
                BGM.clip = Boss1;
                BGM.Play();
            }
        }
    }
}
