using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StudentDatabase", menuName = "ISA/Student Database")]
public class StudentDatabase : ScriptableObject
{
    public List<StudentData> students;

    public StudentData GetStudentByID(string id)
    {
        return students.Find(student => student.studentID == id);
    }
}

