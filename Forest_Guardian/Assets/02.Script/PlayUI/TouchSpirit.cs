using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSpirit : MonoBehaviour
{
    [Header("AbsorbKey")]
    public GameObject Absorbkey;
    public GameObject Particle;

    [Header("Effect Sound")]
    public SoundManager soundManager;

    [Header("Player Controller")]
    public PlayerController playerController;

    private GameObject spiritObject;
    private bool touchSpirit = false;

    private System.Action<int> plusMethod; // 외부 클래스에서 활용.
    private int spiritNum; // 외부 클래스에서 활용.

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && touchSpirit == true)
        {
            Particle.SetActive(false);
            Particle.SetActive(true);
            spiritObject.transform.GetChild(0).GetComponent<DestroySpiritLight>().enabled = true;
            plusMethod(spiritNum);
            soundManager.PlayEffect(soundManager.AbsorbSpirit);
            playerController.moveOn = false;
            playerController.jumpOn = false;
            Invoke("enablePlayer", 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpiritObject"))
        {
            touchSpirit = true;
            spiritObject = collision.gameObject;
            Absorbkey.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SpiritObject"))
        {
            touchSpirit = false;
            spiritObject = null;
            Absorbkey.SetActive(false);
        }
    }

    public void plusSpirit(System.Action<int> method, int num)
    {
        plusMethod = method;
        spiritNum = num;
    }

    private void enablePlayer()
    {
        playerController.moveOn = true;
        playerController.jumpOn = true;
    }
}
