using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public Rigidbody2D rigd;
    public int damage = 1; // 데미지
    public float radius = 3; // 범위
    public int maxAttackNum = 3; // 최대 공격가능한 수
    public AudioClip hitSound; // 땅에 부딫힐 때 효과음
    public ParticleSystem hitParticle; // 땅에 부딫힐 때 이펙트

    private void Awake()
    {
        rigd = GetComponent<Rigidbody2D>();
    }

    public void Init(int damage, int maxAttackNum)
    {
        this.damage = damage;
        this.maxAttackNum = maxAttackNum;
    }

    public void NearDamage()
    {
        // 마우스 위치에 지정된 범위만큼 적들을 반환받음
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Enemy"));

        int length = Mathf.Min(colliders.Length, maxAttackNum);

        // 적에게 데미지 주기
        for (int i = 0; i < length; i++)
        {
            Character character = colliders[i].GetComponent<Character>();

            if (character)
            {
                character.Damage(damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 땅과 부딫히면 데미지입히고 비활성화
        if (collision.CompareTag("Ground"))
        {
            NearDamage();

            if (hitParticle)
            {
                Instantiate(hitParticle, transform.position, Quaternion.identity); // 파티클 생성
            }

            Destroy(gameObject);
            // gameObject.SetActive(false);
        }
    }

}
