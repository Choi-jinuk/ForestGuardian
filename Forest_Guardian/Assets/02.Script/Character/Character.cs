using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rigd;
    public AudioSource audioSource;

    [Header("Stat")]
    public int maxHp = 100; // 최대 체력
    public int hp = 100; // 현재 체력
    public int damageLevel;
    public bool damageOn = true; // 데미지 활성화 여부
    public bool healOn = true; // 힐 활성화 여부

    public float MoveSpeed = 1f;
    
    public bool isMoving = false;
    public List<CharacterStatus> statuses = new List<CharacterStatus>();

    private void Awake()
    {
        Init();
    }

    // 초기화
    public virtual void Init()
    {
        if(animator == null)
            animator = GetComponentInChildren<Animator>();
        if (rigd == null)
            rigd = GetComponent<Rigidbody2D>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();


        hp = maxHp;
        
    }

    // 데미지
    public virtual void Damage(int damage)
    {
        if (!damageOn) return;
        if(damage == 0) return;

        hp -= damage;

        Debug.Log(name + " " + damage + " Damage");

        if (hp <= 0)
        {
            Die();
        }
    }

    // 치유
    public virtual void Heal(int heal)
    {
        if (!healOn) return;

        hp += heal;

        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    // 사망
    public virtual void Die()
    {

        Debug.Log(name + " Die");
    }

}

