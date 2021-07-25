using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MouseType
{
    NORMAL,
    DELETE
}

public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;

    public Texture2D normalTexture; // 평소 마우스 텍스쳐
    public Texture2D deleteTexture; // 정령 삭제시 텍스쳐

    private Vector2 normalHotSpot;
    private Vector2 deleteHotSpot;

    private MouseType mouseTextureType;

    private void Awake()
    {
        instance = this;
        /*
        normalHotSpot.x = normalTexture.width / 2f;
        normalHotSpot.y = normalTexture.height / 2f;

        deleteHotSpot.x = deleteTexture.width / 2f;
        deleteHotSpot.y = deleteTexture.height / 2f;
        */
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            SetMouseType(MouseType.DELETE);
        }
    }

    public static void SetMouseType(MouseType mouseTextureType)
    {
        if (instance == null)
        {
            MouseManager manager = Resources.Load<MouseManager>("MouseManager");
            Instantiate(manager);
            instance = manager;
        }

        instance.mouseTextureType = mouseTextureType;

        switch (mouseTextureType)
        {
            case MouseType.NORMAL:
                Cursor.SetCursor(instance.normalTexture, instance.normalHotSpot, CursorMode.Auto);
                break;
            case MouseType.DELETE:
                Cursor.SetCursor(instance.deleteTexture, instance.deleteHotSpot, CursorMode.Auto);
                break;
            default:
                break;
        }
    }

    public MouseType GetMouseType()
    {
        return mouseTextureType;
    }


}
