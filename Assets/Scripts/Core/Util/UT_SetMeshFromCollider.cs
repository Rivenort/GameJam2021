using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    [RequireComponent(typeof(MeshCollider))]
    public class UT_SetMeshFromCollider : MonoBehaviour
    {
        public PolygonCollider2D collider;

        void Start()
        {
            if (collider == null)
                throw new CE_ComponentNotFullyInitialized();
            GetComponent<MeshCollider>().sharedMesh = collider.CreateMesh(false, true);
        }

    }
}
