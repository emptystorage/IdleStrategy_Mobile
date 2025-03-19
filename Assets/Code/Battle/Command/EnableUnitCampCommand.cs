using Code.Core;
using Code.Battle.Camp;

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
}
