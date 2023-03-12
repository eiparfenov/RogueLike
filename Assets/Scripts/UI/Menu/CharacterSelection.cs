using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Menu
{
    public class CharacterSelection: MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private StartSettings settings;
        [SerializeField] private float movementMult;
        [SerializeField] private float offset;
        [SerializeField] private float returnSpeed;
        
        
        private Transform _charactersLine;
        private bool _isDragging;
        private int _selectedCharacter = 0;

        public int SelectedCharacter
        {
            get { return Mathf.Abs(_selectedCharacter); }
            set
            {
                if(_selectedCharacter == value) return;
                _selectedCharacter = value;
            }
        }

        private void Start()
        {
            _charactersLine = new GameObject("Character line").transform;
            for (int i = 0; i < settings.players.Length; i++)
            {
                Instantiate(settings.players[i], _charactersLine.position + Vector3.right * offset * i,
                    Quaternion.identity, _charactersLine).enabled = false;
            }

            SelectedCharacter = -settings.SelectedCharacter;
            _charactersLine.transform.position = _selectedCharacter * offset * Vector3.right;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var delta = eventData.delta.x;
            var pos = _charactersLine.position; 
            pos += Vector3.right * delta * movementMult;
            pos.x = Mathf.Clamp(pos.x, -(settings.players.Length - 1) * offset, 0);
            _charactersLine.position = pos;
            print(pos);
            SelectedCharacter = Mathf.RoundToInt(pos.x / offset);
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
        }

        private void Update()
        {
            if(_isDragging) return;
            var delta = _selectedCharacter * offset - _charactersLine.position.x;
            delta = Mathf.Clamp(delta, -returnSpeed * Time.deltaTime, returnSpeed * Time.deltaTime);
            _charactersLine.position += Vector3.right * delta;
        }
    }
}