using UnityEngine;

using Code.Battle.Units;
using Code.Battle.Command;

namespace Code.Battle.Data
{
    [CreateAssetMenu(fileName = "New" + nameof(UnitData), menuName = "GameData/New"+nameof(UnitData))]
    public sealed class UnitData : ScriptableObject
    {
        [SerializeField] private Unit _unitPrefab;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Team _unitTeam;
        [SerializeField] private int _health;
        [SerializeField] private int _cost;

        public Unit UnitPrefab => _unitPrefab;
        public Sprite Icon => _icon;
        public Team Team => _unitTeam;
        public int Health => _health;
        public int Cost => _cost;
    }
}