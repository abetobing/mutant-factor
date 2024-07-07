using Brains;

namespace FSM.UIState
{
    public class EntitySelected : IState
    {
        private UISelectDeselect _ui;
        private BasicNeeds _basicNeeds;
        private BaseProfession _baseProfession;

        public EntitySelected(UISelectDeselect theUiPanel)
        {
            _ui = theUiPanel;
        }

        public string String()
        {
            return "selected";
        }

        public void OnEnter()
        {
            if (_ui == null)
                return;

            _ui.entityInfoPanel.gameObject.SetActive(true);
            _ui.nameText.text = _ui.selectedEntity.name;
            if (_ui.selectedEntity.GetComponent<BasicNeeds>() != null)
            {
                _basicNeeds = _ui.selectedEntity.GetComponent<BasicNeeds>();
            }

            if (_ui.selectedEntity.GetComponent<BaseProfession>() != null)
            {
                _baseProfession = _ui.selectedEntity.GetComponent<BaseProfession>();
            }

            _ui.currentInstanceID = _ui.selectedEntity.GetInstanceID();
        }

        public void Tick()
        {
            if (_basicNeeds != null)
            {
                _ui.basicNeedsStateText.text = _basicNeeds.CurrentStateString;
                _ui.healthSlider.value = _basicNeeds.HealthLevel / Constants.DefaultMaxHealth;
                _ui.hungerSlider.value = _basicNeeds.HungerLevel / Constants.DefaultMaxHunger;
                _ui.thirstSlider.value = _basicNeeds.ThirstLevel / Constants.DefaultMaxThirst;
                _ui.staminaSlider.value = _basicNeeds.StaminaLevel / Constants.DefaultMaxStamina;
            }

            if (_baseProfession != null)
            {
                _ui.professionStateText.text = _baseProfession.ActivtyText();
            }
        }


        public void OnExit()
        {
            _ui.entityInfoPanel.gameObject.SetActive(false);
        }
    }
}