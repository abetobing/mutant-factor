using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Selector : MonoBehaviour
    {
        private Transform highlight;
        private Transform selection;
        private RaycastHit raycastHit;

        void FixedUpdate()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.yellow);

            if (!EventSystem.current.IsPointerOverGameObject() &&
                Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
            {
                Debug.DrawLine(ray.origin, raycastHit.transform.position, Color.yellow);
                highlight = raycastHit.transform;

                if (highlight.CompareTag("Selectable") && highlight != selection)
                {
                    if (highlight.gameObject.GetComponent<Outline>() == null)
                    {
                        Outline outline = highlight.gameObject.AddComponent<Outline>();
                        outline.enabled = false;
                        highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                        highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                    }
                }
                else
                {
                    highlight = null;
                }
            }

            // Selection
            if (Input.GetMouseButtonDown(0))
            {
                // Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
                if (highlight)
                {
                    if (selection != null)
                    {
                        selection.gameObject.GetComponent<Outline>().enabled = false;
                    }

                    selection = raycastHit.transform;
                    selection.gameObject.GetComponent<Outline>().enabled = true;
                    highlight = null;
                }
                else
                {
                    if (selection)
                    {
                        selection.gameObject.GetComponent<Outline>().enabled = false;
                        selection = null;
                    }
                }
            }
        }
    }
}