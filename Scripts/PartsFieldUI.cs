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
    /// �h���b�v�_�E���őI������Ă���ړ��^�C�v
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
    /// �~������̃C���f�b�N�X
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
    /// "<", ">"�{�^�����N���b�N�����Ƃ�
    /// </summary>
    /// <param name="direction">���̂Ƃ�-1, �E�̂Ƃ�1</param>
    private void OnClickMoveButton(int direction)
    {
        if (SelectedIndex < 0) return; // ���͂���Ă��Ȃ��Ƃ�

        // �f�[�^���X�V
        switch (SelectedMoveType)
        {
            case MoveType.Line:
                _fieldManager.MoveLine(SelectedIndex, direction);
                break;
            default:
                Debug.LogWarning($"���̑���͎�������Ă��܂��� MoveType: {SelectedMoveType}");
                break;
        }

        // �t�B�[���h�̕\�����X�V
        ClearField();
        UpdateField();
    }
}
