﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExChangeParts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ExchangeMenu : MonoBehaviour
    {
        [SerializeField] private UI_PartChooser[] partChoosers;
        private UIPartchooserItem[] _items;
        
        [SerializeField] private Button okButton;
        [SerializeField] private Button cancelButton;
        
        private readonly Dictionary<UIPartchooserItem, UI_PartChooser> _itemChooserMap = new();
        
        public event Action OnExchangeMenuOpened;
        public event Action OnExchangeMenuClosed;
        
        private void Awake()
        {
            foreach (var chooser in partChoosers)
            {
                chooser.OnPartSelected += OnChooserChanged;
            }
        }

        private void Start()
        {
            foreach (var chooser in partChoosers)
            {
                foreach (var item in chooser.Items)
                {
                    _itemChooserMap.Add(item, chooser);
                }
                if(chooser.GetCurrentItem != null && !chooser.GetCurrentItem.IsEnabled) okButton.interactable = false;
            }
            okButton.onClick.AddListener(OnOkButtonPressed);
            cancelButton.onClick.AddListener(CloseMenu);
        }
        
        public void OpenMenu()
        {
            gameObject.SetActive(true);
            OnExchangeMenuOpened?.Invoke();
        }

        public void CloseMenu()
        {
            gameObject.SetActive(false);
            OnExchangeMenuClosed?.Invoke();
        }

        public void EnableItem(ExChangeParts.ExchangePart part)
        {
            foreach (var item in _items)
            {
                if (item.PartGameObject == part)
                {
                    item.Enable();
                    _itemChooserMap[item].Enable();
                    break;
                }
            }
        }
        
        public void OnChooserChanged(UIPartchooserItem item)
        {
            Debug.Log("Chooser changed");
            if (!item || item.IsEnabled)
            {
               okButton.interactable = true;
            }
            else
            {
                okButton.interactable = false;
            }
        }

        public void OnOkButtonPressed()
        {
            Debug.Log("OK");
            var parts = (from chooser in partChoosers select chooser.GetCurrentItem into part where part != null select part.PartGameObject).ToList();
            Debug.Log(parts.Count);
            if (parts.Count == 0) return;
            ExchangeSystem.Instance.ChangeParts(parts);
            gameObject.SetActive(false);
        }

    }
}