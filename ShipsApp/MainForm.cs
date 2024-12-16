using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShipsApp
{
    public partial class MainForm : Form
    {
        private List<ShipBase> ships = new List<ShipBase>();

        public MainForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Управление кораблями";
            this.Width = 800;
            this.Height = 600;

            var addShipButton = new Button
            {
                Text = "Добавить корабль",
                Location = new System.Drawing.Point(20, 20),
                Width = 150
            };
            addShipButton.Click += (s, e) => new AddShipForm(ships).ShowDialog();

            var showShipsButton = new Button
            {
                Text = "Показать все корабли",
                Location = new System.Drawing.Point(20, 60),
                Width = 150
            };
            showShipsButton.Click += ShowShipsButton_Click;

            var compareShipsButton = new Button
            {
                Text = "Сравнить корабли",
                Location = new System.Drawing.Point(20, 100),
                Width = 150
            };
            compareShipsButton.Click += CompareShipsButton_Click;

            this.Controls.AddRange(new Control[] { addShipButton, showShipsButton, compareShipsButton });
        }

        private void ShowShipsButton_Click(object sender, EventArgs e)
        {
            if (ships.Count == 0)
            {
                MessageBox.Show("Список кораблей пуст!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var showForm = new ShowShipsForm(ships);
            showForm.ShowDialog();
        }

        private void CompareShipsButton_Click(object sender, EventArgs e)
        {
            if (ships.Count < 2)
            {
                MessageBox.Show("Необходимо минимум два корабля для сравнения!", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var compareForm = new CompareShipsForm(ships);
            compareForm.ShowDialog();
        }
    }
}
