using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void Reset();
    void PriestBoarding();
    void DevilBoarding();
    void PriestGoAshore();
    void DevilGoAshore();
    void BoatGo();
}