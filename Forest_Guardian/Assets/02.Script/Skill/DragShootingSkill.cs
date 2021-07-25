using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragShootingSkill : SkillBase
{
    public LineRenderer lineRenderer;

    float unitTime = 1f / 50f; //초당 50프레임으로 계산
    int steps = 50 * 10; // 10초 계산(총 500프레임)
    CircleCollider2D circleCollider; // 지니 콜라이더
    float originColliderRadius; // 본래의 콜라이더 두께

    // 스킬 정보
    public DragShootingSkillInfo[] dragShootingInfos = new DragShootingSkillInfo[3];

    // 현재 레벨정보
    public DragShootingSkillInfo CurrentInfo { get { return dragShootingInfos[level - 1]; } }

    [Header("Effect Sound")]
    public SoundManager audioSource;

    [Header("Player")]
    public GameObject player;

    public new void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        circleCollider = GetComponent<CircleCollider2D>();
        originColliderRadius = circleCollider.radius;
    }

    public override void UseSkill()
    {
        if (level <= 0) return; // 스킬 안배웠으면 중단
        if (inUseSkill) return; // 스킬 사용중이면 중단
        if (genie.runSkill.inUseSkill) return; // 대쉬중일때는 사용불가능
        if (nextSkillTime > Time.time) return; // 쿨타임이면 중단

        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        Time.timeScale = 0.1f; // 시간 느려짐
        inUseSkill = true;

        lineRenderer.enabled = true; // 화살표 활성화

        // 마우스 드래그중일 때
        while (Input.GetMouseButton(0))
        {
            lineRenderer.SetPosition(0, (Vector2)transform.position + GetDir() * 1.0f);
            lineRenderer.SetPosition(1, (Vector2)transform.position + GetDir() * 1.5f);
            yield return null;
        }

        // 마우스 뗄 때
        Instantiate(CurrentInfo.effect, player.transform.position, player.transform.rotation);
        lineRenderer.enabled = false; // 화살표 비활성화
        
        transform.position += Vector3.up * 0.5f; // 시작하자마자 부딫히는 판정 안나게 살짝 띄움
        genie.rigd.velocity = GetDir() * CurrentInfo.speed; // 날라감
        genie.coll.isTrigger = true; // 콜라이더 트리거로 변경
        audioSource.PlayEffect(CurrentInfo.sound);

        // 날라가는 방향으로 이미지 뒤집기
        if((genie.VelocityX > 0 && !genie.controller.lookingRight) || (genie.VelocityX < 0 && !genie.controller.lookingRight))
        {
            genie.controller.Flip();
        }
        
        Time.timeScale = 1f; // 시간 빨라짐

        genie.trailRenderer.startColor = CurrentInfo.startColor;
        genie.trailRenderer.endColor = CurrentInfo.endColor;
        genie.trailRenderer.startWidth = 0.6f;
        genie.trailRenderer.endWidth = 0.1f;

        if (genie.trailRenderer) genie.trailRenderer.enabled = true;

        genie.coll.enabled = false;
        yield return null;
        genie.coll.enabled = true;
        yield return new WaitUntil(() => genie.controller.isGrounded); // 땅에 닿을때까지 기다림

        if (genie.trailRenderer) genie.trailRenderer.enabled = false;

        genie.coll.isTrigger = false; // 트리거 해제
        inUseSkill = false;
        nextSkillTime = Time.time + CurrentInfo.coolTime; // 쿨타임 업데이트

        if (coolTimeIcon)
        {
            coolTimeIcon.StartCoolTime(CurrentInfo.coolTime);
        }
    }

    
    // 포물선 계산
    void Trail()
    {
        List<Vector3> list = new List<Vector3>();

        Vector2 velocity = GetDir() * CurrentInfo.speed;
        Vector2 position = transform.position;

        for (int i = 0; i < steps; i++)
        {
            velocity += Physics2D.gravity * genie.rigd.gravityScale * unitTime;
            position += velocity * unitTime;
            list.Add(position);
        }

        lineRenderer.SetPositions(list.ToArray());
    }
    

    public override EnergyInfo GetUpgradeEnergy()
    {
        return dragShootingInfos[level].upgradeEnergy;
    }

    Vector2 GetDir()
    {
        Vector2 dir = (Vector2)transform.position - Genie.GetMousePos();
        dir = dir.normalized;

        return dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!inUseSkill) return;

        // 적과 충돌하면 데미지줌
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Character enemy = collision.GetComponent<Character>();

            if(enemy)
            {
                enemy.Damage(CurrentInfo.damage);
            }
            else
            {
                Character e = collision.GetComponent<Character>();
                e.hp -= CurrentInfo.damage;
            }
            
        }
    }
}
