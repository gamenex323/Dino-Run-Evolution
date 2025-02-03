using Cinemachine;
using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;
using UnityEngine;

public class FlagIdManager : MonoBehaviour
{
    public static FlagIdManager Instance;
    FlagID[] ids;
    public Transform flagTransform;

    private void Awake()
    {
        Instance = this;
        ids = GetComponentsInChildren<FlagID>();
    }

    public int ReturnID(FlagID insectID)
    {
        int id = -1;

        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] == insectID)
            {
                id = i;
            }
        }

        return id;
    }

    GameObject particle;

    public void DisableFlag(int flag)
    {
        for (int i = 0; i < ReferenceManager.instance.characterController.moveScript.Length; i++)
        {
            ReferenceManager.instance.characterController.moveScript[i].gameObject.GetComponent<MoveTowardPlayer>().enabled = false;
        }

        ReferenceManager.instance.characterController.cutSceneCamera.gameObject.transform.GetComponent<CinemachineVirtualCamera>().Follow =
           DisFlag(ReferenceManager.instance.characterController.id);

        ReferenceManager.instance.characterController.cutSceneCamera.gameObject.transform.GetComponent<CinemachineVirtualCamera>().LookAt =
        DisFlag(ReferenceManager.instance.characterController.id);

        ReferenceManager.instance.characterController.cutSceneCamera.gameObject.SetActive(true);


        if (ReferenceManager.instance.characterController.gameObject.GetComponent<InsectController>())
            ReferenceManager.instance.characterController.gameObject.GetComponent<InsectController>().enabled = false;

        DOVirtual.DelayedCall(1.5f, () =>
        {
            ids[flag].UpdateMat();

            SoundManager.Instance.PlayOneShot(SoundManager.Instance.areaWinSound, 1);
            Instantiate(ReferenceManager.instance.characterController.areaWinParticle, ReferenceManager.instance.characterController.areaWinParticleSpawnPoint.transform.position,
                ReferenceManager.instance.characterController.areaWinParticleSpawnPoint.transform.rotation);

        });

        DOVirtual.DelayedCall(3f, () =>
        {
            ReferenceManager.instance.characterController.cutSceneCamera.gameObject.SetActive(false);
            if (ReferenceManager.instance.characterController.gameObject.GetComponent<InsectController>())
                ReferenceManager.instance.characterController.gameObject.GetComponent<InsectController>().enabled = true;
        });

        Invoke(nameof(EnableMoveScrpt), 4f);
    }

    void EnableMoveScrpt()
    {
        for (int i = 0; i < ReferenceManager.instance.characterController.moveScript.Length; i++)
        {
            ReferenceManager.instance.characterController.moveScript[i].gameObject.GetComponent<MoveTowardPlayer>().enabled = true;
        }
    }

    public Transform DisFlag(int flag)
    {
        flagTransform = ids[flag].transform;
        return flagTransform;
    }
}
