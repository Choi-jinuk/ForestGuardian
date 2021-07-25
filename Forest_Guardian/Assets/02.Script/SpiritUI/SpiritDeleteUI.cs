using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritDeleteUI : MonoBehaviour
{
    public static SpiritDeleteUI instance;

    [Header("Main")]
    public GameObject panel; // UI 패널
    public Spirit targetSpirit; // 삭제할 정령

    [Header("Energy")]
    public EnergyManager energyManager;
    public EnergyInfo deleteEnergy;
    public Text treeText;
    public Text waterText;
    public Text stoneText;
    public Text fireText;
    public Text lightText;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        panel.SetActive(false);
    }

    // ui 활성화
    public void Open(Spirit spirit)
    {
        if (spirit == null) return;

        targetSpirit = spirit;

        DeleteEnergyUpdate();
        DeleteEnergyTextUpdate();

        panel.SetActive(true);

        MouseManager.SetMouseType(MouseType.DELETE);
    }

    // ui 비활성화
    public void Close()
    {
        panel.SetActive(false);

        MouseManager.SetMouseType(MouseType.NORMAL);
    }

    // 기운 흡수량 업데이트
    public void DeleteEnergyUpdate()
    {
        if (targetSpirit == null) return;

        deleteEnergy = targetSpirit.needEnergy;

        deleteEnergy.tree = (int)(deleteEnergy.tree * 0.7f);
        deleteEnergy.water = (int)(deleteEnergy.water * 0.7f);
        deleteEnergy.stone = (int)(deleteEnergy.stone * 0.7f);
        deleteEnergy.fire = (int)(deleteEnergy.fire * 0.7f);
        deleteEnergy.light = (int)(deleteEnergy.light * 0.7f);
    }
    
    // 기운 흡수량 텍스트 업데이트
    public void DeleteEnergyTextUpdate()
    {
        treeText.text = deleteEnergy.tree.ToString();
        waterText.text = deleteEnergy.water.ToString();
        stoneText.text = deleteEnergy.stone.ToString();
        fireText.text = deleteEnergy.fire.ToString();
        lightText.text = deleteEnergy.light.ToString();
    }

    // 정령 삭제 및 기운 흡수
    public void Delete()
    {
        if (targetSpirit == null) return;

        Destroy(targetSpirit.gameObject);

        if (energyManager)
        {
            energyManager.PlusTree(deleteEnergy.tree);
            energyManager.PlusWater(deleteEnergy.water);
            energyManager.PlusRock(deleteEnergy.stone);
            energyManager.PlusFire(deleteEnergy.fire);
            energyManager.PlusLight(deleteEnergy.light);
        }

        Close();
    }


}
