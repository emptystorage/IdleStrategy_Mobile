using Code.Battle.Command;
using UnityEngine;

namespace Code.Battle.Units
{
    public sealed class UnitCamp : Unit
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Team _team;

        public SpriteRenderer Renderer => _renderer;

        private void Start()
        {
            Team = _team;

            var cmd = new EnableUnitCampCommand();
            cmd.Execute(this);
        }
    }
}
