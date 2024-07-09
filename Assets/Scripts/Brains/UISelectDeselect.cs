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
        [HideInInspector] public int currentInstanceID;

        [SerializeField] public RectTransform entityInfoPanel;
        [SerializeField] public RectTransform resourceInfoPanel;

        [SerializeField] public TMP_Text nameText;
        [SerializeField] public TMP_Text basicNeedsStateText;
        [SerializeField] public TMP_Text professionStateText;
        [SerializeField] public Slider healthSlider;
        [SerializeField] public Slider hungerSlider;
        [SerializeField] public Slider staminaSlider;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            currentInstanceID = 0;
            Assert.IsNotNull(nameText, "nameText must be assigned");
            Assert.IsNotNull(basicNeedsStateText, "basicNeedsStateText must be assigned");
            Assert.IsNotNull(professionStateText, "professionStateText must be assigned");
            Assert.IsNotNull(entityInfoPanel, "entityInfoPanel must be assigned");
            Assert.IsNotNull(healthSlider, "healthSlider must be assigned");
            Assert.IsNotNull(hungerSlider, "hungerSlider must be assigned");
            Assert.IsNotNull(staminaSlider, "staminaSlider must be assigned");
        }

        private void OnEnable()
        {
            var empty = new EmptyState();
            var selected = new EntitySelected(this);
            var deselected = new EntityDeselected(this);
            _stateMachine.AddTransition(selected, empty,
                () => !selectedEntity.GetInstanceID().Equals(currentInstanceID));
            _stateMachine.AddTransition(empty, selected, EligibleToSelect());
            _stateMachine.AddTransition(deselected, selected, EligibleToSelect());

            Func<bool> EligibleToSelect() => () => (selectedEntity != null &&
                                                    selectedEntity.GetComponent<BasicNeeds>() != null &&
                                                    !currentInstanceID.Equals(selectedEntity.GetInstanceID()));

            _stateMachine.AddAnyTransition(deselected, () => selectedEntity == null);
            _stateMachine.SetState(deselected);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}