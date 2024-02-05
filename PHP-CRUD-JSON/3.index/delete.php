<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

include "StudentManager.php"; // Make sure to include the right file

$studentId = $_GET['id'];
$studentsManager = new StudentManager();

try {
    $studentsManager->deleteStudent($studentId);
    // Redirect to the student list page
    header("Location: index.php");
    exit();
} catch (Exception $e) {
    echo "Error deleting student: " . $e->getMessage();
}
?>
