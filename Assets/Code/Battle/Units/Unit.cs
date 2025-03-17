using UnityEngine;
using Code.Core.ObjectsPool;

namespace Code.Battle.Units
{
    public struct UnitBattleData
    {
        public UnitBattleData(int health, float speed)
        {
            MaxHealth = health;
            CurrentHealth = health;
            Speed = speed;
        }

        public int MaxHealth { get; }
        public int CurrentHealth { get; set; }
        public float Speed { get; }
    }

    public class Unit : MonoBehaviour, IPoolObject
    {
        public UnitBattleData BattleData { get; set; }
        public Team Team { get; set; }
    }
}