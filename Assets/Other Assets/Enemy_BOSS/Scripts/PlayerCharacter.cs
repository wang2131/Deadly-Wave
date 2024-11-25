using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    Rigidbody2D playerRig;
    Animator playerAni;
    Transform playerTra;
    CapsuleCollider2D playerCol;
    LayerMask ground;
    GameObject JumpBox;
    SpriteRenderer playerRenderer;//玩家的图片显示
    
    public GameObject ghosting;//残影
    public AudioSource music;
    public List<AudioClip> list;

    public float MaxHp;//最大血量
    public float Hp;//当前血量

    public int AttackMode;//当前的攻击模式
    public float LightAttackDamge;//轻攻击伤害 15
    public float HeavyAttackDamge;//斧子攻击伤害 20

    public float Speed;//左右移动速度
    public float SlidSpeed;//闪避速度
    public float JumpSpeed;//跳跃速度
    public float AttackSpeed;//轻攻击补偿速度
    public float HeavySpeed;//重攻击补偿速度
    public float BeHitSpeed;//击退速度
    public float HeavyBeHitSpeed;//重攻击击退速度

    public float FallMultiplier;//下落加速度
    public float LowJumpMultiplier;//跳跃加速度

    public float BeHitShield;//受到攻击时的伤害 

    public Vector3 NowDir;//记录输入时的按键方向

    public bool IsGround;//判断能否跳跃
    public bool IsJump;//判断是否在空中
    public bool IsSliding;//是否再闪避中
    public bool IsAttack;//是否在攻击中
    public bool canInput;//攻击中不能输入
    public bool canSliding;//闪避间隔
    public bool IsHit;//是否处于被攻击状态
    public bool IsDefend;//是否处于无敌状态
    public bool IsDisplay;//闪烁中的开关
    public bool IsBack;//后撤步
    public bool IsMove;//奔跑的音频控制

    public int JumpMax;//最大跳跃数
    public int JumpCount;//当前跳跃数
    public int ComboCount;//当前攻击动作

    public float StartSlidTime;//冲刺开始时间（没用）
    public float SlidTime;//冲刺时间
    public float SlidCountTime;//冲刺计时器
    public float SlidCd;//冲刺Cd
    public float StartCombo;//开始连击时间
    public float ComboTime;//连击时间
    public float DefendTime;//无敌时间
    public float FlashingTime;//闪烁间隔
    public float FlashTep;//闪烁计时器
    public float BackTime;//后撤步时间

    public Vector2 InitialSize;//初始碰撞体大小
    public Vector2 InitialOffset;//初始碰撞体位置

    public float time;
    void Awake()
    {
        playerRig = GetComponent<Rigidbody2D>();
        playerAni = GetComponent<Animator>();
        playerTra = GetComponent<Transform>();
        playerCol = GetComponent<CapsuleCollider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        music = GetComponent<AudioSource>();
        JumpBox = GameObject.Find("GroundCheck");
        ground = 1<<3;

        MaxHp = 100;
        Hp = MaxHp;

        AttackMode = 0;
        LightAttackDamge = 15f;
        HeavyAttackDamge = 20f;

        Speed = 5;
        JumpSpeed = 6.5f;
        SlidSpeed = 10;
        AttackSpeed = 0.5f;
        HeavySpeed = 0.2f;
        BeHitSpeed = 0.5f;
        HeavyBeHitSpeed = 10f;

        FallMultiplier = 4f;
        LowJumpMultiplier = 2.5f;

        BeHitShield = 1f;

        NowDir = playerTra.localScale;

        IsJump = false;
        IsSliding = false;
        IsAttack = false;
        canInput = true;
        IsDisplay = true;
        IsMove = true;

        JumpMax = 2;
        JumpCount = 0;
        ComboCount = 1;

        SlidTime = 0.5f;
        SlidCd = 1f;
        SlidCountTime = SlidCd;
        ComboTime = 1f;
        DefendTime = 3f;
        FlashingTime = 0.2f;
        FlashTep = 0;
        BackTime = 0.2f;

        InitialSize = new Vector2(playerCol.size.x, playerCol.size.y);
        InitialOffset = new Vector2(playerCol.offset.x, playerCol.offset.y);
    }
    void Start()
    {
        
    }
    void Update()
    {
        CheckGround();
        PlayFallAni();
        Defend();
        Death();
        SlidCountTime -= Time.deltaTime;
    }
    public void Move(float h)//移动
    {
        if (!canInput || IsHit) 
        {
            return;
        }
        playerRig.velocity = new Vector2(h * Speed, playerRig.velocity.y);
        if (h > 0) 
        {
            playerTra.localScale = new Vector2(-NowDir.x, NowDir.y);
            if (!IsJump && !IsSliding && !IsAttack)
            {
                playerAni.Play("Run");
                music.clip = list[4];
                if (!music.isPlaying) 
                {
                    music.Play();
                }
            }
        }
        if (h < 0) 
        {
            playerTra.localScale = new Vector2(NowDir.x, NowDir.y);
            if (!IsJump && !IsSliding && !IsAttack)
            {
                playerAni.Play("Run");
                music.clip = list[4];
                if (!music.isPlaying) 
                {
                    music.Play();
                }
            }
        }
        if (h == 0)
        {
            if (!IsJump && !IsSliding && !IsAttack && !playerAni.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            {
                playerAni.Play("Idle");
                music.Pause();
            }
        }
    }
    public void Jump() //跳跃
    {
        if (!canInput ) 
        {
            return;
        }
        if (IsGround)
        {
            Debug.Log(IsJump);
            IsJump = true;
            Debug.Log(IsJump);

            playerAni.Play("Jump");
            JumpCount = 0;
            playerRig.velocity = new Vector2(playerRig.velocity.x, JumpSpeed);
            JumpCount++;
            IsGround = false;
        }
        else if (JumpCount > 0 && JumpCount < JumpMax) 
        {
            if (!IsHit) 
            {
                playerRig.velocity = new Vector2(playerRig.velocity.x, JumpSpeed);
                JumpCount++;
                playerAni.Play("Jump");
            }
        }
    }
    public void CheckGround()//地板检测 
    {
        IsGround = Physics2D.OverlapCircle(JumpBox.transform.position, 0.1f, ground);
        IsJump = !IsGround;
    }
    public void PlayFallAni() //播放下落动画
    {
        if (!IsGround && canInput && !IsSliding && !IsHit) 
        {
            if (playerRig.velocity.y < 0)
            {
                playerRig.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
                playerAni.Play("Fall");
            }
            else if (playerRig.velocity.y > 0 && Input.GetAxis("Jump")!=1) 
            {
                playerRig.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }
    public void StartSliding() //滑铲时的操作
    {
        if (!IsSliding && canInput && IsGround && !IsHit && SlidCountTime <= 0)
        {
            IsSliding = true;
            StartSlidTime = SlidTime;
            playerCol.offset = new Vector2(playerCol.offset.x, -0.7f);
            playerCol.size = new Vector2(playerCol.size.x, playerCol.size.x);
            StartCoroutine(SlidMove(SlidTime));
            playerAni.Play("Sliding");
            gameObject.layer = LayerMask.NameToLayer("Flashing");
        }
    }
    IEnumerator SlidMove(float time) //滑铲时间
    {
        canInput = false;
        music.Pause();
        if (NowDir.x == playerTra.localScale.x)
        {
            playerRig.velocity = new Vector2(-SlidSpeed, 0);
        }
        else if(NowDir.x == -playerTra.localScale.x)
        {
            playerRig.velocity = new Vector2(SlidSpeed, 0);
        }
        yield return new WaitForSeconds(time);
        playerCol.offset = InitialOffset;
        playerCol.size = InitialSize;
        canInput = true;
        IsSliding = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
        SlidCountTime=SlidCd;
    }
    public void StartBackStep() 
    {
        if (!IsBack && IsGround && !IsHit)
        {
            IsBack = true;
            StartCoroutine(Backstep(BackTime));
            gameObject.layer = LayerMask.NameToLayer("Flashing");
            ghosting.SetActive(true);
        }
    }
    IEnumerator Backstep(float time) 
    {
        canInput = false;
        music.Pause();
        if (playerTra.localScale.x > 0)
        {
            playerRig.velocity = new Vector2(Speed, 0);
        }
        else if (playerTra.localScale.x < 0) 
        {
            playerRig.velocity = new Vector2(-Speed, 0);
        }
        yield return new WaitForSeconds(time);
        canInput = true;
        IsBack = false;
        IsAttack = false;
        ghosting.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Player");

    }
    public void Attack()//轻攻击
    {
        if (!IsAttack && !IsSliding && !IsHit)
        {
            IsAttack = true;
            canInput = false;
            AttackMode = 1;
            playerRig.velocity = Vector2.zero;
            ComboCount++;
            if (ComboCount > 3)
            {
                ComboCount = 1;
            }
            StartCombo = ComboTime;
            if (ComboCount == 1) 
            {
                playerAni.Play("Attack1");
            }
            if (ComboCount == 2)
            {
                playerAni.Play("Attack2");
            }
            if (ComboCount == 3)
            {
                playerAni.Play("Attack3");
            }
            playerRig.velocity = new Vector2(-playerTra.localScale.x * AttackSpeed, playerRig.velocity.y); 
        }
    }
    public void HeavyAttack() //重攻击
    {
        if (!IsAttack && !IsSliding && !IsHit)
        {
            IsAttack = true;
            canInput = false;
            AttackMode = 2;
            playerRig.velocity = Vector2.zero;
            ComboCount++;
            if (ComboCount > 3)
            {
                ComboCount = 1;
            }
            StartCombo = ComboTime;
            if (ComboCount == 1)
            {
                playerAni.Play("Heavy Attack1");
            }
            if (ComboCount == 2)
            {
                playerAni.Play("Heavy Attack2");
            }
            if (ComboCount == 3)
            {
                playerAni.Play("Heavy Attack3");
            }
            playerRig.velocity = new Vector2(-playerTra.localScale.x * HeavySpeed, playerRig.velocity.y);
        }
    }
    public void InAttack()//连击
    {
        if (StartCombo != 0)
        {
            StartCombo -= Time.deltaTime;
            if (StartCombo <= 0)
            {
                StartCombo = 0;
                ComboCount = 0;
            }
        }
    }
    public void AttackPlayAudio() 
    {
        music.clip = list[0];
        music.Play();
    }
    public void HeavyAttackPlayAudio() 
    {
        music.clip = list[1];
        music.Play();
    }
    public void AttackOver() //攻击状态退出
    {
        IsAttack = false;
    }
    public void CanInput() //能进行操作
    {
        canInput = true;
    }
    public void BeHit(Vector2 Dir,float damge)//被敌人攻击（轻微位移）
    {
        music.clip = list[3];
        music.Play();
        IsHit = true;
        playerRig.velocity = Dir * BeHitSpeed;
        playerAni.SetTrigger("Hit");
        damge = damge * BeHitShield;
        Hp -= damge;
    }
    public void Defend() 
    {
        if (IsDefend) 
        {
            DefendTime -= Time.deltaTime;
            if (DefendTime > 0) 
            {
                gameObject.layer = LayerMask.NameToLayer("Flashing");
                FlashTep += Time.deltaTime;
                if (FlashTep >= FlashingTime)
                {
                    if (IsDisplay)
                    {
                        playerRenderer.enabled = false;
                        IsDisplay = false;
                        FlashTep = 0;
                    }
                    else 
                    {
                        playerRenderer.enabled = true;
                        IsDisplay = true;
                        FlashTep = 0;
                    }
                }
            }
            else if (DefendTime <= 0) 
            {
                gameObject.layer = LayerMask.NameToLayer("Player");
                IsDefend = false;
                IsDisplay = true;
                playerRenderer.enabled = true;
                DefendTime = 3f;
            }
        }
    }
    public void BeHitOver() //被攻击状态的退出
    {
        IsHit = false;
        IsDefend = true;
    }
    public void Death() 
    {
        if (Hp <= 0) 
        {
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss")) 
        {
            if (AttackMode == 1)
            {
                collision.GetComponent<FireCentipede>().BeHit(LightAttackDamge);
            }
            else if (AttackMode == 2) 
            {
                collision.GetComponent<FireCentipede>().BeHit(HeavyAttackDamge);
            }
        }
    }
}
