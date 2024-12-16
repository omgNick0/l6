using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

public abstract class AbstractPublication
{
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

    public string Title => title;

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
        Console.WriteLine($"Номер ошибки: {IssueNumber}");
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
        Console.WriteLine($"Номер ошибки: {IssueNumber}");
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

class PublicationForm : Form
{
    private List<AbstractPublication> publications = new List<AbstractPublication>();
    private ComboBox publicationTypeCombo;
    private Button addButton;
    private TextBox titleTextBox, pagesTextBox, authorTextBox, issueNumberTextBox, subjectTextBox;
    private Label titleLabel, pagesLabel, authorLabel, issueNumberLabel, subjectLabel;
    private RichTextBox displayTextBox;
    private TableLayoutPanel inputPanel;
    private Button displayAllButton;

    public PublicationForm()
    {
        Text = "Управление публикациями";
        Size = new Size(600, 500);

        // Initialize controls
        publicationTypeCombo = new ComboBox
        {
            Dock = DockStyle.Top,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        publicationTypeCombo.Items.AddRange(new string[] { "Журнал", "Книга", "Учебник" });
        publicationTypeCombo.SelectedIndexChanged += PublicationTypeCombo_SelectedIndexChanged;

        inputPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            ColumnCount = 2,
            Padding = new Padding(10)
        };

        // Create input controls
        titleLabel = new Label { Text = "Название:" };
        titleTextBox = new TextBox { Width = 200 };

        pagesLabel = new Label { Text = "Страницы:" };
        pagesTextBox = new TextBox { Width = 200 };

        authorLabel = new Label { Text = "Автор:" };
        authorTextBox = new TextBox { Width = 200 };

        issueNumberLabel = new Label { Text = "Номер выпуска:" };
        issueNumberTextBox = new TextBox { Width = 200 };

        subjectLabel = new Label { Text = "Предмет:" };
        subjectTextBox = new TextBox { Width = 200 };

        addButton = new Button
        {
            Text = "Добавить",
            Dock = DockStyle.Top,
            Height = 30
        };
        addButton.Click += AddButton_Click;

        displayAllButton = new Button
        {
            Text = "Показать все публикации",
            Dock = DockStyle.Top,
            Height = 30
        };
        displayAllButton.Click += DisplayAllButton_Click;

        displayTextBox = new RichTextBox
        {
            Dock = DockStyle.Fill,
            ReadOnly = true
        };

        // Add controls to form
        Controls.Add(displayTextBox);
        Controls.Add(displayAllButton);
        Controls.Add(addButton);
        Controls.Add(inputPanel);
        Controls.Add(publicationTypeCombo);

        publicationTypeCombo.SelectedIndex = 0;
        UpdateInputFields();
    }

    private void UpdateInputFields()
    {
        inputPanel.Controls.Clear();
        inputPanel.RowCount = 0;

        // Always show title and pages
        AddField(titleLabel, titleTextBox);
        AddField(pagesLabel, pagesTextBox);

        switch (publicationTypeCombo.SelectedIndex)
        {
            case 0: // Magazine
                AddField(issueNumberLabel, issueNumberTextBox);
                break;
            case 1: // Book
                AddField(authorLabel, authorTextBox);
                break;
            case 2: // Textbook
                AddField(authorLabel, authorTextBox);
                AddField(issueNumberLabel, issueNumberTextBox);
                AddField(subjectLabel, subjectTextBox);
                break;
        }
    }

    private void AddField(Label label, Control control)
    {
        int row = inputPanel.RowCount++;
        inputPanel.Controls.Add(label, 0, row);
        inputPanel.Controls.Add(control, 1, row);
    }

    private void PublicationTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateInputFields();
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
                throw new ArgumentException("Введите название");

            if (!int.TryParse(pagesTextBox.Text, out int pages) || pages <= 0)
                throw new ArgumentException("Введите корректное количество страниц");

            AbstractPublication publication = null;

            switch (publicationTypeCombo.SelectedIndex)
            {
                case 0: // Magazine
                    if (!int.TryParse(issueNumberTextBox.Text, out int issueNumber))
                        throw new ArgumentException("Введите корректный номер выпуска");
                    publication = new MagazineBase(titleTextBox.Text, pages, issueNumber);
                    break;

                case 1: // Book
                    if (string.IsNullOrWhiteSpace(authorTextBox.Text))
                        throw new ArgumentException("Введите автора");
                    publication = new BookBase(titleTextBox.Text, pages, authorTextBox.Text);
                    break;

                case 2: // Textbook
                    if (string.IsNullOrWhiteSpace(authorTextBox.Text))
                        throw new ArgumentException("Введите автора");
                    if (!int.TryParse(issueNumberTextBox.Text, out issueNumber))
                        throw new ArgumentException("Введите корректный номер выпуска");
                    if (string.IsNullOrWhiteSpace(subjectTextBox.Text))
                        throw new ArgumentException("Введите предмет");
                    publication = new Textbook(titleTextBox.Text, pages, authorTextBox.Text, issueNumber, subjectTextBox.Text);
                    break;
            }

            publications.Add(publication);
            MessageBox.Show("Публикация успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearInputFields();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DisplayAllButton_Click(object sender, EventArgs e)
    {
        displayTextBox.Clear();
        if (publications.Count == 0)
        {
            displayTextBox.AppendText("Нет публикаций для отображения.");
            return;
        }

        foreach (var publication in publications)
        {
            displayTextBox.AppendText("-------------------\n");
            var oldOut = Console.Out;
            using (var writer = new System.IO.StringWriter())
            {
                Console.SetOut(writer);
                publication.Display();
                Console.SetOut(oldOut);
                displayTextBox.AppendText(writer.ToString() + "\n");
            }
        }
    }

    private void ClearInputFields()
    {
        titleTextBox.Clear();
        pagesTextBox.Clear();
        authorTextBox.Clear();
        issueNumberTextBox.Clear();
        subjectTextBox.Clear();
    }
}

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new PublicationForm());
    }
}
