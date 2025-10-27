namespace Upgaming.Domain.Entities;

public class Book
{
    public int ID { get; private set; }

    public string Title { get; private set; }

    public int AuthorID { get; private set; } // Foreign Key

    public int PublicationYear { get; set; }
}