﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_MapManager : UT_IDoOnGameStart
    {
        private static M_MapManager s_instance = null;
        private static readonly object s_lock = new object();

        private const string OBJECTNAME_GRID = "Level/Grid";
        private const string OBJECTNAME_OBSTACLES = "Level/Obstacles";

        private Tilemap m_tilemapGrid;
        private Tilemap m_tilemapObstacles;

        private TileData[] m_tiles;
        private BoundsInt m_gridBounds;

        private M_MapManager()
        {
            m_tilemapGrid = GameObject.Find(OBJECTNAME_GRID).GetComponent<Tilemap>();
            m_tilemapObstacles = GameObject.Find(OBJECTNAME_OBSTACLES).GetComponent<Tilemap>();
        }

        public static M_MapManager GetInstance()
        {
            lock (s_lock)
            {
                if (s_instance == null)
                    s_instance = new M_MapManager();
                return s_instance;
            }
        }


        private void ScanGrid()
        {
            Debug.Log("Scanning grid...");
            BoundsInt bounds = m_tilemapGrid.cellBounds;
            Debug.Log("Grid Bounds: " + bounds);

            TileBase[] allTiles = m_tilemapGrid.GetTilesBlock(bounds);

            m_tiles = new TileData[bounds.size.x * bounds.size.y];
            m_gridBounds = bounds;

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    if (tile == null)
                    {
                        continue;
                    }

                    Vector3Int cellPos = new Vector3Int(x - bounds.size.x / 2, y - bounds.size.y / 2, 0);

                    TileData newTileData = new TileData(cellPos);
      

                    m_tiles[x + y * bounds.size.x] = newTileData;

                }
            }
        }

        private Vector3 GridCellToWorldPos(Vector3Int cellPos)
        {
            if (m_tilemapGrid == null)
                throw new CE_RequiredObjectNotInitialized(OBJECTNAME_GRID + " was not initialized!");
            Vector3 worldPos = m_tilemapGrid.CellToWorld(cellPos);
            worldPos.x += m_tilemapGrid.tileAnchor.x;
            worldPos.y += m_tilemapGrid.tileAnchor.y;
            return worldPos;
        }

        private Vector3Int WorldPosToGridCell(Vector3 pos)
        {
            if (m_tilemapGrid == null)
                throw new CE_RequiredObjectNotInitialized(OBJECTNAME_GRID + " was not initialized!");
            return m_tilemapGrid.WorldToCell(pos);
        }

        public static Vector3 SGridCellToWorldPos(Vector3Int cellPos)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.GridCellToWorldPos(cellPos);
        }

        public static Vector3Int SWorldPosToGridCell(Vector3 pos)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.WorldPosToGridCell(pos);
        }

        private bool IsObstacle(Vector3Int pos)
        {
            int index = CellCombinedFromTilemap(pos, m_gridBounds);
            if (index >= 0 && index < m_tiles.Length)
            {
                return m_tiles[index].IsObstacle();
            }
            Debug.LogWarning("Something went wrong with index retrieving. Check!");
            return false;
        }

        private void SetObstacle(Vector3Int pos, bool val)
        {
            int index = CellCombinedFromTilemap(pos, m_gridBounds);
            if (index >= 0 && index < m_tiles.Length)
            {
                m_tiles[index].SetObstacle(val);
                return;
            }
            Debug.LogWarning("Something went wrong with index retrieving. Check!");
        }

        public static void SSetObstacle(Vector3Int pos, bool val)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.SetObstacle(pos, val);
        }

        public static bool SIsObstacle(Vector3Int pos)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.IsObstacle(pos); 
        }

        private void GameObjectToCell(GameObject gameObject)
        {
            Vector3Int cellPos = WorldPosToGridCell(gameObject.transform.position);
            Vector3 finalPos = GridCellToWorldPos(cellPos);
            finalPos.x += m_tilemapGrid.tileAnchor.x;
            finalPos.y += m_tilemapGrid.tileAnchor.y;
            gameObject.transform.position = finalPos;
        }

        private Vector3 GetDiffFromGrid(Vector3 pos)
        {
            Vector3Int cellPos = WorldPosToGridCell(pos);
            Vector3 finalPos = GridCellToWorldPos(cellPos);
            return finalPos - pos;
        }

        public static Vector3 SGetDiffFromGrid(Vector3 pos)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.GetDiffFromGrid(pos);
        }

        public static void SGameObjectToCell(GameObject gameObject)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.GameObjectToCell(gameObject);
        }

        // Utility
        private static int CellCombinedFromTilemap(Vector3Int cellPos, BoundsInt tilemapBounds)
        {
            return (cellPos.x + tilemapBounds.size.x / 2) + (cellPos.y + tilemapBounds.size.y / 2) * tilemapBounds.size.x;
        }

        public void OnGameStart()
        {
            ScanGrid();
        }
    }

}