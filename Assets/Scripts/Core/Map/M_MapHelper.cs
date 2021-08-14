using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_MapHelper : MonoBehaviour
    {
        private static M_MapHelper s_instance = null;

        public TileBase tileGridNormal;
        public TileBase tileGridSpecial;
        public Color specialColor = Color.red;

        private List<Vector3Int> m_highlighted = new List<Vector3Int>();

        void Start()
        {
            if (s_instance != null & s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;
        }

        private void HighlightTile(Vector3Int position)
        {
            if (m_highlighted.Contains(position))
                return;

            m_highlighted.Add(position);
            SSetTileSpecial(position);
            M_MapManager.SSetTileColor(position, specialColor);
        }

        private void UnHighlighAll()
        {
            foreach (Vector3Int pos in m_highlighted)
            {
                SSetTileNormal(pos);
                M_MapManager.SSetTileColor(pos, Color.white);
            }
            m_highlighted.Clear();
        }

        public static void SHighligh(Vector3Int position)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.HighlightTile(position);
        }

        public static void SUnHighlightAll()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.UnHighlighAll();
        }

        public static void SSetTileNormal(Vector3Int position)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            M_MapManager.SSetTileBase(position, s_instance.tileGridNormal);
        }

        public static void SSetTileSpecial(Vector3Int position)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            M_MapManager.SSetTileBase(position, s_instance.tileGridSpecial);
        }
    }

}