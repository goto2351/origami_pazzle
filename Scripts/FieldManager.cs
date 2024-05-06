using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィールド状態を管理するクラス
/// </summary>
public class FieldManager : MonoBehaviour
{
    public class Position {
        public int Column;
        public int Line;

        private const int COLUMN_MAX_INDEX = 11;

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

    public void Initialize() {
        // 敵データを仮で入れる
        _enemyList = new List<Enemy>() {
            new Enemy(11, 0, "A"),
            new Enemy(0, 1, "A"),
            new Enemy(2, 0, "A"),
            new Enemy(3, 1, "A"),
        };
    }
}
