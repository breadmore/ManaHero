using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<Monster> monsters = new List<Monster>();

    public void SpawnMonster(Monster monsterPrefab)
    {
        Monster monster = Instantiate(monsterPrefab, transform);
        monsters.Add(monster);
    }

    public void ActivateMonster(string monsterName)
    {
        foreach (Monster monster in monsters)
        {
            if (!monster.dead && monster.name == monsterName + "(Clone)" && !monster.gameObject.activeSelf)
            {
                monster.gameObject.SetActive(true);
                return; // Exit the loop after activating one monster
            }
        }
    }

    public void SetBossMonster(string monsterName)
    {
        foreach (Monster monster in monsters)
        {
            if (!monster.dead && monster.name == monsterName + "(Clone)" && !monster.gameObject.activeSelf)
            {
                monster.SetBoss();
                return; // Exit the loop after activating one monster
            }
        }
    }
}
