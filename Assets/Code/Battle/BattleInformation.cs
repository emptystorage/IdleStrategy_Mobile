using System;
using System.Collections.Generic;
using UnityEngine;

using Code.Battle.Units;
using Code.Core.Locator;

namespace Code.Battle
{
    public sealed class BattleContext : MonoBehaviour
    {
        [SerializeField] private Transform _playerCampPointUI;
        [SerializeField] private Transform _enemyCampPointUI;

        private void Start()
        {
            
        }
    }

    public sealed class BattleInformation : IDisposable, IUseLocator
    {
        private readonly Dictionary<Team, LinkedList<Unit>> UnitsInBattle;        

        public BattleInformation()
        {
            UnitsInBattle = new();
            UnitsInBattle[Team.PLayer] = new();
            UnitsInBattle[Team.Enemy] = new();
        }

        public void Add(in Unit unit)
        {
            UnitsInBattle[unit.Team].AddFirst(unit);
        }

        public void Remove(in Unit unit)
        {
            UnitsInBattle[unit.Team].Remove(unit);
        }

        public bool TryGetTarget(in Unit unit, out Unit target)
        {
            target = default;

            var list = UnitsInBattle[unit.Team == Team.PLayer ? Team.Enemy : Team.PLayer];
            var node = list.First;
            var minDistance = float.PositiveInfinity;
            var distance = float.PositiveInfinity;

            while (node != null)
            {
                distance = Vector3.Distance(unit.transform.position, node.Value.transform.position);

                if(distance < minDistance)
                {
                    target = node.Value;
                    minDistance = distance;
                }

                node = node.Next;
            }

            return target != null;
        }

        public void Dispose()
        {
            UnitsInBattle.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
