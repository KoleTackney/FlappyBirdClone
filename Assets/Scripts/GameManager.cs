using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject PipePrefab;

    bool gameOver = false;
    int score = 0;

    float pipeSpawnTime = 5f;
    float pipeSpawnTimer = 0f;
    float pipeSpawnAccelaration = 0.1f;
    float pipeSpeed = 5f;
    float pipeGap = 6f;
    float pipeGapAccelaration = 0.1f;
    float previousPipeHeight = 0f;
    float maxHeightDiff = 1f;
    float heightDiffAccel = 0.1f;

    List<GameObject> pipes = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;

        updatePipes();

        pipeSpawnTimer += Time.deltaTime;
        if (pipeSpawnTimer > pipeSpawnTime)
        {
            pipeSpawnTimer = 0f;
            pipeSpawnTime -= pipeSpawnAccelaration;
            maxHeightDiff += heightDiffAccel;
            spawnPipe();
        }
    }

    void spawnPipe()
    {
        float maxHeight = 5 - pipeGap / 2;
        float height = Mathf.Clamp(Random.Range(previousPipeHeight - maxHeightDiff, previousPipeHeight + maxHeightDiff), -maxHeight, maxHeight);
        GameObject pipe = Instantiate(PipePrefab, new Vector3(15f, height, 0f), Quaternion.identity);
        pipe.GetComponent<Pipe>().SetGap(pipeGap);
        pipes.Add(pipe);
    }

    void updatePipes()
    {
        if (pipes.Count == 0) return;

        if (pipes[0].transform.position.x < -15f)
        {
            Destroy(pipes[0]);
            pipes.RemoveAt(0);
        }

        foreach (GameObject pipe in pipes)
        {
            pipe.transform.position += Vector3.left * pipeSpeed * Time.deltaTime;

            //Current position of bird is 7.
            if (pipe.transform.position.x < -7f && !pipe.GetComponent<Pipe>().isScored)
            {
                scorePoint(pipe.GetComponent<Pipe>());
            }
        }
    }

    void scorePoint(Pipe pipe)
    {
        score++;
        pipe.isScored = true;
        pipeGap -= pipeGapAccelaration;
        pipeSpeed += pipeSpawnAccelaration;
    }

    void OnGUI()
    {
        if (gameOver)
        {
            float buttonWidth = 100f;
            float buttonHeight = 50f;
            float gap = 20;

            GUI.Label(new Rect(Screen.width / 2 - (buttonWidth / 2 - 5), Screen.height / 2 - gap, buttonWidth, buttonHeight), "You Scored: " + score);
            if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 + gap, 100, 50), "Restart"))
            {
                restartGame();
            }
        }
        else
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Score: " + score);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }
        pipes.Clear();
    }
    void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
