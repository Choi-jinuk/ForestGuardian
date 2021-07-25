using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpirit : Spirit
{
    [Header("Rock")]
    public Boom rockObject; // 돌
    public Transform rockSpawnTr; // 돌 생성위치

    public override void Attack()
    {
        if (!attackTarget) return; // enemy가 아니면 중단
        
        if (animator)
        {
            ActionAnimSpeedUpdate();
            animator.SetBool("isAction", true); // 애니메이션에서 던지기 이벤트 실행
        }
    }

    // 돌 던지기
    public void Throw()
    {

        Boom rock = Instantiate(rockObject, rockSpawnTr.position, Quaternion.identity); // 돌 생성
        rock.Init(damage, maxAttackNum); // 돌 초기화

        rock.rigd.velocity = GetVelocity(rockSpawnTr.position, attackTarget.transform.position, 45); // 적 이동전 좌표로 날림

        if (!attackTarget.isMoving) return; // 적이 이동중이 아니면 예측샷안하고 날림

        // 이동중이면 예측샷 시작함

        Vector2 velocity = rock.rigd.velocity; // 돌의 속도
        Vector2 position = rock.transform.position; // 돌의 위치

        float time = 0f;
        float timestep = Time.fixedDeltaTime * Physics.defaultSolverVelocityIterations; // 실제 단위시간
        Vector2 gravityAccel = Physics2D.gravity * timestep * timestep; // 단위시간당 중력가속도
        float drag = 1f - timestep * rock.rigd.drag; // 실제 적용될 저항값
        Vector2 moveStep = velocity * timestep; // 단위시간당 이동량

        // 돌 날라가는 예측시간, 위치 구함 (100프레임동안)
        for (int i = 0; i < 100; ++i)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            position += moveStep;

            if (Physics2D.OverlapCircle(position, 1f, Genie.instance.controller.whatIsGround))
            {
                time = timestep * i;
                break;
            }

        }

        // 어느 방향으로 이동중인지 알아냄
        Vector2 dir = attackTarget.transform.position.x > 0 ? Vector2.left : Vector2.right;

        // 돌 예측 시간으로 적 이동 예측지점 알아냄 (enemy.Speed는 방향에 따라서 -값을 넣어줘야됨)
        Vector3 futurePosition = (Vector2)attackTarget.transform.position + (dir * attackTarget.MoveSpeed * time);

        // 위치 재조정
        rock.rigd.velocity = GetVelocity(rockSpawnTr.position, futurePosition, 45);
    }

    // 원하는 위치를 원하는 각도로 날라가는 Velocity를 구함 (각도상으로 날라가는게 불가능한 방향이면 에러 호출)
    protected Vector3 GetVelocity(Vector3 currentPos, Vector3 targetPos, float initialAngle)
    {
        float gravity = Physics2D.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector2 planarTarget = new Vector2(targetPos.x, 0);
        Vector2 planarPosition = new Vector2(currentPos.x, 0);

        float distance = Vector2.Distance(planarTarget, planarPosition);
        float yOffset = currentPos.y - targetPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector2 velocity = new Vector2(initialVelocity * Mathf.Cos(angle), initialVelocity * Mathf.Sin(angle));

        float angleBetweenObjects = Vector2.Angle(Vector2.right, planarTarget - planarPosition) * (targetPos.x > currentPos.x ? 1 : -1);
        Vector2 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector2.up) * velocity;

        return finalVelocity;
    }

}
