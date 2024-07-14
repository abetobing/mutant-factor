using Brains;
using DefaultNamespace;

namespace FSM.UIState
{
    public class EntitySelected : IState
    {
        private UISelectDeselect _ui;
        private BasicNeeds _basicNeeds;
        private Metabolism _metabolism;
        private BaseProfession _baseProfession;
        private CombatSystem _combatSystem;

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

            if (_ui.selectedEntity.GetComponent<Metabolism>() != null)
            {
                _metabolism = _ui.selectedEntity.GetComponent<Metabolism>();
            }

            if (_ui.selectedEntity.GetComponent<BaseProfession>() != null)
            {
                _baseProfession = _ui.selectedEntity.GetComponent<BaseProfession>();
            }

            if (_ui.selectedEntity.GetComponent<CombatSystem>() != null)
            {
                _combatSystem = _ui.selectedEntity.GetComponent<CombatSystem>();
            }

            _ui.currentInstanceID = _ui.selectedEntity.GetInstanceID();
        }

        public void Tick()
        {
            if (_metabolism != null && _basicNeeds != null)
            {
                _ui.basicNeedsStateText.text = _basicNeeds.CurrentStateString;
                _ui.healthSlider.value = _metabolism.health / Constants.DefaultMaxHealth;
                _ui.hungerSlider.value = _metabolism.hunger / Constants.DefaultMaxHunger;
                _ui.staminaSlider.value = _metabolism.stamina / Constants.DefaultMaxStamina;
            }

            if (_baseProfession != null)
                _ui.professionStateText.text = _baseProfession.ActivtyText();

            if (_combatSystem != null)
                _ui.combatStateText.text = _combatSystem.ActivityText();
        }


        public void OnExit()
        {
            _ui.entityInfoPanel.gameObject.SetActive(false);
        }
    }
}