using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chracter_Monster : Character
{
    private Renderer monsterRenderer;
    protected override void Init()
    {
        base.Init();
        Target_Player = CharacterManager.instance.Player;
        Target_Enemy = null;
        monsterRenderer = transform.GetComponent<Renderer>();
    }


    protected override void DoUpdate()
    {
        Vector2 pos = Target_Player.transform.position;
        Move_Pos = pos;


            Move(.5f);

        if (pos.y+1f > transform.position.y)
        {
            monsterRenderer.sortingOrder = 2;
        }
        else
        {
            monsterRenderer.sortingOrder = 0;
        }
    }
}
