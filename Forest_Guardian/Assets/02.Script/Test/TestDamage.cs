using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public int damage = 10;
    public Character character;
    public KeyCode keyCode;

    private void Awake()
    {
        if(character == null)
        {
            character = GetComponent<Character>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            character.Damage(damage);
        }
    }
}
