using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [Header("Move")]
    public bool moveOn = true; // 이동기능 활성화 여부
    public bool jumpOn = true; // 점프기능 활성화 여부
	public float maxSpeed = 6f; // 최대 이동속도
	public float jumpForce = 1000f; // 점프 힘
	public LayerMask whatIsGround; // 땅 레이어 지정
	[HideInInspector]
	public bool lookingRight = true; // 현재 바라보는 방향
	bool doubleJump = false; // 더블점프 했는지
	public GameObject Boost;
    
	private Rigidbody2D rb2d;
	public Animator anim;
    private Genie genie;
    public bool isGrounded = false; // 현재 땅인지

    [Header("KeyCanvas")]
    public Image RkeyRectTransfrom;

    void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        genie = GetComponent<Genie>();
	}
    
	void Update () {

        if (genie.runSkill.inUseSkill) return; // 대쉬중일 때 중단

        Jump();
        TakeDown();
        Move();
        GroundCheck();

    }

    // 구름 이펙트 생성
    public void CreateCloud()
    {
        Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
    }

    // 점프
    void Jump()
    {
        if (!jumpOn) return;
        if (genie.runSkill.inUseSkill) return; // 대쉬중일 때 중지
        if (genie.dragSkill.inUseSkill) return; // 포탄발사중일 때 중지
        if (genie.takeDownSkill.inUseSkill) return; // 내려찍기할 때 중지

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb2d.AddForce(new Vector2(0, jumpForce));
                CreateCloud();
            }
            else
            {
                if (!doubleJump)
                {
                    doubleJump = true;
                    genie.doubleJumpSkill.UseSkill();
                }
            }
        }
    }

    // 내려찍기
    void TakeDown()
    {
        if (Input.GetKeyDown(KeyCode.S) && !isGrounded)
        {
            genie.takeDownSkill.UseSkill();
        }
    }

    // 이동
    void Move()
    {
        if (!moveOn) return;
        if (genie.runSkill.inUseSkill) return; // 대쉬중일 때 중지
        if (genie.dragSkill.inUseSkill) return; // 포탄발사중일 때 중지
        if (genie.takeDownSkill.inUseSkill) return; // 내려찍기할 때 중지

        float hor = Input.GetAxis("Horizontal");

        genie.isMoving = hor != 0 ? true : false; // 현재 이동중인지 확인

        if ((hor > 0 && !lookingRight) || (hor < 0 && lookingRight))
            Flip();

        anim.SetFloat("Speed", 0f); // 이동 애니메이션
        anim.SetFloat("vSpeed", 0); // 점프 애니메이션

        rb2d.velocity = new Vector2(hor * maxSpeed, rb2d.velocity.y); // 속도 적용

        anim.SetFloat("Speed", Mathf.Abs(hor)); // 이동 애니메이션
        anim.SetFloat("vSpeed", rb2d.velocity.y); // 높이 애니메이션
    }
    
	public void Flip()
	{
		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
        myScale.x *= -1f;

        if (RkeyRectTransfrom)
        {
            Vector3 rSacle = RkeyRectTransfrom.rectTransform.localScale;
            rSacle.x *= -1f;
            RkeyRectTransfrom.rectTransform.localScale = rSacle;
        }

        transform.localScale = myScale;
	}
    
    public void GroundCheck()
    {
        // isGrounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround); // 점으로 땅 검사
        // isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); // 원으로 땅 검사
        isGrounded = Physics2D.OverlapCircle(transform.position, genie.ColliderRadius, whatIsGround); // 원으로 땅 검사

        if (!isGrounded && genie.VelocityY < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Abs(genie.VelocityY * 0.1f), whatIsGround);
            if (hit.collider)
            {
                isGrounded = true;
            }
        }


        if (isGrounded)
        {
            doubleJump = false;
        }

        anim.SetBool("IsGrounded", isGrounded);
    }
    

}
