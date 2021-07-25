using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI instance;

    [Header("Info")]
    public SelectSkillButton selectSkillButton; // 선택한 스킬
    public Text skillNameText; // 스킬 이름 텍스트
    public Text descriptionText; // 스킬 설명 텍스트
    public VideoPlayer skillVideo; // 스킬 영상

    [Header("SelectButton")]
    public SelectSkillButton[] selectSkillButtons_MouseAttack;
    public SelectSkillButton[] selectSkillButtons_Run;
    public SelectSkillButton[] selectSkillButtons_Drag;
    public SelectSkillButton[] selectSkillButtons_DoubleJump;
    public SelectSkillButton[] selectSkillButtons_TakeDown;
    public Color upgradedColor; // 스킬 업그레이드한 버튼의 색상
    public Color possibleColor; // 스킬 업그레이드가 가능한 버튼의 색상
    public Color impossibleColor; // 스킬 업그레이드가 불가능한 버튼의 색상
    public Transform selectSlot; // 선택한 스킬 버튼의 테두리 슬롯

    [Header("GenieEnergy")]
    public Text genieTreeText;
    public Text genieWaterText;
    public Text genieStoneText;
    public Text genieFireText;
    public Text genieLightText;

    [Header("SkillEnergy")]
    public Transform skillEnergyPanel;
    public Text skillTreeText;
    public Text skillWaterText;
    public Text skillStoneText;
    public Text skillFireText;
    public Text skillLightText;
    public Vector2 skillEnergyOffset = new Vector2(200, -50);

    [Header("Button")]
    public Button upgradeButton;
    public Button cancelButton;

    [Header("Energy Manager")]
    public EnergyManager energyManager;

    // 싱글턴 구현
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }
    private void OnEnable()
    {
        genieTreeText.text = energyManager.energyInfo.tree.ToString();
        genieWaterText.text = energyManager.energyInfo.water.ToString();
        genieStoneText.text = energyManager.energyInfo.rock.ToString();
        genieFireText.text = energyManager.energyInfo.fire.ToString();
        genieLightText.text = energyManager.energyInfo.light.ToString();
    }

    private void OnDisable() // Skill UI 종료시 Play UI Text 변경
    {
        energyManager.PlusTree(0);
        energyManager.PlusWater(0);
        energyManager.PlusRock(0);
        energyManager.PlusFire(0);
        energyManager.PlusLight(0);
    }
    private void Start()
    {
        if (selectSkillButton)
        {
            UI_Update(selectSkillButton);
        }
    }

    public void UI_Update(SkillInfo skillInfo)
    {
        if (skillInfo == null) return;

        UpgradeButtonActiveUpdate(skillInfo);
        TextUpdate(skillInfo);

        if (skillVideo == null) return;

        if (skillVideo.isPlaying)
        {
            skillVideo.Stop();
            skillVideo.frame = 0;
        }

        if (skillInfo.video)
        {
            skillVideo.clip = skillInfo.video;
            skillVideo.Play(); // 스킬 영상 시작
        }
    }

    public void UI_Update(SelectSkillButton skillButton)
    {
        if (skillButton == null) return;

        UI_Update(skillButton.skillInfo);
        selectSkillButton = skillButton;
        selectSlot.transform.position = skillButton.transform.position; // 선택슬롯 스킬버튼으로 이동
        SkillEnergyTextUpdate(skillButton.skillInfo.upgradeEnergy);
        skillEnergyPanel.position = skillButton.transform.position + (Vector3)skillEnergyOffset; // 필요한 기운패널 버튼 옆으로 이동
    }

    public void SelectButtonUpdate()
    {
        for (int i = 0; i < selectSkillButtons_MouseAttack.Length; i++)
        {
            selectSkillButtons_MouseAttack[i].ButtonUpdate();
        }

        for (int i = 0; i < selectSkillButtons_Run.Length; i++)
        {
            selectSkillButtons_Run[i].ButtonUpdate();
        }

        for (int i = 0; i < selectSkillButtons_Drag.Length; i++)
        {
            selectSkillButtons_Drag[i].ButtonUpdate();
        }

        for (int i = 0; i < selectSkillButtons_DoubleJump.Length; i++)
        {
            selectSkillButtons_DoubleJump[i].ButtonUpdate();
        }

        for (int i = 0; i < selectSkillButtons_TakeDown.Length; i++)
        {
            selectSkillButtons_TakeDown[i].ButtonUpdate();
        }
    }

    // 업그레이드 버튼 조건에 따라 활성화
    public void UpgradeButtonActiveUpdate(SkillInfo skillInfo)
    {
        Genie genie = Genie.instance;
        int level = genie.GetSkillType(skillInfo).level;

        // 업그레이드할 스킬 레벨이 다음 지니의 스킬 레벨일 때
        upgradeButton.interactable = skillInfo.level == level + 1;
    }

    public void TextUpdate(SkillInfo skillInfo)
    {
        skillNameText.text = skillInfo.skillName;
        descriptionText.text = skillInfo.description;
    }

    public void GenieEnergyTextUpdate(EnergyManager energyManager)
    {
        genieTreeText.text = energyManager.energyInfo.tree.ToString();
        genieWaterText.text = energyManager.energyInfo.water.ToString();
        genieStoneText.text = energyManager.energyInfo.rock.ToString();
        genieFireText.text = energyManager.energyInfo.fire.ToString();
        genieLightText.text = energyManager.energyInfo.light.ToString();
    }
    
    public void SkillEnergyTextUpdate(EnergyInfo energyInfo)
    {
        skillTreeText.text = energyInfo.tree.ToString();
        skillWaterText.text = energyInfo.water.ToString();
        skillStoneText.text = energyInfo.stone.ToString();
        skillFireText.text = energyInfo.fire.ToString();
        skillLightText.text = energyInfo.light.ToString();
    }

}
