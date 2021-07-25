using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // 피해량
    public float speed = 3f; // 이동속도
    public int maxCount = 3; // 최대 맞출수 있는 수
    public float maxDist = 30f; // 이동 가능한 최대 거리
    private float currentDist = 0f; // 현재 이동 거리
    protected Character lastHitCha; // 마지막에 맞은 캐릭터

    private void Start()
    {
        Destroy(gameObject, 5f); // 5초후에 삭제
    }

    private void Update()
    {
        float moveAmount = speed * Time.deltaTime;
        currentDist += moveAmount;

        transform.position += transform.right * moveAmount;

        if (maxDist <= currentDist)
        {
            Destroy(gameObject);
        }
    }

    public virtual void HitAction()
    {
        lastHitCha.Damage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 땅과 부딫히면 데미지입히고 비활성화
        if (collision.CompareTag("Monster"))
        {
            Character character = collision.GetComponent<Character>();

            if (character == lastHitCha) return;

            lastHitCha = character;
            HitAction();

            if (--maxCount <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Ground"))
        {
            // Destroy(gameObject);
        }
    }
}
