using System;
using FSM;
using FSM.UIState;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Brains
{
    public class UISelectDeselect : MonoBehaviour
    {
        private StateMachine _stateMachine;
        [HideInInspector] public GameObject selectedEntity;
        
        [SerializeField] public RectTransform entityInfoPanel;
        [SerializeField] public RectTransform resourceInfoPanel;

        [SerializeField] public TMP_Text nameText;
        [SerializeField] public TMP_Text stateText;
        [SerializeField] public Slider healthSlider;
        [SerializeField] public Slider hungerSlider;
        [SerializeField] public Slider thirstSlider;
        [SerializeField] public Slider staminaSlider;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            Assert.IsNotNull(nameText, "nameText must be assigned");
            Assert.IsNotNull(stateText, "stateText must be assigned");
            Assert.IsNotNull(entityInfoPanel, "entityInfoPanel must be assigned");
            Assert.IsNotNull(healthSlider, "healthSlider must be assigned");
            Assert.IsNotNull(hungerSlider, "hungerSlider must be assigned");
            Assert.IsNotNull(thirstSlider, "thirstSlider must be assigned");
            Assert.IsNotNull(staminaSlider, "staminaSlider must be assigned");
        }

        private void OnEnable()
        {
            var selected = new EntitySelected(this);
            var deselected = new EntityDeselected(this);
            _stateMachine.AddTransition(deselected, selected, () =>
            {
                return selectedEntity != null && selectedEntity.GetComponent<BasicNeeds>();
            });
            _stateMachine.AddAnyTransition(deselected, () => selectedEntity == null);
            _stateMachine.SetState(deselected);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}