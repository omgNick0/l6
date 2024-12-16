using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace PublicationManager
{
    public partial class Form1 : Form
    {
        private List<AbstractPublication> publications = new List<AbstractPublication>();

        public Form1()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            publicationTypeCombo.SelectedIndexChanged += PublicationTypeCombo_SelectedIndexChanged;
            addButton.Click += AddButton_Click;
            displayAllButton.Click += DisplayAllButton_Click;
            compareButton.Click += CompareButton_Click;
        }

        private void UpdateInputFields()
        {
            inputPanel!.Controls.Clear();
            inputPanel.RowCount = 0;

            AddField(titleLabel!, titleTextBox!);
            AddField(pagesLabel!, pagesTextBox!);

            if (publicationTypeCombo!.SelectedIndex >= 0)
            {
                switch (publicationTypeCombo!.SelectedItem?.ToString())
                {
                    case "Журнал":
                        AddField(issueNumberLabel!, issueNumberTextBox!);
                        break;

                    case "Книга":
                        AddField(authorLabel!, authorTextBox!);
                        break;

                    case "Учебник":
                        AddField(authorLabel!, authorTextBox!);
                        AddField(issueNumberLabel!, issueNumberTextBox!);
                        AddField(subjectLabel!, subjectTextBox!);
                        break;
                }
            }
        }

        private void AddField(Label label, Control control)
        {
            int row = inputPanel!.RowCount++;
            inputPanel.Controls.Add(label, 0, row);
            inputPanel.Controls.Add(control, 1, row);
        }

        private void PublicationTypeCombo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateInputFields();
        }

        private void AddButton_Click(object? sender, EventArgs e)
        {
            try
            {
                string title = titleTextBox!.Text;
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Введите название");

                if (!int.TryParse(pagesTextBox!.Text, out int pages) || pages <= 0)
                    throw new ArgumentException("Введите корректное количество страниц");

                string selectedType = publicationTypeCombo!.SelectedItem?.ToString() ?? string.Empty;

                AbstractPublication? publication = null;

                switch (selectedType)
                {
                    case "Журнал":
                        if (!int.TryParse(issueNumberTextBox!.Text, out int issueNumber))
                            throw new ArgumentException("Введите корректный номер выпуска");
                        publication = new MagazineBase(titleTextBox!.Text, pages, issueNumber);
                        break;

                    case "Книга":
                        if (string.IsNullOrWhiteSpace(authorTextBox!.Text))
                            throw new ArgumentException("Введите автора");
                        publication = new BookBase(titleTextBox!.Text, pages, authorTextBox!.Text);
                        break;

                    case "Учебник":
                        if (string.IsNullOrWhiteSpace(authorTextBox!.Text))
                            throw new ArgumentException("Введите автора");
                        if (!int.TryParse(issueNumberTextBox!.Text, out issueNumber))
                            throw new ArgumentException("Введите корректный номер выпуска");
                        if (string.IsNullOrWhiteSpace(subjectTextBox!.Text))
                            throw new ArgumentException("Введите предмет");
                        publication = new Textbook(titleTextBox!.Text, pages, authorTextBox!.Text, issueNumber, subjectTextBox!.Text);
                        break;
                }

                if (publication != null)
                {
                    publications.Add(publication);
                    UpdateCompareButton();
                    MessageBox.Show("Публикация успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayAllButton_Click(object? sender, EventArgs e)
        {
            displayTextBox!.Clear();
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

        private void CompareButton_Click(object? sender, EventArgs e)
        {
            if (publications.Count < 2)
            {
                MessageBox.Show("Необходимо добавить как минимум 2 публикации для сравнения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var compareForm = new Form())
            {
                compareForm.Text = "Сравнение публикаций";
                compareForm.Size = new Size(300, 200);
                compareForm.StartPosition = FormStartPosition.CenterParent;

                var firstCombo = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(20, 20),
                    Width = 240
                };

                var secondCombo = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(20, 60),
                    Width = 240
                };

                for (int i = 0; i < publications.Count; i++)
                {
                    string item = $"Публикация {i + 1}: {publications[i].Title}";
                    firstCombo.Items.Add(item);
                    secondCombo.Items.Add(item);
                }

                var compareBtn = new Button
                {
                    Text = "Сравнить",
                    Location = new Point(100, 100),
                    DialogResult = DialogResult.OK
                };

                compareForm.Controls.AddRange(new Control[] { firstCombo, secondCombo, compareBtn });
                compareForm.AcceptButton = compareBtn;

                if (compareForm.ShowDialog() == DialogResult.OK &&
                    firstCombo.SelectedIndex != -1 &&
                    secondCombo.SelectedIndex != -1)
                {
                    var pub1 = publications[firstCombo.SelectedIndex];
                    var pub2 = publications[secondCombo.SelectedIndex];

                    using (var writer = new System.IO.StringWriter())
                    {
                        var oldOut = Console.Out;
                        Console.SetOut(writer);
                        AbstractPublication.ComparePages(pub1, pub2);
                        Console.SetOut(oldOut);
                        MessageBox.Show(writer.ToString(), "Результат сравнения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void UpdateCompareButton()
        {
            compareButton!.Enabled = publications.Count >= 2;
        }

        private void ClearInputFields()
        {
            titleTextBox!.Clear();
            pagesTextBox!.Clear();
            authorTextBox!.Clear();
            issueNumberTextBox!.Clear();
            subjectTextBox!.Clear();
        }
    }
}
