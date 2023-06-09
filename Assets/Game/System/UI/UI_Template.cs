using UnityEngine;

public abstract class UI_Template<T> : MonoBehaviour
{
    public abstract void RefreshTemplate(T input);
}
