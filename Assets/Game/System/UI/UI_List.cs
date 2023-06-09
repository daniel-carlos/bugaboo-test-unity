using System.Collections.Generic;
using UnityEngine;

public abstract class UI_List<T> : MonoBehaviour
{
    [Tooltip("Objeto que servirá como template para as cópias")]
    public UI_Template<T> template;


    List<UI_Template<T>> clones = new List<UI_Template<T>>();

    public void UpdateList(List<T> values)
    {
        template.gameObject.SetActive(false);

        if (values.Count == 0)
        {
            clones.ForEach(c => c.gameObject.SetActive(false));
        }
        else
        {
            if (values.Count > clones.Count)
            {
                for (int i = 0; i < values.Count - clones.Count; i++)
                {
                    UI_Template<T> go = Instantiate(template, template.transform.parent);
                    clones.Add(go);
                }
            }

            for (int i = 0;i < clones.Count;i++)
            {
                if(i < values.Count)
                {
                    clones[i].gameObject.SetActive(true);
                    clones[i].RefreshTemplate(values[i]);
                }
                else
                {
                    clones[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
