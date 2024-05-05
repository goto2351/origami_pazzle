using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィールド状態を管理するクラス
/// </summary>
public class FieldManager : MonoBehaviour
{
    public class Position {
        public int column;
        public int line;

        private const int COLUMN_MAX_INDEX = 11;

        public Position(int column, int line) {
            this.column = column;
            this.line = line;
        }

        /// <summary>
        /// 円周に沿って動かす
        /// </summary>
        /// <param name="direction"></param>
        public void MoveInLine(int direction) {
            var newcolumn = column + direction;
            if (newcolumn < 0) {
                newcolumn = COLUMN_MAX_INDEX;
            }
            else if (newcolumn > COLUMN_MAX_INDEX) {
                newcolumn = 0;
            }

            this.column = newcolumn;
        }
    }

    public class Enemy {
        public Position position;
        public string name;

        public Enemy(int row, int line, string name) {
            this.position = new Position(row, line);
            this.name = name;
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
