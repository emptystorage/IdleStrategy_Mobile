using UnityEngine;

using Code.Battle.Units;
using Code.Battle.Command;
using Code.Core;

namespace Code.Battle.Camp
{
    public sealed class UnitCamp : Unit
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Team _team;

        public SpriteRenderer Renderer => _renderer;

        private void Start()
        {
            if (!World.IsLived) return;

            Team = _team;

            var battleInfo = World.Locator.Get<BattleInformation>();
            battleInfo.Add(this);

            var widthValue = _team == Team.Player ? default : Screen.width;
            var heightValue = Screen.height / 2;
            var positionInSafeArea = Camera.main.ScreenToWorldPoint(new Vector2(widthValue, heightValue));
            positionInSafeArea.z = 0;

            transform.position = positionInSafeArea;
        }

        private void OnDestroy()
        {
            if (!World.IsLived) return;

            var battleInfo = World.Locator.Get<BattleInformation>();
            battleInfo.Add(this);
        }
    }
}
