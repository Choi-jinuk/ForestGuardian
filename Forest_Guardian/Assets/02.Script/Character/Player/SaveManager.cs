using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[SerializeField]
public class SpiritSaveInfo
{
    public string name; // 정령 이름
    public string pos; // 정령 위치
    public int hp; // 정령 체력

    public SpiritSaveInfo(string name, string pos, int hp)
    {
        this.name = name;
        this.pos = pos;
        this.hp = hp;
    }
}

[SerializeField]
public class EnemySaveInfo
{
    public string name; // 적 이름
    public string pos; // 적 위치
    public int hp; // 적 체력

    public EnemySaveInfo(string name, string pos, int hp)
    {
        this.name = name;
        this.pos = pos;
        this.hp = hp;
    }
}

[SerializeField]
public class SaveData
{
    public int id = 0; // 저장 순서

    #region Game
    public int treeHp; // 세계수 체력
    #endregion

    #region Player
    public int playerHp; // 지니 체력
    public string playerPos; // 플레이어 위치
    public string playerVelocity; // 플레이어 물리 값
    #endregion

    #region Skill
    public int mouseSkillLevel = 1; // 마우스공격 스킬레벨
    public int runSkillLevel = 0; // 대쉬스킬 레벨
    public int dragSkillLevel = 0; // 드래그 스킬레벨
    public int jumpSkillLevel = 0; // 더블점프 스킬레벨
    public int downSkillLevel = 0; // 내려찍기 스킬레벨
    #endregion

    #region Energy
    public int treeEnergy; // 나무 에너지
    public int waterEnergy; // 물 에너지
    public int stoneEnergy; // 돌 에너지
    public int fireEnergy; // 불 에너지
    public int lightEnergy; // 빛 에너지
    #endregion

    #region Spirit
    // 정령들의 위치 및 스텟 정보
    public List<SpiritSaveInfo> spiritSaveInfos = new List<SpiritSaveInfo>();
    #endregion

    #region Enemy
    // 적들의 위치 및 스탯 정보
    #endregion

    public SaveData() { }

    public SaveData(int id, int playerHp)
    {
        this.id = id;
        this.playerHp = playerHp;
    }
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager saveManager;
    public static SaveData saveData = new SaveData();
    public EnergyManager energyManager;
    public JsonData jsonData;
    public Genie genie;

    private void Awake()
    {
        if(saveManager == null)
        {
            saveManager = this;
        }

        if(energyManager == null)
        {
            energyManager = FindObjectOfType<EnergyManager>();
        }

        if (genie == null)
        {
            genie = FindObjectOfType<Genie>();
        }
    }

    Vector3 StringToPos(string str)
    {
        string[] tmpPosArray = str.Split(',');
        Vector2 pos = new Vector2(float.Parse(tmpPosArray[0]), float.Parse(tmpPosArray[1]));

        return pos;
    }

    int StringToInt(string str)
    {
        return int.Parse(str);
    }

    public void Save(string fileName)
    {
        // 플레이어 정보 저장
        saveData.playerHp = genie.hp;
        saveData.playerPos = genie.transform.position.x + "," + genie.transform.position.y;

        // 스킬 정보 저장
        saveData.mouseSkillLevel = genie.mouseSkill.level;
        saveData.runSkillLevel = genie.runSkill.level;
        saveData.dragSkillLevel = genie.dragSkill.level;
        saveData.jumpSkillLevel = genie.doubleJumpSkill.level;
        saveData.downSkillLevel = genie.takeDownSkill.level;

        // 기운 정보 저장
        if (energyManager)
        {
            saveData.treeEnergy = energyManager.energyInfo.tree;
            saveData.waterEnergy = energyManager.energyInfo.water;
            saveData.stoneEnergy = energyManager.energyInfo.rock;
            saveData.fireEnergy = energyManager.energyInfo.fire;
            saveData.lightEnergy = energyManager.energyInfo.light;
        }

        // 정령들 정보 저장
        Spirit[] spirits = FindObjectsOfType<Spirit>();
        
        foreach (var spirit in spirits)
        {
            saveData.spiritSaveInfos.Add(new SpiritSaveInfo(spirit.name, spirit.transform.position.x + "," + spirit.transform.position.y, spirit.hp));
        }

        // 적들 정보 저장
        Character[] enemys = FindObjectsOfType<Character>();
        


        jsonData = JsonMapper.ToJson(saveData);
        File.WriteAllText(Application.dataPath + "/Resource/" + fileName + ".json", jsonData.ToString()); // 지정된 경로에 jsonData 저장
    }

    public void Load(string fileName)
    {
        string path = Application.dataPath + "/Resource/" + fileName + ".json"; // 경로 지정

        // 저장경로에 파일이 존재하는지 검사
        if (File.Exists(path))
        {
            string jsonStr = File.ReadAllText(path); // 모든 텍스트 불러옴

            JsonData data = JsonMapper.ToObject(jsonStr); // 텍스트를 jsonData로 변환

            // SaveData data = JsonMapper.ToObject<SaveData>(jsonStr); 배열이 들어가있으면 원인모를 에러발생
            
            // 플레이어 정보 불러옴
            genie.hp = StringToInt(data["playerHp"].ToString());
            genie.transform.position = StringToPos(data["playerPos"].ToString());

            // 스킬 정보 불러옴
            genie.mouseSkill.level = StringToInt(data["mouseSkillLevel"].ToString());
            genie.runSkill.level = StringToInt(data["runSkillLevel"].ToString());
            genie.dragSkill.level = StringToInt(data["dragSkillLevel"].ToString());
            genie.doubleJumpSkill.level = StringToInt(data["jumpSkillLevel"].ToString());
            genie.takeDownSkill.level = StringToInt(data["downSkillLevel"].ToString());

            // 기운 정보 불러옴
            if (energyManager)
            {
                energyManager.energyInfo.tree = StringToInt(data["treeEnergy"].ToString());
                energyManager.energyInfo.water = StringToInt(data["waterEnergy"].ToString());
                energyManager.energyInfo.rock = StringToInt(data["stoneEnergy"].ToString());
                energyManager.energyInfo.fire = StringToInt(data["fireEnergy"].ToString());
                energyManager.energyInfo.light = StringToInt(data["lightEnergy"].ToString());
            }

            // 정령들 정보 불러옴
            GameObject spiritObject;

            int length = data["spiritSaveInfos"].Count;

            for (int i = 0; i < length; i++)
            {
                string spiritName = data["spiritSaveInfos"][i]["name"].ToString();
                Vector2 pos = StringToPos(data["spiritSaveInfos"][i]["pos"].ToString());
                int hp = StringToInt(data["spiritSaveInfos"][i]["hp"].ToString());

                Debug.Log(Resources.Load(spiritName));
                spiritObject = Resources.Load(spiritName) as GameObject;

                if (spiritObject)
                {
                    Spirit spirit = Instantiate(spiritObject, pos, Quaternion.identity).GetComponent<Spirit>();
                    spirit.hp = hp;
                }
                else
                {
                    Debug.Log(spiritName + " 이름을 가진 Spirit가 Resources에 없습니다!");
                }
            }
        }
        else
        {
            Debug.Log("파일이 존재하지 않습니다.");
        }
    }
}