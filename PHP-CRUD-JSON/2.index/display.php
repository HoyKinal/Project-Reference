<?php
// Database connection details
$servername = "localhost";
$username = "root";
$password = "@kinal123";
$dbname = "sampledb";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Retrieve all data from the database
$sql = "SELECT name, email FROM users";
$result = $conn->query($sql);

// Display data in a table
if ($result->num_rows > 0) {
    while ($row = $result->fetch_assoc()) {
        echo "<tr><td>" . $row["name"] . "</td><td>" . $row["email"] . "</td></tr>";
    }
} else {
    echo "<tr><td colspan='2'>No records found.</td></tr>";
}

// Close the database connection
$conn->close();
?>
