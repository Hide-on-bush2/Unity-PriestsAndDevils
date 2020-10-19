using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSReferee : MonoBehaviour
{

    
    public FirstController sceneController;
    // Start is called before the first frame update

    //private static SSReferee _referee;
    //public static SSReferee getInstance()
    //{
    //    if(_referee == null)
    //    {
    //        _referee = new SSReferee();
    //    }
    //    return _referee;
    //}
    public void Start()
    {
        sceneController = (FirstController)SSDirector.getInstance().currentSceneController;
        sceneController.referee = this;
    }

    // Update is called once per frame
    public void Update()
    {
        if (!IsSafe())
        {
            sceneController.status = sceneController.LOSS;
        }
        else if(sceneController.status == sceneController.UNDEFINE && sceneController.PriestStackB.Count == 3 && sceneController.DevilsStackB.Count == 3)
        {
            sceneController.status = sceneController.WIN;
        }
    }

    private bool IsSafe()
    {
        if (sceneController.curr_boat == sceneController.A)
        {
            return (sceneController.PriestStackB.Count >= sceneController.DevilsStackB.Count || sceneController.PriestStackB.Count == 0) && ((sceneController.PriestStackA.Count + sceneController.PriestStackBoat.Count >= sceneController.DevilsStackA.Count + sceneController.DevilsStackBoat.Count) || (sceneController.PriestStackA.Count + sceneController.PriestStackBoat.Count == 0));
        }
        else
        {
            return (sceneController.PriestStackA.Count >= sceneController.DevilsStackA.Count || sceneController.PriestStackA.Count == 0) && ((sceneController.PriestStackB.Count + sceneController.PriestStackBoat.Count >= sceneController.DevilsStackB.Count + sceneController.DevilsStackBoat.Count) || (sceneController.PriestStackB.Count + sceneController.PriestStackBoat.Count == 0));
        }
    }
}
