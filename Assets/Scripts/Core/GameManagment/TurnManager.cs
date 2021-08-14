using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    int Player;



    private void Start()
    {
        Player = 1;
    }
    
    public void NextTurn()
    {
        if (Player == 1) Player = 2;
        if (Player == 2) Player = 1;
    }
}
