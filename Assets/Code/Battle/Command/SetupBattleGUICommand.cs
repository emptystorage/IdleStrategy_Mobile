using System.Collections.Generic;

using Code.Battle.Data;
using Code.Core;

namespace Code.Battle.Command
{
    public ref struct SetupBattleGUICommand
    {
        public IReadOnlyCollection<UnitData> UnitData { get; private set; }

        public void Execute()
        {
            var resourceContainer = World.Locator.Get<BattleResourceContainer>();

            //TEST
            UnitData = resourceContainer.GetUnitData(Units.Team.Player);

            ///TODO fill data from resources and check if data opened in player information
        }
    }
}
