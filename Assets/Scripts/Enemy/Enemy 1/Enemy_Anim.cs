using System;
using UnityEngine;

public enum State
{
    Idle,
    Walk,
    Attack,
    Charge
}
public class Enemy_Anim : MonoBehaviour
{
    Transform E_tra;
    Rigidbody2D E_rig;
    private Transform P_tra;
    public Quaternion E_rot;
    Animator E_ani;
    public Vector3 E_tra_left;
    public Vector3 E_tra_right;
    private PolygonCollider2D EnemyAttack;
    private PlayerController player;
    
    private float attackRange = 2f;
    private float ChargeRange = 7f;
    private float attackTimer = 1f;
    private float attackCooldown = 1f;

    public State currentState;
    private static readonly int E_Idle = Animator.StringToHash("Idle");
    private static readonly int E_Walk = Animator.StringToHash("Walk");
    private static readonly int E_Attack = Animator.StringToHash("Attack");
    private static readonly int E_Charge = Animator.StringToHash("Charge");

    
    public float Walk_speed;//巡逻速度
    public float Charge_Speed;//奔向玩家速度

    public bool IsRotate = false;//是否需要旋转
    public float Stance_time;//站立的时间
    public string Toword="Right";
    
    void Awake()
    {
        P_tra = GameObject.FindWithTag("Player").GetComponent<Transform>();
        E_tra = GetComponent<Transform>();
        E_rig = GetComponent<Rigidbody2D>();
        E_ani = GetComponent<Animator>();
        EnemyAttack = GetComponentInChildren<PolygonCollider2D>();
        E_rot = E_tra.rotation;
        var position = E_tra.position;
        E_tra_left = new Vector3(position.x - 5,position.y,position.z);
        E_tra_right = new Vector3(position.x + 5,position.y,position.z);
        Walk_speed = 3;
        Charge_Speed = 7;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        currentState = State.Walk;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Walk:
                Walk();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Charge:
                Charge();
                break;
        }
    }

    void Idle()
    {
        E_ani.SetBool(E_Idle,true);
        E_ani.SetBool(E_Walk,false);
        E_ani.SetBool(E_Charge,false);
        E_ani.SetBool(E_Attack,false);


        Stance_time += Time.deltaTime;
        if (Stance_time>2f)
        {
            if (Toword.Equals("Right"))
            {
                E_rot.y = 180;
                E_tra.rotation = E_rot;
                Stance_time = 0f;
                Toword = "Left";
                currentState = State.Walk;
                return;
            }

            if (Toword.Equals("Left"))
            {
                E_rot.y = 0;
                E_tra.rotation = E_rot;
                currentState = State.Walk;
                Stance_time = 0f;
                Toword = "Right";
                return;
            }
        }
        // 怪物处于空闲状态
        // 可能需要随机转移到其他状态

    }

    void Walk()
    {
        E_ani.SetBool(E_Walk, true);
        E_ani.SetBool(E_Idle,false);
        E_ani.SetBool(E_Charge,false);
        E_ani.SetBool(E_Attack,false);


        // 怪物正在移动
        if (Toword.Equals("Right"))
        {
            E_tra.position = Vector3.MoveTowards(E_tra.position, E_tra_right, Walk_speed*Time.deltaTime);
            if (E_tra.position.x-E_tra_right.x>-0.1)
            {
                IsRotate = true;
                currentState = State.Idle;
            }
        }
        if (Toword.Equals("Left"))
        {
            
            E_tra.position = Vector3.MoveTowards(E_tra.position, E_tra_left, Walk_speed*Time.deltaTime);
            if (E_tra.position.x-E_tra_left.x<0.1)
            {
                IsRotate = true;
                currentState = State.Idle;
            }
        }

        // 可能需要根据距离和方向转移到其他状态
    }

    void Attack()
    {
        E_ani.SetBool(E_Attack,true);
        E_ani.SetBool(E_Idle,false);
        E_ani.SetBool(E_Charge,false);
        E_ani.SetBool(E_Walk,false);

        /*if (Vector2.Distance(E_tra.position, P_tra.position)>attackRange+1&&Vector2.Distance(E_tra.position, P_tra.position)<ChargeRange)
        {
            currentState = State.Charge;
        }*/

        AttackPlayer();
        // 怪物正在攻击玩家
        // 可能需要根据距离和方向转移到其他状态
    }



    void Charge()
    {
        E_ani.SetBool(E_Charge,true);
        E_ani.SetBool(E_Idle,false);
        E_ani.SetBool(E_Walk,false);
        E_ani.SetBool(E_Attack,false);
        if (Math.Abs(P_tra.position.x-E_tra.position.x)<7&&Math.Abs(P_tra.position.x-E_tra.position.x)>attackRange)
        {
            if ((P_tra.position.x-E_tra.position.x)>0)
            {
                E_rot.y = 0;
                E_tra.rotation = E_rot; 
                E_tra.position = Vector3.MoveTowards(E_tra.position, new Vector3(P_tra.position.x,E_tra.position.y,0), Charge_Speed*Time.deltaTime);
            }
            else
            {
                E_rot.y = 180;
                E_tra.rotation = E_rot; 
                E_tra.position = Vector3.MoveTowards(E_tra.position, new Vector3(P_tra.position.x,E_tra.position.y,0), Charge_Speed*Time.deltaTime);
            }
            //Debug.Log(Vector2.Distance(E_tra.position, P_tra.position));
            //E_tra.position = Vector3.MoveTowards(E_tra.position, new Vector3(P_tra.position.x,E_tra.position.y,0), Charge_Speed*Time.deltaTime);
        }
        else
        {
            currentState = State.Walk;
        }

        // 怪物正在冲锋
        
        // 可能需要根据距离和方向转移到其他状态
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 如果碰到了玩家
        {
            if (Vector2.Distance(E_tra.position, P_tra.position)<ChargeRange&&Vector2.Distance(E_tra.position, P_tra.position)>attackRange)
            {
                Debug.Log("切换至Charge状态");
                currentState = State.Charge; // 切换到Charge状态
            }
            if (Vector2.Distance(E_tra.position, P_tra.position)<attackRange)
            {
                Debug.Log("切换至Attack状态");
                currentState = State.Attack; // 切换到Charge状态
            }
            
            
            //Debug.Log("111");
            //currentState = State.Charge; // 切换到Charge状态
        }
    }
    private void AttackPlayer()
    {
        // 检查攻击冷却时间
        if (attackTimer <= 0)
        {
            // 在这里写下敌人的攻击逻辑
            // 玩家受伤接口：
            player.BeHit(10);
            EnemyAttack.enabled = true;
            Debug.Log("玩家受伤");
            // 重置攻击冷却时间
            attackTimer = attackCooldown;
        }
        else
        {
            EnemyAttack.enabled = false;
            // 减少攻击冷却时间
            attackTimer -= Time.deltaTime;
        }

        // 如果敌人与玩家的距离大于攻击范围，则切换到追击状态
        if (Vector2.Distance(E_tra.position, P_tra.position)>attackRange+1&&Vector2.Distance(E_tra.position, P_tra.position)<ChargeRange)
        {
            currentState = State.Charge;
        }
    }
}
