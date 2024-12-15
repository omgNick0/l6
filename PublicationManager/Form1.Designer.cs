namespace PublicationManager;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    // Control declarations
    private System.Windows.Forms.ComboBox publicationTypeCombo;
    private System.Windows.Forms.Button addButton;
    private System.Windows.Forms.Button displayAllButton;
    private System.Windows.Forms.Button compareButton;
    private System.Windows.Forms.TableLayoutPanel inputPanel;
    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.TextBox titleTextBox;
    private System.Windows.Forms.Label pagesLabel;
    private System.Windows.Forms.TextBox pagesTextBox;
    private System.Windows.Forms.Label issueNumberLabel;
    private System.Windows.Forms.TextBox issueNumberTextBox;
    private System.Windows.Forms.Label authorLabel;
    private System.Windows.Forms.TextBox authorTextBox;
    private System.Windows.Forms.Label subjectLabel;
    private System.Windows.Forms.TextBox subjectTextBox;
    private System.Windows.Forms.TextBox displayTextBox;
        
    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        publicationTypeCombo = new ComboBox();
        addButton = new Button();
        displayAllButton = new Button();
        compareButton = new Button();
        inputPanel = new TableLayoutPanel();
        titleLabel = new Label();
        titleTextBox = new TextBox();
        pagesLabel = new Label();
        pagesTextBox = new TextBox();
        issueNumberLabel = new Label();
        issueNumberTextBox = new TextBox();
        authorLabel = new Label();
        authorTextBox = new TextBox();
        subjectTextBox = new TextBox();
        subjectLabel = new Label();
        displayTextBox = new TextBox();
        inputPanel.SuspendLayout();
        SuspendLayout();
        // 
        // publicationTypeCombo
        // 
        publicationTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        publicationTypeCombo.FormattingEnabled = true;
        publicationTypeCombo.Items.AddRange(new object[] { "Журнал", "Книга", "Учебник" });
        publicationTypeCombo.Location = new Point(22, 26);
        publicationTypeCombo.Margin = new Padding(6);
        publicationTypeCombo.Name = "publicationTypeCombo";
        publicationTypeCombo.Size = new Size(368, 40);
        publicationTypeCombo.TabIndex = 0;
        // 
        // addButton
        // 
        addButton.Location = new Point(22, 533);
        addButton.Margin = new Padding(6);
        addButton.Name = "addButton";
        addButton.Size = new Size(186, 49);
        addButton.TabIndex = 3;
        addButton.Text = "Добавить";
        // 
        // displayAllButton
        // 
        displayAllButton.Location = new Point(219, 533);
        displayAllButton.Margin = new Padding(6);
        displayAllButton.Name = "displayAllButton";
        displayAllButton.Size = new Size(186, 49);
        displayAllButton.TabIndex = 4;
        displayAllButton.Text = "Показать все";
        // 
        // compareButton
        // 
        compareButton.Location = new Point(416, 533);
        compareButton.Margin = new Padding(6);
        compareButton.Name = "compareButton";
        compareButton.Size = new Size(186, 49);
        compareButton.TabIndex = 5;
        compareButton.Text = "Сравнить";
        // 
        // inputPanel
        // 
        inputPanel.AutoSize = true;
        inputPanel.ColumnCount = 2;
        inputPanel.ColumnStyles.Add(new ColumnStyle());
        inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        inputPanel.Controls.Add(titleLabel, 0, 0);
        inputPanel.Controls.Add(titleTextBox, 1, 0);
        inputPanel.Controls.Add(pagesLabel, 0, 1);
        inputPanel.Controls.Add(pagesTextBox, 1, 1);
        inputPanel.Controls.Add(issueNumberLabel, 0, 2);
        inputPanel.Controls.Add(issueNumberTextBox, 1, 2);
        inputPanel.Controls.Add(authorLabel, 0, 3);
        inputPanel.Controls.Add(authorTextBox, 1, 3);
        inputPanel.Controls.Add(subjectTextBox, 1, 4);
        inputPanel.Controls.Add(subjectLabel, 0, 4);
        inputPanel.Location = new Point(22, 87);
        inputPanel.Margin = new Padding(6);
        inputPanel.Name = "inputPanel";
        inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        inputPanel.Size = new Size(854, 427);
        inputPanel.TabIndex = 1;
        // 
        // titleLabel
        // 
        titleLabel.Location = new Point(6, 15);
        titleLabel.Margin = new Padding(6, 15, 6, 0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(162, 36);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Заголовок";
        // 
        // titleTextBox
        // 
        titleTextBox.Location = new Point(180, 12);
        titleTextBox.Margin = new Padding(6, 12, 6, 0);
        titleTextBox.Name = "titleTextBox";
        titleTextBox.Size = new Size(528, 39);
        titleTextBox.TabIndex = 1;
        // 
        // pagesLabel
        // 
        pagesLabel.Location = new Point(6, 75);
        pagesLabel.Margin = new Padding(6, 15, 6, 0);
        pagesLabel.Name = "pagesLabel";
        pagesLabel.Size = new Size(124, 36);
        pagesLabel.TabIndex = 2;
        pagesLabel.Text = "Страницы";
        // 
        // pagesTextBox
        // 
        pagesTextBox.Location = new Point(180, 72);
        pagesTextBox.Margin = new Padding(6, 12, 6, 0);
        pagesTextBox.Name = "pagesTextBox";
        pagesTextBox.Size = new Size(528, 39);
        pagesTextBox.TabIndex = 3;
        // 
        // issueNumberLabel
        // 
        issueNumberLabel.Location = new Point(6, 135);
        issueNumberLabel.Margin = new Padding(6, 15, 6, 0);
        issueNumberLabel.Name = "issueNumberLabel";
        issueNumberLabel.Size = new Size(162, 36);
        issueNumberLabel.TabIndex = 4;
        issueNumberLabel.Text = "Номер Главы";
        // 
        // issueNumberTextBox
        // 
        issueNumberTextBox.Location = new Point(180, 132);
        issueNumberTextBox.Margin = new Padding(6, 12, 6, 0);
        issueNumberTextBox.Name = "issueNumberTextBox";
        issueNumberTextBox.Size = new Size(528, 39);
        issueNumberTextBox.TabIndex = 5;
        // 
        // authorLabel
        // 
        authorLabel.Location = new Point(6, 195);
        authorLabel.Margin = new Padding(6, 15, 6, 0);
        authorLabel.Name = "authorLabel";
        authorLabel.Size = new Size(148, 36);
        authorLabel.TabIndex = 6;
        authorLabel.Text = "Автор";
        // 
        // authorTextBox
        // 
        authorTextBox.Location = new Point(180, 192);
        authorTextBox.Margin = new Padding(6, 12, 6, 0);
        authorTextBox.Name = "authorTextBox";
        authorTextBox.Size = new Size(528, 39);
        authorTextBox.TabIndex = 7;
        // 
        // subjectTextBox
        // 
        subjectTextBox.Location = new Point(180, 252);
        subjectTextBox.Margin = new Padding(6, 12, 6, 0);
        subjectTextBox.Name = "subjectTextBox";
        subjectTextBox.Size = new Size(528, 39);
        subjectTextBox.TabIndex = 9;
        // 
        // subjectLabel
        // 
        subjectLabel.Location = new Point(6, 255);
        subjectLabel.Margin = new Padding(6, 15, 6, 0);
        subjectLabel.Name = "subjectLabel";
        subjectLabel.Size = new Size(136, 36);
        subjectLabel.TabIndex = 8;
        subjectLabel.Text = "Предмет";
        // 
        // displayTextBox
        // 
        displayTextBox.Location = new Point(22, 597);
        displayTextBox.Margin = new Padding(6);
        displayTextBox.Multiline = true;
        displayTextBox.Name = "displayTextBox";
        displayTextBox.ReadOnly = true;
        displayTextBox.ScrollBars = ScrollBars.Vertical;
        displayTextBox.Size = new Size(851, 315);
        displayTextBox.TabIndex = 2;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(13F, 32F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(899, 943);
        Controls.Add(publicationTypeCombo);
        Controls.Add(inputPanel);
        Controls.Add(displayTextBox);
        Controls.Add(addButton);
        Controls.Add(displayAllButton);
        Controls.Add(compareButton);
        Margin = new Padding(6);
        Name = "Form1";
        Text = "Publication Manager";
        inputPanel.ResumeLayout(false);
        inputPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
