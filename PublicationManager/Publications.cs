using System;

namespace PublicationManager
{
    public abstract class AbstractPublication
    {
        public abstract string Title { get; }
        public abstract void Display();
        public abstract int GetPages();

        public static void ComparePages(AbstractPublication pub1, AbstractPublication pub2)
        {
            if (pub1.GetPages() > pub2.GetPages())
                Console.WriteLine("В первом издании больше страниц.");
            else if (pub1.GetPages() < pub2.GetPages())
                Console.WriteLine("Во втором издании больше страниц.");
            else
                Console.WriteLine("Обе публикации имют одинаковое колличество страниц.");
        }
    }

    public interface IMagazine
    {
        int IssueNumber { get; set; }
        void DisplayMagazineInfo();
    }

    public interface IBook
    {
        string Author { get; set; }
        void DisplayBookInfo();
    }

    public class PrintPublication : AbstractPublication
    {
        private readonly string title;
        private readonly int pages;

        public override string Title => title;

        public PrintPublication(string title, int pages)
        {
            this.title = title ?? throw new ArgumentNullException(nameof(title));
            if (pages < 0)
                throw new ArgumentException("Страницы не могут быть отрицательными");
            this.pages = pages;
        }

        public override void Display()
        {
            Console.WriteLine($"Заголовок: {title}\nСтраницы: {pages}");
        }

        public override int GetPages()
        {
            return pages;
        }
    }

    public class MagazineBase : PrintPublication, IMagazine
    {
        public int IssueNumber { get; set; }

        public MagazineBase(string title, int pages, int issueNumber)
            : base(title, pages)
        {
            IssueNumber = issueNumber;
        }

        public void DisplayMagazineInfo()
        {
            Console.WriteLine($"Номер выпуска: {IssueNumber}");
        }

        public override void Display()
        {
            base.Display();
            DisplayMagazineInfo();
        }
    }

    public class BookBase : PrintPublication, IBook
    {
        public string Author { get; set; }

        public BookBase(string title, int pages, string author)
            : base(title, pages)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
        }

        public void DisplayBookInfo()
        {
            Console.WriteLine($"Автор: {Author}");
        }

        public override void Display()
        {
            base.Display();
            DisplayBookInfo();
        }
    }

    public class Textbook : PrintPublication, IMagazine, IBook
    {
        public int IssueNumber { get; set; }
        public string Author { get; set; }
        private readonly string subject;

        public Textbook(string title, int pages, string author, int issueNumber, string subject)
            : base(title, pages)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            IssueNumber = issueNumber;
            this.subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }

        public void DisplayMagazineInfo()
        {
            Console.WriteLine($"Номер выпуска: {IssueNumber}");
        }

        public void DisplayBookInfo()
        {
            Console.WriteLine($"Автор: {Author}");
        }

        public override void Display()
        {
            base.Display();
            DisplayBookInfo();
            DisplayMagazineInfo();
            Console.WriteLine($"Предмет: {subject}");
        }
    }
}
