using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Metabolism _metabolism;

    private void Awake()
    {
        _metabolism = GetComponent<Metabolism>();
    }

    private void OnEnable()
    {
        // shpw the health bar
        slider.transform.parent.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        // hide the health bar
        slider.transform.parent.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (_metabolism == null && slider == null)
            return;
        slider.value = _metabolism.health;
        var parentCanvas = slider.GetComponentInParent<Canvas>().transform;
        parentCanvas.LookAt(parentCanvas.position + Camera.main.transform.forward, Vector3.up);
    }
}