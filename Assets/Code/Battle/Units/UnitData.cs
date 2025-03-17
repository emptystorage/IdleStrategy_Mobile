using UnityEngine;

namespace Code.Battle.Units
{
    [CreateAssetMenu(fileName = "New" + nameof(UnitData), menuName = "GameData/New"+nameof(UnitData))]
    public sealed class UnitData : ScriptableObject
    {
        [SerializeField] private Unit _unitPrefab;
        [SerializeField] private int _health;

        public Unit UnitPrefab => _unitPrefab;
        public int Health => _health;
    }
}