using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selected_dictionary : MonoBehaviour
{
    // Script que contiene el diccionario de unidades selecciondas con los metodos de añadir y quitar

    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();

    public void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();
        if (!selectedTable.ContainsKey(id))
        {
            selectedTable.Add(id, go);
            go.AddComponent<selection_component>();
        }
    }

    public void deselect(int id)
    {
        Destroy(selectedTable[id].GetComponent<selection_component>());
        selectedTable.Remove(id);
    }

    public void deselectAll()
    {
        foreach(KeyValuePair<int, GameObject> pair in selectedTable)
        {
            if(pair.Value != null)
            {
                Destroy(selectedTable[pair.Key].GetComponent<selection_component>());

            }
        }
       
        selectedTable.Clear();
    }

}
