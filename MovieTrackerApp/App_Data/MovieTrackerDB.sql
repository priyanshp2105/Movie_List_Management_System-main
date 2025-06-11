
-- Create Database
CREATE DATABASE IF NOT EXISTS MovieTrackerDB;
USE MovieTrackerDB;

-- Create Users Table
CREATE TABLE IF NOT EXISTS Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    RegistrationDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Create Movies Table
CREATE TABLE IF NOT EXISTS Movies (
    MovieID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NOT NULL,
    Title VARCHAR(100) NOT NULL,
    Director VARCHAR(100),
    ReleaseYear INT,
    Genre VARCHAR(50),
    WatchStatus ENUM('Watched', 'Unwatched', 'Plan to Watch') DEFAULT 'Unwatched',
    Rating INT,
    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
    Notes TEXT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Insert Sample Data
INSERT INTO Users (Username, Password, Email) VALUES
('admin', 'admin123', 'admin@example.com'),
('user1', 'user123', 'user1@example.com');

INSERT INTO Movies (UserID, Title, Director, ReleaseYear, Genre, WatchStatus, Rating, Notes) VALUES
(1, 'The Shawshank Redemption', 'Frank Darabont', 1994, 'Drama', 'Watched', 5, 'A classic movie about hope'),
(1, 'The Godfather', 'Francis Ford Coppola', 1972, 'Crime', 'Watched', 5, 'One of the greatest films of all time'),
(1, 'Inception', 'Christopher Nolan', 2010, 'Sci-Fi', 'Watched', 4, 'Mind-bending thriller'),
(1, 'Pulp Fiction', 'Quentin Tarantino', 1994, 'Crime', 'Unwatched', NULL, 'Need to watch this soon'),
(2, 'The Dark Knight', 'Christopher Nolan', 2008, 'Action', 'Watched', 5, 'Best superhero movie'),
(2, 'Interstellar', 'Christopher Nolan', 2014, 'Sci-Fi', 'Plan to Watch', NULL, 'Heard great things about this');