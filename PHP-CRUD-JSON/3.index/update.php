<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

include "Student.php";
include "StudentManager.php";

$studentsManager = new StudentManager();

$studentId = $_GET['id'];
$studentData = $studentsManager->getStudentById($studentId);

if ($studentData === null) {
    echo "Error: Student not found.";
    exit();
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    try {
        // Process form submission
        $student = new Student();
        $student->setId($studentData['id']);
        
        // Validate form data
        $requiredFields = ["phone", "email", "sub1", "sub2", "sub3", "sub4", "sub5"];
        
        foreach ($requiredFields as $field) {
            if (empty($_POST[$field])) {
                throw new Exception("Please fill in all required fields.");
            }
        }

        // Set properties from the form data
        $student->getAddress()->setPhone($_POST["phone"]);
        $student->getAddress()->setEmail($_POST["email"]);
        $student->getScore()->setSub1($_POST["sub1"]);
        $student->getScore()->setSub2($_POST["sub2"]);
        $student->getScore()->setSub3($_POST["sub3"]);
        $student->getScore()->setSub4($_POST["sub4"]);
        $student->getScore()->setSub5($_POST["sub5"]);

        // Update the student data
        $studentsManager->updateStudent($student);

        // Redirect to the student list page
        header("Location: index.php");
        exit();
    } catch (Exception $e) {
        // Display an alert with the error message
        echo "<script>alert('Error updating student: " . $e->getMessage() . "');</script>";
    }
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Update Student</title>

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
    <h1 class="mt-4 mb-4">Update Student</h1>

    <form action="update.php?id=<?php echo $studentId; ?>" method="post" onsubmit="return validateForm()">
        <div class="mb-3">
            <label for="phone" class="form-label">Phone:</label>
            <input type="text" id="phone" name="phone" class="form-control" value="<?php echo $studentData['address']['phone']; ?>">
        </div>

        <div class="mb-3">
            <label for="email" class="form-label">Email:</label>
            <input type="text" id="email" name="email" class="form-control" value="<?php echo $studentData['address']['email']; ?>">
        </div>

        <div class="mb-3">
            <label for="sub1" class="form-label">Subject 1:</label>
            <input type="number" id="sub1" name="sub1" class="form-control" value="<?php echo $studentData['score']['sub1']; ?>">
        </div>

        <div class="mb-3">
            <label for="sub2" class="form-label">Subject 2:</label>
            <input type="number" id="sub2" name="sub2" class="form-control" value="<?php echo $studentData['score']['sub2']; ?>">
        </div>

        <div class="mb-3">
            <label for="sub3" class="form-label">Subject 3:</label>
            <input type="number" id="sub3" name="sub3" class="form-control" value="<?php echo $studentData['score']['sub3']; ?>">
        </div>

        <div class="mb-3">
            <label for="sub4" class="form-label">Subject 4:</label>
            <input type="number" id="sub4" name="sub4" class="form-control" value="<?php echo $studentData['score']['sub4']; ?>">
        </div>

        <div class="mb-3">
            <label for="sub5" class="form-label">Subject 5:</label>
            <input type="number" id="sub5" name="sub5" class="form-control" value="<?php echo $studentData['score']['sub5']; ?>">
        </div>

        <button type="submit" class="btn btn-primary">Update</button>
    </form>

    <!-- Bootstrap JS (optional) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

    <script>
        function validateForm() {
            // Perform additional validation if needed
            var phone = document.getElementById("phone").value;
            var email = document.getElementById("email").value;
            var sub1 = document.getElementById("sub1").value;
            var sub2 = document.getElementById("sub2").value;
            var sub3 = document.getElementById("sub3").value;
            var sub4 = document.getElementById("sub4").value;
            var sub5 = document.getElementById("sub5").value;

            if (phone === "" || email === "" || sub1 === "" || sub2 === "" || sub3 === "" || sub4 === "" || sub5 === "") {
                alert("Please fill in all required fields.");
                return false;
            }

            // Add more validation logic here if needed

            return true;
        }
    </script>
</div>

</body>
</html>
