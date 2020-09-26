using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
    }

    public void LoadResources()
    {
        GameObject faker = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/BlueSuitFree01"),
            Vector3.zero, Quaternion.identity);
        faker.name = "faker";
        Debug.Log("load faker ...  \n");
    }

    public void GameOver()
    {
        Debug.Log("GameOver\n");
    }

    public void Pause()
    {
        
    }

    public void Resume()
    {

    }
}
