using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StudentCard : MonoBehaviour
{
    [Header("Student Info")]
    public string studentID;  // Unique ID like "student00", "student01", etc.

    [Header("Button Component")]
    public Button cardButton; // Optional if you want to hook this manually

    void Start()
    {
        // Auto-hook up the button if not assigned
        if (cardButton == null)
            cardButton = GetComponent<Button>();

        if (cardButton != null)
        {
            cardButton.onClick.AddListener(OnCardClicked);
        }
        else
        {
            Debug.LogWarning("StudentCard is missing a Button component!");
        }
    }

    void OnCardClicked()
    {
        if (ProfileManager.Instance == null)
        {
            Debug.LogError("ProfileManager is missing in scene!");
            return;
        }

        // Set the selected ID and load the profile scene
        ProfileManager.Instance.SelectedStudentID = studentID;
        Debug.Log("Student card clicked: " + studentID);

        SceneManager.LoadScene("StudentProfile"); // Make sure scene is in Build Settings
    }
}
