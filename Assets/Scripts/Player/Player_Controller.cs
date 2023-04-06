using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public enum PlayerState
{
    Normal,
    Reload,
    GetHit,
    Die,
}

public class Player_Controller : SingletonMono<Player_Controller>
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform firePoint;

    private PlayerState playerState;

    #region 参数
    private float moveSpeed;
    private int currentBulletNum;
    public int CurrentBulletNum
    {
        get => currentBulletNum;
        set
        {
            currentBulletNum = value;
            EventManager.EventTrigger<int, int>("UpdateBullet", CurrentBulletNum, maxBulletNum);
        }
    }
    private int maxBulletNum;
    private float shootInterval;
    private float bulletMovePower;
    private int attack;
    private bool canShoot = true;
    private int hp;
    public int HP
    {
        get => hp;
        set
        {
            hp = value;
            EventManager.EventTrigger("UpdateHp", hp);
        }
    }
    #endregion
    private int groundLayerMask;
    public PlayerState PlayerState
    {
        get => playerState;
        set
        {
            playerState = value;
            switch (playerState)
            {
                case PlayerState.Reload:
                    StartCoroutine(DoReLoad());
                    break;
                case PlayerState.GetHit:
                    //重置上一次受伤带来的效果
                    StopCoroutine(DoGetHit());
                    animator.SetBool("GetHit", false);
                    //开始这一次受伤带来的效果
                    animator.SetBool("GetHit", true);
                    StartCoroutine(DoGetHit());
                    break;
                case PlayerState.Die:
                    EventManager.EventTrigger("GameOver");
                    animator.SetTrigger("Die");
                    break;
            }
        }
    }



    public void Init(Player_Config config)
    {
        HP = config.HP;
        moveSpeed = config.MoveSpeed;
        maxBulletNum = config.MaxBulletNum;
        CurrentBulletNum = maxBulletNum;
        shootInterval = config.ShootInterval;
        bulletMovePower = config.BulletMovePower;
        attack = config.Attack;
    }

    private void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
#if UNITY_ANDROID
        EventManager.AddEventListener<Vector2>("JoystickMove", JoystickMove);
        EventManager.AddEventListener<Vector2>("JoystickFire", JoystickFire);
#endif

    }

    private void Update()
    {
#if UNITY_STANDALONE
        if (Time.deltaTime != 0)
        {
            StateOnUpdate();
        }
#endif
    }

    private void StateOnUpdate()
    {
        switch (PlayerState)
        {
            case PlayerState.Normal:
                Move();
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Reload();
                }
                break;
            case PlayerState.Reload:
                Move();
                break;
        }
    }

    private void JoystickMove(Vector2 dir)
    {
        if (dir!=Vector2.zero)
        {
            Vector3 moveDir = new Vector3(dir.x, -5, dir.y);
            characterController.Move(moveDir * moveSpeed * Time.deltaTime);
        }
        animator.SetFloat("MoveX", dir.x);
        animator.SetFloat("MoveY", dir.y);
    }

    private void JoystickFire(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {
            Vector3 dir3 = new Vector3(dir.x, 0, dir.y);
            Quaternion targetQuaternion = Quaternion.FromToRotation(Vector3.forward, dir3);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetQuaternion, Time.deltaTime * 20f);
            Shoot();
        }
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(h, -5, v);
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);

        Ray ray = Camera_Controller.Instance.camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, groundLayerMask))
        {
            if (hitInfo.point.z < transform.position.z)
            {
                h *= -1;
                v *= -1;
            }
            Vector3 dir = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z) - transform.position;
            Quaternion targetQuaternion = Quaternion.FromToRotation(Vector3.forward, dir);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetQuaternion, Time.deltaTime * 20f);
        }

        animator.SetFloat("MoveX", h);
        animator.SetFloat("MoveY", v);
    }

    private void Shoot()
    {
        if (canShoot && CurrentBulletNum > 0)
        {
            StartCoroutine(DoShoot());
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }

    private IEnumerator DoShoot()
    {
        CurrentBulletNum -= 1;

        animator.SetBool("Shoot", true);
        canShoot = false;
        AudioManager.Instance.PlayOnShot("Audio/Shoot/laser_01", transform.position);
        //生成子弹
        Bullet bullet = ResManager.Load<Bullet>("Bullet", LVManager.Instance.TempObjRoot);
        bullet.transform.position = firePoint.position;
        bullet.Init(firePoint.forward, bulletMovePower, attack);
        yield return new WaitForSeconds(shootInterval);
        canShoot = true;
        //子弹打完需要换弹
        if (CurrentBulletNum == 0)
        {
            PlayerState = PlayerState.Reload;
        }
    }

    public void Reload()
    {
        if (CurrentBulletNum < maxBulletNum)
        {
            PlayerState = PlayerState.Reload;
        }
    }

    private IEnumerator DoReLoad()
    {
        animator.SetBool("ReLoad", true);
        AudioManager.Instance.PlayOnShot("Audio/Shoot/Reload", this);
        yield return new WaitForSeconds(1.9f);
        animator.SetBool("ReLoad", false);
        PlayerState = PlayerState.Normal;
        CurrentBulletNum = maxBulletNum;
        EventManager.EventTrigger<int, int>("UpdateBullet", CurrentBulletNum, maxBulletNum);
    }

    public void GetHit(int damage)
    {
        if (hp == 0)
        {
            return;
        }
        hp -= damage;
        if (hp <= 0)
        {
            HP = 0;
            PlayerState = PlayerState.Die;
        }
        else
        {
            HP = hp;
            PlayerState = PlayerState.GetHit;
        }
    }

    private IEnumerator DoGetHit()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("GetHit", false);
        if (PlayerState == PlayerState.GetHit)
        {
            PlayerState = PlayerState.Normal;
        }
    }

    private void OnDestroy()
    {
#if UNITY_ANDROID
        EventManager.RemoveEventListener<Vector2>("JoystickMove", JoystickMove);
        EventManager.RemoveEventListener<Vector2>("JoystickFire", JoystickFire);
#endif
    }
}
