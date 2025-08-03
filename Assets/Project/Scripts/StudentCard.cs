using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StudentCard : MonoBehaviour
{
    [Header("Student Info")]
    public string studentID;  // Unique ID like "student00", "student01", etc.

    [Header("Button Component")]
    public Button cardButton; // Optional — auto-assigned if null

    void Start()
    {
        // Auto-assign the Button component if it's not set in the Inspector
        if (cardButton == null)
            cardButton = GetComponent<Button>();

        if (cardButton != null)
        {
            cardButton.onClick.AddListener(OnCardClicked);
        }
        else
        {
            Debug.LogWarning($"StudentCard on '{gameObject.name}' is missing a Button component!");
        }
    }

    void OnCardClicked()
    {
        // Check that ProfileManager is available
        if (ProfileManager.Instance == null)
        {
            Debug.LogError("ProfileManager.Instance is null! Ensure it's in the starting scene.");
            return;
        }

        if (string.IsNullOrEmpty(studentID))
        {
            Debug.LogWarning("studentID is empty or null on StudentCard.");
            return;
        }

        // Save selected student ID
        ProfileManager.Instance.SelectedStudentID = studentID;
        Debug.Log($"Student card clicked: {studentID}");

        // Load the profile scene — this must be added to Build Settings
        SceneManager.LoadScene("StudentProfile");
    }
}
