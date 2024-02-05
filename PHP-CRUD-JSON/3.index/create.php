<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Add New Student</title>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">

    <style>
        /* Your additional CSS styles here */
        body {
            padding: 20px;
        }
    </style>
</head>
<body>

<div class="container">
    <h1 class="mt-4 mb-4">Add New Student</h1>

    <form action="create.php" method="post">
        <div class="mb-3">
            <label for="phone" class="form-label">Phone:</label>
            <input type="text" id="phone" name="phone" class="form-control" placeholder="Enter phone number">
        </div>

        <div class="mb-3">
            <label for="email" class="form-label">Email:</label>
            <input type="text" id="email" name="email" class="form-control" placeholder="Enter email">
        </div>

        <div class="mb-3">
            <label for="sub1" class="form-label">Subject 1:</label>
            <input type="number" id="sub1" name="sub1" class="form-control" placeholder="Enter subject 1 score">
        </div>

        <div class="mb-3">
            <label for="sub2" class="form-label">Subject 2:</label>
            <input type="number" id="sub2" name="sub2" class="form-control" placeholder="Enter subject 2 score">
        </div>

        <div class="mb-3">
            <label for="sub3" class="form-label">Subject 3:</label>
            <input type="number" id="sub3" name="sub3" class="form-control" placeholder="Enter subject 3 score">
        </div>

        <div class="mb-3">
            <label for="sub4" class="form-label">Subject 4:</label>
            <input type="number" id="sub4" name="sub4" class="form-control" placeholder="Enter subject 4 score">
        </div>

        <div class="mb-3">
            <label for="sub5" class="form-label">Subject 5:</label>
            <input type="number" id="sub5" name="sub5" class="form-control" placeholder="Enter subject 5 score">
        </div>

        <button type="submit" class="btn btn-primary">Submit</button>
    </form>

    <!-- Bootstrap JS (optional) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</div>

</body>
</html>

<?php
include "Student.php";
include "StudentManager.php";

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    try {
        // Validate form data
        $requiredFields = ["phone", "email", "sub1", "sub2", "sub3", "sub4", "sub5"];
        
        foreach ($requiredFields as $field) {
            if (empty($_POST[$field])) {
                throw new Exception("Please fill in all required fields.");
            }
        }

        // Process form submission
        $studentsManager = new StudentManager();
        $student = new Student();
        // Set properties from the form data
        $student->getAddress()->setPhone($_POST["phone"]);
        $student->getAddress()->setEmail($_POST["email"]);
        $student->getScore()->setSub1($_POST["sub1"]);
        $student->getScore()->setSub2($_POST["sub2"]);
        $student->getScore()->setSub3($_POST["sub3"]);
        $student->getScore()->setSub4($_POST["sub4"]);
        $student->getScore()->setSub5($_POST["sub5"]);
        // Add the new student data
        $studentsManager->createStudent($student);
        // Redirect to the student list page
        header("Location: index.php");
        exit();
    } catch (Exception $e) {
        // Display an alert with the error message
        echo "<script>alert('Error creating student: " . $e->getMessage() . "');</script>";
        // You might want to redirect the user back to the form or another appropriate page
    }
}
?>


