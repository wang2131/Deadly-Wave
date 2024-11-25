using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Collider2D _collider;

    public Rigidbody2D _rigidbody;

    public PlayerStateMachine statemachine;

    public PlayerInput input;

    public GameObject arrow;

    private GroundDetected groundDetected;

    private AttackPool pool;

    public bool canAirJump = true;
    public bool isGrounded => groundDetected.isGrounded;

    [SerializeField]
    private Camera camera;

    public bool canAttacked;

    [SerializeField]
    private Vector2 mouseWorldPosition = new Vector2();

    public int maxHp;//最大血量
    public int nowHp;//当前血量

    public int basePower;//基础攻击力
    public int addPower;//加成攻击力
    public int finalPower => basePower + addPower;//攻击力总和
    
    [Tooltip("移动速度")][SerializeField] private float moveSpeed;//移动速度
    [Tooltip("移动加速度")][SerializeField] private float moveAccelerate;//移动加速度
    [Tooltip("跳跃力度")][SerializeField] private float jumpForce;//跳跃力度
    
    [Tooltip("滑铲时间")][SerializeField] private float slidTime;//滑铲时间
    [Tooltip("滑铲速度")][SerializeField] private float slidSpeed;//滑铲速度
    [HideInInspector]public bool isSlidFinished;//滑铲是否完成
    private WaitForSeconds slidWaitForSeconds;
    
    [Tooltip("后退时间")][SerializeField] private float backStepTime;//后退时间
    [Tooltip("后退速度")][SerializeField] private float backStepSpeed;//后退速度
    [HideInInspector]public bool isBackStepFinished;//后退是否完成
    private WaitForSeconds backStepWaitForSeconds;


    [SerializeField]private VoidParameterEventChannel gameOverEventChannel;
    [SerializeField] private VoidParameterEventChannel gameWinEventChannel;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        statemachine = GetComponent<PlayerStateMachine>();
        input = GetComponent<PlayerInput>();
        pool = GetComponentInChildren<AttackPool>();
        groundDetected = GetComponentInChildren<GroundDetected>();
        slidWaitForSeconds = new WaitForSeconds(slidTime);
        backStepWaitForSeconds = new WaitForSeconds(backStepTime);
        arrow = GameObject.Find("Arrow");
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        nowHp = maxHp;
        canAttacked = true;
        
    }

    public void aa()
    {
        Debug.Log("GameOver");
    }
    private void Update()
    {
        //Debug.Log(Mouse.current.position.ReadValue());
        
        //Debug.Log(Camera.main);
        ArrowDirectionChange();
        if (input.isAttack)
        {
            GameObject wave = pool.MoveOutPool(new Vector2(transform.localScale.normalized.x, 0));
            
            if (wave != null && wave.TryGetComponent<WaveScript>(out WaveScript waveScript))
            {
                waveScript.attackDamage = finalPower;
                waveScript.setActiveTrue();
            }
            
        }
        
    }

    public void PlayerMove()
    {
        if (input.isMove)
        {
            transform.localScale = new Vector3(input.moveAxes*Math.Abs(transform.localScale.x),
                transform.localScale.y, transform.localScale.z);
            _rigidbody.velocity = Vector2.MoveTowards(_rigidbody.velocity,
                new Vector2(moveSpeed * input.moveAxes, _rigidbody.velocity.y), moveAccelerate*Time.deltaTime);
        }
        
    }

    public void PlayerJump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
    }

    public void Slid()//滑铲
    {
        isSlidFinished = false;
        StartCoroutine(StartSlid());
    }

    IEnumerator StartSlid()
    {
        _rigidbody.velocity = new Vector2(transform.localScale.x * slidSpeed, _rigidbody.velocity.y);
        yield return slidWaitForSeconds;
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        isSlidFinished = true;
    }

    public void BackStep() //后退
    {
        isBackStepFinished = false;
        StartCoroutine(StartBackStep());
    }

    IEnumerator StartBackStep()
    {
        _rigidbody.velocity = new Vector2(-transform.localScale.x * backStepSpeed, _rigidbody.velocity.y);
        yield return backStepWaitForSeconds;
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        isBackStepFinished = true;
    }

    public void ArrowDirectionChange()
    {
        if (!camera.orthographic)
        {
            return;
            
        }
        else
        {
            
            //mouseWorldPosition = Camera.current.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        float angle = Mathf.Atan2(mouseWorldPosition.y - arrow.transform.position.y,
            mouseWorldPosition.x - arrow.transform.position.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0,0, angle-90f));
    }
    
    IEnumerator AttackPowerUp(int power, float time)
    {
        addPower += power;
        yield return new WaitForSeconds(time);
        addPower -= power;
    }
    
    /// <summary>
    /// 增加一段时间的攻击力
    /// </summary>
    /// <param name="power">加成攻击力</param>
    /// <param name="time">持续时间</param>
    /// <returns></returns>
    public void PowerUp(int power, float time)
    {
        StartCoroutine(AttackPowerUp(power, time));
    }
    
    /// <summary>
    /// 回复血量
    /// </summary>
    /// <param name="value">回复值</param>
    public void addHp(int value)
    {
        if (nowHp + value > maxHp) nowHp = maxHp;
        else nowHp += value;
    }
    
    public void BeHit(int damage)
    {
        if (canAttacked)
        {
            nowHp -= damage;
            statemachine.SwitchState(typeof(PlayerState_BeHit));
            if(nowHp<=0) gameOverEventChannel.BroadCast();
        }
    }
}