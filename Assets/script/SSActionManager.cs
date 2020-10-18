using System.Collections;
using System.Collections.Generic;
using Unity.Rendering.HybridV2;
using UnityEngine;

public class SSActionManager : MonoBehaviour
{

    protected Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    protected List<SSAction> waitingAdd = new List<SSAction>();
    protected List<int> waitingDelete = new List<int>();

    protected void Update()
    {
        foreach(SSAction ac in waitingAdd)
        {
            actions[ac.GetInstanceID()] = ac;
        }
        waitingAdd.Clear();

        foreach(KeyValuePair<int, SSAction>kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }else if (ac.enable)
            {
                ac.Update();
            }
        }

        
        foreach(int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            Object.Destroy(ac);
        }

        waitingDelete.Clear();
    }

    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    protected void Start()
    {
        
    }
}
