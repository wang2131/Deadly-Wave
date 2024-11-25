using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPool : MonoBehaviour
{
    public GameObject wavePrefab;
    [SerializeField]private int waveMaxCount;
    public Queue<GameObject> pool;

    private void Awake()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < waveMaxCount; i++)
        {
            GameObject temp = Instantiate(wavePrefab, this.transform);
            temp.GetComponent<WaveScript>().setActivefasle();
            pool.Enqueue(temp);
        }
    }

    public void MoveInPool(GameObject wave)
    {
        wave.transform.position = this.transform.position;
        pool.Enqueue(wave);
    }

    public GameObject MoveOutPool(Vector2 direction)
    {
        if (pool.Count > 0)
        {
            WaveScript wave = pool.Dequeue().GetComponent<WaveScript>();
            wave.direction = direction;
            return wave.GameObject();
        }
        else
        {
            return null;
        }
    }
}
