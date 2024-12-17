using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShipsApp
{
    public partial class AddShipForm : Form
    {
        private List<ShipBase> ships;
        private ComboBox shipTypeComboBox;
        private TextBox nameTextBox;
        private NumericUpDown lengthNumeric;
        private NumericUpDown crewNumeric;
        private NumericUpDown sailsNumeric;
        private NumericUpDown enginePowerNumeric;
        private TextBox luxuryLevelTextBox;
        private Label sailsLabel;
        private Label enginePowerLabel;
        private Label luxuryLevelLabel;

        public AddShipForm(List<ShipBase> ships)
        {
            this.ships = ships;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Добавить корабль";
            this.Width = 400;
            this.Height = 500;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            var typeLabel = new Label { Text = "Тип корабля:", Location = new System.Drawing.Point(20, 20) };
            shipTypeComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(150, 20),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            shipTypeComboBox.Items.AddRange(new string[] { "Парусник", "Пароход", "Яхта" });
            shipTypeComboBox.SelectedIndexChanged += ShipTypeComboBox_SelectedIndexChanged;

            var nameLabel = new Label { Text = "Название:", Location = new System.Drawing.Point(20, 60) };
            nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(150, 60),
                Width = 200
            };

            var lengthLabel = new Label { Text = "Длина (м):", Location = new System.Drawing.Point(20, 100) };
            lengthNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(150, 100),
                Width = 200,
                Minimum = 1,
                Maximum = 365,
                DecimalPlaces = 2
            };

            var crewLabel = new Label { Text = "Экипаж:", Location = new System.Drawing.Point(20, 140) };
            crewNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(150, 140),
                Width = 200,
                Minimum = 1,
                Maximum = 1000
            };

            sailsLabel = new Label { Text = "Количество парусов:", Location = new System.Drawing.Point(20, 180), Visible = false };
            sailsNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(150, 180),
                Width = 200,
                Minimum = 1,
                Maximum = 100,
                Visible = false
            };

            enginePowerLabel = new Label { Text = "Мощность двигателя:", Location = new System.Drawing.Point(20, 220), Visible = false };
            enginePowerNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(150, 220),
                Width = 200,
                Minimum = 1,
                Maximum = 10000,
                Visible = false
            };

            luxuryLevelLabel = new Label { Text = "Уровень роскоши:", Location = new System.Drawing.Point(20, 260), Visible = false };
            luxuryLevelTextBox = new TextBox
            {
                Location = new System.Drawing.Point(150, 260),
                Width = 200,
                Visible = false
            };

            var addButton = new Button
            {
                Text = "Добавить",
                Location = new System.Drawing.Point(150, 300),
                Width = 100
            };
            addButton.Click += AddButton_Click;

            this.Controls.AddRange(new Control[]
            {
                typeLabel, shipTypeComboBox,
                nameLabel, nameTextBox,
                lengthLabel, lengthNumeric,
                crewLabel, crewNumeric,
                sailsLabel, sailsNumeric,
                enginePowerLabel, enginePowerNumeric,
                luxuryLevelLabel, luxuryLevelTextBox,
                addButton
            });
        }

        private void ShipTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sailsLabel.Visible = false;
            sailsNumeric.Visible = false;
            enginePowerLabel.Visible = false;
            enginePowerNumeric.Visible = false;
            luxuryLevelLabel.Visible = false;
            luxuryLevelTextBox.Visible = false;

            if (shipTypeComboBox.SelectedItem == null) return;

            switch (shipTypeComboBox.SelectedItem.ToString())
            {
                case "Парусник":
                    sailsLabel.Visible = true;
                    sailsNumeric.Visible = true;
                    break;
                case "Пароход":
                    enginePowerLabel.Visible = true;
                    enginePowerNumeric.Visible = true;
                    break;
                case "Яхта":
                    sailsLabel.Visible = true;
                    sailsNumeric.Visible = true;
                    enginePowerLabel.Visible = true;
                    enginePowerNumeric.Visible = true;
                    luxuryLevelLabel.Visible = true;
                    luxuryLevelTextBox.Visible = true;
                    break;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || shipTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ShipBase newShip = null;
            string shipType = shipTypeComboBox.SelectedItem.ToString();
            string name = nameTextBox.Text;
            double length = (double)lengthNumeric.Value;
            int crew = (int)crewNumeric.Value;

            switch (shipType)
            {
                case "Парусник":
                    newShip = new SailingShip(name, length, crew, (int)sailsNumeric.Value);
                    break;
                case "Пароход":
                    newShip = new Steamship(name, length, crew, (int)enginePowerNumeric.Value);
                    break;
                case "Яхта":
                    newShip = new Yacht(name, length, crew, (int)sailsNumeric.Value,
                        (int)enginePowerNumeric.Value, luxuryLevelTextBox.Text);
                    break;
            }

            if (newShip != null)
            {
                ships.Add(newShip);
                MessageBox.Show("Корабль успешно добавлен!", "Успех", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
