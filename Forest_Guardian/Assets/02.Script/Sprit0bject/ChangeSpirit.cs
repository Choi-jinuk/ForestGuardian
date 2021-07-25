using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeSpirit : MonoBehaviour
{

    enum particleColor { RED, ORANGE, YELLOW, GREEN, BLUE, INDIGO, PURPLE, WHITE, LIGHTBRWON };

    [Header("Player Particle")]
    [SerializeField]
    private particleColor ParticleColor = particleColor.RED;
    public ParticleSystem particleSystem;
    public SpriteRenderer palyerParticle;

    [Header("Spirit Object")]
    public TouchSpirit touchSpirit;
    public EnergyManager energyManager;
    enum WhatThisSpirit { OneTree, TwoTree, OneWater, TwoWater, OneStone, TwoStone, OneFire, TwoFire, OneLight, TwoLight }
    [SerializeField]
    private WhatThisSpirit whatThisSpirit;

    private ParticleSystem.MainModule main;
    private Color color;
    private void Start()
    {
        main = particleSystem.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ChangeParcileColor();
            PlusSpiritObject();
        }
    }

    private void ChangeParcileColor()
    {
        switch (ParticleColor)
        {
            case particleColor.RED:
                main.startColor = Color.red;
                palyerParticle.color = Color.red;
                break;
            case particleColor.ORANGE:
                color = new Color32(255, 127, 0, 255);
                main.startColor = color;
                palyerParticle.color = new Color32(255, 180, 0, 255);
                break;
            case particleColor.YELLOW:
                main.startColor = Color.yellow;
                palyerParticle.color = Color.yellow;
                break;
            case particleColor.GREEN:
                main.startColor = Color.green;
                palyerParticle.color = Color.green;
                break;
            case particleColor.BLUE:
                color = new Color32(0, 255, 255, 255);
                main.startColor = color;
                palyerParticle.color = new Color32(0, 255, 255, 255);
                break;
            case particleColor.INDIGO:
                color = new Color32(18, 0, 120, 255);
                main.startColor = color;
                palyerParticle.color = new Color32(18, 0, 120, 255);
                break;
            case particleColor.PURPLE:
                main.startColor = new Color(1, 0, 1, 1);
                palyerParticle.color = new Color(1, 0, 1, 1);
                break;
            case particleColor.WHITE:
                main.startColor = new Color(1, 1, 1, 1);
                palyerParticle.color = new Color(1, 1, 1, 1);
                break;
            case particleColor.LIGHTBRWON:
                color = new Color32(219, 198, 148, 255);
                main.startColor = color;
                palyerParticle.color = new Color32(219, 198, 148, 255);
                break;
        }
    }

    private void PlusSpiritObject()
    {
        switch (whatThisSpirit)
        {
            case (WhatThisSpirit.OneTree):
                plusOneTree();
                break;
            case (WhatThisSpirit.TwoTree):
                plusTwoTree();
                break;
            case (WhatThisSpirit.OneWater):
                plusOneWater();
                break;
            case (WhatThisSpirit.TwoWater):
                plusTwoWater();
                break;
            case (WhatThisSpirit.OneStone):
                plusOneStone();
                break;
            case (WhatThisSpirit.TwoStone):
                plusTwoStone();
                break;
            case (WhatThisSpirit.OneFire):
                plusOneFire();
                break;
            case (WhatThisSpirit.TwoFire):
                plusTwoFire();
                break;
            case (WhatThisSpirit.OneLight):
                plusOneLight();
                break;
            case (WhatThisSpirit.TwoLight):
                plusTwoLight();
                break;
        }
    }

    #region PlusObejct
    private void plusOneTree()
    {
        touchSpirit.plusSpirit(energyManager.PlusTree, 1);
    }
    private void plusTwoTree()
    {
        touchSpirit.plusSpirit(energyManager.PlusTree, 2);
    }
    private void plusOneWater()
    {
        touchSpirit.plusSpirit(energyManager.PlusWater, 1);
    }
    private void plusTwoWater()
    {
        touchSpirit.plusSpirit(energyManager.PlusWater, 2);
    }
    private void plusOneStone()
    {
        touchSpirit.plusSpirit(energyManager.PlusRock, 1);
    }
    private void plusTwoStone()
    {
        touchSpirit.plusSpirit(energyManager.PlusRock, 2);
    }
    private void plusOneFire()
    {
        touchSpirit.plusSpirit(energyManager.PlusFire, 1);
    }
    private void plusTwoFire()
    {
        touchSpirit.plusSpirit(energyManager.PlusFire, 2);
    }
    private void plusOneLight()
    {
        touchSpirit.plusSpirit(energyManager.PlusLight, 1);
    }
    private void plusTwoLight()
    {
        touchSpirit.plusSpirit(energyManager.PlusLight, 2);
    }
    #endregion
}
