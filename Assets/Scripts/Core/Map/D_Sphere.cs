using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    public class D_Sphere : MonoBehaviour
    {

        private void Start()
        {
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(gameObject.transform.position, 1f);
        }
    }

}