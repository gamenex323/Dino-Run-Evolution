using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Obi;
public class LineRendererController : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public LineRenderer lineRenderer;
    //public GameObject RopeObject;
    public bool canDraw;

    // Update is called once per frame
    void Update()
    {
        if (canDraw)
        {
            //RopeObject.transform.DOMove(endPos.position, 0.12f).SetEase(Ease.Linear);
            lineRenderer.SetPosition(0, startPos.position);
            lineRenderer.SetPosition(1, endPos.position);
        }
    }
}
