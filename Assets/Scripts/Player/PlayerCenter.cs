using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCenter : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(
            CharacterManager.instance.Player.transform.position.x,
            CharacterManager.instance.Player.transform.position.y);

        //transform.localScale = CharacterManager.instance.Player.transform.localScale;
    }
}
