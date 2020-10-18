using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;


public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    public CCActionManager actionManager;
    public bool buttonActive;

    private Stack<GameObject> PriestStackA = new Stack<GameObject>();
    private Stack<GameObject> DevilsStackA = new Stack<GameObject>();

    private Stack<GameObject> PriestStackB = new Stack<GameObject>();
    private Stack<GameObject> DevilsStackB = new Stack<GameObject>();

    private Stack<GameObject> PriestStackBoat = new Stack<GameObject>();
    private Stack<GameObject> DevilsStackBoat = new Stack<GameObject>();

    private int boarding_num;
    private GameObject boat;
    private GameObject river;

    private const int A = 1;
    private const int B = 2;
    private const int WIN = 3;
    private const int UNDEFINE = 4;

    private int curr_boat;

    private int status;

    private string textFieldString = "Game has started";
    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
        boarding_num = 0;
        curr_boat = A;
        status = UNDEFINE;
        this.buttonActive = true;
        this.actionManager = new CCActionManager();
    }

    public void LoadResources()
    {
        river = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/river"),
            Vector3.zero, Quaternion.identity);
        river.name = "river";
        river.transform.localScale = new Vector3(3f, 1f, 2f);
        river.transform.position = new Vector3(-7, 0, 0);

        Debug.Log("load river ...  \n");

        for (int i = 0; i < 3; i++)
        {
            GameObject priest = Instantiate<GameObject>(
                Resources.Load<GameObject>("prefabs/priest"),
                new Vector3(10, 0, (float)(i * 1.2 + 2.5)), Quaternion.identity);
            priest.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            priest.transform.LookAt(Vector3.zero);
            priest.name = "priest" + i;
            PriestStackA.Push(priest);
            Debug.Log("load priest " + i + " ...\n");
        }

        for (int i = 0; i < 3; i++)
        {
            GameObject devil = Instantiate<GameObject>(
                Resources.Load<GameObject>("prefabs/devil"),
                new Vector3(10, 0, (float)(i * 1.5 - 3)), Quaternion.identity);
            devil.transform.localScale = new Vector3(2f, 2f, 2f);
            devil.transform.LookAt(Vector3.zero);
            devil.name = "devil" + i;
            DevilsStackA.Push(devil);
            Debug.Log("load devil " + i + " ...\n");
        }

        boat = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/fishing_boat"),
            new Vector3(4, 0, 0), Quaternion.identity);
        boat.transform.localScale = new Vector3(5f, 5f, 5f);
        boat.transform.LookAt(new Vector3(4, 0, 10));
        boat.name = "boat";
        Debug.Log("Load boat...\n");
    }

    public void GameOver()
    {
        Reset();
    }

    public void Reset()
    {
        actionManager.ClearAction();
        //revover priest 
        while (PriestStackBoat.Count != 0)
        {
            GameObject priest = PriestStackBoat.Pop();
            priest.transform.position = new Vector3(10, 0, (float)((PriestStackA.Count) * 1.2 + 2.5));
            PriestStackA.Push(priest);
        }

        while(PriestStackB.Count != 0)
        {
            GameObject priest = PriestStackB.Pop();
            priest.transform.position = new Vector3(10, 0, (float)((PriestStackA.Count) * 1.2 + 2.5));
            PriestStackA.Push(priest);
        }

        //recover devils
        while (DevilsStackBoat.Count != 0)
        {
            GameObject devils = DevilsStackBoat.Pop();
            devils.transform.position = new Vector3(10, 0, (float)(DevilsStackA.Count * 1.5 - 3));
            DevilsStackA.Push(devils);
        }

        while (DevilsStackB.Count != 0)
        {
            GameObject devils = DevilsStackB.Pop();
            devils.transform.position = new Vector3(10, 0, (float)(DevilsStackA.Count * 1.5 - 3));
            DevilsStackA.Push(devils);
        }

        boat.transform.position = new Vector3(4, 0, 0);

        boarding_num = 0;
        curr_boat = A;
        textFieldString = "";
        status = UNDEFINE;
        Debug.Log("Reset\n");
    }

    private void moveAction(GameObject gameobject, Vector3 target)
    {
        CCMoveToAction moveToTarget = CCMoveToAction.GetSSAction(target, 3);
        actionManager.RunAction(gameobject, moveToTarget);
    }

    public void PriestBoarding()
    {
        if (!buttonActive)
        {
            return;
        }
        Stack<GameObject> PriestStack = curr_boat == A ? PriestStackA : PriestStackB;
        if(boarding_num < 2 && PriestStack.Count != 0)
        {
            GameObject priest = PriestStack.Pop();
            Vector3 target;
            if (boarding_num == 0)
            {
                target = boat.transform.position + (new Vector3(-2, 0, 0));
                moveAction(priest, target);
            }
            else
            {
                //priest.transform.position = boat.transform.position;
                if(PriestStackBoat.Count != 0)
                {
                    target = PriestStackBoat.Peek().transform.position == boat.transform.position ? boat.transform.position + (new Vector3(-2, 0, 0)) : boat.transform.position;
                    moveAction(priest, target);
                }
                else
                {
                    target = DevilsStackBoat.Peek().transform.position == boat.transform.position ? boat.transform.position + (new Vector3(-2, 0, 0)) : boat.transform.position;
                    moveAction(priest, target);
                }
            }
            PriestStackBoat.Push(priest);
            boarding_num += 1;
            Debug.Log("Priest boarding\n");
        }
        
    }

    public void DevilBoarding()
    {
        if (!buttonActive)
        {
            return;
        }
        Stack<GameObject> DevilsStack = curr_boat == A ? DevilsStackA : DevilsStackB;
        Vector3 target;
        if (boarding_num < 2 && DevilsStack.Count != 0)
        {
            GameObject devil = DevilsStack.Pop();
            if (boarding_num == 0)
            {
                target = boat.transform.position + (new Vector3(-2, 0, 0));
                moveAction(devil, target);
            }
            else
            {
                //devil.transform.position = boat.transform.position;
                if (PriestStackBoat.Count != 0)
                {
                    target = PriestStackBoat.Peek().transform.position == boat.transform.position ? boat.transform.position + (new Vector3(-2, 0, 0)) : boat.transform.position;
                    moveAction(devil, target);
                }
                else
                {
                    target = DevilsStackBoat.Peek().transform.position == boat.transform.position ? boat.transform.position + (new Vector3(-2, 0, 0)) : boat.transform.position;
                    moveAction(devil, target);
                }
            }
            DevilsStackBoat.Push(devil);
            boarding_num += 1;
            Debug.Log("Devil boarding\n");

        }
    }
    public void PriestGoAshore()
    {
        if (!buttonActive)
        {
            return;
        }
        Stack<GameObject> PriestStack = curr_boat == A ? PriestStackA : PriestStackB;
        if(boarding_num > 0 && PriestStackBoat.Count != 0)
        {
            GameObject priest = PriestStackBoat.Pop();
            Vector3 target = curr_boat == A ? new Vector3(10, 0, (float)((PriestStack.Count) * 1.2 + 2.5)) : new Vector3(-23, 0, (float)((PriestStack.Count) * 1.2 + 2.5));
            moveAction(priest, target);
            PriestStack.Push(priest);
            boarding_num--;
            Debug.Log("Priest go ashore\n");
        }
    }
    public void DevilGoAshore()
    {
        if (!buttonActive)
        {
            return;
        }
        Stack<GameObject> DevilsStack = curr_boat == A ? DevilsStackA : DevilsStackB;
        if (boarding_num > 0 && DevilsStackBoat.Count != 0)
        {
            GameObject devil = DevilsStackBoat.Pop();
            Vector3 target = curr_boat == A ? new Vector3(10, 0, (float)(DevilsStack.Count * 1.5 - 3)) : new Vector3(-23, 0, (float)(DevilsStack.Count * 1.5 - 3));
            moveAction(devil, target);
            DevilsStack.Push(devil);
            boarding_num--;
            Debug.Log("Devil go ashore\n");
        }
    }

    private bool IsSafe()
    {
        if(curr_boat == A)
        {
            return (PriestStackB.Count >= DevilsStackB.Count || PriestStackB.Count == 0) && ((PriestStackA.Count + PriestStackBoat.Count >= DevilsStackA.Count + DevilsStackBoat.Count) || (PriestStackA.Count + PriestStackBoat.Count == 0));
        }
        else
        {
            return (PriestStackA.Count >= DevilsStackA.Count || PriestStackA.Count == 0) && ((PriestStackB.Count + PriestStackBoat.Count >= DevilsStackB.Count + DevilsStackBoat.Count) || (PriestStackB.Count + PriestStackBoat.Count == 0));
        }
    }
   
    public void BoatGo()
    {
        if (!buttonActive)
        {
            return;
        }
        if(boarding_num >= 1)
        {
            Vector3 target;
            Vector3 direction = curr_boat == A ? new Vector3(-23, 0, 0) : new Vector3(23, 0, 0);
            target = boat.transform.position + direction;
            moveAction(boat, target);
            List<GameObject> tmp = new List<GameObject>();
            while(PriestStackBoat.Count != 0)
            {
                tmp.Add(PriestStackBoat.Pop());
            }
            for (int i = 0; i < tmp.Count; i++)
            {
                target = tmp[i].transform.position + direction;
                moveAction(tmp[i], target);
                PriestStackBoat.Push(tmp[i]);
            }

            tmp.Clear();
            while (DevilsStackBoat.Count != 0)
            {
                tmp.Add(DevilsStackBoat.Pop());
            }
            for (int i = 0; i < tmp.Count; i++)
            {
                target = tmp[i].transform.position + direction;
                moveAction(tmp[i], target);
                DevilsStackBoat.Push(tmp[i]);
            }

            curr_boat = curr_boat == A ? B : A;
            if (!IsSafe())
            {
                Debug.Log("Some Priests killed by Devils\n");
                textFieldString = "Some Priests killed by Devils";
                GameOver();
            }

            /*if(PriestStackB.Count == 3 && DevilsStackB.Count == 3)
            {
                Debug.Log("You win\n");
                textFieldString = "You win";
                GameOver();
            }*/
        }
    }

    public void Update()
    {
        if (status == UNDEFINE && PriestStackB.Count == 3 && DevilsStackB.Count == 3)
        {
            Debug.Log("You win\n");
            textFieldString = "You win";
            status = WIN;
            GameOver();
        }
    }

    void OnGUI()
    {
        textFieldString = GUI.TextField(new Rect(700, 25, 200, 50), textFieldString);
    }

    public void Pause()
    {

    }

    public void Resume()
    {

    }
}
