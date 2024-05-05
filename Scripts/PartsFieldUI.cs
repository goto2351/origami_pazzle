using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsFieldUI : MonoBehaviour
{
    [SerializeField] private FieldManager _fieldManager;
    [SerializeField] private PartsFieldColumn[] _columnArray;

    // Start is called before the first frame update
    void Start()
    {
        _fieldManager.Initialize();
        ClearField();
        UpdateField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateField() {
        foreach (var enemy in _fieldManager.EnemyList) {
            _columnArray[enemy.position.column].SetLabel(enemy.position.line, enemy.name);
        }
    }

    private void ClearField() {
        foreach (var column in _columnArray) {
            column.Clear();
        }
    }
}
