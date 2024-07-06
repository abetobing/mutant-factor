using Brains;
using UnityEngine;

namespace FSM.UIState
{
    public class EntityDeselected : IState
    {
        private UISelectDeselect _ui;
        private GameObject _selectedGameObject;

        public EntityDeselected(UISelectDeselect theUiPanel)
        {
            _ui = theUiPanel;
        }

        public string String()
        {
            return "deselected";
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _ui.entityInfoPanel.gameObject.SetActive(false);
            _ui.currentInstanceID = 0;
        }

        public void OnExit()
        {
        }
    }
}