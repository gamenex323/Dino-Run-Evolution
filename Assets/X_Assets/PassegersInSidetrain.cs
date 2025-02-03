using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassegersInSidetrain : MonoBehaviour
{
    //public GameObject charatersParent;
    public string ChacatresInt;
    public int Counter, CounterForAssign;
    // Start is called before the first frame update
    public List<GameObject> Blend = new List<GameObject>();
    public bool IsComplete, IsThis;
    public GameObject TrainBodyOrg, BOx2Des, Engine, TrainBox, TrainBodyDestroy, Child1, Child2, Child3, Child4, Child5, Child6, Child7, Child8, Child9, Child10, Child11, Child12;

    public GameObject Door5;
    void Start()
    {
        IsComplete = false;
        //charatersParent = GameObject.Find("CharatersParent");
        //ChacatresInt =  System.Convert.ToInt32(charatersParent);
        if (gameObject.transform.parent.name == "TrainBox1")
        {

            IsComplete = true;

        }
    }
    public void DoorAnim()
    {
        Door5.GetComponent<Animation>().Play("DoorAnim");

    }
    public void DoorClose()
    {
        Door5.GetComponent<Animation>().Play("DoorAnimClose");

    }
    public void CharacterRigidbody()
    {


    }
    public void Enginedes()
    {
        Engine.SetActive(false);
        TrainBodyDestroy.SetActive(true);

    }
    public void RagDollActivate()
    {
        Child1.GetComponent<Rigidbody>().useGravity = true;

        Child1.GetComponent<CapsuleCollider>().isTrigger = false;
        Child2.GetComponent<Rigidbody>().useGravity = true;

        Child2.GetComponent<CapsuleCollider>().isTrigger = false;
        Child2.GetComponent<Rigidbody>().useGravity = true;

        Child3.GetComponent<CapsuleCollider>().isTrigger = false;
        Child3.GetComponent<Rigidbody>().useGravity = true;

        Child4.GetComponent<CapsuleCollider>().isTrigger = false;
        Child4.GetComponent<Rigidbody>().useGravity = true;

        Child5.GetComponent<CapsuleCollider>().isTrigger = false;
        Child5.GetComponent<Rigidbody>().useGravity = true;

        Child6.GetComponent<CapsuleCollider>().isTrigger = false;
        Child6.GetComponent<Rigidbody>().useGravity = true;

        Child7.GetComponent<CapsuleCollider>().isTrigger = false;
        Child7.GetComponent<Rigidbody>().useGravity = true;

        Child8.GetComponent<CapsuleCollider>().isTrigger = false;
        Child8.GetComponent<Rigidbody>().useGravity = true;

        Child9.GetComponent<CapsuleCollider>().isTrigger = false;
        Child9.GetComponent<Rigidbody>().useGravity = true;

        Child10.GetComponent<CapsuleCollider>().isTrigger = false;
        Child10.GetComponent<Rigidbody>().useGravity = true;

        Child11.GetComponent<CapsuleCollider>().isTrigger = false;
        Child11.GetComponent<Rigidbody>().useGravity = true;

        Child12.GetComponent<CapsuleCollider>().isTrigger = false;
        Child12.GetComponent<Rigidbody>().useGravity = true;

        //Child1.GetComponent<Animator>().enabled = false;
        //Child2.GetComponent<Animator>().enabled = false;
        //Child3.GetComponent<Animator>().enabled = false;
        //Child4.GetComponent<Animator>().enabled = false;
        //Child5.GetComponent<Animator>().enabled = false;
        //Child6.GetComponent<Animator>().enabled = false;
        //Child7.GetComponent<Animator>().enabled = false;
        //Child8.GetComponent<Animator>().enabled = false;
        //Child9.GetComponent<Animator>().enabled = false;
        //Child10.GetComponent<Animator>().enabled = false;
        //Child11.GetComponent<Animator>().enabled = false;
        //Child12.GetComponent<Animator>().enabled = false;

    }
    public void PassengersOn()
    {
        Counter++;
    }
    // Update is called once per frame
    public void LoopOnBlend()
    {
        //if ( IsThis == false)
        // {
        for (int i = 0; i < Blend.Count; i++)
        {
            if (i == 0)
            {
                Blend[0].GetComponent<Animation>().Play("TrainBlend");
            }
            if (i == 1)
            {
                Blend[1].GetComponent<Animation>().Play("TrainBlend 1");
            }
            if (i == 2)
            {
                Blend[2].GetComponent<Animation>().Play("TrainBlend 2");
            }
            if (i == 3)
            {
                Blend[3].GetComponent<Animation>().Play("TrainBlend 3");
            }
            if (i == 4)
            {
                Blend[4].GetComponent<Animation>().Play("TrainBlend 5");

            }
            if (i == 5)
            {
                Blend[5].GetComponent<Animation>().Play("TrianExpand");

            }


        }
        // IsThis = false;
        IsThis = true;

        //   }

    }
    public void LoopOnBlendDestory()
    {
        //if ( IsThis == false)
        // {
        for (int i = 0; i < Blend.Count; i++)
        {
            if (i == 0)
            {
                Blend[0].GetComponent<Animation>().Play("Blast1");
            }
            if (i == 1)
            {
                Blend[1].GetComponent<Animation>().Play("Blast2");
            }
            if (i == 2)
            {
                Blend[2].GetComponent<Animation>().Play("Blast3");
            }
            if (i == 3)
            {
                Blend[3].GetComponent<Animation>().Play("Blast4");
            }
            if (i == 4)
            {
                Blend[4].GetComponent<Animation>().Play("Blast6");
            }
            if (i == 5)
            {
                Blend[5].GetComponent<Animation>().Play("Blast");
            }


        }
        // IsThis = false;
        IsThis = true;

        //   }

    }
    void Update()
    {
        //if (Triggers.instance.TotalPassengersIn >= 6 && IsThis==false)
        //{
        //    for (int i = 0; i < Blend.Count; i++)
        //    {
        //        Blend[i].GetComponent<Animation>().Play();


        //    }
        //    // IsThis = false;
        //    IsThis = true;

        //}
        //if (Triggers.instance.TotalPassengersIn == 11)
        //{
        //    IsComplete = true;
        //}

        if (IsComplete == false)
        {
            //   gameObject.transform.GetChild(Triggers.instance.TotalPassengersIn).gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}
