using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class NoteLeft : MonoBehaviour, INote
{
    //this just gonna have like ms, positional info and then when the player tries to interact with it then it will return something like a judgement
    private bool isHit;

    private long msValue;

    public NoteLeft(float msValue) {
        msValue = this.msValue;
    }
    public void Interact()
    {
        if (!isHit) {
        isHit = true;
        }
    }

    public bool IsHit()
    {
        return isHit;
    }
}

