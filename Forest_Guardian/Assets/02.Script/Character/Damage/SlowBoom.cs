using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBoom : MonoBehaviour
{
    public Rigidbody2D rigd;
    public int damage = 0; // 데미지
    public float radius = 3; // 범위
    public int maxAttackNum = 3; // 최대 공격가능한 수
    public AudioClip hitSound; // 효과음
    public ParticleSystem hitParticle; // 이펙트
    public Slow slow; // 슬로우

    private void Awake()
    {
        rigd = GetComponent<Rigidbody2D>();
        slow = GetComponent<Slow>();
    }

    public void Init(int damage, int maxAttackNum)
    {
        this.damage = damage;
        this.maxAttackNum = maxAttackNum;
    }

    public void NearSlow(float amount)
    {
        // 마우스 위치에 지정된 범위만큼 적들을 반환받음
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Enemy"));

        int length = Mathf.Min(colliders.Length, maxAttackNum);

        for (int i = 0; i < length; i++)
        {
            Character character = colliders[i].GetComponent<Character>();

            if (character)
            {
                character.Damage(damage);
                Slow slow = character.gameObject.AddComponent<Slow>(); // Slow 스크립트 추가
                slow.amount = amount;
                slow.AddStatus(character);
            }
        }

        Destroy(gameObject);
    }

}
