<!DOCTYPE html>
<html>
<head>
    <title>PHP Form Example</title>
</head>
<body>

    <h2>Contact Form</h2>
    <form action="index.php" method="post">
        <label for="name">Name:</label>
        <input type="text" id="name" name="name" value="" required><br>

        <label for="email">Email:</label>
        <input type="email" id="email" name="email" value="" required><br>

        <input type="submit" value="Submit">
    </form>

   
    <h2>Search Users</h2>
    <form action="index.php" method="get">
        <label for="search">Search by Name:</label>
        <input type="text" id="search" name="search" value="" required>
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
