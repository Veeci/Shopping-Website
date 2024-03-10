-- Create the database if it does not exist
USE master
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ShoppingWebsite')
BEGIN
    CREATE DATABASE ShoppingWebsite
END
GO

-- Switch to the database
USE ShoppingWebsite
GO

-- Drop the 'user' table if it exists
IF OBJECT_ID('user', 'U') IS NOT NULL
BEGIN
    DROP TABLE [user]
END
GO

-- Create the 'user' table
CREATE TABLE [user] (
    userid INT PRIMARY KEY,
    username NVARCHAR(50),
    [password] NVARCHAR(50),
    email NVARCHAR(100),
    phone_number NVARCHAR(20),
    full_name VARCHAR(100)
)

-- Drop the 'admin' table if it exists
IF OBJECT_ID('admin', 'U') IS NOT NULL
BEGIN
    DROP TABLE admin
END
GO

-- Create the 'admin' table
CREATE TABLE admin (
    adminid INT PRIMARY KEY,
    username NVARCHAR(50),
    [password] NVARCHAR(50),
    full_name VARCHAR(100)
)

--Insert data
-- Insert 30 users with full names
INSERT INTO [user] (userid, username, [password], email, phone_number, full_name)
VALUES
(1, 'user1', 'password1', 'user1@example.com', '1234567890', 'John Doe'),
(2, 'user2', 'password2', 'user2@example.com', '1234567891', 'Jane Smith'),
(3, 'user3', 'password3', 'user3@example.com', '1234567892', 'Robert Johnson'),
(4, 'user4', 'password4', 'user4@example.com', '1234567893', 'Emily Davis'),
(5, 'user5', 'password5', 'user5@example.com', '1234567894', 'Michael Wilson'),
(6, 'user6', 'password6', 'user6@example.com', '1234567895', 'Jennifer Martinez'),
(7, 'user7', 'password7', 'user7@example.com', '1234567896', 'William Anderson'),
(8, 'user8', 'password8', 'user8@example.com', '1234567897', 'Jessica Taylor'),
(9, 'user9', 'password9', 'user9@example.com', '1234567898', 'David Thomas'),
(10, 'user10', 'password10', 'user10@example.com', '1234567899', 'Mary Garcia'),
(11, 'user11', 'password11', 'user11@example.com', '1234567800', 'James Brown'),
(12, 'user12', 'password12', 'user12@example.com', '1234567801', 'Patricia Miller'),
(13, 'user13', 'password13', 'user13@example.com', '1234567802', 'John Wilson'),
(14, 'user14', 'password14', 'user14@example.com', '1234567803', 'Linda Rodriguez'),
(15, 'user15', 'password15', 'user15@example.com', '1234567804', 'Robert Lee'),
(16, 'user16', 'password16', 'user16@example.com', '1234567805', 'Karen Clark'),
(17, 'user17', 'password17', 'user17@example.com', '1234567806', 'Richard Hall'),
(18, 'user18', 'password18', 'user18@example.com', '1234567807', 'Lisa Young'),
(19, 'user19', 'password19', 'user19@example.com', '1234567808', 'Daniel Allen'),
(20, 'user20', 'password20', 'user20@example.com', '1234567809', 'Betty Hernandez'),
(21, 'user21', 'password21', 'user21@example.com', '1234567810', 'Joseph King'),
(22, 'user22', 'password22', 'user22@example.com', '1234567811', 'Nancy Lewis'),
(23, 'user23', 'password23', 'user23@example.com', '1234567812', 'Paul Walker'),
(24, 'user24', 'password24', 'user24@example.com', '1234567813', 'Karen Hill'),
(25, 'user25', 'password25', 'user25@example.com', '1234567814', 'Andrew Scott'),
(26, 'user26', 'password26', 'user26@example.com', '1234567815', 'Helen Green'),
(27, 'user27', 'password27', 'user27@example.com', '1234567816', 'Edward Baker'),
(28, 'user28', 'password28', 'user28@example.com', '1234567817', 'Susan Allen'),
(29, 'user29', 'password29', 'user29@example.com', '1234567818', 'Christopher Cook'),
(30, 'user30', 'password30', 'user30@example.com', '1234567819', 'Anna King');
GO

-- Insert 10 more admins with real names
INSERT INTO admin (adminid, username, [password], full_name)
VALUES
(1, 'admin1', 'adminpassword1', 'John Doe'),
(2, 'admin2', 'adminpassword2', 'Jane Smith'),
(3, 'admin3', 'adminpassword3', 'Michael Johnson'),
(4, 'admin4', 'adminpassword4', 'Emily Davis'),
(5, 'admin5', 'adminpassword5', 'William Wilson'),
(6, 'admin6', 'adminpassword6', 'Olivia Martinez'),
(7, 'admin7', 'adminpassword7', 'David Taylor'),
(8, 'admin8', 'adminpassword8', 'Sophia Brown'),
(9, 'admin9', 'adminpassword9', 'Daniel Anderson'),
(10, 'admin10', 'adminpassword10', 'Ava Thomas');
GO

CREATE TABLE Collection (
    collectionID CHAR(5) PRIMARY KEY,
    collectionName VARCHAR(50),
    description VARCHAR(100)
);

INSERT INTO Collection (collectionID, collectionName, description) VALUES 
('c1', 'Classic Collection', 'Timeless and elegant luggage and bags for everyday use'),
('c2', 'Artistic Collection', 'Creative and unique designs inspired by art and culture'),
('c3', 'Travel Essentials', 'Functional and durable travel accessories for frequent travelers');

CREATE TABLE Product (
    productID CHAR(5) PRIMARY KEY,
    productName VARCHAR(50),
    description VARCHAR(100),
    price DECIMAL(10, 2),
    quantity INT,
    image VARCHAR(50),
    stockDate DATE,
    gender VARCHAR(50),
    collectionID CHAR(5) FOREIGN KEY REFERENCES Collection(collectionID) ON UPDATE CASCADE ON DELETE CASCADE
);

INSERT INTO Product (productID, productName, description, price, quantity, image, stockDate, gender, collectionID) VALUES 
('p1', 'Travel Bag - Black', 'Large capacity travel bag, perfect for long trips', 136.41, 21, 'b1.jpg', '2022-09-21', 'Female', 'c3'),
('p2', 'Luggage Set - European Style', 'Premium quality luggage set for your international travels', 916.55, 36, 'b2.jpg', '2022-08-23', 'Polygender', 'c1'),
('p3', 'Weekend Duffle - Campbells Edition', 'Compact and stylish duffle bag, great for short trips', 659.21, 23, 'b3.jpg', '2022-11-10', 'Male', 'c3'),
('p4', 'Wine - Piper Heidsieck Brut', 'Myasthenia gravis and other myoneural disorders', 402.48, 19, 'b4.jpg', '2022-03-01', 'Female', 'c3'),
('p5', 'Carry-On - Dark Rye Edition', 'Compact carry-on luggage, perfect for business trips', 799.22, 7, 'b5.jpg', '2022-09-11', 'Female', 'c2'),
('p6', 'Backpack - Linen Collection', 'Stylish and durable backpack for everyday use', 884.01, 19, 'b6.jpg', '2022-09-26', 'Male', 'c1'),
('p7', 'Travel Bag - French Mini Assorted', 'Compact travel bag with multiple compartments', 391.72, 12, 'b7.jpg', '2022-07-19', 'Male', 'c3'),
('p8', 'Luggage Set - Paprika Edition', 'Premium luggage set with vibrant paprika color', 828.42, 16, 'b8.jpg', '2022-06-19', 'Female', 'c1'),
('p9', 'Rolling Suitcase - Calabrese Edition', 'Durable rolling suitcase with Calabrese design', 840.83, 14, 'b9.jpg', '2022-11-08', 'Female', 'c3'),
('p10', 'Chocolate - Mi - Amere Semi', 'Explosion and rupture of aerosol can, sequela', 179.28, 34, 'b10.jpeg', '2022-02-20', 'Female', 'c3'),
('p11', 'Travel Backpack - Tenderloin Edition', 'Stylish travel backpack with Tenderloin design', 276.36, 6, 'b11.jpeg', '2022-08-01', 'Male', 'c1'),
('p12', 'Suitcase - English Classic', 'Classic English style suitcase for timeless elegance', 295.16, 17, 'b12.jpeg', '2022-05-19', 'Genderqueer', 'c3'),
('p13', 'Wine - Vidal Icewine Magnotta', 'Open bite, left elbow', 190.12, 14, 'b13.jpg', '2022-08-25', 'Non-binary', 'c1'),
('p14', 'Carry-On - Spanish Cheese Edition', 'Compact carry-on luggage with Spanish cheese design', 252.90, 35, 'b14.jpg', '2022-06-16', 'Female', 'c3'),
('p15', 'Weekender - Apple Muffins Edition', 'Stylish weekend bag with apple muffins design', 346.58, 16, 'b15.jpeg', '2022-05-14', 'Female', 'c1'),
('p16', 'Ice Cream - Turtles Stick Bar', 'Other streptococcal arthritis, left shoulder', 727.93, 35, 'b16.jpg', '2022-12-13', 'Female', 'c2'),
('p17', 'Travel Backpack - Mi - Amere Semi', 'Atresia of vas deferens', 515.56, 36, 'b17.jpg', '2022-08-09', 'Male', 'c2'),
('p18', 'Backpack - English Classic', 'Classic English style backpack for timeless elegance', 741.60, 13, 'b18.jpg', '2022-03-29', 'Male', 'c3'),
('p19', 'Travel Bag - White, Sliced Edition', 'Stylish travel bag with sliced white design', 727.92, 20, 'b19.jpeg', '2022-10-09', 'Male', 'c1');
GO

CREATE TABLE Subscribtion(
	subID CHAR(5) PRIMARY KEY,
	Email CHAR(50),
	userid INT FOREIGN KEY REFERENCES [user](userid) ON DELETE CASCADE ON UPDATE CASCADE
);
GO


-- View the data
SELECT * FROM [user]
SELECT * FROM admin
SELECT * FROM Collection
SELECT * FROM Product
GO