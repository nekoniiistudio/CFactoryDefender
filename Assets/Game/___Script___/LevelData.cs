using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Game
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Data/Level Data")]
    public class LevelData : ScriptableObject
    {
        public int levelID;
        public List<EnemyLocationData> enemyLocations;
    }

    [Serializable]
    public struct EnemyLocationData
    {
        public int enemyID;
        public Vector2Int gridLocation;
    }
}