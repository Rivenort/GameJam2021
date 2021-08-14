using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    public class D_SetToGrid : MonoBehaviour
    {

        void Start()
        {
            M_MapManager.SGameObjectToCell(gameObject);
        }

    }

}