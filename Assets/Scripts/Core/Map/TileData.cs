using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    [Serializable]
    public class TileData
    {
        private Vector3Int m_position;
        private bool m_isObstacle;
        private Guid m_mobId;

        public TileData(Vector3Int position)
        {
            m_position = position;
        }

        public bool IsObstacle()
        {
            return m_isObstacle;
        }

        public void SetObstacle(bool val)
        {
            m_isObstacle = val;
        }

        public void SetMob(Guid mobId)
        {
            m_mobId = mobId;
        }

        public Guid GetMob()
        {
            return m_mobId;
        }

        public override int GetHashCode()
        {
            return m_position.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (obj == null || obj.GetType() != this.GetType()) return false;
            return obj.GetHashCode() == this.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(typeof(TileData).Name).Append(":{");
            stringBuilder.Append("\nm_position: ").Append(m_position);
            stringBuilder.Append("\nm_isObstacle: ").Append(m_isObstacle).Append("\n}");
            return stringBuilder.ToString();
        }
    }

}