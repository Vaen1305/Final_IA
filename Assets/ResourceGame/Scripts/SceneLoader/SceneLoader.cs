using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject[] Civils;
    public string nextSceneName = "NextScene";
    public float checkInterval = 1.0f;
    public bool isGameScene = false;
    int civilsLoot = 0;
    int civilsMinLoot = 0;

    private float nextCheckTime = 0f;
    private void Start()
    {
        civilsLoot = 0;
        civilsMinLoot = (Civils.Length * 50) / 2;
    }
    private void Update()
    {
        if (isGameScene == false) return;

        if (Time.time >= nextCheckTime)
        {
            CheckAllObjectsDestroyed();
            nextCheckTime = Time.time + checkInterval;
        }
    }

    private void CheckAllObjectsDestroyed()
    {
        foreach (GameObject obj in Civils)
        {
            if (obj != null && !ReferenceEquals(obj, null))
                return;
        }

        SceneManager.LoadScene(nextSceneName);
    }
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            civilsLoot += 50;
            if (civilsLoot >= civilsMinLoot)
                SceneManager.LoadScene(nextSceneName);
        }
    }
}
