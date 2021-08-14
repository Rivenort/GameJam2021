using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_MapManager : UT_IDoOnGameStart,
                                UT_IOnMobActionCompleted,
                                UT_IClearable,
                                UT_IOnMobCreated,
                                UT_IOnMobDestroyed
    {
        private static M_MapManager s_instance = null;
        private static readonly object s_lock = new object();

        private const string OBJECTNAME_GRID = "Level/Grid";
        private const string OBJECTNAME_OBSTACLES = "Level/Obstacles";
        private const string OBJECTNAME_MOBS = "Mobs";

        private Transform m_groupMobs;
        private Tilemap m_tilemapGrid;
        private Tilemap m_tilemapObstacles;

        private Dictionary<Vector3Int, TileData> m_tiles = new Dictionary<Vector3Int, TileData>();
        private BoundsInt m_gridBounds;

        private M_MapManager()
        {
            m_tilemapGrid = GameObject.Find(OBJECTNAME_GRID).GetComponent<Tilemap>();
            m_tilemapObstacles = GameObject.Find(OBJECTNAME_OBSTACLES).GetComponent<Tilemap>();
            m_groupMobs = GameObject.Find(OBJECTNAME_MOBS).transform;
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
      

                    m_tiles.Add(cellPos, newTileData);

                }
            }
        }

        private void ScanMobs()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Scanning mobs: ");
            foreach (var tileData in m_tiles)
            {
                tileData.Value.SetMob(Guid.Empty);
            }
            foreach (Transform child in m_groupMobs)
            {
                IMob mobComp = child.gameObject.GetComponent<IMob>();
                if (mobComp == null)
                    continue;

                Vector3Int cell = WorldPosToGridCell(mobComp.GetRootPosition());

                m_tiles[cell].SetMob(mobComp.GetId());
                stringBuilder.Append("\nIMob: " + mobComp.GetName() + " Id: " + mobComp.GetId() + " Pos: " + mobComp.GetRootPosition());
            }
            Debug.Log(stringBuilder);
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
            return m_tiles[pos].IsObstacle();
        }

        private void SetObstacle(Vector3Int pos, bool val)
        {
            m_tiles[pos].SetObstacle(val);
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
            ScanMobs();
        }

        private bool IsAvailable(Vector3 currentPos, Directory directory)
        {
            Vector3Int cellPos = WorldPosToGridCell(currentPos);

            switch (directory)
            {
                case Directory.LEFT:
                    cellPos.x -= 1;
                    break;
                case Directory.RIGHT:
                    cellPos.x += 1;
                    break;
                case Directory.UP:
                    cellPos.y += 1;
                    break;
                case Directory.DOWN:
                    cellPos.y -= 1;
                    break;
            }



            if (m_tilemapGrid.GetTile(cellPos) == null)
                return false;

            TileData tileData = m_tiles[cellPos];
            if (tileData.IsObstacle() || tileData.GetMob() != Guid.Empty)
                return false;
            return true;
        }

        public static bool SIsAvailable(Vector3 currentPos, Directory directory)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.IsAvailable(currentPos, directory);
        }



        private IMob GetEnemyForMelee(PlayerType callerType, Vector3 position)
        {
            Vector3Int cellPos = WorldPosToGridCell(position);

            if (callerType == PlayerType.PLAYER_ONE)
                cellPos.x += 1;
            else if (callerType == PlayerType.PLAYER_TWO)
                cellPos.x -= 1;



            Guid mobId = m_tiles[cellPos].GetMob();
            IMob mob = null;
            if (mobId != Guid.Empty)
                mob = M_MobManager.SGetMob(mobId);

            if (mob == null)
                return null;

            if (mob.GetPlayer() == callerType)
                return null;
            return mob;
        }

        private IMob GetEnemyForRanger(PlayerType callerType, Vector3 position, int range)
        {
            int checkCount = 0;
            Vector3Int cellPos = WorldPosToGridCell(position);
            int dir = 1;
            if (callerType == PlayerType.PLAYER_TWO)
                dir = -1;

            IMob enemy = null;
            cellPos.x += dir; // candidate

            while (m_tilemapGrid.GetTile(cellPos) != null && enemy == null && checkCount < range)
            {

                Guid mobId = m_tiles[cellPos].GetMob();
                if (mobId != Guid.Empty)
                {
                    // Check if Mob is of the same Player
                    enemy = M_MobManager.SGetMob(mobId);
                    if (enemy.GetPlayer() == callerType)
                        enemy = null;
                }
                cellPos.x += dir;
                checkCount += 1;
            }

            return enemy;
        }

        public static IMob SGetEnemyForRanger(PlayerType playerType, Vector3 position, int range)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.GetEnemyForRanger(playerType, position, range);
        }

        public static IMob SGetEnemyForMelee(PlayerType callerType, Vector3 position)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.GetEnemyForMelee(callerType, position);
        }

        public static void SSetTileBase(Vector3Int position, TileBase tileBase)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_tilemapGrid.SetTile(position, tileBase);
        }

        public static void SSetTileColor(Vector3Int position, Color color)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_tilemapGrid.SetColor(position, color);
        }

        public void OnMobActionCompleted(IMob mob)
        {
            ScanMobs();
        }

        public void Clear()
        {
            
        }

        public void OnMobCreated(IMob mob)
        {
            ScanMobs();
        }

        public void OnMobDestroyed(Guid mobId)
        {
            ScanMobs();
        }
    }

}