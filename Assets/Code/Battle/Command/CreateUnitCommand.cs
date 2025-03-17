using UnityEngine;

using Code.Battle.Pool;
using Code.Battle.Units;
using Code.Core;

namespace Code.Battle.Command
{
    public ref struct EnableUnitCampCommand
    {
        public void Execute(in UnitCamp camp)
        {
            var battleInfo = World.Locator.Get<BattleInformation>();
            battleInfo.Add(camp);
        }
    }

    public ref struct CreateUnitCommand
    {
        public void Execute(in Unit prefab, Vector3 position, Team team)
        {
            var pool = World.Locator.Get<UnitPool>();
            var battleInfo = World.Locator.Get<BattleInformation>();
            var unit = pool.Spawn(prefab, position);
            unit.Team = team;
            unit.transform.localScale = team == Team.PLayer ? Vector3.one : new Vector3(-1,1,1);
            battleInfo.Add(unit);
        }
    }
}
