using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Code.MainMenu.Command;

namespace Code.MainMenu.Gui
{
    public sealed class BattleLevelInfoScreen : MonoBehaviour
    {
        [SerializeField] private BattleLevelData[] _levelDatas;
        [SerializeField] private Button _startBattleButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _previousLevelButton;
        private int _currentLevelIndex;

        private void Awake()
        {
            _nextLevelButton.onClick.AddListener(Next);
            _previousLevelButton.onClick.AddListener(Previous);
            _startBattleButton.onClick.AddListener(StartBattle);
        }

        private void OnDestroy()
        {
            _nextLevelButton.onClick.RemoveAllListeners();
            _previousLevelButton.onClick.RemoveAllListeners();
            _startBattleButton.onClick.RemoveAllListeners();
        }

        private void StartBattle()
        {
            var cmd = new StartBattleCommand();
            cmd.Execute(_levelDatas[_currentLevelIndex]);
        }

        private void Next()
        {
            _currentLevelIndex++;
            _currentLevelIndex = Math.Clamp(_currentLevelIndex, 0, _levelDatas.Length - 1);            
        }

        private void Previous()
        {
            _currentLevelIndex--;
            _currentLevelIndex = Math.Clamp(_currentLevelIndex, 0, _levelDatas.Length - 1);
        }
    }
}
