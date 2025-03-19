using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Code.Battle.Data;
using Code.Battle.Command;

namespace Code.Battle.Gui
{
    [RequireComponent(typeof(Button), typeof(CanvasGroup))]
    public sealed class SelectButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _costText;
        private CanvasGroup _group;

        public UnitData Data { get; private set; }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnButtonClicked);
            _group = GetComponent<CanvasGroup>();
        }

        private void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveAllListeners();
        }

        public void Show(UnitData data)
        {
            Data = data;

            _icon.sprite = data.Icon;
            _costText.text = data.Cost.ToString();

            _group.alpha = 1;
            _group.blocksRaycasts = true;
        }

        public void Hide()
        {
            _group.alpha = 0;
            _group.blocksRaycasts = false;
        }

        private void OnButtonClicked()
        {
            var cmd = new CreateUnitCommand();
            cmd.Execute(Data.UnitPrefab, Data.Team);
        }
    }
}
