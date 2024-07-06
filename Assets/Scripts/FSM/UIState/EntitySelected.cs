using Brains;
using UnityEngine;

namespace FSM.UIState
{
    public class EntitySelected : IState
    {
        private UISelectDeselect _ui;
        private BasicNeeds _basicNeeds;
        public EntitySelected(UISelectDeselect theUiPanel)
        {
            _ui = theUiPanel;
        }

        public string String()
        {
            return "selected";
        }

        public void Tick()
        {
            if (_basicNeeds != null)
            {
                _ui.stateText.text = $"{_ui.nameText.text} is {_basicNeeds.CurrentStateString}";   
                _ui.healthSlider.value = _basicNeeds.HealthLevel / Constants.DefaultMaxHealth;
                _ui.hungerSlider.value = _basicNeeds.HungerLevel / Constants.DefaultMaxHunger;
                _ui.thirstSlider.value = _basicNeeds.ThirstLevel / Constants.DefaultMaxThirst;
                _ui.staminaSlider.value = _basicNeeds.StaminaLevel / Constants.DefaultMaxStamina;
            }
        }

        public void OnEnter()
        {
            Debug.Log(String());
            if (_ui == null)
                return;
                
            _ui.entityInfoPanel.gameObject.SetActive(true);
            _ui.nameText.text = _ui.selectedEntity.name;
            if (_ui.selectedEntity.GetComponent<BasicNeeds>() != null)
            {
                _basicNeeds = _ui.selectedEntity.GetComponent<BasicNeeds>();
            }
        }

        public void OnExit()
        {
            _ui.entityInfoPanel.gameObject.SetActive(false);
        }
    }
}