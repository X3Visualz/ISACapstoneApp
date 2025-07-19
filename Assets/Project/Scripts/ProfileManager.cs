using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance;

    public string SelectedStudentID;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this across scene loads
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
}
