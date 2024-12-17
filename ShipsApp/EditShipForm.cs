using System;
using System.Windows.Forms;

namespace ShipsApp
{
    public partial class EditShipForm : Form
    {
        private ShipBase ship;
        private TextBox nameTextBox;
        private NumericUpDown lengthNumeric;
        private NumericUpDown crewNumeric;
        private NumericUpDown sailsNumeric;
        private NumericUpDown enginePowerNumeric;
        private TextBox luxuryLevelTextBox;
        private Label sailsLabel;
        private Label enginePowerLabel;
        private Label luxuryLevelLabel;

        public EditShipForm(ShipBase ship)
        {
            this.ship = ship;
            InitializeComponents();
            LoadShipData();
        }

        private void InitializeComponents()
        {
            this.Text = "Редактировать корабль";
            this.Width = 400;
            this.Height = 500;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            var nameLabel = new Label { Text = "Название:", Location = new System.Drawing.Point(20, 20) };
            nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(150, 20),
                Width = 200
            };

            var lengthLabel = new Label { Text = "Длина (м):", Location = new System.Drawing.Point(20, 60) };
            lengthNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(150, 60),
                Width = 200,
                Minimum = 1,
                Maximum = 365,
                DecimalPlaces = 2
            };
            lengthNumeric.ValueChanged += (s, e) => {
                UpdateCrewMaximum();
                UpdateMinEnginePower();
            };

            var crewLabel = new Label { Text = "Экипаж:", Location = new System.Drawing.Point(20, 100) };
            crewNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(150, 100),
                Width = 200,
                Minimum = 1,
                Maximum = 1000
            };
            crewNumeric.ValueChanged += (s, e) => {
                UpdateMinEnginePower();
                UpdateMinSails();
            };

            sailsLabel = new Label { Text = "Количество парусов:", Location = new System.Drawing.Point(20, 140) };
            sailsNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(150, 140),
                Width = 200,
                Minimum = 1,
                Maximum = 100
            };
            sailsNumeric.ValueChanged += (s, e) => UpdateMinEnginePower();

            enginePowerLabel = new Label { Text = "Мощность двигателя:", Location = new System.Drawing.Point(20, 180) };
            enginePowerNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(150, 180),
                Width = 200,
                Minimum = 1,
                Maximum = 100000
            };
            enginePowerNumeric.ValueChanged += EnginePowerNumeric_ValueChanged;

            luxuryLevelLabel = new Label { Text = "Уровень роскоши:", Location = new System.Drawing.Point(20, 220) };
            luxuryLevelTextBox = new TextBox
            {
                Location = new System.Drawing.Point(150, 220),
                Width = 200
            };

            var saveButton = new Button
            {
                Text = "Сохранить",
                Location = new System.Drawing.Point(150, 260),
                Width = 100,
                DialogResult = DialogResult.OK
            };
            saveButton.Click += SaveButton_Click;

            var cancelButton = new Button
            {
                Text = "Отмена",
                Location = new System.Drawing.Point(260, 260),
                Width = 100,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[]
            {
                nameLabel, nameTextBox,
                lengthLabel, lengthNumeric,
                crewLabel, crewNumeric,
                sailsLabel, sailsNumeric,
                enginePowerLabel, enginePowerNumeric,
                luxuryLevelLabel, luxuryLevelTextBox,
                saveButton, cancelButton
            });

            // Показываем только нужные поля в зависимости от типа корабля
            sailsLabel.Visible = sailsNumeric.Visible = false;
            enginePowerLabel.Visible = enginePowerNumeric.Visible = false;
            luxuryLevelLabel.Visible = luxuryLevelTextBox.Visible = false;
        }

        private void LoadShipData()
        {
            if (ship is Ship baseShip)
            {
                var shipInfo = baseShip.GetVesselInfo().Split('\n');
                nameTextBox.Text = shipInfo[0].Replace("Название: ", "");
                lengthNumeric.Value = (decimal)baseShip.GetLength();
                UpdateCrewMaximum();  // Обновляем максимум экипажа перед установкой значения
                crewNumeric.Value = int.Parse(shipInfo[2].Replace("Экипаж: ", "").Replace(" человек", ""));
                UpdateMinSails();

                if (ship is SailingShip sailingShip)
                {
                    sailsLabel.Visible = sailsNumeric.Visible = true;
                    sailsNumeric.Value = int.Parse(shipInfo[3].Replace("Количество парусов: ", ""));
                }
                else if (ship is Steamship steamship)
                {
                    enginePowerLabel.Visible = enginePowerNumeric.Visible = true;
                    enginePowerNumeric.Value = int.Parse(shipInfo[3].Replace("Мощность двигателя: ", "").Replace(" л.с.", ""));
                    UpdateMinEnginePower();
                }
                else if (ship is Yacht yacht)
                {
                    sailsLabel.Visible = sailsNumeric.Visible = true;
                    enginePowerLabel.Visible = enginePowerNumeric.Visible = true;
                    luxuryLevelLabel.Visible = luxuryLevelTextBox.Visible = true;

                    sailsNumeric.Value = int.Parse(shipInfo[3].Replace("Количество парусов: ", ""));
                    enginePowerNumeric.Value = int.Parse(shipInfo[4].Replace("Мощность двигателя: ", "").Replace(" л.с.", ""));
                    luxuryLevelTextBox.Text = shipInfo[5].Replace("Уровень роскоши: ", "");
                    UpdateMinEnginePower();
                }
            }
        }

        private void UpdateMinEnginePower()
        {
            int minPower;
            if (ship is Steamship)
            {
                minPower = (int)((double)lengthNumeric.Value * (int)crewNumeric.Value);
            }
            else if (ship is Yacht)
            {
                minPower = (int)((double)lengthNumeric.Value * (int)crewNumeric.Value + (int)sailsNumeric.Value);
            }
            else
            {
                return;
            }

            enginePowerNumeric.Minimum = minPower;
            if (enginePowerNumeric.Value < minPower)
            {
                enginePowerNumeric.Value = minPower;
            }
        }

        private void UpdateMinSails()
        {
            if (ship is SailingShip || ship is Yacht)
            {
                // Вычисляем минимальное количество парусов: корень из (экипаж + 1)
                int minSails = (int)Math.Ceiling(Math.Sqrt(crewNumeric.Value + 1));
                sailsNumeric.Minimum = minSails;
                if (sailsNumeric.Value < minSails)
                {
                    sailsNumeric.Value = minSails;
                }
            }
        }

        private void UpdateCrewMaximum()
        {
            // Устанавливаем максимум экипажа на 1 меньше длины корабля
            int maxCrew = (int)lengthNumeric.Value - 1;
            if (maxCrew < 1) maxCrew = 1;
            
            // Учитываем ограничение по длине корабля
            if (lengthNumeric.Value < 50 && maxCrew > 30)
                maxCrew = 30;
            else if (lengthNumeric.Value < 150 && maxCrew > 100)
                maxCrew = 100;
            else if (maxCrew > 200)
                maxCrew = 200;

            crewNumeric.Maximum = maxCrew;
            if (crewNumeric.Value > maxCrew)
            {
                crewNumeric.Value = maxCrew;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Заполните название корабля!", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
                return;
            }

            try
            {
                if (ship is Ship baseShip)
                {
                    if (ship is SailingShip sailingShip)
                    {
                        var newShip = new SailingShip(
                            nameTextBox.Text,
                            (double)lengthNumeric.Value,
                            (int)crewNumeric.Value,
                            (int)sailsNumeric.Value
                        );
                        CopyProperties(newShip);
                    }
                    else if (ship is Steamship steamship)
                    {
                        var newShip = new Steamship(
                            nameTextBox.Text,
                            (double)lengthNumeric.Value,
                            (int)crewNumeric.Value,
                            (int)enginePowerNumeric.Value
                        );
                        CopyProperties(newShip);
                    }
                    else if (ship is Yacht yacht)
                    {
                        var newShip = new Yacht(
                            nameTextBox.Text,
                            (double)lengthNumeric.Value,
                            (int)crewNumeric.Value,
                            (int)sailsNumeric.Value,
                            (int)enginePowerNumeric.Value,
                            luxuryLevelTextBox.Text
                        );
                        CopyProperties(newShip);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }

        private void EnginePowerNumeric_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ship is Steamship steamship)
                {
                    var testShip = new Steamship(
                        nameTextBox.Text,
                        (double)lengthNumeric.Value,
                        (int)crewNumeric.Value,
                        (int)enginePowerNumeric.Value
                    );
                }
                else if (ship is Yacht yacht)
                {
                    var testShip = new Yacht(
                        nameTextBox.Text,
                        (double)lengthNumeric.Value,
                        (int)crewNumeric.Value,
                        (int)sailsNumeric.Value,
                        (int)enginePowerNumeric.Value,
                        luxuryLevelTextBox.Text
                    );
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CopyProperties(Ship newShip)
        {
            var properties = ship.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    var value = property.GetValue(newShip);
                    property.SetValue(ship, value);
                }
            }
        }
    }
}
