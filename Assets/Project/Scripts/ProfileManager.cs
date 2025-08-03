using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance;

    public string SelectedStudentID;  //  Keep this exact name

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
