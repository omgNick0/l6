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
        private Button editButton;

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

            editButton = new Button
            {
                Text = "Редактировать",
                Location = new System.Drawing.Point(20, 330),
                Width = 120,
                Enabled = false
            };
            editButton.Click += EditButton_Click;

            this.Controls.AddRange(new Control[] { shipsListBox, detailsTextBox, editButton });
        }

        private void LoadShips()
        {
            shipsListBox.Items.Clear();
            foreach (var ship in ships)
            {
                string shipType = ship.GetType().Name;
                string shipName = "";
                
                if (ship is IVessel vessel)
                {
                    shipName = vessel.GetVesselInfo().Split('\n')[0].Replace("Название: ", "");
                }
                
                shipsListBox.Items.Add($"{shipType}: {shipName}");
            }
        }

        private void ShipsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editButton.Enabled = shipsListBox.SelectedIndex != -1;
            
            if (shipsListBox.SelectedIndex != -1)
            {
                var selectedShip = ships[shipsListBox.SelectedIndex];
                if (selectedShip is IVessel vessel)
                {
                    detailsTextBox.Text = vessel.GetVesselInfo();
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (shipsListBox.SelectedIndex != -1)
            {
                var selectedShip = ships[shipsListBox.SelectedIndex];
                var editForm = new EditShipForm(selectedShip);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadShips();
                    ShipsListBox_SelectedIndexChanged(null, EventArgs.Empty);
                }
            }
        }
    }
}
