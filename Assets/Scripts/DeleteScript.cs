using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScript : MonoBehaviour
{
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
