using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StudentDatabase", menuName = "Student/Database")]
public class StudentDatabase : ScriptableObject
{
    public List<StudentData> students;

    public StudentData GetStudentByID(string id)
    {
        return students.Find(student => student.studentID == id);
    }
}
