using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Narrator.SayTextStatic(false, "C'est l'histoire de ma vie");
        Narrator.SayTextStatic(false, "Gay as fuck");
    }
}
