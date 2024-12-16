using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShipsApp
{
    public partial class ShowShipsForm : Form
    {
        private List<ShipBase> ships;
        private ListBox shipsListBox;
        private TextBox detailsTextBox;

        public ShowShipsForm(List<ShipBase> ships)
        {
            this.ships = ships;
            InitializeComponents();
            LoadShips();
        }

        private void InitializeComponents()
        {
            this.Text = "Список кораблей";
            this.Width = 600;
            this.Height = 400;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            shipsListBox = new ListBox
            {
                Location = new System.Drawing.Point(20, 20),
                Width = 200,
                Height = 300
            };
            shipsListBox.SelectedIndexChanged += ShipsListBox_SelectedIndexChanged;

            detailsTextBox = new TextBox
            {
                Location = new System.Drawing.Point(240, 20),
                Width = 320,
                Height = 300,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };

            this.Controls.AddRange(new Control[] { shipsListBox, detailsTextBox });
        }

        private void LoadShips()
        {
            shipsListBox.Items.Clear();
            foreach (var ship in ships)
            {
                string shipType = ship.GetType().Name;
                string shipName = "";
                
                if (ship is Ship s)
                {
                    shipName = s.GetShipInfo().Split('\n')[0].Replace("Название: ", "");
                }
                
                shipsListBox.Items.Add($"{shipType}: {shipName}");
            }
        }

        private void ShipsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (shipsListBox.SelectedIndex != -1)
            {
                var selectedShip = ships[shipsListBox.SelectedIndex];
                if (selectedShip is Ship ship)
                {
                    detailsTextBox.Text = ship.GetShipInfo();
                }
            }
        }
    }
}
