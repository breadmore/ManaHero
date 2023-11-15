using UnityEngine;
using System.Collections;

public class SummonFX : MonoBehaviour
{
    public void FXOff()
    {
        gameObject.SetActive(false);
    }

    public void FXDelete()
    {
        Destroy(gameObject);
    }
}
