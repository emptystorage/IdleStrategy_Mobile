using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Code.Core.Common;
using Code.Battle.Data;
using Code.Battle.Command;
using Code.Core;

namespace Code.Battle.Gui
{
    public sealed class BattleGUI : SafeAreaGUI
    {
        [SerializeField] private SelectButton[] _selectButtons;
        public IReadOnlyCollection<UnitData> PlayerUnitData { get; private set; }

        private void Start()
        {
            if (!World.IsLived) return;

            var cmd = new SetupBattleGUICommand();
            cmd.Execute();
            PlayerUnitData = cmd.UnitData;

            RandomUpdateButton();
        }

        private void RandomUpdateButton()
        {
            for (int i = 0; i < _selectButtons.Length; i++)
            {
                _selectButtons[i].Show(GetRandomUnitData());
            }
        }

        private UnitData GetRandomUnitData()
        {
            var randomIndex = Random.Range(default, PlayerUnitData.Count);

            foreach(var unitData in PlayerUnitData)
            {
                if (randomIndex <= 0)
                {
                    return unitData;
                }

                randomIndex--;
            }

            return PlayerUnitData.First();
        }
    }
}
