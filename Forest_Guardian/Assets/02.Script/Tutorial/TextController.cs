using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public string[] textString;
    public Text uiText;

    [Header("Tutorial")]
    public GameObject tutorial;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;
    

    [Header("UI Image")]
    public GameObject[] uiImage;
    public GameObject background;
    public Sprite SkillbackgroundImage;
    public Sprite originbackgroundImage;

    [Header("Skill Panel")]
    public GameObject skillPanel;

    [Header("Player")]
    public GameObject Player;

    [Header("UI Audio")]
    public SoundManager soundManager;

    [Header("Audio Clip")]
    public AudioClip uiClip;

    [Header("Timer")]
    public TimePanel timePanel;

    private string currentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private int currentLine = 0;
    private int lastUpdateCharacter = -1;

    public bool IsCompleteDisplayText {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        Invoke("stayPlayer", 0.3f);

        SetNextLine();
    }

    void Update()
    {
        if (IsCompleteDisplayText)
        {
            if (currentLine < textString.Length && Input.GetMouseButtonDown(0))
            {
                soundManager.PlayEffect(uiClip);
                SetNextLine();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }
        }

        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        if (displayCharacterCount != lastUpdateCharacter)
        {
            uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
    }

    void stayPlayer()
    {
        Player.GetComponent<PlayerController>().enabled = false;
        Player.GetComponent<Genie>().enabled = false;
    }
    void SetNextLine()
    {
        currentText = textString[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;

        if(currentLine == 7)
        {
            turnOnImage(uiImage[0]);
        }

        if(currentLine == 10)
        {
            turnOffImage(uiImage[0]);
            turnOnImage(uiImage[1]);
        }

        if(currentLine == 12)
        {
            turnOffImage(uiImage[1]);
            turnOnImage(uiImage[2]);
            skillPanel.SetActive(true);
            background.GetComponent<RectTransform>().localPosition = new Vector3(0f, -400f, 0f);
            background.GetComponent<Image>().sprite = SkillbackgroundImage;
        }

        if (currentLine == 16)
        {
            turnOffImage(uiImage[2]);
            turnOnImage(uiImage[3]);
        }
        if (currentLine == 17)
        {
            turnOffImage(uiImage[3]);
            turnOnImage(uiImage[4]);
        }

        if(currentLine == 18)
        {
            turnOffImage(uiImage[4]);
            turnOnImage(uiImage[5]);
            skillPanel.SetActive(false);
            background.GetComponent<RectTransform>().localPosition = new Vector3(0f, 150f, 0f);
            background.GetComponent<Image>().sprite = originbackgroundImage;
        }

        if(currentLine == 20)
        {
            turnOffImage(uiImage[5]);
            turnOnImage(uiImage[6]);
        }
        if(currentLine == 23)
        {
            turnOffImage(uiImage[6]);
            turnOnImage(uiImage[0]);
        }
        if (currentLine == 27)
        {
            turnOffImage(uiImage[0]);
        }
        if (currentLine == 30)
        {
            Player.GetComponent<PlayerController>().enabled = true;
            Player.GetComponent<Genie>().enabled = true;
            Destroy(tutorial);
        }
    }

    void turnOnImage(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    void turnOffImage(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}