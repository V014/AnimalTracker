using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SQLite;
using System.Data;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnimalTracker
{
    public partial class Home : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public Home()
        {
            InitializeComponent();
            // collects current date and time
            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            // check current theme
            try
            {
                string queryTheme = "SELECT Theme FROM Settings";
                string Theme = Connection.ReadString(queryTheme);
                if (Theme == "Dark")
                {
                    dark_btn.Checked = true;
                    materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.DARK;
                    // style tabs
                    styleTabs(animalTabs);
                    styleTabs(mealTabs);
                    styleTabs(physiqueTabs);

                    // style datagrids
                    // animal page
                    styleDarkDataGridView(animalDataGrid);
                    styleDarkDataGridView(lionDataGrid);
                    styleDarkDataGridView(monkeyDataGrid);
                    styleDarkDataGridView(rabbitDataGrid);
                    // meal page
                    styleDarkDataGridView(mealDataGrid);
                    styleDarkDataGridView(feedingDataGrid);
                    // exercise page
                    styleDarkDataGridView(exerciseDataGrid);
                    // activity page
                    styleDarkDataGridView(activityDataGrid);
                    // physique page
                    styleDarkDataGridView(weightDataGrid);
                    styleDarkDataGridView(waistDataGrid);
                }
                else
                {
                    light_btn.Checked = true;
                    materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    // style datagrids
                    // animal page
                    styleDataGridView(animalDataGrid);
                    styleDataGridView(lionDataGrid);
                    styleDataGridView(monkeyDataGrid);
                    styleDataGridView(rabbitDataGrid);
                    // meal page
                    styleDataGridView(mealDataGrid);
                    styleDataGridView(feedingDataGrid);
                    // exercise page
                    styleDataGridView(exerciseDataGrid);
                    // activity page
                    styleDataGridView(activityDataGrid);
                    // physique page
                    styleDataGridView(weightDataGrid);
                    styleDataGridView(waistDataGrid);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to detect theme!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // change color scheme
        private int colorSchemeIndex;
        private void scheme_btn_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 2) colorSchemeIndex = 0;

            //These are just example color schemes
            switch (colorSchemeIndex)
            {
                case 0:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;
                case 1:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
                    break;
                case 2:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                    break;
            }
        }

        // styling the data grid views
        void styleDataGridView(DataGridView dataGrid)
        {
            dataGrid.BorderStyle = BorderStyle.None;
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGrid.AlternatingRowsDefaultCellStyle.Font = new Font("Roboto", 11);
            dataGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 71, 79);
            dataGrid.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGrid.DefaultCellStyle.Font = new Font("Roboto", 11);
            dataGrid.BackgroundColor = Color.White;
            dataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGrid.EnableHeadersVisualStyles = false;
            dataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Roboto", 11);
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        }

        // styling the tab controls
        void styleTabs(TabControl page)
        {
            page.BackColor = Color.FromArgb(51, 51, 51);
            
        }

        void styleDarkDataGridView(DataGridView dataGrid)
        {
            dataGrid.BackgroundColor = Color.FromArgb(51, 51, 51);
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 51);
            dataGrid.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            dataGrid.RowsDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 51);
            dataGrid.RowsDefaultCellStyle.ForeColor = Color.White;
        }

        // close application and all hidden forms
        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /* ======== Pulls data from the database and fills the datagrids ======== */
        // Returns all data from requested tables
        private void LoadData(string query, DataGridView dataGrid)
        {
            var con = Connection.GetConnection();
            var DB = new SQLiteDataAdapter(query, con);
            var DS = new DataSet();
            var DT = new DataTable();
            DB.Fill(DS);
            DT = DS.Tables[0];
            dataGrid.DataSource = DT;
            con.Close();
        }


        /* ======== this section loads the data from the pull function on runtime ========= */
        // load animal table when program starts
        private void Home_Load(object sender, EventArgs e)
        {
            // Load lions
            string queryLion = "SELECT * FROM Lion";
            LoadData(queryLion, lionDataGrid);

            // Load monkeys
            string queryMonkey = "SELECT * FROM Monkey";
            LoadData(queryMonkey, monkeyDataGrid);

            // Load rabbits
            string queryRabbit = "SELECT * FROM Rabbit";
            LoadData(queryRabbit, rabbitDataGrid);

            // Load all animals
            string queryAnimals = "SELECT * FROM Animal";
            LoadData(queryAnimals, animalDataGrid);

            //animalDataGrid.Columns[0].Visible = false; // hide ID column during runtime
        }
        private void lionDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = lionDataGrid.Rows[e.RowIndex];
                name_txt.Text = row.Cells[1].Value.ToString();
                species_txt.Text = row.Cells[2].Value.ToString();
                age_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                if (row.Cells[4].Value.ToString() == "Male")
                {
                    male_radio_btn.Checked = true;
                }
                else
                {
                    female_radio_btn.Checked = true;
                }

            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                name_txt.Text = "";
                name_txt.Focus();
                species_txt.Text = "";
                age_txt.Value = 0;
                male_radio_btn.Checked = true;
                age_txt.Value = 0;
            }
        }

        private void monkeyDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = monkeyDataGrid.Rows[e.RowIndex];
                name_txt.Text = row.Cells[1].Value.ToString();
                species_txt.Text = row.Cells[2].Value.ToString();
                age_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                if (row.Cells[4].Value.ToString() == "Male")
                {
                    male_radio_btn.Checked = true;
                }
                else
                {
                    female_radio_btn.Checked = true;
                }

            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                name_txt.Text = "";
                name_txt.Focus();
                species_txt.Text = "";
                age_txt.Value = 0;
                male_radio_btn.Checked = true;
                age_txt.Value = 0;
            }
        }

        private void rabbitDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = rabbitDataGrid.Rows[e.RowIndex];
                name_txt.Text = row.Cells[1].Value.ToString();
                species_txt.Text = row.Cells[2].Value.ToString();
                age_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                if (row.Cells[4].Value.ToString() == "Male")
                {
                    male_radio_btn.Checked = true;
                }
                else
                {
                    female_radio_btn.Checked = true;
                }
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                name_txt.Text = "";
                name_txt.Focus();
                species_txt.Text = "";
                age_txt.Value = 0;
                male_radio_btn.Checked = true;
                age_txt.Value = 0;
            }
        }

        private void mealDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = mealDataGrid.Rows[e.RowIndex];
                calories_lbl.Text = row.Cells[2].Value.ToString();

                var calories = Convert.ToInt32(row.Cells[2].Value.ToString());
                string queryWeight = "SELECT Calories FROM Meal WHERE Id ='" + mealDataGrid.Rows[e.RowIndex] + "'";
                var weight = Convert.ToInt32(Connection.ReadString(queryWeight).ToString());
                var weightDifference = 


                meal_txt.Text = row.Cells[1].Value.ToString();
                portion_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());

            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                meal_txt.Text = " ";
                meal_txt.Focus();
                calories_lbl.Text = "Waiting on you...";
                portion_txt.Value = 0;
            }
        }

        private void feedingDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = feedingDataGrid.Rows[e.RowIndex];
                AnimalId_txt.Value = Convert.ToInt32(row.Cells[1].Value.ToString());

            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                meal_txt.Text = " ";
                meal_txt.Focus();
                calories_lbl.Text = "Waiting on you...";
                portion_txt.Value = 0;
            }
        }

        private void weightDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = weightDataGrid.Rows[e.RowIndex];
                // calculate weight difference
                var weightAverage = Convert.ToInt32(row.Cells[4].Value.ToString());
                var initialWeight = Convert.ToInt32(row.Cells[2].Value.ToString());
                var difference = weightAverage - initialWeight;
                // display readings
                physique_AnimalId_txt.Value = Convert.ToInt32(row.Cells[1].Value.ToString());
                morning_txt.Value = Convert.ToInt32(row.Cells[2].Value.ToString());
                evening_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                average_txt.Text = row.Cells[4].Value.ToString() + "KG";
                weight_difference_txt.Text = difference.ToString() + "KG";
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show(weight_difference_txt.Text);
                // refresh fields
                physique_AnimalId_txt.Value = 0;
                morning_txt.Value = 0;
                evening_txt.Value = 0;
                average_txt.Text = "Calculating...";
            }
        }

        private void waistDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = waistDataGrid.Rows[e.RowIndex];
                physique_AnimalId_txt.Value = Convert.ToInt32(row.Cells[1].Value.ToString());
                morning_txt.Value = Convert.ToInt32(row.Cells[2].Value.ToString());
                evening_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                average_txt.Text = row.Cells[4].Value.ToString();
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                physique_AnimalId_txt.Value = 0;
                morning_txt.Value = 0;
                evening_txt.Value = 0;
                average_txt.Text = "Calculating...";
            }
        }

        private void activityDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = activityDataGrid.Rows[e.RowIndex];
                activity_AnimalId_txt.Value = Convert.ToInt32(row.Cells[1].Value.ToString());
                if (row.Cells[2].Value.ToString() == "Inactive")
                {
                    inactive_radio_btn.Checked = true;
                }
                else if (row.Cells[2].Value.ToString() == "Moderately Active")
                {
                    moderate_radio_btn.Checked = true;
                }
                else
                {
                    active_radio_btn.Checked = true;
                }
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                activity_AnimalId_txt.Value = 0;
            }
        }

        private void exerciseDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = exerciseDataGrid.Rows[e.RowIndex];
                exercise_txt.Text = row.Cells[1].Value.ToString();
                duration_txt.Value = Convert.ToInt32(row.Cells[2].Value.ToString());
                calories_burnt_txt.Text = row.Cells[3].Value.ToString();
                exercise_AnimalId_txt.Value = Convert.ToInt32(row.Cells[4].Value.ToString());
                // collect the amount of calories consumed that day.
                string queryCalories = "SELECT SUM(calories) From Feeding AS f INNER JOIN Meal AS m ON f.MealId = m.Id WHERE f.AnimalId = '"+ exercise_AnimalId_txt.Value + "' AND f.date LIKE '2021-11-08%'";
                string calories = Connection.ReadString(queryCalories);
                calories_eaten_txt.Text = calories;
                // calculate the difference
                int calorieBurnt = Convert.ToInt32(row.Cells[3].Value.ToString());
                int caloricDiff = Convert.ToInt32(calories) - calorieBurnt;
                average_gain_txt.Text = caloricDiff.ToString();
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                exercise_AnimalId_txt.Value = 0;
                duration_txt.Value = 0;
                calories_burnt_txt.Text = "Waiting...";
            }
        }

        // =========================== Radio Buttons ================== //
        // Create a gender variable
        string gender = "Male";

        // react to user input when they select the female radio button
        private void female_radio_btn_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
        }

        // Create an activity variable
        string activity = "Inactive";

        // react to user input when they select the other avtivity buttons
        private void moderate_radio_btn_CheckedChanged(object sender, EventArgs e)
        {
            activity = "Moderately Active"; 
        }

        private void active_radio_btn_CheckedChanged(object sender, EventArgs e)
        {
            activity = "Active";
        }

        /* ========= Pull data from the db depending on which tab is selected ========= */
        private void materialTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this displays the data in the feeding table when feeding tab selected
            if (materialTabControl.SelectedTab == meal_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Feeding";
                LoadData(query, feedingDataGrid);
           
                string queryMeal = "SELECT * FROM Meal";
                LoadData(queryMeal, mealDataGrid);
                mealDataGrid.Columns[4].Visible = false;
                //mealDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }

            // this displays the data in the exercise table after exercise tab selected
            if (materialTabControl.SelectedTab == exercise_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Exercise";
                LoadData(query, exerciseDataGrid);
                //exerciseDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }

            // this displays the data in the activity table after activity tab selected
            if (materialTabControl.SelectedTab == activity_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Activity";
                LoadData(query, activityDataGrid);
                //actvityDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }

            // this displays the data in the weight table after weight tab selected
            if (materialTabControl.SelectedTab == physique_page)
            {
                // Returns all data from the meals table
                string queryWeight = "SELECT * FROM Weight";
                LoadData(queryWeight, weightDataGrid);

                // Returns all data from the meals table
                string queryWaist = "SELECT * FROM Waist";
                LoadData(queryWaist, waistDataGrid);
                //weightDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }
        }

        /* ========== this section queries the database after user interaction ======== */

        void resetAnimalFields()
        {
            name_txt.Text = "";
            name_txt.Focus();
            gender = "Male";
            male_radio_btn.Checked = true;
            age_txt.Value = 0;
            species_txt.Text = "";
        }

        void resetMealFields()
        {
            // refresh fields
            meal_txt.Text = "";
            meal_txt.Focus();
            calories_lbl.Text = "Waiting on you";
            portion_txt.Value = 0;
            AnimalId_txt.Value = 0;
        }

        void resetExerciseFields()
        {
            exercise_txt.Text = "";
            exercise_txt.Focus();
            calories_burnt_txt.Text = "";
            duration_txt.Value = 0;
            exercise_AnimalId_txt.Value = 0;
        }
            
        // adds animal to database
        private void add_ani_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // if the lion tab is in view, insert into the Lion database
                if (animalTabs.SelectedTab == lionTab)
                {
                    string queryLion = "INSERT INTO Lion (Name, Gender, Age, Species, Registered) VALUES ('" + name_txt.Text + "','" + gender + "','" + age_txt.Text + "','" + species_txt.
                    Text + "','" + DateTime.Now.ToString("s") + "')";
                    AnimalControls.Querydb(queryLion);

                    // pull the animal's Id to reference to in the animal table
                    string queryAnimallId = "SELECT Id FROM Lion WHERE Name = '" + name_txt.Text + "'";
                    string AnimalId = Connection.ReadString(queryAnimallId);
                    string queryAnimal = "INSERT INTO Animal (AnimalId, Registered) VALUES ('" + AnimalId.ToString() + "', '" + DateTime.Now.ToString("s") + "')";
                    AnimalControls.Querydb(queryAnimal);

                    // refresh the data grids
                    string queryLions = "SELECT * FROM Lion";
                    LoadData(queryLions, lionDataGrid);

                    string queryAnimals = "SELECT * FROM Animal";
                    LoadData(queryAnimals, animalDataGrid);
                }

                // if the Monkey tab is in view, insert into the Monkey database
                if (animalTabs.SelectedTab == monkeyTab)
                {
                    string queryLion = "INSERT INTO Monkey (Name, Gender, Age, Species, Registered) VALUES ('" + name_txt.Text + "','" + gender + "','" + age_txt.Text + "','" + species_txt.
                    Text + "','" + DateTime.Now.ToString("s") + "')";
                    AnimalControls.Querydb(queryLion);

                    // pull the animal's Id to reference to in the animal table
                    string queryAnimallId = "SELECT Id FROM Monkey WHERE Name = '" + name_txt.Text + "'";
                    string AnimalId = Connection.ReadString(queryAnimallId);
                    string queryAnimal = "INSERT INTO Animal (AnimalId, Registered) VALUES ('" + AnimalId.ToString() + "', '" + DateTime.Now.ToString("s") + "')";
                    AnimalControls.Querydb(queryAnimal);

                    string queryMonkeys = "SELECT * FROM Monkey";
                    LoadData(queryMonkeys, monkeyDataGrid);

                    string queryAnimals = "SELECT * FROM Animal";
                    LoadData(queryAnimals, animalDataGrid);
                }

                // if the Rabbit tab is in view, insert into the Rabbit database
                if (animalTabs.SelectedTab == rabbitTab)
                {
                    string queryRabbit = "INSERT INTO Rabbit (Name, Gender, Age, Species, Registered) VALUES ('" + name_txt.Text + "','" + gender + "','" + age_txt.Text + "','" + species_txt.
                    Text + "','" + DateTime.Now.ToString("s") + "')";
                    AnimalControls.Querydb(queryRabbit);

                    // pull the animal's Id to reference to in the animal table
                    string queryAnimallId = "SELECT Id FROM Rabbit WHERE Name = '" + name_txt.Text + "'";
                    string AnimalId = Connection.ReadString(queryAnimallId);
                    string queryAnimal = "INSERT INTO Animal (AnimalId, Registered) VALUES ('" + AnimalId.ToString() + "', '" + DateTime.Now.ToString("s") + "')";
                    AnimalControls.Querydb(queryAnimal);

                    // refresh the data grids
                    string queryRabbits = "SELECT * FROM Rabbit";
                    LoadData(queryRabbits, rabbitDataGrid);

                    string queryAnimals = "SELECT * FROM Animal";
                    LoadData(queryAnimals, animalDataGrid);
                }

                // refresh fields
                resetAnimalFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // updates animal list
        private void update_ani_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // if the lion tab is in view, insert into the Lion database
                if (animalTabs.SelectedTab == lionTab)
                {
                    int id = Convert.ToInt32(lionDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string queryLion = "UPDATE Lion SET Name = '" + @name_txt.Text + "', Age = '" + @age_txt.Value + "', Gender = '" + @gender + "', Species = '" + @species_txt.Text + "' WHERE Id ='" + id + "'";
                    AnimalControls.Querydb(queryLion);
                    string queryList = "SELECT * FROM Lion";
                    LoadData(queryList, lionDataGrid);
                }

                // if the Monkey tab is in view, insert into the Monkey database
                if (animalTabs.SelectedTab == monkeyTab)
                {
                    int id = Convert.ToInt32(monkeyDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string queryMonkey = "UPDATE Monkey SET Name = '" + @name_txt.Text + "', Age = '" + @age_txt.Value + "', Gender = '" + @gender + "', Species = '" + @species_txt.Text + "' WHERE Id ='" + id + "'";
                    AnimalControls.Querydb(queryMonkey);
                    string queryList = "SELECT * FROM Monkey";
                    LoadData(queryList, monkeyDataGrid);
                }

                // if the Rabbit tab is in view, insert into the Rabbit database
                if (animalTabs.SelectedTab == rabbitTab)
                {
                    int id = Convert.ToInt32(rabbitDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string queryRabbit = "UPDATE Rabbit SET Name = '" + @name_txt.Text + "', Age = '" + @age_txt.Value + "', Gender = '" + @gender + "', Species = '" + @species_txt.Text + "' WHERE Id ='" + id + "'";
                    AnimalControls.Querydb(queryRabbit);
                    string queryList = "SELECT * FROM Rabbit";
                    LoadData(queryList, rabbitDataGrid);
                }

                // refresh fields
                resetAnimalFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // deletes animal from database
        private void delete_ani_btn_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DialogResult dialogResult = MessageBox.Show("Delete Animal?", "Are you sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        // if the lion tab is in view, delete the Lion
                        if (animalTabs.SelectedTab == lionTab)
                        {
                            int lionId = Convert.ToInt32(lionDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                            string queryLion = "DELETE FROM Lion WHERE ID = '" + lionId + "' ";
                            AnimalControls.Querydb(queryLion);
                            string query = "SELECT * FROM Lion";
                            LoadData(query, lionDataGrid);
                        }

                        // if the Monkey tab is in view, delete the Monkey
                        if (animalTabs.SelectedTab == monkeyTab)
                        {
                            int monkeyId = Convert.ToInt32(monkeyDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                            string queryMonkey = "DELETE FROM Monkey WHERE ID = '" + monkeyId + "' ";
                            AnimalControls.Querydb(queryMonkey);
                            string query = "SELECT * FROM Monkey";
                            LoadData(query, monkeyDataGrid);
                        }

                        // if the Rabbit tab is in view, delete the Rabbit
                        if (animalTabs.SelectedTab == rabbitTab)
                        {
                            int rabbitId = Convert.ToInt32(rabbitDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                            string queryRabbit = "DELETE FROM Rabbit WHERE ID = '" + rabbitId + "' ";
                            AnimalControls.Querydb(queryRabbit);
                            string query = "SELECT * FROM Rabbit";
                            LoadData(query, rabbitDataGrid);
                        }
                         // refresh fields
                        resetAnimalFields();
                    } // edd of dialogue
                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Meal tab */
        // records meals to the database
        private void rec_meal_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Try to select the meal from the meals table where the name of the meal is
                // the meal text from the text box, if the query succeeds then the meal is
                // already in the system. Retrieve columns and use in later queries. Else the
                // meal is not in the system. Insert meal into meal table and use those values
                // instead
                // check to see if meal has alredy been recorded before
                
                if (mealTabs.SelectedTab == feeding_page)
                {
                    // pull the meal name to compare it to the user input
                    string queryMealName = "SELECT Name FROM Meal WHERE Name = '" + meal_txt.Text + "'";
                    string MealName = Connection.ReadString(queryMealName);

                    // pull the meal Id to reference to the meal
                    string queryMealId = "SELECT Id FROM Meal WHERE Name = '" + meal_txt.Text + "'";
                    string mealId = Connection.ReadString(queryMealId);

                    // if the meal's name already exists then add the feeding only
                    if (MealName.ToString() == meal_txt.Text)
                    {
                        string feedingQuery = "INSERT INTO Feeding (AnimalId, MealId, Date) VALUES ('" + AnimalId_txt.Value + "', '" + mealId.ToString() + "','" + DateTime.Now.ToString("s") + "')";
                        AnimalControls.Querydb(feedingQuery);

                        string loadMeals = "SELECT * FROM Meal";
                        LoadData(loadMeals, mealDataGrid);

                        string loadFeedings = "SELECT * FROM Feeding";
                        LoadData(loadFeedings, feedingDataGrid);

                        // refresh fields
                        resetMealFields();

                    }
                    else
                    {
                        // calculate calories per gram, 4 being the multiple digit to calculate protein
                        var calories = portion_txt.Value * 4;
                        // if the meal didn't exist, add the feeding and the meal details to the database
                        // we build our query in the form page which has references to the its controls.
                        string mealQuery = "INSERT INTO Meal (Name, Calories, Portion, Date) VALUES ('" + meal_txt.Text + "','" + calories + "','" + portion_txt.Value + "','" + DateTime.Now.ToString("s") + "')";
                        AnimalControls.Querydb(mealQuery);

                        // pull the meal Id to reference to the meal
                        string queryNewMealId = "SELECT Id FROM Meal WHERE Name = '" + meal_txt.Text + "'";
                        string newMealId = Connection.ReadString(queryNewMealId);

                        string feedingQuery = "INSERT INTO Feeding (AnimalId, MealId, Date) VALUES ('" + AnimalId_txt.Value + "', '" + newMealId.ToString() + "','" + DateTime.Now.ToString("s") + "')";
                        AnimalControls.Querydb(feedingQuery);

                        string loadMeals = "SELECT * FROM Meal";
                        LoadData(loadMeals, mealDataGrid);

                        string loadFeedings = "SELECT * FROM Feeding";
                        LoadData(loadFeedings, feedingDataGrid);

                        MessageBox.Show("Recorded new meal!");

                        // refresh fields
                        resetMealFields();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record feeding!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_meal_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (mealTabs.SelectedTab == meal_page)
                {
                    // calculate calories per gram, 4 being the multiple digit to calculate protein
                    var calories = portion_txt.Value * 4;
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(mealDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Meal SET Name = '" + @meal_txt.Text + "', Calories = '" + @calories + "', Portion = '" + @portion_txt.Value + "' WHERE Id ='" + id + "'";

                    // we push the query to the AnimalControl class to process the query which links back to the connection class
                    AnimalControls.Querydb(txtQuery);
                    MessageBox.Show("Meal record updated!");
                    string query = "SELECT * FROM Meal";
                    LoadData(query, mealDataGrid);
                    resetMealFields();
                }

                if (mealTabs.SelectedTab == feeding_page)
                {
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(feedingDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Feeding SET AnimalId = '" + AnimalId_txt.Value + "' WHERE Id ='" + id + "'";

                    // we push the query to the AnimalControl class to process the query which links back to the connection class
                    AnimalControls.Querydb(txtQuery);
                    MessageBox.Show("Feeding record updated!");
                    string query = "SELECT * FROM Feeding";
                    LoadData(query, feedingDataGrid);
                    resetMealFields();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update meal record!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delete_meal_btn_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DialogResult dialogResult = MessageBox.Show("Delete record?", "Are you sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (mealTabs.SelectedTab == meal_page)
                        {
                            // we build our query in the form page which has references to the its controls.
                            int id = Convert.ToInt32(mealDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                            string txtQuery = "DELETE FROM Meal WHERE ID = '" + id + "' ";

                            // we push the query to the AnimalControl class to process the query which links back to the connection class
                            AnimalControls.Querydb(txtQuery);
                            string query = "SELECT * FROM Meal";
                            LoadData(query, mealDataGrid);
                            MessageBox.Show("Meal deleted!");
                        }

                        if (mealTabs.SelectedTab == feeding_page)
                        {
                            int id = Convert.ToInt32(feedingDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                            string txtQuery = "DELETE FROM Feeding WHERE ID = '" + id + "' ";

                            // we push the query to the AnimalControl class to process the query which links back to the connection class
                            AnimalControls.Querydb(txtQuery);
                            string query = "SELECT * FROM Feeding";
                            LoadData(query, feedingDataGrid);
                            resetMealFields();
                        }
                    }
                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Exercise tab */
        private void rec_exe_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // pull the animal's weight average
                string queryWeightAverage = "SELECT WeightAverage FROM Weight WHERE AnimalId = '" + exercise_AnimalId_txt.Value + "'";
                string Average = Connection.ReadString(queryWeightAverage);
                int Weight = Convert.ToInt32(Average);

                // if the weight is beyond zero as a check
                if (Weight > 0)
                {
                    var caloriesBurned = 70 + Convert.ToInt32(duration_txt.Value) * (Weight * 0.75);

                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Exercise (Name, Duration, CaloriesBurned, AnimalId, Date) VALUES ('" + exercise_txt.Text + "','" + duration_txt.Value + "','" + caloriesBurned + "','" + exercise_AnimalId_txt.
                    Value + "','" + DateTime.Now.ToString("s") + "')";
                    
                    // we push the query to the AnimalControl class to process the query which links back to the connection class
                    AnimalControls.Querydb(txtQuery);
                    
                    string query = "SELECT * FROM Exercise";
                    LoadData(query, exerciseDataGrid);
                }
                else
                {
                    MessageBox.Show("The animal did not appear in the database!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // refresh fields
                resetExerciseFields();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record exercise!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_exe_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // pull the animal's weight average
                string queryWeightAverage = "SELECT WeightAverage FROM Weight WHERE AnimalId = '" + exercise_AnimalId_txt.Value + "'";
                string Average = Connection.ReadString(queryWeightAverage);
                int Weight = Convert.ToInt32(Average);

                // if the meal's name already exists then add the feeding only
                if (Weight  > 0)
                {
                    var caloriesBurned = 70 + Convert.ToUInt32(duration_txt.Value) * (Weight * 0.75);
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(exerciseDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Exercise SET Name = '" + @exercise_txt.Text + "', Duration = '" + @duration_txt.Value + "', CaloriesBurned = '" + @caloriesBurned + "', AnimalId = '" + @exercise_AnimalId_txt.Value + "' WHERE Id ='" + id + "'";

                    // we push the query to the AnimalControl class to process the query which links back to the connection class
                    AnimalControls.Querydb(txtQuery);
                    MessageBox.Show("Exercise record updated!");
                    string query = "SELECT * FROM Exercise";
                    LoadData(query, exerciseDataGrid);
                }
                else
                {
                    MessageBox.Show("The animal did not appear in the database!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // refresh fields
                resetExerciseFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update exercise record!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void del_exercise_btn_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    // we build our query in the form page which has references to the its controls.
                    int id = Convert.ToInt32(exerciseDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "DELETE FROM Exercise WHERE ID = '" + id + "' ";

                    DialogResult dialogResult = MessageBox.Show("Delete Animal '" + @exercise_AnimalId_txt.Value + "' exercise reading?", "Are you sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // we push the query to the AnimalControl class to process the query which links back to the connection class
                        AnimalControls.Querydb(txtQuery);
                        string query = "SELECT * FROM Exercise";
                        LoadData(query, exerciseDataGrid);
                    }
                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* physique tab */
        private void rec_physique_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = (morning_txt.Value + evening_txt.Value) / 2;

                if (physiqueTabs.SelectedTab == weight_page)
                {
                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Weight (AnimalId, WeightMorning, WeightEvening, WeightAverage,  Date) VALUES ('" + physique_AnimalId_txt.Value + "','" + morning_txt.Value + "','" + evening_txt.Value + "','" + average + "','" + DateTime.Now.ToString("s") + "')";
                    AnimalControls.Querydb(txtQuery);

                    string query = "SELECT * FROM Weight";
                    LoadData(query, weightDataGrid);
                }

                if (physiqueTabs.SelectedTab == waist_page)
                {
                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Waist (AnimalId, WaistMorning, WaistEvening, WaistAverage,  Date) VALUES ('" + physique_AnimalId_txt.Value + "','" + morning_txt.Value + "','" + evening_txt.Value + "','" + average + "','" + DateTime.Now.ToString("s") + "')";
                    AnimalControls.Querydb(txtQuery);

                    string query = "SELECT * FROM Waist";
                    LoadData(query, waistDataGrid);
                }
                
                // refresh fields
                physique_AnimalId_txt.Value = 0;
                morning_txt.Value = 0;
                evening_txt.Value = 0;
                average_txt.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_physique_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = (morning_txt.Value + evening_txt.Value) / 2;

                if (physiqueTabs.SelectedTab == weight_page)
                {
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(weightDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Weight SET AnimalId = '" + physique_AnimalId_txt.Value + "', WeightMorning = '" + morning_txt.Value + "', WeightEvening = '" + evening_txt.Value + "', WeightAverage = '" + average + "' WHERE Id ='" + id + "'";
                    AnimalControls.Querydb(txtQuery);
                    string query = "SELECT * FROM Weight";
                    LoadData(query, weightDataGrid);
                }

                if (physiqueTabs.SelectedTab == waist_page)
                {
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(waistDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Waist SET AnimalId = '" + @physique_AnimalId_txt.Value + "', WaistMorning = '" + @morning_txt.Value + "', WaistEvening = '" + @evening_txt.Value + "', WaistAverage = '" + average + "' WHERE Id ='" + id + "'";
                    AnimalControls.Querydb(txtQuery);
                    string query = "SELECT * FROM Waist";
                    LoadData(query, waistDataGrid);
                }
                

                // refresh fields
                physique_AnimalId_txt.Value = 0;
                morning_txt.Value = 0;
                evening_txt.Value = 0;
                average_txt.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update weight record!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void del_physique_btn_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (physiqueTabs.SelectedTab == weight_page)
                    {
                        // we build our query in the form page which has references to the its controls.
                        int id = Convert.ToInt32(weightDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                        string txtQuery = "DELETE FROM Weight WHERE ID = '" + id + "' ";

                        DialogResult dialogResult = MessageBox.Show("Delete Animal '" + physique_AnimalId_txt.Value + "' weight reading?", "Are you sure?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // we push the query to the AnimalControl class to process the query which links back to the connection class
                            AnimalControls.Querydb(txtQuery);
                            string query = "SELECT * FROM Weight";
                            LoadData(query, weightDataGrid);
                        }
                    }

                    if (physiqueTabs.SelectedTab == waist_page)
                    {
                        // we build our query in the form page which has references to the its controls.
                        int id = Convert.ToInt32(waistDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                        string txtQuery = "DELETE FROM Waist WHERE ID = '" + id + "' ";

                        DialogResult dialogResult = MessageBox.Show("Delete Animal '" + @physique_AnimalId_txt.Value + "' waist reading?", "Are you sure?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // we push the query to the AnimalControl class to process the query which links back to the connection class
                            AnimalControls.Querydb(txtQuery);
                            string query = "SELECT * FROM Waist";
                            LoadData(query, waistDataGrid);
                            // MessageBox.Show("Weight Reading deleted!");
                        }
                    }

                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Activity tab */
        private void rec_activity_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Activity (AnimalId, ActivityLevel, Date) VALUES ('" + activity_AnimalId_txt.Value + "','" + activity + "','" + DateTime.Now.ToString("s") + "')";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                //MessageBox.Show("Activity Registered!");
                string query = "SELECT * FROM Activity";
                LoadData(query, activityDataGrid);

                // refresh fields
                activity_AnimalId_txt.Text = "";
                inactive_radio_btn.Checked = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to insert!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_activity_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to its controls
                int id = Convert.ToInt32(activityDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                string txtQuery = "UPDATE Activity SET AnimalId = '" + @activity_AnimalId_txt.Value + "', ActivityLevel = '" + @activity + "' WHERE Id ='" + id + "'";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                //MessageBox.Show("Details updated!");
                string query = "SELECT * FROM Activity";
                LoadData(query, activityDataGrid);

                // refresh fields
                activity_AnimalId_txt.Text = "";
                inactive_radio_btn.Checked = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void del_activity_btn_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    // we build our query in the form page which has references to the its controls.
                    int id = Convert.ToInt32(activityDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "DELETE FROM Activity WHERE ID = '" + id + "' ";

                    DialogResult dialogResult = MessageBox.Show("Delete Animal '" + @activity_AnimalId_txt.Value + "' activity Level?", "Are you sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // we push the query to the AnimalControl class to process the query which links back to the connection class
                        AnimalControls.Querydb(txtQuery);
                        string query = "SELECT * FROM Activity";
                        LoadData(query, activityDataGrid);
                    }

                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadGraph_btn_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand cmd = new SQLiteCommand("SELECT CAST(WeightAverage AS INT), date FROM Weight WHERE AnimalId = '" + chart_AnimalId_num.Value + "'", con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            try
            {
                chart.Series[0].Points.Clear(); // clear previous trend
                while (reader.Read())
                {
                    chart.Series[0].Points.Add(reader.GetInt32(0));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Select AnimalId or record weight fist!");
                con.Close();
            }
        }

        private void chart_AnimalId_num_ValueChanged(object sender, EventArgs e)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand cmd = new SQLiteCommand("SELECT CAST(WeightAverage AS INT), date FROM Weight WHERE AnimalId = '" + chart_AnimalId_num.Value + "'", con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            try
            {
                chart.Series[0].Points.Clear(); // clear previous trend
                while (reader.Read())
                {
                    chart.Series[0].Points.Add(reader.GetInt32(0));
                }
                if(chart_AnimalId_num.Value > 0)
                {
                    loadingAnimation.Hide();
                    chart_lbl.Hide();
                }
                else
                {
                    loadingAnimation.Show();
                    chart_lbl.Show();
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Select AnimalId or record weight fist!");
                con.Close();
            }
        }

        private void light_btn_CheckedChanged(object sender, EventArgs e)
        {
            string queryTheme = "UPDATE Settings SET Theme = 'Light' WHERE Id = 1";
            Connection.ExecuteQuery(queryTheme);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            // style datagrids
            // animal page
            styleDataGridView(animalDataGrid);
            styleDataGridView(lionDataGrid);
            styleDataGridView(monkeyDataGrid);
            styleDataGridView(rabbitDataGrid);
            // meal page
            styleDataGridView(mealDataGrid);
            styleDataGridView(feedingDataGrid);
            // exercise page
            styleDataGridView(exerciseDataGrid);
            // activity page
            styleDataGridView(activityDataGrid);
            // physique page
            styleDataGridView(weightDataGrid);
            styleDataGridView(waistDataGrid);
        }

        private void dark_btn_CheckedChanged(object sender, EventArgs e)
        {
            string queryTheme = "UPDATE Settings SET Theme = 'Dark' WHERE Id = 1";
            Connection.ExecuteQuery(queryTheme);
            materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.DARK;
            // style tabs
            styleTabs(animalTabs);
            styleTabs(mealTabs);
            styleTabs(physiqueTabs);

            // style datagrids
            // animal page
            styleDarkDataGridView(animalDataGrid);
            styleDarkDataGridView(lionDataGrid);
            styleDarkDataGridView(monkeyDataGrid);
            styleDarkDataGridView(rabbitDataGrid);
            // meal page
            styleDarkDataGridView(mealDataGrid);
            styleDarkDataGridView(feedingDataGrid);
            // exercise page
            styleDarkDataGridView(exerciseDataGrid);
            // activity page
            styleDarkDataGridView(activityDataGrid);
            // physique page
            styleDarkDataGridView(weightDataGrid);
            styleDarkDataGridView(waistDataGrid);
        }
    }
}
