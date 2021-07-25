using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct SceneBackground
{
    public enum BackgroundInfo
    {
        Forest, Water, Stone, Fire, Light
    }
    public GameObject Background1;
    public GameObject Background2;
    public GameObject Background3;
    public GameObject Background4;
    public GameObject Background5;
    public GameObject Background6;

    public GameObject FrontGround3;
    public GameObject FrontGround4;

    public GameObject NarrowMapImage;//작은 미니맵 추가
    public string NarrowMapName;
}

public class SceneChange : MonoBehaviour
{
    [Header("SceneBackground")]
    public SceneBackground[] sceneBackground;
    enum Background { Forest, Water, Stone, Fire, Light };

    [Header("ChangeBackground")]
    [SerializeField]
    private Background trueBackground;
    [SerializeField]
    private Background falseBackground;

    [Header("BGM")]
    public GameObject BackgroundManager;

    [Header("NarrowMapText")]
    public Text narrowMapText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ChangeScene((int)trueBackground, (int)falseBackground);
            ChangeBGM((int)trueBackground);
        }
    }

    private void ChangeScene(int SetTrueBackground, int SetFalseBakcground)
    {
        #region BackgroundSetting

        sceneBackground[SetTrueBackground].Background1.SetActive(true);
        sceneBackground[SetTrueBackground].Background2.SetActive(true);
        sceneBackground[SetTrueBackground].Background3.SetActive(true);
        sceneBackground[SetTrueBackground].Background4.SetActive(true);
        sceneBackground[SetTrueBackground].Background5.SetActive(true);
        sceneBackground[SetTrueBackground].Background6.SetActive(true);

        sceneBackground[SetTrueBackground].FrontGround3.SetActive(true);
        sceneBackground[SetTrueBackground].FrontGround4.SetActive(true);
        sceneBackground[SetTrueBackground].NarrowMapImage.SetActive(true); //작은 미니맵 추가

        sceneBackground[SetFalseBakcground].Background1.SetActive(false);
        sceneBackground[SetFalseBakcground].Background2.SetActive(false);
        sceneBackground[SetFalseBakcground].Background3.SetActive(false);
        sceneBackground[SetFalseBakcground].Background4.SetActive(false);
        sceneBackground[SetFalseBakcground].Background5.SetActive(false);
        sceneBackground[SetFalseBakcground].Background6.SetActive(false);

        sceneBackground[SetFalseBakcground].FrontGround3.SetActive(false);
        sceneBackground[SetFalseBakcground].FrontGround4.SetActive(false);
        sceneBackground[SetFalseBakcground].NarrowMapImage.SetActive(false); //작은 미니맵 추가


        narrowMapText.text = sceneBackground[SetTrueBackground].NarrowMapName;//작은 미니맵 이름
        #endregion
    }

    private void ChangeBGM(int BGM)
    {
        BackgroundManager.GetComponent<SoundManager>().PlayBGM(BGM);
    }
}
