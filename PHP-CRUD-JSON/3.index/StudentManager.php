<?php

class StudentManager
{
    private $students;

    public function __construct()
    {
        // Load existing student data from the JSON file
        $this->loadStudents();
    }

    private function loadStudents()
    {
        $jsonContent = file_get_contents('students.json');
        $this->students = json_decode($jsonContent, true) ?: [];
    }

    private function saveStudents()
    {
        file_put_contents('students.json', json_encode($this->students, JSON_PRETTY_PRINT));
    }

    public function createStudent(Student $student)
    {
        // Generate a unique ID (you may use a better approach)
        $student->setId(uniqid());

        // Add the new student data to the array
        $this->students[] = $student->toArray();

        // Save the updated student data back to the JSON file
        $this->saveStudents();
    }

    public function getStudents()
    {
        return $this->students;
    }

    public function getStudentById($studentId)
    {
        foreach ($this->students as $student) {
            if ($student['id'] === $studentId) {
                return $student;
            }
        }

        return null;
    }

    public function updateStudent(Student $student)
    {
        // Update the student data in the array based on the ID
        $idToUpdate = $student->getId();
        foreach ($this->students as &$existingStudent) {
            if ($existingStudent['id'] === $idToUpdate) {
                $existingStudent = $student->toArray();
                break;
            }
        }

        // Save the updated student data back to the JSON file
        $this->saveStudents();
    }

    public function deleteStudent($studentId)
    {
        // Remove the student data based on the ID
        $this->students = array_filter($this->students, function ($student) use ($studentId) {
            return $student['id'] !== $studentId;
        });

        // Save the updated student data back to the JSON file
        $this->saveStudents();
    }
}
?>
