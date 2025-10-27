-- ============================================================================
-- Upgaming Book Catalog Database Schema
-- Part 2: SQL Scripts for Authors and Books Management
-- ============================================================================

-- ============================================================================
-- 1. CREATE TABLE SCRIPTS
-- ============================================================================
CREATE TABLE Authors (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL
);

CREATE TABLE Books (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    AuthorID INT NOT NULL,
    PublicationYear INT NOT NULL,
    CONSTRAINT FK_BOOK_AUTHOR FOREIGN KEY(AuthorID)
                   REFERENCES Authors(ID)
                   ON DELETE CASCADE
                   ON UPDATE CASCADE
);

-- Books will often be queried via authorID
CREATE INDEX IX_Books_AuthorID ON Books(AuthorID);

GO

-- ============================================================================
-- 2. INSERT SAMPLE DATA
-- ============================================================================
INSERT INTO Authors (Name) VALUES ('Robert C. Martin');
INSERT INTO Authors (Name) VALUES ('Jeffrey Richter');

INSERT INTO Books (Title, AuthorID, PublicationYear) VALUES ('Clean Code', 1, 2008);

INSERT INTO Books (Title, AuthorID, PublicationYear) VALUES ('CLR via C#', 2, 2012);

INSERT INTO Books (Title, AuthorID, PublicationYear) VALUES ('The Clean Coder', 1, 2011);

GO

-- ============================================================================
-- 3. UPDATE SCRIPT
-- Example: Update the publication year of a specific book
-- ============================================================================

-- Update PublicationYear for book with ID 2
UPDATE Books
SET PublicationYear = 2013
WHERE ID = 2;

GO

-- ============================================================================
-- 4. DELETE SCRIPT
-- Example: Delete a specific book by its ID
-- ============================================================================

-- Delete book with ID 3
DELETE FROM Books
WHERE ID = 3;

GO

-- ============================================================================
-- 5. SELECT SCRIPT
-- Retrieve all books with their author names for books published after 2010
-- ============================================================================

SELECT
    b.ID AS BookID,
    b.Title AS BookTitle,
    a.Name AS AuthorName,
    b.PublicationYear
FROM Books b
INNER JOIN Authors a ON b.AuthorID = a.ID
WHERE PublicationYear > 2010
ORDER BY b.ID;

GO