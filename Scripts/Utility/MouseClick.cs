using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public GameObject selected;
    public GameObject interactionMenu;

    public bool canInteract;

    public Timer timer;
    public GameController gameController;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    private void OnEnable()
    {
        timer.Begin += ToggleInteract;
    }

    private void OnDisable()
    {
        timer.Begin -= ToggleInteract;
    }

    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

                if (hit)
                {
                    if (hitInfo.transform.gameObject.TryGetComponent(out SelectableObject go))
                    {
                        if (selected == null)
                        {
                            selected = go.gameObject;
                            selected.gameObject.SendMessage("Select");
                            interactionMenu.SetActive(true);
                            interactionMenu.transform.position = Input.mousePosition;
                        }
                        else
                        {
                            interactionMenu.SetActive(false);
                            selected.gameObject.SendMessage("Deselect");
                            if (go.gameObject != selected.gameObject)
                            {
                                interactionMenu.SetActive(true);
                                interactionMenu.transform.position = Input.mousePosition;
                            }
                            selected = go.gameObject;
                            selected.gameObject.SendMessage("Select");
                        }
                        
                    }
                    else
                    {
                        DeselectObject();
                    }
                }
                else
                {
                    DeselectObject();
                }
                
            }
        }
    }

    private void ToggleInteract()
    {
        canInteract = !canInteract;
    }

    private void DeselectObject()
    {
        if (selected != null)
        {
            interactionMenu.SetActive(false);

            selected.gameObject.SendMessage("Deselect");
            
            selected = null;
        }
    }
}
