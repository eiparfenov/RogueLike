using System;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration;
using Signals;
using UnityEngine;
using Utils.Signals;

namespace UI
{
    public class MiniMap: MonoBehaviour
    {
        [SerializeField] private MinimapCell minimapCellPref;
        [SerializeField] private Vector2 size;
        [SerializeField] private GameObject point;
        private List<MazeCell> _generated;
        private List<MinimapCell> _generatedCells;
        
        private void Start()
        {
            SignalBus.AddListener<RoomSwitchSignal>(OnRoomChanged);
            SignalBus.AddListener<LevelFinishSignal>(OnLevelFinished);
            SignalBus.AddListener<FightSignal>(OnFight);
            _generated = new List<MazeCell>();
            _generatedCells = new List<MinimapCell>();
        }

        private void OnRoomChanged(RoomSwitchSignal signal)
        {
            var cell = signal.CellData;
            transform.localPosition = -Vector3.right * size.x * cell.Position.x - Vector3.up * size.y * cell.Position.y;
            if (_generated.Any(x => x.Position == signal.CellData.Position)) return;
            _generated.Add(cell);
            var minimapCell = Instantiate(minimapCellPref, transform);
            minimapCell.ShowCell(cell);
            _generatedCells.Add(minimapCell);
        }

        private void OnLevelFinished(LevelFinishSignal signal)
        {
            foreach (var generatedCell in _generatedCells)
            {
                Destroy(generatedCell.gameObject);
            }
            _generatedCells.Clear();
            _generated.Clear();
        }

        private void OnFight(FightSignal signal)
        {
            gameObject.SetActive(!signal.InProgress);
            point.SetActive(!signal.InProgress);
        }

        private void OnDestroy()
        {
            SignalBus.RemoveListener<RoomSwitchSignal>(OnRoomChanged);
            SignalBus.RemoveListener<FightSignal>(OnFight);
            SignalBus.RemoveListener<LevelFinishSignal>(OnLevelFinished);
        }
    }
}