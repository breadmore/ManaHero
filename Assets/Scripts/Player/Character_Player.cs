using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Player : Character
{
    protected override void DoUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !PlayerHealth.instance.dead)
        {
            Move_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Move(0);
        }
    }

    private void Upgrade()
    {
        if(GameManager.instance.mana >= 100)
        {
            Time.timeScale = 0;
            


            GameManager.instance.ReduceMana(100);

            Time.timeScale = 1;
        }


    }
}
