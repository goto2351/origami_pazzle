using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class PartsFieldColumn : MonoBehaviour
{
    [SerializeField] private Text[] labelArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLabel(int line, string label) {
        labelArray[line].text = label;
    }

    public void Clear() {
        foreach (var label in labelArray) {
            label.text = "";
        }
    }
}
