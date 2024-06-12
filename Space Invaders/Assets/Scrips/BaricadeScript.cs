using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaricadeScript : MonoBehaviour
{
    public HalfBaricadeScript[] HalfBaricadeScripts;
    public int leftBaricade;
    public int rightBaricade;

    void Start()
    {
        HalfBaricadeScripts = GetComponentsInChildren<HalfBaricadeScript>();

        //leftBaricade = HalfBaricadeScripts[0] != null ? HalfBaricadeScripts[0].GetDurability() : 0;
        //rightBaricade = HalfBaricadeScripts[1] != null ? HalfBaricadeScripts[1].GetDurability() : 0;

        SetDurability(ref leftBaricade, HalfBaricadeScripts[0]);
        SetDurability(ref rightBaricade, HalfBaricadeScripts[1]);
    }

    private void Update()
    {
        SetDurability(ref leftBaricade, HalfBaricadeScripts[0]);
        SetDurability(ref rightBaricade, HalfBaricadeScripts[1]);
    }

    public void SetDurability(ref int baricade, HalfBaricadeScript hb)
    {
        if (hb != null)
            baricade = hb.GetDurability();

        else
            baricade = 0;
    }
}
