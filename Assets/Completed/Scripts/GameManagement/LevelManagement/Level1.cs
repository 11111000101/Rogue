using UnityEngine;
using System.Collections;
using System;

public class Level1 : ILevel {
    public int getColumns()
    {
        return 20;
    }

    public Vector3[] getWallPosition()
    {
        Vector3[] posWall = new Vector3[42];
        for (int i = 0; i < 42; i++)
        {

        
            for (int y = 3; y < 9; y++)
            {
                posWall[i] = new Vector3(3, y, 0f);
            }
            for (int y = 11; y < 17; y++)
            {
                posWall[i] = new Vector3(3, y, 0f);
            }
            for (int x = 4; x < 8; x++)
            {
                posWall[i] = new Vector3(x, 16, 0f);
            }
            for (int x = 4; x < 8; x++)
            {
                posWall[i] = new Vector3(x, 3, 0f);
            }
            for (int y = 0; y < 2; y++)
            {
                posWall[i] = new Vector3(9, y, 0f);
            }
            for (int y = 18; y < 20; y++)
            {
                posWall[i] = new Vector3(9, y, 0f);
            }
            for (int y = 8; y < 12; y++)
            {
                for (int x = 8; x < 12; x++)
                {
                    posWall[i] = new Vector3(x, y, 0f);
                }
        }
    }


        return posWall;
    }

    public int getRows()
    {
        return 20;
    }

    public Vector3[] getEnemyMeleePosition()
    {
        Vector3[] posMelee = new Vector3[2];
        posMelee[0] = new Vector3(19, 8, 0f);
        posMelee[1] = new Vector3(19, 11, 0f);
        return posMelee;
    }

    public Vector3[] getEnemyRangedPosition()
    {
        Vector3[] posRanged = new Vector3[2];
        posRanged[0] = new Vector3(10, 0, 0f);
        posRanged[1] = new Vector3(10, 19, 0f);
        return posRanged;
    }
}
