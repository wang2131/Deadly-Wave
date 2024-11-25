using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    Animator ani;
    AudioSource music;
    public float Speed;
    public int Damge;
    public bool IsBoom;
    void Awake()
    {
        ani = GetComponent<Animator>();
        music = GetComponent<AudioSource>();

        Speed = 5;
        Damge = 15;
        IsBoom = false;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!IsBoom) 
        {
            Move();
        }
    }
    public void Move() 
    {
        transform.position += Speed *Vector3.down * Time.deltaTime;
    }
    public void ChangePosition() 
    {
        transform.position = new Vector3(transform.position.x, -3.35f, 0);
    }
    public void Destroy() 
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            IsBoom = true;
            transform.position = new Vector3(transform.position.x, -1.6f, 0);
            ani.Play("boom");
            music.Play();
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("boom")) 
            {
                music.Play();
            }
        }
        else if (collision.CompareTag("Player")) 
        {
            IsBoom = true;
            transform.position = new Vector3(transform.position.x, -1.6f, 0);
            ani.Play("boom");
            music.Play();
            if (transform.position.x < collision.transform.position.x)
            {
                collision.GetComponent<PlayerController>().BeHit(Damge);
            }
            else if (transform.position.x >= collision.transform.position.x)
            {
                collision.GetComponent<PlayerController>().BeHit(Damge);
            }
        }
    }
}
