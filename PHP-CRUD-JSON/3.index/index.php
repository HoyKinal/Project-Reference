<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Student Information</title>
    <link rel="icon" href="php.png">

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
    <h1 class="mt-4 mb-4">Student Information List</h1>

    <!-- Add Create button to navigate to create.php -->
    <a href="create.php" class="btn btn-success mb-4">Add New Student</a>

    <?php
    include "Student.php";
    include "StudentManager.php";

    // Read
    $students = json_decode(file_get_contents('students.json'), true);

    echo "<table class='table table-success table-striped table-hover'>";
    echo "<thead>";
    echo "<tr><th>Student ID</th><th>Phone</th><th>Email</th><th>Subject 1</th><th>Subject 2</th><th>Subject 3</th><th>Subject 4</th><th>Subject 5</th><th>Total</th><th>Grade</th><th>Actions</th></tr>";
    echo "</thead>";
    echo "<tbody>";

    // Check if $students is not empty before iterating
    if (!empty($students)) {
        // Display all students in the table
        foreach ($students as $studentData) {
            echo "<tr>";
            echo "<td>" . $studentData['id'] . "</td>";
            echo "<td>" . $studentData['address']['phone'] . "</td>";
            echo "<td>" . $studentData['address']['email'] . "</td>";
            echo "<td>" . $studentData['score']['sub1'] . "</td>";
            echo "<td>" . $studentData['score']['sub2'] . "</td>";
            echo "<td>" . $studentData['score']['sub3'] . "</td>";
            echo "<td>" . $studentData['score']['sub4'] . "</td>";
            echo "<td>" . $studentData['score']['sub5'] . "</td>";
            echo "<td>" . $studentData['score']['total'] . "</td>";
            echo "<td>" . $studentData['score']['grade'] . "</td>";
            echo "<td>";
            // Add Update and Delete buttons with links to update.php and delete.php
            echo "<a href='update.php?id={$studentData['id']}' class='btn btn-primary btn-sm'>Update</a> ";
            // Add a confirmation dialog for the delete action
            echo "<button class='btn btn-danger btn-sm' onclick='confirmDelete(\"{$studentData['id']}\")'>Delete</button>";
            echo "</td>";
            echo "</tr>";
        }
        } else {
            echo "<tr><td colspan='11'>No students found.</td></tr>";
        }

        echo "</tbody>";
        echo "</table>";

        // JavaScript function for confirmation dialog
        echo "<script>
            function confirmDelete(studentId) {
                if (confirm('Are you sure you want to delete this student?')) {
                    window.location.href = 'delete.php?id=' + studentId;
                }
            }
        </script>";
    ?>

    <!-- Bootstrap JS (optional) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</div>

</body>
</html>
