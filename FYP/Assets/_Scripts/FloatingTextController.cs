using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    private static FloatingText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupText)
        {
            popupText = Resources.Load<FloatingText>("_Prefabs/PopupParent");
        }
        
    }

    public static void CreateFLoatingText(string text, Transform location, int color)
    {
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x, location.position.y + 2));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition; 
        instance.SetText(text, color);

    }

}
