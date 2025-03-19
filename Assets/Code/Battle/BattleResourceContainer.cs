using System;
using System.Collections.Generic;
using UnityEngine;

using Code.Battle.Data;
using Code.Battle.Units;
using Code.Core.Locator;

namespace Code.Battle
{
    public sealed class BattleResourceContainer : IDisposable, IUseLocator
    {
        private readonly Dictionary<Team, LinkedList<UnitData>> _unitDataTable;

        public BattleResourceContainer()
        {
            _unitDataTable = new();
            _unitDataTable[Team.Player] = new();
            _unitDataTable[Team.Enemy] = new();

            LoadUnitData();
        }

        public IReadOnlyCollection<UnitData> GetUnitData(Team team)
        {
            return _unitDataTable[team];
        }        

        public void Dispose()
        {
            _unitDataTable.Clear();

            GC.SuppressFinalize(this);
        }

        private void LoadUnitData()
        {
            var datas = Resources.LoadAll<UnitData>(string.Empty);

            for (int i = 0; i < datas.Length; i++)
            {
                _unitDataTable[datas[i].Team].AddFirst(datas[i]);
            }
        }
    }
}
