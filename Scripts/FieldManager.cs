using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// フィールド状態を管理するクラス
/// </summary>
public class FieldManager : MonoBehaviour
{
    public class Position {
        public int Column;
        public int Line;

        public Position(int column, int line) {
            this.Column = column;
            this.Line = line;
        }

        /// <summary>
        /// 円周に沿って動かす
        /// </summary>
        /// <param name="direction"></param>
        public void MoveInLine(int direction) {
            var newcolumn = Column + direction;
            if (newcolumn < 0) {
                newcolumn = COLUMN_MAX_INDEX;
            }
            else if (newcolumn > COLUMN_MAX_INDEX) {
                newcolumn = 0;
            }

            this.Column = newcolumn;
        }

        /// <summary>
        /// 列に沿って動かす
        /// </summary>
        /// <param name="direction"></param>
        public void MoveInColumn(int direction)
        {
            var newColumn = Column;
            var newLine = Line + direction;

            if (newLine > LINE_MAX_INDEX)
            {
                // 外側に溢れたときは反対側のいちばん外に移動する
                newColumn = (Column + COLUMN_HALF_NUM) % COLUMN_NUM;
                newLine = LINE_MAX_INDEX;
            }
            else if (newLine < 0)
            {
                // 内側に溢れたときは反対側のいちばん内側に移動する
                newColumn = (Column + COLUMN_HALF_NUM) % COLUMN_NUM;
                newLine = 0;
            }

            this.Column = newColumn;
            this.Line = newLine;
        }
    }

    public class Enemy {
        public Position Position;
        public string Name;

        public Enemy(int row, int line, string name) {
            this.Position = new Position(row, line);
            this.Name = name;
        }
    }

    private List<Enemy> _enemyList = new List<Enemy>();
    public List<Enemy> EnemyList => _enemyList;

    private const int COLUMN_MAX_INDEX = 11;
    private const int LINE_MAX_INDEX = 3;

    private const int COLUMN_NUM = 12;
    private const int COLUMN_HALF_NUM = 6;

    public void Initialize() {
        // 敵データを仮で入れる
        _enemyList = new List<Enemy>() {
            new Enemy(11, 0, "A"),
            new Enemy(0, 1, "A"),
            new Enemy(2, 0, "A"),
            new Enemy(3, 1, "A"),
        };
    }

    /// <summary>
    /// 円周方向の移動
    /// </summary>
    /// <param name="lineIndex"></param>
    /// <param name="direction"></param>
    public void MoveLine(int lineIndex, int direction)
    {
        var targetEnemyArray = _enemyList.Where(x => x.Position.Line == lineIndex)
            .ToArray();

        foreach (var enemy in targetEnemyArray)
        {
            enemy.Position.MoveInLine(direction);
        }
    }

    /// <summary>
    /// 列方向の移動
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <param name="direction"></param>
    public void MoveRow(int rowIndex, int direction)
    {
        var rightSideRowIndex = rowIndex % 6;
        var leftSideRowIndex = rightSideRowIndex + 6;
        var targetEnemyArray = _enemyList.Where(x => x.Position.Column == rightSideRowIndex
            || x.Position.Column == leftSideRowIndex)
            .ToArray();

        foreach (var enemy in targetEnemyArray)
        {
            if (enemy.Position.Column == rightSideRowIndex)
            {
                enemy.Position.MoveInColumn(direction);
            }
            else
            {
                enemy.Position.MoveInColumn(-direction);
            }
        }
    }
}
