using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private Button button;
    public Text warningText; // 에러 문구

    public EnergyManager energyManager;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnUpgradeSkill()
    {
        SkillInfo info = UpgradeUI.instance.selectSkillButton.skillInfo;

        // 현재 업그레이드 할 기운 있는지 검사
        if (UpgradeEnergyCheck(info))
        {
            // 기운 있으면 업그레이드
            SkillUpgrade(info);

            // 텍스트, 버튼 업데이트
            button.interactable = false;
        }
        // 기운이 없으면
        else
        {
            // 경고창 띄움
            WarningSetText("업그레이드에 필요한 기운이 부족합니다.");

            // 2초 후 경고창 닫음
            Invoke("WarningFade", 2f);
        }
    }

    public void WarningSetText(string str)
    {
        warningText.text = str;
    }
    
    public void WarningFade()
    {
        warningText.text = "";
    }

    // 스킬 업그레이드
    public void SkillUpgrade(SkillInfo skillInfo)
    {
        // 레벨업
        Genie.instance.GetSkillType(skillInfo).level++;

        // 에너지 감소
        EnergyInfo skillEnergy = skillInfo.upgradeEnergy;


        energyManager.energyInfo.tree -= skillEnergy.tree;
        energyManager.energyInfo.water -= skillEnergy.water;
        energyManager.energyInfo.rock -= skillEnergy.stone;
        energyManager.energyInfo.fire -= skillEnergy.fire;
        energyManager.energyInfo.light -= skillEnergy.light;

        UpgradeUI.instance.SelectButtonUpdate();
        UpgradeUI.instance.GenieEnergyTextUpdate(energyManager);
    }

    // 업그레이드가 가능한지 체크
    public bool UpgradeEnergyCheck(SkillInfo skillInfo)
    {
        EnergyInfo skillEnergy = skillInfo.upgradeEnergy;

        if (energyManager.energyInfo.tree >= skillEnergy.tree)
        {
            if (energyManager.energyInfo.water >= skillEnergy.water)
            {
                if (energyManager.energyInfo.light >= skillEnergy.light)
                {
                    if (energyManager.energyInfo.rock >= skillEnergy.stone)
                    {
                        if (energyManager.energyInfo.fire >= skillEnergy.fire)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}
