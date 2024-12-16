using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShipsApp
{
    public partial class CompareShipsForm : Form
    {
        private List<ShipBase> ships;
        private ComboBox ship1ComboBox;
        private ComboBox ship2ComboBox;
        private TextBox resultTextBox;

        public CompareShipsForm(List<ShipBase> ships)
        {
            this.ships = ships;
            InitializeComponents();
            LoadShips();
        }

        private void InitializeComponents()
        {
            this.Text = "Сравнение кораблей";
            this.Width = 500;
            this.Height = 300;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            var ship1Label = new Label { Text = "Первый корабль:", Location = new System.Drawing.Point(20, 20) };
            ship1ComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(150, 20),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var ship2Label = new Label { Text = "Второй корабль:", Location = new System.Drawing.Point(20, 60) };
            ship2ComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(150, 60),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var compareButton = new Button
            {
                Text = "Сравнить",
                Location = new System.Drawing.Point(150, 100),
                Width = 100
            };
            compareButton.Click += CompareButton_Click;

            resultTextBox = new TextBox
            {
                Location = new System.Drawing.Point(20, 140),
                Width = 440,
                Height = 100,
                Multiline = true,
                ReadOnly = true
            };

            this.Controls.AddRange(new Control[]
            {
                ship1Label, ship1ComboBox,
                ship2Label, ship2ComboBox,
                compareButton, resultTextBox
            });
        }

        private void LoadShips()
        {
            foreach (var ship in ships)
            {
                string shipInfo = "";
                if (ship is Ship s)
                {
                    shipInfo = $"{ship.GetType().Name}: {((IVessel)s).GetVesselInfo().Split('\n')[0].Replace("Название: ", "")}";
                }
                
                ship1ComboBox.Items.Add(shipInfo);
                ship2ComboBox.Items.Add(shipInfo);
            }
        }

        private void CompareButton_Click(object sender, EventArgs e)
        {
            if (ship1ComboBox.SelectedIndex != -1 && ship2ComboBox.SelectedIndex != -1)
            {
                var ship1 = ships[ship1ComboBox.SelectedIndex];
                var ship2 = ships[ship2ComboBox.SelectedIndex];

                if (ship1 != null && ship2 != null)
                {
                    string ship1Info = ((IVessel)ship1).GetVesselInfo();
                    string ship2Info = ((IVessel)ship2).GetVesselInfo();

                    var length1 = ship1.GetLength();
                    var length2 = ship2.GetLength();

                    string comparison;
                    if (length1 > length2)
                        comparison = "больше";
                    else if (length1 < length2)
                        comparison = "меньше";
                    else
                        comparison = "равна";

                    resultTextBox.Text = $"Длина первого корабля ({length1} м) {comparison} длины второго корабля ({length2} м)";
                }
            }
        }
    }
}
