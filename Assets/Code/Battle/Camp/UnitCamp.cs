using UnityEngine;

using Code.Battle.Units;
using Code.Battle.Command;

namespace Code.Battle.Camp
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

            var widthValue = _team == Team.Player ? default : Screen.width;
            var heightValue = Screen.height / 2;
            var positionInSafeArea = Camera.main.ScreenToWorldPoint(new Vector2(widthValue, heightValue));
            positionInSafeArea.z = 0;

            transform.position = positionInSafeArea;
        }
    }
}
