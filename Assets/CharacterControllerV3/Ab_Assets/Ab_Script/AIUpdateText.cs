using Cinemachine;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.UI;

public class AIUpdateText : MonoBehaviour
{
    public static AIUpdateText Instance;
    [SerializeField] private GameObject endPoint;
    [Header("GameObjects")]
    public Transform racePlayer;
    public GameObject[] ai;

    [Header("UI references :")]
    [SerializeField] private Text playerText;
    [SerializeField] private Text[] aiText;
    [SerializeField] private int updateTextCount;

    [SerializeField] private int updateTextCountPlayer;
    [SerializeField] private int updateTextCountAi;

    public GameObject raceinsectMesh;
    public GameObject raceinsectCanvas;
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera stageCamera;
    private float totalDistance;



    private void Awake()
    {
        Instance = this;
        SetAiIdleStae();
    }

    private void Start()
    {
        CalculateTotalDistance();
    }

    public void SetAiIdleStae()
    {
        for (int i = 0; i < ai.Length; i++)
        {
            if (ai[i] != null)
            {
                ai[i].gameObject.GetComponentInChildren<Animator>().SetTrigger("Idle");
            }
        }
    }

    public void SetAiWalkStae()
    {
        for (int i = 0; i < ai.Length; i++)
        {
            if (ai[i] != null)
            {
                ai[i].gameObject.GetComponentInChildren<Animator>().SetTrigger("Walk");
            }
        }
    }

    private void CalculateTotalDistance()
    {
        totalDistance = Vector3.Distance(racePlayer.transform.position, endPoint.transform.position);
    }

    private void Update()
    {
        UpdatePlayerCount();
        UpdateAICount();

        Check_PositionPlayer();
        Check_PositionAi();
    }

    void Check_PositionPlayer()
    {
        int position = ai.Length;

        for (int i = 0; i < ai.Length; i++)
        {
            if (playerDistance < aiDistance[i])
            {
                position--;
            }
        }

        position++;
        playerText.text = position.ToString();
    }

    void Check_PositionAi()
    {
        int[] positions = new int[ai.Length + 1]; // Include player in the ranking
        float[] allDistances = new float[ai.Length + 1];

        // Calculate distances for all AI objects and the player
        allDistances[0] = playerDistance; // Distance for player
        for (int i = 0; i < ai.Length; i++)
        {
            allDistances[i + 1] = aiDistance[i]; // Distance for each AI object
        }

        // Calculate positions
        for (int i = 0; i < positions.Length; i++)
        {
            int position = 1; // Starting position
            for (int j = 0; j < positions.Length; j++)
            {
                if (allDistances[j] < allDistances[i])
                {
                    position++;
                }
            }
            positions[i] = position;
        }

        // Update UI texts
        for (int i = 0; i < ai.Length; i++)
        {
            aiText[i].text = positions[i + 1].ToString(); // Starting from index 1 for AI objects
        }
        playerText.text = positions[0].ToString(); // Player's position
    }


    float playerDistance;
    private void UpdatePlayerCount()
    {
        if (racePlayer != null)
        {
            playerDistance = Vector3.Distance(racePlayer.transform.position, endPoint.transform.position);
        }
    }

    float[] aiDistance;
    private void UpdateAICount()
    {
        aiDistance = new float[ai.Length];

        for (int i = 0; i < ai.Length; i++)
        {
            if (ai[i] != null)
            {
                aiDistance[i] = Vector3.Distance(ai[i].transform.position, endPoint.transform.position);
            }
        }
    }
}
