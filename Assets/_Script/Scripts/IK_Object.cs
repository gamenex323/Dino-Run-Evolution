using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IK_Object : MonoBehaviour
{

    public IKControl iKControl;
    public Ik_Data ik_Data;

    private void OnEnable()
    {
        //iKControl = IKControl.Instance;

        //if (iKControl == null)
        //{
        //    iKControl = FindObjectOfType<IKControl>();
        //}

        ik_Data.Update_ikData(ref iKControl.ik_Data);

    }

    private void OnDisable()
    {
        
    }

}


[System.Serializable]
public class Ik_Data
{
    public Transform LookAtTraget;
    public Transform RH, LH, RF, LF;
    public Transform Spine_01;

    public void Update_ikData(ref Ik_Data data)
    {

        if (RH != null)
        {
            data.RH = this.RH;
        }
        if (LH != null)
        {
            data.LH = this.LH;
        }
        if (RF != null)
        {
            data.RF = this.RF;
        }
        if (LF != null)
        {
            data.LF = this.LF;
        }
        if (Spine_01 != null)
        {
            data.Spine_01 = this.Spine_01;
        }
        if (LookAtTraget != null)
        {
            data.LookAtTraget = this.LookAtTraget;
        }

    }
}