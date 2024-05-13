using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteRight : MonoBehaviour, INote
{
    private bool isHit;

    public void Interact()
    {
        if (!isHit) {
        isHit = true;
        Destroy(GetComponent<BoxCollider>());
        }
    }

    public bool IsHit()
    {
        return isHit;
    }

}
