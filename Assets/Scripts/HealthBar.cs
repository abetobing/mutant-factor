using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private Metabolism _metabolism;

        private void OnEnable()
        {
            _metabolism = GetComponent<Metabolism>();
        }

        private void LateUpdate()
        {
            if (_metabolism == null && slider == null)
                return;
            slider.value = _metabolism.health;
            var parentCanvas = slider.GetComponentInParent<Canvas>().transform;
            parentCanvas.LookAt(parentCanvas.position + Camera.main.transform.forward);
        }
    }
}