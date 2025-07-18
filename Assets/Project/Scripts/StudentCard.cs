using UnityEngine;
using UnityEngine.SceneManagement;

public class StudentCard : MonoBehaviour
{
    public string studentID;

    public void LoadProfile()
    {
        PlayerPrefs.SetString("SelectedStudent", studentID);
        SceneManager.LoadScene("StudentProfile");
    }
}

