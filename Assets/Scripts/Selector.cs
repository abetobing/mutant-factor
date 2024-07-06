using Brains;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Selector : MonoBehaviour
    {
        public UISelectDeselect UISelectDeselect;
        private Transform highlight;
        private Transform selection;
        private RaycastHit raycastHit;

        [Range(0.1f, 10f)] [SerializeField] private float width = 5f;


        private void ToggleHighlight(GameObject targetGameObject, bool enable)
        {
            Outline outline = targetGameObject.GetComponent<Outline>();
            if (outline == null)
            {
                outline = targetGameObject.AddComponent<Outline>();
            }

            outline.OutlineColor = Color.magenta;
            outline.OutlineWidth = width;
            outline.enabled = enable;
        }


        void LateUpdate()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!EventSystem.current.IsPointerOverGameObject() &&
                Physics.Raycast(ray,
                    out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
            {
                highlight = raycastHit.transform;

                if (highlight.CompareTag("Selectable") && highlight != selection)
                {
                    if (highlight.gameObject.GetComponent<Outline>() == null)
                    {
                        ToggleHighlight(highlight.gameObject, false);
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
                if (highlight)
                {
                    // clear the old selection
                    if (selection != null)
                    {
                        selection.gameObject.GetComponent<Outline>().enabled = false;
                    }

                    // get new selection from highlight
                    selection = highlight;
                    ToggleHighlight(selection.gameObject, true);
                    UISelectDeselect.selectedEntity = selection.gameObject;
                    highlight = null;
                }
                else
                {
                    if (selection)
                    {
                        ToggleHighlight(selection.gameObject, false);
                        UISelectDeselect.selectedEntity = null;
                        selection = null;
                    }
                }
            }
        }
    }
}