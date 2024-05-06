using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsFieldUI : MonoBehaviour
{
    private enum MoveType
    {
        None,
        Line,
        Column,
    }

    [SerializeField] private FieldManager _fieldManager;
    [SerializeField] private PartsFieldColumn[] _columnArray;

    [SerializeField] private Dropdown _dropdown;
    [SerializeField] private InputField _numField;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    /// <summary>
    /// ドロップダウンで選択されている移動タイプ
    /// </summary>
    private MoveType SelectedMoveType { get
        {
            switch (_dropdown.value)
            {
                case 0:
                    return MoveType.Line;
                case 1:
                    return MoveType.Column;
                default:
                    return MoveType.None;
            }
        } 
    }
    
    /// <summary>
    /// 円周か列のインデックス
    /// </summary>
    private int SelectedIndex { get
        {
            if (int.TryParse(_numField.text, out int num))
            {
                return num;
            }

            return -1;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        _fieldManager.Initialize();
        ClearField();
        UpdateField();

        _leftButton.onClick.AddListener(() => OnClickMoveButton(direction: -1));
        _rightButton.onClick.AddListener(() => OnClickMoveButton(direction: 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateField() {
        foreach (var enemy in _fieldManager.EnemyList) {
            _columnArray[enemy.Position.Column].SetLabel(enemy.Position.Line, enemy.Name);
        }
    }

    private void ClearField() {
        foreach (var column in _columnArray) {
            column.Clear();
        }
    }

    /// <summary>
    /// "<", ">"ボタンをクリックしたとき
    /// </summary>
    /// <param name="direction">左のとき-1, 右のとき1</param>
    private void OnClickMoveButton(int direction)
    {
        if (SelectedIndex < 0) return; // 入力されていないとき

        // データを更新
        switch (SelectedMoveType)
        {
            case MoveType.Line:
                _fieldManager.MoveLine(SelectedIndex, direction);
                break;
            default:
                Debug.LogWarning($"この操作は実装されていません MoveType: {SelectedMoveType}");
                break;
        }

        // フィールドの表示を更新
        ClearField();
        UpdateField();
    }
}
