using UnityEngine;

using Code.Core;
using Code.Battle.Pool;
using Code.Battle.Units;

namespace Code.Battle.Command
{
    public ref struct CreateUnitCommand
    {
        public void Execute(in Unit prefab, Team team)
        {
            var pool = World.Locator.Get<UnitPool>();
            var battleInfo = World.Locator.Get<BattleInformation>();
            var position = team == Team.Player ? battleInfo.PlayerCamp.transform.position : battleInfo.EnemyCamp.transform.position;
            position += (Vector3)Random.insideUnitCircle;
            var unit = pool.Spawn(prefab, position);
            unit.Team = team;
            unit.transform.localScale = team == Team.Player ? Vector3.one : new Vector3(-1,1,1);
            battleInfo.Add(unit);
        }
    }
}
