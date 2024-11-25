using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    private AttackPool pool;
    private Collider2D collider;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float time;//音波持续时间
    private WaitForSeconds waveTime;
    [SerializeField] private float waveSpeed;
    public Vector2 direction;
    public int attackDamage;
    public float existTime;

    private void Awake()
    {
        waveTime = new WaitForSeconds(time);
        pool = GameObject.Find("AttackPool").GetComponent<AttackPool>();
        _rigidbody = GetComponent<Rigidbody2D>();
        direction = new Vector2();
    }

    IEnumerator Startwave()
    {
        Vector2 sourceTransformPosition = pool.transform.position;
        _rigidbody.velocity = new Vector2(direction.x * waveSpeed, _rigidbody.velocity.y);
        existTime = 0f;
        yield return waveTime;
        
        setActivefasle();
    }
    

    private void OnEnable()
    {
        StartCoroutine(Startwave());
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        transform.localScale = new Vector3(1f, 1f + 0.5f * Mathf.Sin(Mathf.PI * Time.time), 1f);
    }

    public void setActiveTrue()
    {
        this.transform.SetParent(null);
        this.gameObject.SetActive(true);
    }

    public void setActivefasle()
    {
        this.transform.SetParent(pool.transform);
        StopCoroutine(Startwave());
        _rigidbody.velocity = Vector2.zero;
        pool.MoveInPool(this.gameObject);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (col.TryGetComponent<Enemy_Health>(out Enemy_Health enemy))
            {
                enemy.TakeDamage(attackDamage);
                
                setActivefasle(); 
            }
            else if (col.gameObject.TryGetComponent<FireCentipede>(out FireCentipede boss))
            {
                boss.BeHit((float)attackDamage);
                
                setActivefasle();
            }
        }
        
    }
}
