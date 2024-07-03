﻿using TMPro;
using UnityEngine;

namespace Entities
{
    [ExecuteInEditMode]
    public class StockPile : MonoBehaviour
    {
        // private static readonly int Pct = Animator.StringToHash("Pct");
    
        // [SerializeField] private TMP_Text _stockpileText;
        [SerializeField] private int _maxHeld = 2000;
    
        private Animator _animator;
        private int _gathered;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _gathered = 0;
        }

        public void Add()
        {
            _gathered++;
            var pct = Mathf.Clamp01((float)_gathered / _maxHeld);
            // _animator.SetFloat(Pct, pct);
            // _stockpileText.SetText($"{_gathered}/{_maxHeld}");
            Debug.LogFormat("Gathered: {0}/{1}", _gathered, _maxHeld);
        }

        private void Update()
        {
            // _stockpileText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
    }
}