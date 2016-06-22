using UnityEngine;

public interface ILevel{

    int getColumns();
    int getRows();

    Vector3[] getWallPosition();
    Vector3[] getEnemyMeleePosition();
    Vector3[] getEnemyRangedPosition();
}
