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
    private int maxBulletNum;
    private float shootInterval;
    private float bulletMovePower;
    private int attack;
    private bool canShoot = true;
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
                case PlayerState.Normal:
                    break;
                case PlayerState.Reload:
                    StartCoroutine(ReLoad());
                    break;
                case PlayerState.GetHit:
                    break;
                case PlayerState.Die:
                    break;
                default:
                    break;
            }
        }
    }

    public void Init(Player_Config config)
    {
        moveSpeed = config.MoveSpeed;
        maxBulletNum = config.MaxBulletNum;
        currentBulletNum = maxBulletNum;
        shootInterval = config.ShootInterval;
        bulletMovePower = config.BulletMovePower;
    }

    private void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        if (Time.deltaTime!=0)
        {
            StateOnUpdate();
        }
    }

    private void StateOnUpdate()
    {
        switch (PlayerState)
        {
            case PlayerState.Normal:
                Move();
                Shoot();
                if (currentBulletNum<maxBulletNum&&Input.GetKeyDown(KeyCode.R))
                {
                    PlayerState = PlayerState.Reload;
                }
                break;
            case PlayerState.Reload:
                Move();
                break;
            case PlayerState.GetHit:
                break;
            case PlayerState.Die:
                break;
            default:
                break;
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
        if (canShoot && currentBulletNum > 0 && Input.GetMouseButton(0))
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
        currentBulletNum -= 1;
        //TODO:修改UI
        EventManager.EventTrigger<int, int>("UpdateBullet",currentBulletNum,maxBulletNum);
        animator.SetBool("Shoot", true);
        canShoot = false;
        AudioManager.Instance.PlayOnShot("Audio/Shoot/laser_01", transform.position);
        //生成子弹
        Bullet bullet = ResManager.Load<Bullet>("Bullet",LVManager.Instance.TempObjRoot);
        bullet.transform.position = firePoint.position;
        bullet.Init(firePoint.forward, bulletMovePower, attack);
        yield return new WaitForSeconds(shootInterval);
        canShoot = true;
        //子弹打完需要换弹
        if (currentBulletNum == 0)
        {
            PlayerState = PlayerState.Reload;
        }
    }

    private IEnumerator ReLoad()
    {
        animator.SetBool("ReLoad", true);
        AudioManager.Instance.PlayOnShot("Audio/Shoot/Reload", this);
        yield return new WaitForSeconds(1.9f);
        animator.SetBool("ReLoad", false);
        PlayerState = PlayerState.Normal;
        currentBulletNum = maxBulletNum;
        EventManager.EventTrigger<int, int>("UpdateBullet", currentBulletNum, maxBulletNum);
    }
}
