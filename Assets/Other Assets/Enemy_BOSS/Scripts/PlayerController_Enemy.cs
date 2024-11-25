using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController_Enemy : MonoBehaviour
{
    PlayerCharacter player;
    void Awake()
    {
        player = GetComponent<PlayerCharacter>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        player.Move(h);
        player.InAttack();
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            player.music.Pause();
            player.Jump();
        }
        if (Input.GetKeyDown(KeyCode.J)) 
        {
            player.StartSliding();
        }
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            player.Attack();
        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            player.StartBackStep();
        }
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            player.HeavyAttack();
        }
    }
}
