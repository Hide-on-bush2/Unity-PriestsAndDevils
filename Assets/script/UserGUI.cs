using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    public void Start()
    {
        action = SSDirector.getInstance().currentSceneController as IUserAction;
    }
    public void OnGUI()
    {
        float width = Screen.width / 6;
        float height = Screen.height / 12;
        if(GUI.Button(new Rect(0, 0, width, height), "Game Over!"))
        {
            action.GameOver();
        }
    }
}
