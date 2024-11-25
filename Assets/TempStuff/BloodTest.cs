using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTest : MonoBehaviour
{
    private Animator ani;

    private PlayerInput input;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        input = GetComponentInParent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.isAttack)
        {
            ani.Play("Blood");
        }
    }
}
