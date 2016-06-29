using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class Level1 : ILevel {
    public int getColumns()
    {
        return 20;
    }
	int lvl = Random.Range(0, 3);
    public Vector3[] getWallPosition()
	{
		Vector3[] posWall = null;
		if (lvl == 0){
			posWall = new Vector3[100];
			int i = 0;
				
				for (int y = 3; y < 9; y++)
				{
					posWall[i] = new Vector3(3, y, 0f);
					i++;
				}
				for (int y = 11; y < 17; y++)
				{
					posWall[i] = new Vector3(3, y, 0f);
					i++;
				}
				for (int x = 4; x < 7; x++)
				{
					posWall[i] = new Vector3(x, 16, 0f);
					i++;
					posWall[i] = new Vector3(x, 3, 0f);
					i++;
				}
				for (int y = 0; y < 2; y++)
				{
					posWall[i] = new Vector3(9, y, 0f);
					i++;
				}
				for (int y = 18; y < 20; y++)
				{
					posWall[i] = new Vector3(9, y, 0f);
					i++;
				}
				for (int y = 8; y < 12; y++)
				{
					for (int x = 8; x < 12; x++)
					{
						posWall[i] = new Vector3(x, y, 0f);
						i++;
					}
				}
				for (int y = 2; y < 7; y++)
				{
					for (int x = 13; x < 18; x++){
						posWall[i] = new Vector3(x, y, 0f);
						i++;
					}
				}
				for (int y = 13; y < 18; y++)
				{
					for (int x = 13; x < 18; x++){
						posWall[i] = new Vector3(x, y, 0f);
						i++;
					}
				}
			

		}
		if (lvl == 1){
			posWall = new Vector3[86];
			int i = 0;
				for (int y = 1; y < 20; y = y + 2){
					posWall[i] = new Vector3(9, y, 0f);
					i++;
					posWall[i] = new Vector3(10, y, 0f);
					i++;
				}
				for (int y = 3; y < 20; y++){
					posWall[i] = new Vector3(7, y, 0f);
					i++;
				}
				for (int y = 0; y < 17; y++){
					posWall[i] = new Vector3(12, y, 0f);
					i++;
				}
				for (int x = 3; x < 7; x++){
					posWall[i] = new Vector3(x, 3, 0f);
					i++;
					posWall[i] = new Vector3(x, 11, 0f);
					i++;
				}
				for (int x = 0; x < 4; x++){
					posWall[i] = new Vector3(x, 7, 0f);
					i++;
					posWall[i] = new Vector3(x, 15, 0f);
					i++;
				}
				for (int x = 16; x < 19; x++){
					posWall[i] = new Vector3(x, 4, 0f);
					i++;
					posWall[i] = new Vector3(x, 12, 0f);
					i++;
				}
				for (int x = 13; x < 17; x++){
					posWall[i] = new Vector3(x, 8, 0f);
					i++;
					posWall[i] = new Vector3(x, 16, 0f);
					i++;
				}
			
		}
		if (lvl == 2){
			posWall = new Vector3[40];
			int i = 0;
				for (int x = 0; x < 17; x++){
					posWall[i] = new Vector3(x, 3, 0f);
					i++;
					posWall[i] = new Vector3(x, 16, 0f);
					i++;
				}
				posWall[i] = new Vector3(10, 4, 0f);
				i++;
				posWall[i] = new Vector3(13, 4, 0f);
				i++;
				posWall[i] = new Vector3(16, 4, 0f);
				i++;
				posWall[i] = new Vector3(10, 15, 0f);
				i++;
				posWall[i] = new Vector3(13, 15, 0f);
				i++;
				posWall[i] = new Vector3(16, 15, 0f);
				i++;
			}
		
        return posWall;
		
    }

    public int getRows()
    {
        return 20;
    }

    public Vector3[] getEnemyMeleePosition()
    {
		Vector3[] posMelee = null;
		if (lvl == 0){
        posMelee = new Vector3[2];
        posMelee[0] = new Vector3(19, 8, 0f);
        posMelee[1] = new Vector3(19, 11, 0f);
        }
		if (lvl == 1){
		posMelee = new Vector3[2];
		posMelee[0] = new Vector3(0, 0, 0f);
		posMelee[1] = new Vector3(19, 0, 0f);
		}
		if (lvl == 2){
		posMelee = new Vector3[2];
		posMelee[0] = new Vector3(0, 0, 0f);
		posMelee[1] = new Vector3(0, 19, 0f);
		}
		return posMelee;
	}
		


    public Vector3[] getEnemyRangedPosition()
    {
		Vector3[] posRanged = null;
		if (lvl == 0){
        posRanged = new Vector3[2];
        posRanged[0] = new Vector3(10, 0, 0f);
        posRanged[1] = new Vector3(10, 19, 0f);
        }
		if (lvl == 1){
        posRanged = new Vector3[2];
        posRanged[0] = new Vector3(11, 19, 0f);
        posRanged[1] = new Vector3(8, 19, 0f);
        }
		if (lvl == 2){
        posRanged = new Vector3[4];
        posRanged[0] = new Vector3(12, 4, 0f);
        posRanged[1] = new Vector3(15, 4, 0f);
		posRanged[1] = new Vector3(14, 15, 0f);
		posRanged[1] = new Vector3(11, 15, 0f);
        }
		return posRanged;
    }
}
