using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback
{
    public FirstController sceneController;
    
    protected new void Start()
    {
        sceneController = (FirstController)SSDirector.getInstance().currentSceneController;
        sceneController.actionManager = this;
        //moveToA = CCMoveToAction.GetSSAction(new Vector3(5, 0, 0), 1);
        //this.RunAction(sceneController.move1, moveToA, this);
        //moveToC = CCMoveToAction.GetSSAction(new Vector3(-2, -2, -2), 1);
        //moveToD = CCMoveToAction.GetSSAction(new Vector3(3, 3, 3), 1);

        //CCSequenceAction ccs = CCSequenceAction.GetSSAction(3, 0, new List<SSAction> { moveToC, moveToD });
        //this.RunAction(sceneController.move2, ccs, this);
    }

    protected new void Update()
    {
        base.Update();
    }

    public void RunAction(GameObject gameobject, SSAction action)
    {
        sceneController.buttonActive = false;
        base.RunAction(gameobject, action, this);
    }

    public void ClearAction()
    {
        this.actions.Clear();
        this.waitingAdd.Clear();
        this.waitingDelete.Clear();
        this.sceneController.buttonActive = true;
    }

    #region ISSActionCallback implementation
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, object objectParam = null)
    {
        this.sceneController.buttonActive = true;
    }
    #endregion
}


