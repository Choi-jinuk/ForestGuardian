using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerController))]
public class Genie : Character
{
    public static Genie instance;
    public static EnergyManager energyManager;

    [Header("Compenent")]
    public Collider2D coll;
    private CircleCollider2D circleCollider;
    public PlayerController controller;
    public Camera cam;
    public TrailRenderer trailRenderer;
    public Transform groundDetection; // 땅 위치를 체크하는 곳

    [Header("Skill")]
    public SkillBase mouseSkill;
    public SkillBase runSkill;
    public SkillBase dragSkill;
    public SkillBase doubleJumpSkill;
    public SkillBase takeDownSkill;
    public GameObject skillUI;

    [Header("Effect Sound")]
    public SoundManager soundManager;
    public AudioClip SkillUicilp;

    protected Spirit hitSpirit; // 충돌한 정령

    #region Rigidbody
    public float CamSize { get { return cam.orthographicSize; } set { cam.orthographicSize = value; } }
    
    public Vector2 Velocity { get { return rigd.velocity; } set { rigd.velocity = value; } }
    
    public float VelocityX
    {
        get
        {
            return rigd.velocity.x;
        }
        set
        {
            Vector2 velocity = rigd.velocity;
            velocity.x = value;
            rigd.velocity = velocity;
        }
    }

    public float VelocityY
    {
        get
        {
            return rigd.velocity.y;
        }
        set
        {
            Vector2 velocity = rigd.velocity;
            velocity.y = value;
            rigd.velocity = velocity;
        }
    }

    public float ColliderRadius { get { return circleCollider.radius; } set { circleCollider.radius = value; } }
    
    #endregion
    

    private void Awake()
    {
        instance = this;
        energyManager = FindObjectOfType<EnergyManager>();

        rigd = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        controller = GetComponent<PlayerController>();
        cam = Camera.main;
        mouseSkill = GetComponent<MouseAttackSkill>();
        runSkill = GetComponent<RunSkill>();
        dragSkill = GetComponent<DragShootingSkill>();
        doubleJumpSkill = GetComponent<DoubleJumpSkill>();
        takeDownSkill = GetComponent<TakeDownSkill>();

        if(trailRenderer)
            trailRenderer.enabled = false;
    }

    void Update()
    {
        // if (EventSystem.current.IsPointerOverGameObject()) return; // ui일때는 스킬발동 안함 이거 오류가 너무 많이남...

        // 마우스 좌클릭
        if (Input.GetMouseButton(0))
        {
            mouseSkill.UseSkill(); // 마우스 공격 스킬
        }

        // 마우스 우클릭
        if (Input.GetMouseButtonDown(1))
        {
            Collider2D coll = GetMouseHit("Spirit");
            Spirit spirit = null;

            if (coll)
                spirit = GetMouseHit("Spirit").GetComponent<Spirit>();

            if (spirit)
            {
                if (SpiritDeleteUI.instance)
                {
                    SpiritDeleteUI.instance.Open(spirit); // 정령이 있으면 삭제 ui 오픈
                }


            }
            else
            {
                runSkill.UseSkill(); // 대쉬
            }
            
        }

        // 안 움직인 상태로 정령한테 가까이 있으면 업그레이드 ui 활성화
        if (hitSpirit)
        {
            hitSpirit.upgradeUI.SetActive(!isMoving);

            // 다음 레벨의 정령이 있을 때
            if (hitSpirit.nextLevelSpirit)
            {
                // R키 누르면 업그레이드
                if (Input.GetKeyDown(KeyCode.R))
                {
                    hitSpirit.Upgrade();
                }
            }
        }


        // 스킬 UI 활성화
        if (Input.GetKeyDown(KeyCode.U))
        {
            soundManager.PlayUISound(SkillUicilp);
            skillUI.SetActive(!skillUI.activeInHierarchy);
        }
    }

    private void OnMouseDown()
    {
        dragSkill.UseSkill();
    }

    // SkillInfo 타입에 따른 스킬 반환
    public SkillBase GetSkillType(SkillInfo info)
    {
        if(info is MouseAttackSkillInfo)
        {
            return mouseSkill;
        }
        else if (info is RunSkillInfo)
        {
            return runSkill;
        }
        else if (info is DragShootingSkillInfo)
        {
            return dragSkill;
        }
        else if (info is DoubleJumpSkillInfo)
        {
            return doubleJumpSkill;
        }
        else if (info is TakeDownSkillInfo)
        {
            return takeDownSkill;
        }

        return null;
    }

    // 마우스 위치 반환
    public static Vector2 GetMousePos()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return pos;
    }

    // 마우스에 충돌한 것 반환
    public static Collider2D GetMouseHit(string layer = null)
    {
        Collider2D coll;

        if (layer == null)
        {
            coll = Physics2D.OverlapPoint(GetMousePos());
        }
        else
        {
            coll = Physics2D.OverlapPoint(GetMousePos(), 1 << LayerMask.NameToLayer(layer));
        }

        return coll;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 정령과 부딫히면
        if (collision.gameObject.layer == LayerMask.NameToLayer("Spirit"))
        {
            hitSpirit = collision.GetComponent<Spirit>();
            hitSpirit.upgradeUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 정령에서 나가면
        if (collision.gameObject.layer == LayerMask.NameToLayer("Spirit"))
        {
            hitSpirit.upgradeUI.SetActive(false);
            hitSpirit = null;
        }
    }
}
