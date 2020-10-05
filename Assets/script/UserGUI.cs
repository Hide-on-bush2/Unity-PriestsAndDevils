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
        if(GUI.Button(new Rect(0, 0, width, height), "Reset"))
        {
            action.Reset();
        }

        if(GUI.Button(new Rect(0, height + 1, width, height), "Priest boarding"))
        {
            action.PriestBoarding();
        }

        if(GUI.Button(new Rect(0, (height + 1) * 2, width, height), "Devil boarding"))
        {
            action.DevilBoarding();
        }

        if (GUI.Button(new Rect(0, (height + 1) * 3, width, height), "Priest go ashore"))
        {
            action.PriestGoAshore();
        }

        if (GUI.Button(new Rect(0, (height + 1) * 4, width, height), "Devil go ashore"))
        {
            action.DevilGoAshore();
        }

        if(GUI.Button(new Rect(0, (height + 1) * 5, width, height), "Boat go"))
        {
            action.BoatGo();
        }
    }
}
