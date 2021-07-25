using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : Character
{
    [Header("Animation")]
    public Animator anim;

    [Header("Attack Speed")]
    public float AttackSpeed = 5f;

    [Header("Move Speed")]
    private GameObject spirit;
    private bool isAttack;
    private float elpasedTime = 5f;

    [Header("StageManager")]
    public StageManager stageManager;

    private void Update()
    {
        if (isAttack == true && spirit != null)
        {
            DontRun();
            Attack();
        }
        else
        {
            Move();
            Run();
        }
    }
    private void Move()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(0f, 0f), Time.deltaTime * MoveSpeed);
    }
    private void Attack()
    {
        if (elpasedTime > AttackSpeed)
        {
            anim.SetTrigger("isAttack");
            elpasedTime = 0f;
            Invoke("AttackDamage", 2.0f);
        }
        elpasedTime += Time.deltaTime;
    }

    private void AttackDamage()
    {
        spirit.GetComponent<Character>().Damage(damageLevel);
    }

    public override void Die()
    {
        anim.SetTrigger("isDie");
        Invoke("setfalse", 1.5f);
    }

    public override void Damage(int damage)
    {
        if (!damageOn) return;
        if (damage == 0) return;

        hp -= damage;
        anim.SetTrigger("isHit");

        if (hp <= 0)
        {
            Die();
        }
    }

    public void Run()
    {
        if (isMoving == false)
        {
            isMoving = true;
            anim.SetBool("isRun", true);
        }
    }

    public void DontRun()
    {
        if (isMoving == true)
        {
            isMoving = false;
            anim.SetBool("isRun", false);
        }
    }

    private void setfalse()
    {
        stageManager.ReduceEnemyCount();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spirit"))
        {
            isAttack = true;
            spirit = collision.gameObject;
        }
    }
}
