using System;
using Brains;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class EventAction_EntityPanelUI : MonoBehaviour
    {
        [SerializeField] private RectTransform entityInfoPanel;
        [SerializeField] private RectTransform resourceInfoPanel;

        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider hungerSlider;
        [SerializeField] private Slider thirstSlider;
        [SerializeField] private Slider staminaSlider;
        
        private BasicNeeds _basicNeeds;

        private void Awake()
        {
            Assert.IsNotNull(nameText, "Name text must be assigned");
            Assert.IsNotNull(entityInfoPanel, "entityInfoPanel must be assigned");
            Assert.IsNotNull(healthSlider, "healthSlider must be assigned");
            Assert.IsNotNull(hungerSlider, "hungerSlider must be assigned");
            Assert.IsNotNull(thirstSlider, "thirstSlider must be assigned");
            Assert.IsNotNull(staminaSlider, "staminaSlider must be assigned");
        }

        private void Start()
        {
            Debug.Log(GameManager.Instance);
            GameManager.Instance.OnEntitySelected += targetGameObject =>
            {
                if (targetGameObject == null)
                    return;
                
                entityInfoPanel.gameObject.SetActive(true);
                nameText.text = targetGameObject.name;
                if (targetGameObject.GetComponent<BasicNeeds>() != null)
                {
                    _basicNeeds = targetGameObject.GetComponent<BasicNeeds>();
                }
            };
            
            GameManager.Instance.OnEntityDeselected += _ =>
            {
                entityInfoPanel.gameObject.SetActive(false);
                _basicNeeds = null;
            };
        }

        private void Update()
        {
            if (_basicNeeds != null)
            {
                healthSlider.value = _basicNeeds.HealthLevel / Constants.DefaultMaxHealth;
                hungerSlider.value = _basicNeeds.HungerLevel / Constants.DefaultMaxHunger;
                thirstSlider.value = _basicNeeds.ThirstLevel / Constants.DefaultMaxThirst;
                staminaSlider.value = _basicNeeds.StaminaLevel / Constants.DefaultMaxStamina;
            }
        }
    }
}