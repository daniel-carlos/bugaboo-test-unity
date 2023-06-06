using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Template<T> : MonoBehaviour
{
    public abstract void RefreshTemplate(T input);
}
