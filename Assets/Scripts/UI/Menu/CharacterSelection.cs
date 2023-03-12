using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Menu
{
    public class CharacterSelection: MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject[] characters;
        [SerializeField] private float movementMult;
        [SerializeField] private float offset;
        [SerializeField] private float returnSpeed;
        
        private Transform _charactersLine;
        private bool _isDragging;
        private int _selectedCharacter;

        public int SelectedCharacter => Mathf.Abs(_selectedCharacter);

        private void Start()
        {
            _charactersLine = new GameObject("Character line").transform;
            for (int i = 0; i < characters.Length; i++)
            {
                Instantiate(characters[i], _charactersLine.position + Vector3.right * offset * i,
                    Quaternion.identity, _charactersLine);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            var delta = eventData.delta.x;
            var pos = _charactersLine.position; 
            pos += Vector3.right * delta * movementMult;
            pos.x = Mathf.Clamp(pos.x, -(characters.Length - 1) * offset, 0);
            _charactersLine.position = pos;
            print(pos);
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            _selectedCharacter = Mathf.RoundToInt(_charactersLine.position.x / offset);
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