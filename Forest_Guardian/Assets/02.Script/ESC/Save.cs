using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Collections.Concurrent;
using System;
using System.Text;

public class User
{
    public int ID;
    public string Name;
    public int Level;
    public string Pos;
    public string InventoryItemList;      // 인벤토리에 있는 아이템
    public int Spirit; //정령
    public int Skill;


    public User(int id, string name, int level, string pos, string inventoryitemList, int spirit, int skill)
    {
        ID = id;
        Name = name;
        Level = level;
        Pos = pos;
        InventoryItemList = inventoryitemList;
        Spirit = spirit;
        Skill = skill;
    }
}

public class Save : MonoBehaviour
{
    [Header("Panel")]
    public GameObject ESCPanel;
    public GameObject SavePanel;

    public GameObject Player;
    public static Save SM;
    public User Mycharacter;

    void Start()
    {
        //if (SM == null)
        //{

        //    DontDestroyOnLoad(gameObject);
        //    SM = this;
        //}
        //else if (SM != this)
        //{
        //    Destroy(gameObject);
        //    Debug.Log(gameObject);
        //}
    }

    public void SaveBtn()
    {
        int clickCount = 1;

        if (clickCount > 0)
        {
            clickCount++;
            StartCoroutine(SaveUserData());

            SavePanel.SetActive(false);

            ESCPanel.SetActive(false);



            Time.timeScale = 1;
        }

        int length = 1;

        //StartCoroutine(SaveItemData());
        for (int i = 0; i < length; i++)
        {
        StartCoroutine(SaveUserData());

        SavePanel.SetActive(false);

        ESCPanel.SetActive(false);



        Time.timeScale = 1;
        }
        

    }

    IEnumerator SaveUserData()
    {

        int ID = 0;
        string Name = "Purry";
        int Level = 1;
        string Pos = Player.transform.position.x + "," + Player.transform.position.y;
        //string Rotation = Player.transform.rotation.eulerAngles.x + "," + Player.transform.rotation.eulerAngles.y + "," + Player.transform.rotation.eulerAngles.z;
        string InventoryItemList = "0/1";
        int Spirit = 0;
        int Skill = 0;
        Mycharacter = (new User(ID, Name, Level, Pos, InventoryItemList, Spirit, Skill));
        //Debug.Log(Player.name + "::::" + Mycharacter.Rotation);
        JsonData UserJson = JsonMapper.ToJson(Mycharacter);

        // 새 파일을 만들고 지정된 문자열을 파일에 쓴다음 닫습니다.  대상 파일이 이미 있으면 덮어씁니다. 
        File.WriteAllText(Application.dataPath + "/Resource/User.json", UserJson.ToString());
        Debug.Log(UserJson);

        yield return null;
    }

    // 불러오기 .. 
    public void LoadBtn()
    {

        StartCoroutine(LoadUserData());  // 캐릭터 디비를 불러온다.
        StartCoroutine(CreateCharacter());  // 캐릭터 생성.

        // 유저 디비를 불러온다.
        // 아이템과 유저 정보를 적용한다.  

        SavePanel.SetActive(false);

        ESCPanel.SetActive(false);



        Time.timeScale = 1;
    }

    IEnumerator LoadUserData()
    {

        string Jsonstring = File.ReadAllText(Application.dataPath + "/Resource/User.json");
        Debug.Log(Jsonstring);

        JsonData UserData = JsonMapper.ToObject(Jsonstring);

        GetUserInfo(UserData);
        yield return null;
    }

    private void GetUserInfo(JsonData name)
    {




        SM.Mycharacter = new User(int.Parse(name["ID"].ToString()),
                    name["Name"].ToString(), int.Parse(name["Level"].ToString()),
                    name["Pos"].ToString(),
                    name["InventoryItemList"].ToString(),
                    int.Parse (name["Spirit"].ToString()),
                    int.Parse (name["Skill"].ToString()));


        //for (int i = 0; i < GM.Mycharacter.Count; i++)
        //{
        //    Debug.Log(GM.Mycharacter[i].Name);

        //}



    }

    public GameObject PlayerPrefab;
    IEnumerator CreateCharacter()
    {

        string[] tmpPosArray = Mycharacter.Pos.Split(',');
        //string[] tmpRoArray = Mycharacter.Rotation.Split(',');

        //Debug.Log(tmpRoArray[0] + ";" + tmpRoArray[1] + ";" + tmpRoArray[2] + ";");
        Vector2 TmpPos = new Vector2(float.Parse(tmpPosArray[0]), float.Parse(tmpPosArray[1]));
        // Vector3 TmpRo = new Vector3(float.Parse(tmpRoArray[0]), float.Parse(tmpRoArray[1]), float.Parse(tmpRoArray[2]));


        Player = (GameObject)Instantiate(PlayerPrefab, TmpPos, Quaternion.identity);
        //Player.GetComponent<PlayerController>().CurrRo = TmpRo.y;
        //Destroy(Player);
        yield return null;
    }



}
