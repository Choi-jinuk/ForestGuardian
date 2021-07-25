using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePanel : MonoBehaviour
{
    //Manager에 포함해뒀다가 몬스터 처치가 끝날 경우 StartTimer메소드를 호출하여
    //타이머를 실행시키는 방식으로 작성
    [Header("Time")]
    public Text timeText; //시간 표기할 텍스트
    public float fullTime; //전체 시간 초단위

    [Header("Stage")]
    public Text stageText; //stage 텍스트
    public Collider2D fireColl;  // 플레이어가 못지나가게 막을 콜라이더
    public Collider2D stoneColl; //스테이지별로 열리는 Collider 제작
    public Collider2D waterColl;
    public Collider2D lightColl;

    [Header("PlayerLV")]
    public Text lvText;

    [Header("Player")]
    public GameObject Player;
    public float speed;

    [Header("UI")]
    public GameObject mapPanel;
    public GameObject playerPanel;
    public GameObject timePanel;
    public GameObject coolTimeUI;
    public GameObject escUI;
    public GameObject skillUI;

    [Header("Camera")]
    public Camera_Map mapManager;
    public GameObject fireCamera;
    public GameObject stoneCamera;
    public GameObject waterCamera;
    public GameObject lightCamera;
    public GameObject mainCamera;

    [Header("AnimationTime")]
    public float fireAnimTime;
    public float stoneAnimTime;
    public float waterAnimTime;
    public float lightAnimTime;

    [Header("Tower")]
    public GameObject fireTower;
    public GameObject stoneTower;
    public GameObject waterTower;
    public GameObject lightTower;

    [Header("Change Stage Sceen")]
    public GameObject changeSceenObject;
    public changeBackgroundSceen changeSceen;

    [Header("StageManager")]
    public StageManager stageManager;

    int stage = 1; //스테이지 및 플레이어 레벨 기본값 1
    int playerLV = 1;

    Outline timeOutline;
    Outline stageOutline;
    float warningTime = 30.0f;
    Vector3 startPosition = new Vector3(0, 0, 0);

    public void Start()
    {
        timeOutline = timeText.GetComponent<Outline>();
        stageOutline = stageText.GetComponent<Outline>();
        StartTimer();
    }

    #region Timer
    public void StartTimer()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        float time = 0.0f; //줄어드는 시간 초기화
        UIChange(fullTime - time);
        UIColor();

        while (time < fullTime - warningTime) //warningTime default:30초
        {
            yield return new WaitForSeconds(1.0f);
            time += 1.0f;
            UIChange(fullTime - time);
        }

        UIColor(isYellow: true);
        //todo; 30초 남음 Warning경고창 띄우기

        while (time < fullTime)
        {
            yield return new WaitForSeconds(1.0f);
            time += 1.0f;
            UIChange(fullTime - time);
        }
        UIColor(isRed:true);
        StartStage();
    }
    #endregion

    #region UIChange
    void UIColor(bool isRed = false, bool isYellow = false) //시간 텍스트의 색상을 바꿔줌
    {
        if (isRed)
        {
            timeOutline.effectColor = new Color(1f,0f,0f);
            stageOutline.effectColor = new Color(1f, 0f, 0f);
        }
        else if (isYellow)
        {
            timeOutline.effectColor = new Color(1f, 1f, 0f);
            stageOutline.effectColor = new Color(1f, 1f, 0f);
        }
        else
        {
            timeOutline.effectColor = new Color(0f, 100f / 255f, 0f);
            stageOutline.effectColor = new Color(0f, 100f / 255f, 0f);
        }
    }
    void UIChange(float _time) //시간 텍스트 UI를 바꿔주는 메소드
    {
        int minute = (int)_time / 60;
        int second = (int)_time % 60;

        timeText.text = string.Format("{0:00}:{1:00}", minute, second);
    }
    void StageUIChange()
    {
        stageText.text = "stage" + stage;
    }
    void LevelUIChange()
    {
        lvText.text = "LV" + playerLV;
    }
    #endregion

    #region Stage
    public void StageClear()
    {
        switch (stage)
        {
            case 4:
                waterColl.isTrigger = true;
                StartCoroutine(CameraMove(0));
                break;
            case 2:
                stoneColl.isTrigger = true;
                StartCoroutine(CameraMove(1));
                break;
            case 1:
                fireColl.isTrigger = true;
                StartCoroutine(CameraMove(2));
                break;
            case 8:
                lightColl.isTrigger = true;
                StartCoroutine(CameraMove(3));
                break;
            default:
                StartTimer();
                break;
        }

        stage++; // 총 20스테이지
        StageUIChange();
        PlayerLVUp(); //todo; 레벨 상승 및 이펙트&사운드 => 1스테이지 1레벨 인지?
    }

    void PlayerLVUp() //레벨 상승 및 이펙트&사운드 => 1스테이지 1레벨 인지?
    {
        playerLV++;
        LevelUIChange();
        //todo; 지니가 레벨이 오를 때 마다 증가하는 기운 흡수량 기능 추가
    }

    IEnumerator CameraMove(int map) //스테이지가 열릴 때 화면이 전환되면서 해당 구역 한 번 보여주기.
    {
        Invoke("stayPlayer", 0.3f);

        mapPanel.SetActive(false); //UI 모두 끄기
        timePanel.SetActive(false);
        playerPanel.SetActive(false);
        coolTimeUI.SetActive(false);
        skillUI.SetActive(false);
        escUI.SetActive(false);

        changeSceenObject.SetActive(true);

        changeSceen.changeBackSceenFadeOut();
        while (mainCamera.transform.position.sqrMagnitude > 109f)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, startPosition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        changeSceen.changeBackSceenFadeIn();
        switch (map)
        {
            case 0: //water
                mapManager.cam = waterCamera.GetComponent<Camera>();
                Invoke("RestartPlayer", waterAnimTime);
                waterCamera.SetActive(true);
                waterTower.GetComponent<Animator>().SetTrigger("isBroken");
                break;
            case 1: //stone
                mapManager.cam = stoneCamera.GetComponent<Camera>();
                Invoke("RestartPlayer", stoneAnimTime);
                stoneCamera.SetActive(true);
                stoneTower.GetComponent<Animator>().SetTrigger("isBroken");
                break;
            case 2: //fire
                mapManager.cam = fireCamera.GetComponent<Camera>();
                Invoke("RestartPlayer", fireAnimTime);
                fireCamera.SetActive(true);
                fireTower.GetComponent<Animator>().SetTrigger("isBroken");
                break;
            case 3: //light
                break;
        }

    }
    #endregion

    #region PlayerCtrl
    void stayPlayer()
    {
        Player.GetComponent<PlayerController>().enabled = false;
        Player.GetComponent<Genie>().enabled = false;
    }

    void RestartPlayer()
    {
        mapManager.cam = mainCamera.GetComponent<Camera>();
        waterCamera.SetActive(false);
        Player.GetComponent<PlayerController>().enabled = true;
        Player.GetComponent<Genie>().enabled = true;

        mapPanel.SetActive(true); //UI 모두 끄기
        timePanel.SetActive(true);
        playerPanel.SetActive(true);
        coolTimeUI.SetActive(true);

        StartTimer();
    }
    #endregion

    #region Enemy

    void StartStage()
    {
        switch (stage)
        {
            case 1:
                stageManager.SpawnWarrior(3);
                stageManager.ChangeBackGround(true);
                break;
            case 2:
                stageManager.SpawnWarrior(4);
                stageManager.ChangeBackGround(true);
                break;
            case 3:
                stageManager.SpawnArcher(2);
                stageManager.ChangeBackGround(true);
                break;
            case 4: break;
            case 5: break;
            case 6: break;
            case 7: break;
            case 8: break;
            case 9: break;
            case 10: break;
            case 11: break;
            case 12: break;
            case 13: break;
            case 14: break;
            case 15: break;
            case 16: break;
            case 17: break;
            case 18: break;
            case 19: break;
            case 20: break;

        }
    }
    #endregion
}
