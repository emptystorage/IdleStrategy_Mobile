using System;
using System.Collections.Generic;
using UnityEngine;

using Code.Core.Locator;
using Code.Core.Common;
using Code.Battle.Units;
using Code.Battle.Camp;

namespace Code.Battle
{
    public sealed class BattleInformation : IDisposable, IUseLocator
    {
        private const float MoveTime = 15;
        private readonly Dictionary<Team, LinkedList<Unit>> UnitsInBattle;        

        public BattleInformation()
        {
            UnitsInBattle = new();
            UnitsInBattle[Team.Player] = new();
            UnitsInBattle[Team.Enemy] = new();
        }

        public ReactValue<int> GemCount { get; } = new();
        public UnitCamp PlayerCamp { get; private set; }
        public UnitCamp EnemyCamp { get; private set; }
        public float UnitSpeed { get; private set; }

        public void Add(in Unit unit)
        {
            UnitsInBattle[unit.Team].AddFirst(unit);

            if (unit is UnitCamp)
            {
                var camp = unit as UnitCamp;

                switch (unit.Team)
                {
                    case Team.Player:
                        PlayerCamp = camp;
                        break;
                    case Team.Enemy:
                        EnemyCamp = camp;
                        break;
                }

                if(PlayerCamp != null & EnemyCamp != null)
                {
                    var distance = Vector3.Distance(PlayerCamp.transform.position, EnemyCamp.transform.position);
                    UnitSpeed = distance / MoveTime;
                }
            }
        }

        public void Remove(in Unit unit)
        {
            UnitsInBattle[unit.Team].Remove(unit);
        }

        public bool TryGetTarget(in Unit unit, out Unit target)
        {
            target = default;

            var list = UnitsInBattle[unit.Team == Team.Player ? Team.Enemy : Team.Player];
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
