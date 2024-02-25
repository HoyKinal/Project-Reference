<?php
// Initialize variables
$name = $email = $searchName = "";
$recordInserted = false;

// Check if the form is submitted
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Check if it's the main submission form
    if (isset($_POST['name']) && isset($_POST['email'])) {
        // Retrieve form data
        $name = $_POST["name"];
        $email = $_POST["email"];

        // Validate form data (you can add more validation as needed)
        if (empty($name) || empty($email)) {
            echo "Name and email are required fields.";
        } else {
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

            // Insert data into the database
            $sql = "INSERT INTO users (name, email) VALUES ('$name', '$email')";

            if ($conn->query($sql) === TRUE) {
                $recordInserted = true;
            } else {
                echo "Error: " . $sql . "<br>" . $conn->error;
            }

            // Close the database connection
            $conn->close();
        }
    }
}

// Handle GET method for search
if ($_SERVER["REQUEST_METHOD"] == "GET" && isset($_GET['search'])) {
    $searchName = $_GET['search'];

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

    // Retrieve data from the database based on the search parameter
    $sql = "SELECT name, email FROM users WHERE name LIKE '%$searchName%'";
    $result = $conn->query($sql);

    // Display data in a table
    if ($result->num_rows > 0) {
        echo "<table border='1'>
            <tr>
                <th>Name</th>
                <th>Email</th>
            </tr>";
        while ($row = $result->fetch_assoc()) {
            echo "<tr><td>" . $row["name"] . "</td><td>" . $row["email"] . "</td></tr>";
        }
        echo "</table>";
    } else {
        echo "No records found.";
    }

    // Close the database connection
    $conn->close();
}
?>

<!DOCTYPE html>
<html>
<head>
    <title>PHP Form Example</title>
</head>
<body>

    <h2>Contact Form</h2>
    <form action="index.php" method="post">
        <label for="name">Name:</label>
        <input type="text" id="name" name="name" value="<?php echo htmlspecialchars($name); ?>" required><br>

        <label for="email">Email:</label>
        <input type="email" id="email" name="email" value="<?php echo htmlspecialchars($email); ?>" required><br>

        <input type="submit" value="Submit">
    </form>

    <?php
    // Display success message after form submission
    if ($   ) {
        echo "<p>Record inserted successfully.</p>";
    }
    ?>

    <h2>Search Users</h2>
    <form action="index.php" method="get">
        <label for="search">Search by Name:</label>
        <input type="text" id="search" name="search" value="<?php echo htmlspecialchars($searchName); ?>" required>
        <input type="submit" value="Search">
    </form>

    <hr>

    <h2>Registered Users</h2>
    <table border="1">
        <tr>
            <th>Name</th>
            <th>Email</th>
        </tr>
        <?php include 'display.php'; ?>
    </table>

</body>
</html>
