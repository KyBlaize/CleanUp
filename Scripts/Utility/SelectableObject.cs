using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectableObject : MonoBehaviour, IClickable
{
    public delegate void CallMenu();
    public event CallMenu OpenInteractMenu;

    public GameObject interactionMenu;
    public abstract void Deselect();
    public abstract void Select();
}
