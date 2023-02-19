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
    public partial class Home : Form
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Connection con = new Connection();
        private string activity = "Inactive";
        private string gender = "Male";
        private string Theme = "";
        private string date = DateTime.Now.ToString("f");

        public Home()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            // check current theme
            try
            {
                string Theme = checkTheme();
                if (Theme == "Dark")
                {
                    DarkTheme();
                }
                else
                {
                    LightTheme();
                }
            }
            catch (Exception)
            {
                // do nothing
            }
        }
        // method that takes place when the app first loads
        private void Home_Load(object sender, EventArgs e)
        {
            // Load all animals
            con.LoadData("SELECT * FROM Animal", data_animal);
            data_animal.Columns[0].Visible = false;
        }
        // check the previous theme
        public string checkTheme()
        {
            string queryTheme = "SELECT Theme FROM Settings";
            string Theme = con.ReadString(queryTheme);
            return Theme;
        }
        // handing light theme
        void LightTheme()
        {
            // Initialize MaterialSkinManager
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            // style datagrids
            // animal page
            styleDataGridView(data_animal);
            // meal page
            styleDataGridView(data_meal);
            styleDataGridView(data_feeding);
            // exercise page
            styleDataGridView(exerciseDataGrid);
            // activity page
            styleDataGridView(activityDataGrid);
            // physique page
            styleDataGridView(data_weight);
            styleDataGridView(data_waist);
            // style chart
            //chart.BackColor = Color.White;
        }
        // handling dark theme
        void DarkTheme()
        {
            // Initialize MaterialSkinManager
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.DARK;
            // style tabs
            //styleTabs(animalTabs);
            styleTabs(mealTabs);
            styleTabs(physiqueTabs);
            // style datagrids
            // animal page
            styleDarkDataGridView(data_animal);
            // meal page
            styleDarkDataGridView(data_meal);
            styleDarkDataGridView(data_feeding);
            // exercise page
            styleDarkDataGridView(exerciseDataGrid);
            // activity page
            styleDarkDataGridView(activityDataGrid);
            // physique page
            styleDarkDataGridView(data_weight);
            styleDarkDataGridView(data_waist);

            // style chart
            //chart.BackColor = Color.FromArgb(51, 51, 51);
        }
        // styling the tab controls
        void styleTabs(TabControl page)
        {
            page.BackColor = Color.FromArgb(51, 51, 51);
        }
        // styling the data grid views
        void styleDataGridView(DataGridView dataGrid)
        {
            dataGrid.BorderStyle = BorderStyle.None;
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrid.EnableHeadersVisualStyles = false;
            dataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGrid.BackgroundColor = Color.FromArgb(238, 239, 249);
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

            dataGrid.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(37,37,39);

            dataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 71, 79);
            dataGrid.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(37, 37, 39);

            dataGrid.RowsDefaultCellStyle.BackColor = Color.White;
            dataGrid.RowsDefaultCellStyle.ForeColor = Color.FromArgb(37, 37, 39);

            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segeo UI", 11);

            dataGrid.AlternatingRowsDefaultCellStyle.Font = new Font("Roboto", 11);
            dataGrid.DefaultCellStyle.Font = new Font("Roboto", 11);

            
        }
        // styling dark data grid views
        void styleDarkDataGridView(DataGridView dataGrid)
        {
            dataGrid.BorderStyle = BorderStyle.None;
            dataGrid.EnableHeadersVisualStyles = false;
            dataGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dataGrid.BackgroundColor = Color.FromArgb(51, 51, 51);
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 51);
            dataGrid.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            dataGrid.RowsDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 51);
            dataGrid.RowsDefaultCellStyle.ForeColor = Color.White;

            dataGrid.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 51);
            dataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGrid.DefaultCellStyle.Font = new Font("Roboto", 11);
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Roboto", 11);
            dataGrid.AlternatingRowsDefaultCellStyle.Font = new Font("Roboto", 11);

            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        // close application and all hidden forms
        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        // load animal table when program starts

        private void weightDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = data_weight.Rows[e.RowIndex];
                // calculate weight difference
                var weightAverage = Convert.ToInt32(row.Cells[4].Value.ToString());
                var initialWeight = Convert.ToInt32(row.Cells[2].Value.ToString());
                var difference = weightAverage - initialWeight;
                // display readings
                txt_physique_animal_Id.Text = row.Cells[1].Value.ToString();
                txt_phy_morning.Text = row.Cells[2].Value.ToString();
                txt_phy_evening.Text = row.Cells[3].Value.ToString();
                txt_phy_average.Text = row.Cells[4].Value.ToString() + "KG";
                txt_weight_difference.Text = difference.ToString() + "KG";
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show(txt_weight_difference.Text);
                // refresh fields
                txt_physique_animal_Id.Text = "";
                txt_phy_morning.Text = "";
                txt_phy_evening.Text = "";
                txt_phy_average.Text = "Calculating...";
            }
        }

        private void waistDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = data_waist.Rows[e.RowIndex];
                txt_physique_animal_Id.Text = row.Cells[1].Value.ToString();
                txt_phy_morning.Text = row.Cells[2].Value.ToString();
                txt_phy_evening.Text = row.Cells[3].Value.ToString();
                txt_phy_average.Text = row.Cells[4].Value.ToString();
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                txt_physique_animal_Id.Text = "";
                txt_phy_morning.Text = "";
                txt_phy_evening.Text = "";
                txt_phy_average.Text = "Calculating...";
            }
        }

        private void activityDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = activityDataGrid.Rows[e.RowIndex];
                txt_activity_animal_Id.Text = row.Cells[1].Value.ToString();
                if (row.Cells[2].Value.ToString() == "Inactive")
                {
                    radio_inactive.Checked = true;
                }
                else if (row.Cells[2].Value.ToString() == "Moderately Active")
                {
                    radio_moderate.Checked = true;
                }
                else
                {
                    radio_active.Checked = true;
                }
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                txt_activity_animal_Id.Text = "";
            }
        }

        private void exerciseDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = exerciseDataGrid.Rows[e.RowIndex];
                txt_exercise.Text = row.Cells[1].Value.ToString();
                txt_duration.Text = row.Cells[2].Value.ToString();
                txt_calories_burnt.Text = row.Cells[3].Value.ToString();
                txt_exe_animal_id.Text = row.Cells[4].Value.ToString();
                // collect the amount of calories consumed that day.
                string queryCalories = "SELECT SUM(calories) From Feeding AS f INNER JOIN Meal AS m ON f.MealId = m.Id WHERE f.AnimalId = '"+ txt_exe_animal_id.Text + "' AND f.date LIKE '2021-11-08%'";
                string calories = con.ReadString(queryCalories);
                txt_calories_eaten.Text = calories;
                // calculate the difference
                int calorieBurnt = Convert.ToInt32(row.Cells[3].Value.ToString());
                int caloricDiff = Convert.ToInt32(calories) - calorieBurnt;
                txt_average_gain.Text = caloricDiff.ToString();
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                txt_exe_animal_id.Text = "";
                txt_duration.Text = "";
                txt_calories_burnt.Text = "Waiting...";
            }
        }

        private void LoadMeals()
        {
            string queryMeal = "SELECT * FROM Meal AS M INNER JOIN Feeding AS F ON F.MealId = M.Id";
            con.LoadData(queryMeal, data_meal);
            data_meal.Columns[0].Visible = false;
            data_meal.Columns[4].Visible = false;
        }

        /* ========= Pull data from the db depending on which tab is selected ========= */
        private void materialTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this displays the data in the feeding table when feeding tab selected
            if (TabControl.SelectedTab == meal_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Feeding";
                con.LoadData(query, data_feeding);
           
                string queryMeal = "SELECT * FROM Meal";
                con.LoadData(queryMeal, data_meal);
                data_meal.Columns[4].Visible = false;
                //mealDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }

            // this displays the data in the exercise table after exercise tab selected
            if (TabControl.SelectedTab == exercise_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Exercise";
                con.LoadData(query, exerciseDataGrid);
                //exerciseDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }

            // this displays the data in the activity table after activity tab selected
            if (TabControl.SelectedTab == activity_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Activity";
                con.LoadData(query, activityDataGrid);
                //actvityDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }

            // this displays the data in the weight table after weight tab selected
            if (TabControl.SelectedTab == physique_page)
            {
                // Returns all data from the meals table
                string queryWeight = "SELECT * FROM Weight";
                con.LoadData(queryWeight, data_weight);

                // Returns all data from the meals table
                string queryWaist = "SELECT * FROM Waist";
                con.LoadData(queryWaist, data_waist);
                //weightDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }
            // this displays which settings were last selected
            if(TabControl.SelectedTab == settings_page)
            {
                string Theme = checkTheme();
                if(Theme == "Dark")
                {
                    theme_btn.Text = "Light Mode";
                }
                else
                {
                    theme_btn.Text = "Dark Mode";
                }
                
            }
        }

        /* ========== this section queries the database after user interaction ======== */

        void resetAnimalFields()
        {
            txt_animal_name.Text = "";
            txt_animal_name.Focus();
            gender = "Male";
            radio_male.Checked = true;
            txt_animal_age.Text = "";
            txt_animal_species.Text = "";
        }

        void resetMealFields()
        {
            // refresh fields
            txt_meal.Text = "";
            txt_meal.Focus();
            txt_portion.Text = "";
            txt_meal_animal_id.Text = "";
        }

        void resetExerciseFields()
        {
            txt_exercise.Text = "";
            txt_exercise.Focus();
            txt_calories_burnt.Text = "";
            txt_duration.Text = "";
            txt_exe_animal_id.Text = "";
        }
        /* Meal tab */
        // records meals to the database
        /* Exercise tab */
        private void rec_exe_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // pull the animal's weight average
                string queryWeightAverage = "SELECT WeightAverage FROM Weight WHERE AnimalId = '" + txt_exe_animal_id.Text + "'";
                string Average = con.ReadString(queryWeightAverage);
                int Weight = Convert.ToInt32(Average);

                // if the weight is beyond zero as a check
                if (Weight > 0)
                {
                    var caloriesBurned = 70 + Convert.ToInt32(txt_duration.Text) * (Weight * 0.75);

                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Exercise (Name, Duration, CaloriesBurned, AnimalId, Date) VALUES ('" + txt_exercise.Text + "','" + txt_duration.Text + "','" + caloriesBurned + "','" + txt_exe_animal_id.
                    Text + "','" + DateTime.Now.ToString("s") + "')";
                    
                    // we push the query to the AnimalControl class to process the query which links back to the con class
                    con.ExecuteQuery(txtQuery);
                    
                    string query = "SELECT * FROM Exercise";
                    con.LoadData(query, exerciseDataGrid);
                }
                else
                {
                    MessageBox.Show("The animal did not appear in the database!", "Error)", MessageBoxButtons.OK);
                }
                // refresh fields
                resetExerciseFields();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record exercise!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void update_exe_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // pull the animal's weight average
                string queryWeightAverage = "SELECT WeightAverage FROM Weight WHERE AnimalId = '" + txt_exe_animal_id.Text + "'";
                string Average = con.ReadString(queryWeightAverage);
                int Weight = Convert.ToInt32(Average);

                // if the meal's name already exists then add the feeding only
                if (Weight  > 0)
                {
                    var caloriesBurned = 70 + Convert.ToUInt32(txt_duration.Text) * (Weight * 0.75);
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(exerciseDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Exercise SET Name = '" + txt_exercise.Text + "', Duration = '" + txt_duration.Text + "', CaloriesBurned = '" + @caloriesBurned + "', AnimalId = '" + txt_exe_animal_id.Text + "' WHERE Id ='" + id + "'";

                    // we push the query to the AnimalControl class to process the query which links back to the con class
                    con.ExecuteQuery(txtQuery);
                    MessageBox.Show("Exercise record updated!");
                    string query = "SELECT * FROM Exercise";
                    con.LoadData(query, exerciseDataGrid);
                }
                else
                {
                    MessageBox.Show("The animal did not appear in the database!", "Error)", MessageBoxButtons.OK);
                }

                // refresh fields
                resetExerciseFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update exercise record!", "Error)", MessageBoxButtons.OK);
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

                    DialogResult dialogResult = MessageBox.Show("Delete Animal '" + txt_exe_animal_id.Text + "' exercise reading?", "Are you sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // we push the query to the AnimalControl class to process the query which links back to the con class
                        con.ExecuteQuery(txtQuery);
                        string query = "SELECT * FROM Exercise";
                        con.LoadData(query, exerciseDataGrid);
                    }
                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK);
            }
        }

        /* physique tab */
        private void rec_physique_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = Convert.ToInt32((txt_phy_morning.Text + txt_phy_evening.Text)) / 2;

                if (physiqueTabs.SelectedTab == weight_page)
                {
                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Weight (AnimalId, WeightMorning, WeightEvening, WeightAverage,  Date) VALUES ('" + txt_physique_animal_Id.Text + "','" + txt_phy_morning.Text + "','" + txt_phy_evening.Text + "','" + average + "','" + DateTime.Now.ToString("s") + "')";
                    con.ExecuteQuery(txtQuery);

                    string query = "SELECT * FROM Weight";
                    con.LoadData(query, data_weight);
                }

                if (physiqueTabs.SelectedTab == waist_page)
                {
                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Waist (AnimalId, WaistMorning, WaistEvening, WaistAverage,  Date) VALUES ('" + txt_physique_animal_Id.Text + "','" + txt_phy_morning.Text + "','" + txt_phy_evening.Text + "','" + average + "','" + DateTime.Now.ToString("s") + "')";
                    con.ExecuteQuery(txtQuery);

                    string query = "SELECT * FROM Waist";
                    con.LoadData(query, data_waist);
                }
                
                // refresh fields
                txt_physique_animal_Id.Text = "";
                txt_phy_morning.Text = "";
                txt_phy_evening.Text = "";
                txt_phy_average.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void update_physique_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = Convert.ToInt32((txt_phy_morning.Text + txt_phy_evening.Text)) / 2;

                if (physiqueTabs.SelectedTab == weight_page)
                {
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(data_weight.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Weight SET AnimalId = '" + txt_physique_animal_Id.Text + "', WeightMorning = '" + txt_phy_morning.Text + "', WeightEvening = '" + txt_phy_evening.Text + "', WeightAverage = '" + average + "' WHERE Id ='" + id + "'";
                    con.ExecuteQuery(txtQuery);
                    string query = "SELECT * FROM Weight";
                    con.LoadData(query, data_weight);
                }

                if (physiqueTabs.SelectedTab == waist_page)
                {
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(data_waist.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Waist SET AnimalId = '" + txt_physique_animal_Id.Text + "', WaistMorning = '" + txt_phy_morning.Text + "', WaistEvening = '" + txt_phy_evening.Text + "', WaistAverage = '" + average + "' WHERE Id ='" + id + "'";
                    con.ExecuteQuery(txtQuery);
                    string query = "SELECT * FROM Waist";
                    con.LoadData(query, data_waist);
                }
                

                // refresh fields
                txt_physique_animal_Id.Text = "";
                txt_phy_morning.Text = "";
                txt_phy_evening.Text = "";
                txt_phy_average.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update weight record!", "Error)", MessageBoxButtons.OK);
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
                        int id = Convert.ToInt32(data_weight.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                        string txtQuery = "DELETE FROM Weight WHERE ID = '" + id + "' ";

                        DialogResult dialogResult = MessageBox.Show("Delete Animal '" + txt_physique_animal_Id.Text + "' weight reading?", "Are you sure?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // we push the query to the AnimalControl class to process the query which links back to the con class
                            con.ExecuteQuery(txtQuery);
                            string query = "SELECT * FROM Weight";
                            con.LoadData(query, data_weight);
                        }
                    }

                    if (physiqueTabs.SelectedTab == waist_page)
                    {
                        // we build our query in the form page which has references to the its controls.
                        int id = Convert.ToInt32(data_waist.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                        string txtQuery = "DELETE FROM Waist WHERE ID = '" + id + "' ";

                        DialogResult dialogResult = MessageBox.Show("Delete Animal '" + txt_physique_animal_Id.Text + "' waist reading?", "Are you sure?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // we push the query to the AnimalControl class to process the query which links back to the con class
                            con.ExecuteQuery(txtQuery);
                            string query = "SELECT * FROM Waist";
                            con.LoadData(query, data_waist);
                            // MessageBox.Show("Weight Reading deleted!");
                        }
                    }

                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK);
            }
        }

        /* Activity tab */
        private void rec_activity_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Activity (AnimalId, ActivityLevel, Date) VALUES ('" + txt_activity_animal_Id.Text + "','" + activity + "','" + DateTime.Now.ToString("s") + "')";

                // we push the query to the AnimalControl class to process the query which links back to the con class
                con.ExecuteQuery(txtQuery);
                //MessageBox.Show("Activity Registered!");
                string query = "SELECT * FROM Activity";
                con.LoadData(query, activityDataGrid);

                // refresh fields
                txt_activity_animal_Id.Text = "";
                radio_inactive.Checked = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to insert!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void update_activity_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to its controls
                int id = Convert.ToInt32(activityDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                string txtQuery = "UPDATE Activity SET AnimalId = '" + txt_activity_animal_Id.Text + "', ActivityLevel = '" + @activity + "' WHERE Id ='" + id + "'";

                // we push the query to the AnimalControl class to process the query which links back to the con class
                con.ExecuteQuery(txtQuery);
                //MessageBox.Show("Details updated!");
                string query = "SELECT * FROM Activity";
                con.LoadData(query, activityDataGrid);

                // refresh fields
                txt_activity_animal_Id.Text = "";
                radio_inactive.Checked = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update!", "Error)", MessageBoxButtons.OK);
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

                    DialogResult dialogResult = MessageBox.Show("Delete Animal '" + txt_activity_animal_Id.Text + "' activity Level?", "Are you sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // we push the query to the AnimalControl class to process the query which links back to the con class
                        con.ExecuteQuery(txtQuery);
                        string query = "SELECT * FROM Activity";
                        con.LoadData(query, activityDataGrid);
                    }

                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK);
            }
        }

        private void loadGraph_btn_Click(object sender, EventArgs e)
        {
            
        }
        /* Settings tab */

        private void theme_btn_Click(object sender, EventArgs e)
        {
            /*
            string queryTheme = "SELECT Theme FROM Settings";
            string Theme = con.ReadString(queryTheme);
            */
            if(Theme == "Dark")
            {
                materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;
                LightTheme();
                string lightTheme = "UPDATE Settings SET Theme ='Light' WHERE Id=1";
                con.ExecuteQuery(lightTheme);
            }
            else
            {
                materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;
                DarkTheme();
                string darkTheme = "UPDATE Settings SET Theme ='Dark' WHERE Id=1";
                con.ExecuteQuery(darkTheme);
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
        // make sure only text is inserted
        private void age_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            int asciiCode = Convert.ToInt32(e.KeyChar);
            if((asciiCode != 8))
            {
                if((asciiCode >= 48 && asciiCode <= 57))
                {
                    e.Handled = false;
                }
                else
                {
                    MessageBox.Show("Numbers Only Please!", "Error: Number Only", MessageBoxButtons.OK);
                    e.Handled = true;
                }
            }
        }
        // when a cell is clicked
        private void data_animal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = data_animal.Rows[e.RowIndex];

                txt_animal_name.Text = row.Cells[1].Value.ToString();
                txt_animal_species.Text = row.Cells[2].Value.ToString();
                string gender = row.Cells[3].Value.ToString();
                txt_animal_age.Text = row.Cells[4].Value.ToString();

                if (gender == "Male")
                {
                    radio_male.Checked = true;
                }
                else
                {
                    radio_female.Checked = true;
                }

            }
            catch (Exception) // reset textboxes
            {
                // refresh fields
                txt_animal_name.Text = "";
                txt_animal_species.Text = "";
                txt_animal_age.Text = "";
                radio_male.Checked = true;
            }
        }

        private void btn_add_meal_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txt_meal_animal_id.Text);
                // if the meal's name already exists then add the feeding only
                if (id > 0)
                {
                    string feedingQuery = $"INSERT INTO Feeding (AnimalId, MealId, Date) VALUES ({id}, '{date}')";
                    con.ExecuteQuery(feedingQuery);

                    string loadMeals = "SELECT * FROM Meal";
                    con.LoadData(loadMeals, data_meal);

                    string loadFeedings = "SELECT * FROM Feeding";
                    con.LoadData(loadFeedings, data_feeding);

                    // refresh fields
                    resetMealFields();

                }
                else
                {
                    // calculate calories per gram, 4 being the multiple digit to calculate protein
                    var calories = Convert.ToInt32(txt_portion.Text) * 4;
                    // if the meal didn't exist, add the feeding and the meal details to the database
                    // we build our query in the form page which has references to the its controls.
                    string mealQuery = "INSERT INTO Meal (Name, Calories, Portion, Date) VALUES ('" + txt_meal.Text + "','" + calories + "','" + txt_portion.Text + "','" + DateTime.Now.ToString("s") + "')";
                    con.ExecuteQuery(mealQuery);

                    // pull the meal Id to reference to the meal
                    string queryNewMealId = "SELECT Id FROM Meal WHERE Name = '" + txt_meal.Text + "'";
                    string newMealId = con.ReadString(queryNewMealId);

                    string feedingQuery = "INSERT INTO Feeding (AnimalId, MealId, Date) VALUES ('" + txt_meal_animal_id.Text + "', '" + newMealId.ToString() + "','" + DateTime.Now.ToString("s") + "')";
                    con.ExecuteQuery(feedingQuery);

                    string loadMeals = "SELECT * FROM Meal";
                    con.LoadData(loadMeals, data_meal);

                    string loadFeedings = "SELECT * FROM Feeding";
                    con.LoadData(loadFeedings, data_feeding);

                    MessageBox.Show("Recorded new meal!");

                    // refresh fields
                    resetMealFields();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record feeding!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void menu_meal_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (mealTabs.SelectedTab == meal_page)
                {
                    // calculate calories per gram, 4 being the multiple digit to calculate protein
                    var calories = Convert.ToInt32(txt_portion.Text) * 4;
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(data_meal.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Meal SET Name = '" + txt_meal.Text + "', Calories = '" + @calories + "', Portion = '" + txt_portion.Text + "' WHERE Id ='" + id + "'";

                    // we push the query to the AnimalControl class to process the query which links back to the con class
                    con.ExecuteQuery(txtQuery);
                    MessageBox.Show("Meal record updated!");
                    string query = "SELECT * FROM Meal";
                    con.LoadData(query, data_meal);
                    resetMealFields();
                }

                if (mealTabs.SelectedTab == feeding_page)
                {
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(data_feeding.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Feeding SET AnimalId = '" + txt_meal_animal_id.Text + "' WHERE Id ='" + id + "'";

                    // we push the query to the AnimalControl class to process the query which links back to the con class
                    con.ExecuteQuery(txtQuery);
                    MessageBox.Show("Feeding record updated!");
                    string query = "SELECT * FROM Feeding";
                    con.LoadData(query, data_feeding);
                    resetMealFields();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update meal record!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void menu_meal_delete_Click(object sender, EventArgs e)
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
                            int id = Convert.ToInt32(data_meal.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                            string txtQuery = "DELETE FROM Meal WHERE ID = '" + id + "' ";

                            // we push the query to the AnimalControl class to process the query which links back to the con class
                            con.ExecuteQuery(txtQuery);
                            string query = "SELECT * FROM Meal";
                            con.LoadData(query, data_meal);
                            MessageBox.Show("Meal deleted!");
                        }

                        if (mealTabs.SelectedTab == feeding_page)
                        {
                            int id = Convert.ToInt32(data_feeding.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                            string txtQuery = "DELETE FROM Feeding WHERE ID = '" + id + "' ";

                            // we push the query to the AnimalControl class to process the query which links back to the con class
                            con.ExecuteQuery(txtQuery);
                            string query = "SELECT * FROM Feeding";
                            con.LoadData(query, data_feeding);
                            resetMealFields();
                        }
                    }
                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK);
            }
        }

        private void menu_meal_record_Click(object sender, EventArgs e)
        {
            btn_add_meal_Click(sender, e);
        }
        // code this block
        private void btn_add_animal_Click(object sender, EventArgs e)
        {
            try
            {
                // set variables
                string name = txt_animal_name.Text;
                string species = txt_animal_species.Text;
                string age = txt_animal_age.Text;
                // how to handle check boxes
                if(radio_male.Checked == true)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                // insert into the database
                con.ExecuteQuery($"INSERT INTO Animal(Name, species, gender age) VALUES('{name}', '{species}', '{gender}', {age})");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void menu_animal_record_Click(object sender, EventArgs e)
        {
            btn_add_animal_Click(sender, e);
        }
        // code this block
        private void menu_animal_update_Click(object sender, EventArgs e)
        {

        }

        private void btn_exe_record_Click(object sender, EventArgs e)
        {
            try
            {
                // pull the animal's weight average
                string queryWeightAverage = "SELECT WeightAverage FROM Weight WHERE AnimalId = '" + txt_exe_animal_id.Text + "'";
                string Average = con.ReadString(queryWeightAverage);
                int Weight = Convert.ToInt32(Average);

                // if the weight is beyond zero as a check
                if (Weight > 0)
                {
                    var caloriesBurned = 70 + Convert.ToInt32(txt_duration.Text) * (Weight * 0.75);

                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Exercise (Name, Duration, CaloriesBurned, AnimalId, Date) VALUES ('" + txt_exercise.Text + "','" + txt_duration.Text + "','" + caloriesBurned + "','" + txt_exe_animal_id.
                    Text + "','" + DateTime.Now.ToString("s") + "')";

                    // we push the query to the AnimalControl class to process the query which links back to the con class
                    con.ExecuteQuery(txtQuery);

                    string query = "SELECT * FROM Exercise";
                    con.LoadData(query, exerciseDataGrid);
                }
                else
                {
                    MessageBox.Show("The animal did not appear in the database!", "Error)", MessageBoxButtons.OK);
                }
                // refresh fields
                resetExerciseFields();

            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record exercise!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void memu_exe_record_Click(object sender, EventArgs e)
        {
            try
            {
                // pull the animal's weight average
                string queryWeightAverage = "SELECT WeightAverage FROM Weight WHERE AnimalId = '" + txt_exe_animal_id.Text + "'";
                string Average = con.ReadString(queryWeightAverage);
                int Weight = Convert.ToInt32(Average);

                // if the weight is beyond zero as a check
                if (Weight > 0)
                {
                    var caloriesBurned = 70 + Convert.ToInt32(txt_duration.Text) * (Weight * 0.75);

                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Exercise (Name, Duration, CaloriesBurned, AnimalId, Date) VALUES ('" + txt_exercise.Text + "','" + txt_duration.Text + "','" + caloriesBurned + "','" + txt_exe_animal_id.
                    Text + "','" + DateTime.Now.ToString("s") + "')";

                    // we push the query to the AnimalControl class to process the query which links back to the con class
                    con.ExecuteQuery(txtQuery);

                    string query = "SELECT * FROM Exercise";
                    con.LoadData(query, exerciseDataGrid);
                }
                else
                {
                    MessageBox.Show("The animal did not appear in the database!", "Error)", MessageBoxButtons.OK);
                }
                // refresh fields
                resetExerciseFields();

            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record exercise!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void menu_exe_update_Click(object sender, EventArgs e)
        {
            try
            {
                // pull the animal's weight average
                string queryWeightAverage = "SELECT WeightAverage FROM Weight WHERE AnimalId = '" + txt_exe_animal_id.Text + "'";
                string Average = con.ReadString(queryWeightAverage);
                int Weight = Convert.ToInt32(Average);

                // if the meal's name already exists then add the feeding only
                if (Weight > 0)
                {
                    var caloriesBurned = 70 + Convert.ToUInt32(txt_duration.Text) * (Weight * 0.75);
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(exerciseDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Exercise SET Name = '" + txt_exercise.Text + "', Duration = '" + txt_duration.Text + "', CaloriesBurned = '" + @caloriesBurned + "', AnimalId = '" + txt_exe_animal_id.Text + "' WHERE Id ='" + id + "'";

                    // we push the query to the AnimalControl class to process the query which links back to the con class
                    con.ExecuteQuery(txtQuery);
                    MessageBox.Show("Exercise record updated!");
                    string query = "SELECT * FROM Exercise";
                    con.LoadData(query, exerciseDataGrid);
                }
                else
                {
                    MessageBox.Show("The animal did not appear in the database!", "Error)", MessageBoxButtons.OK);
                }

                // refresh fields
                resetExerciseFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update exercise record!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void menu_exe_delete_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    // we build our query in the form page which has references to the its controls.
                    int id = Convert.ToInt32(exerciseDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "DELETE FROM Exercise WHERE ID = '" + id + "' ";

                    DialogResult dialogResult = MessageBox.Show("Delete Animal '" + txt_exe_animal_id.Text + "' exercise reading?", "Are you sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // we push the query to the AnimalControl class to process the query which links back to the con class
                        con.ExecuteQuery(txtQuery);
                        string query = "SELECT * FROM Exercise";
                        con.LoadData(query, exerciseDataGrid);
                    }
                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK);
            }
        }

        private void btn_activity_record_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Activity (AnimalId, ActivityLevel, Date) VALUES ('" + txt_activity_animal_Id.Text + "','" + activity + "','" + DateTime.Now.ToString("s") + "')";

                // we push the query to the AnimalControl class to process the query which links back to the con class
                con.ExecuteQuery(txtQuery);
                //MessageBox.Show("Activity Registered!");
                string query = "SELECT * FROM Activity";
                con.LoadData(query, activityDataGrid);

                // refresh fields
                txt_activity_animal_Id.Text = "";
                radio_inactive.Checked = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to insert!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void menu_activity_record_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Activity (AnimalId, ActivityLevel, Date) VALUES ('" + txt_activity_animal_Id.Text + "','" + activity + "','" + DateTime.Now.ToString("s") + "')";

                // we push the query to the AnimalControl class to process the query which links back to the con class
                con.ExecuteQuery(txtQuery);
                //MessageBox.Show("Activity Registered!");
                string query = "SELECT * FROM Activity";
                con.LoadData(query, activityDataGrid);

                // refresh fields
                txt_activity_animal_Id.Text = "";
                radio_inactive.Checked = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to insert!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void menu_activity_update_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to its controls
                int id = Convert.ToInt32(activityDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                string txtQuery = "UPDATE Activity SET AnimalId = '" + txt_activity_animal_Id.Text + "', ActivityLevel = '" + @activity + "' WHERE Id ='" + id + "'";

                // we push the query to the AnimalControl class to process the query which links back to the con class
                con.ExecuteQuery(txtQuery);
                //MessageBox.Show("Details updated!");
                string query = "SELECT * FROM Activity";
                con.LoadData(query, activityDataGrid);

                // refresh fields
                txt_activity_animal_Id.Text = "";
                radio_inactive.Checked = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void menu_activity_delete_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    // we build our query in the form page which has references to the its controls.
                    int id = Convert.ToInt32(activityDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "DELETE FROM Activity WHERE ID = '" + id + "' ";

                    DialogResult dialogResult = MessageBox.Show("Delete Animal '" + txt_activity_animal_Id.Text + "' activity Level?", "Are you sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // we push the query to the AnimalControl class to process the query which links back to the con class
                        con.ExecuteQuery(txtQuery);
                        string query = "SELECT * FROM Activity";
                        con.LoadData(query, activityDataGrid);
                    }

                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK);
            }
        }

        private void btn_load_activity_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txt_chart_animal_Id.Text);
            // get average
            SQLiteCommand average = new SQLiteCommand($"SELECT CAST(WeightAverage AS INT), date FROM Weight WHERE AnimalId = {id}", con.GetConnection());
            // get morning weight
            SQLiteCommand morningWeight = new SQLiteCommand($"SELECT WeightMorning, date FROM Weight WHERE AnimalId = {id}", con.GetConnection());
            // get evening weight
            SQLiteCommand eveningWeight = new SQLiteCommand($"SELECT WeightEvening, date FROM Weight WHERE AnimalId = {id}", con.GetConnection());
            SQLiteDataReader avg = average.ExecuteReader();
            SQLiteDataReader mw = morningWeight.ExecuteReader();
            SQLiteDataReader ew = eveningWeight.ExecuteReader();
            try
            {
                // clear previous trend
                chart.Series[0].Points.Clear();
                chart.Series[1].Points.Clear();
                chart.Series[2].Points.Clear();

                while (avg.Read() && mw.Read() && ew.Read())
                {
                    chart.Series["Weight Average"].Points.Add(avg.GetInt32(0));
                    chart.Series["Morning Weight"].Points.Add(mw.GetInt32(0));
                    chart.Series["Evening Weight"].Points.Add(ew.GetInt32(0));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Assistant");
                Close();
            }
        }

        private void btn_phy_add_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = Convert.ToInt32((txt_phy_morning.Text + txt_phy_evening.Text)) / 2;

                if (physiqueTabs.SelectedTab == weight_page)
                {
                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Weight (AnimalId, WeightMorning, WeightEvening, WeightAverage,  Date) VALUES ('" + txt_physique_animal_Id.Text + "','" + txt_phy_morning.Text + "','" + txt_phy_evening.Text + "','" + average + "','" + DateTime.Now.ToString("s") + "')";
                    con.ExecuteQuery(txtQuery);

                    string query = "SELECT * FROM Weight";
                    con.LoadData(query, data_weight);
                }

                if (physiqueTabs.SelectedTab == waist_page)
                {
                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Waist (AnimalId, WaistMorning, WaistEvening, WaistAverage,  Date) VALUES ('" + txt_physique_animal_Id.Text + "','" + txt_phy_morning.Text + "','" + txt_phy_evening.Text + "','" + average + "','" + DateTime.Now.ToString("s") + "')";
                    con.ExecuteQuery(txtQuery);

                    string query = "SELECT * FROM Waist";
                    con.LoadData(query, data_waist);
                }

                // refresh fields
                txt_physique_animal_Id.Text = "";
                txt_phy_morning.Text = "";
                txt_phy_evening.Text = "";
                txt_phy_average.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record!", "Error)", MessageBoxButtons.OK);
            }
        }
 
        private void menu_phy_add_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = Convert.ToInt32((txt_phy_morning.Text + txt_phy_evening.Text)) / 2;

                if (physiqueTabs.SelectedTab == weight_page)
                {
                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Weight (AnimalId, WeightMorning, WeightEvening, WeightAverage,  Date) VALUES ('" + txt_physique_animal_Id.Text + "','" + txt_phy_morning.Text + "','" + txt_phy_evening.Text + "','" + average + "','" + DateTime.Now.ToString("s") + "')";
                    con.ExecuteQuery(txtQuery);

                    string query = "SELECT * FROM Weight";
                    con.LoadData(query, data_weight);
                }

                if (physiqueTabs.SelectedTab == waist_page)
                {
                    // we build our query in the form page which has references to the its controls.
                    string txtQuery = "INSERT INTO Waist (AnimalId, WaistMorning, WaistEvening, WaistAverage,  Date) VALUES ('" + txt_physique_animal_Id.Text + "','" + txt_phy_morning.Text + "','" + txt_phy_evening.Text + "','" + average + "','" + DateTime.Now.ToString("s") + "')";
                    con.ExecuteQuery(txtQuery);

                    string query = "SELECT * FROM Waist";
                    con.LoadData(query, data_waist);
                }

                // refresh fields
                txt_physique_animal_Id.Text = "";
                txt_phy_morning.Text = "";
                txt_phy_evening.Text = "";
                txt_phy_average.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void menu_phy_update_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = Convert.ToInt32((txt_phy_morning.Text + txt_phy_evening.Text)) / 2;

                if (physiqueTabs.SelectedTab == weight_page)
                {
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(data_weight.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Weight SET AnimalId = '" + txt_physique_animal_Id.Text + "', WeightMorning = '" + txt_phy_morning.Text + "', WeightEvening = '" + txt_phy_evening.Text + "', WeightAverage = '" + average + "' WHERE Id ='" + id + "'";
                    con.ExecuteQuery(txtQuery);
                    string query = "SELECT * FROM Weight";
                    con.LoadData(query, data_weight);
                }

                if (physiqueTabs.SelectedTab == waist_page)
                {
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(data_waist.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Waist SET AnimalId = '" + txt_physique_animal_Id.Text + "', WaistMorning = '" + txt_phy_morning.Text + "', WaistEvening = '" + txt_phy_evening.Text + "', WaistAverage = '" + average + "' WHERE Id ='" + id + "'";
                    con.ExecuteQuery(txtQuery);
                    string query = "SELECT * FROM Waist";
                    con.LoadData(query, data_waist);
                }


                // refresh fields
                txt_physique_animal_Id.Text = "";
                txt_phy_morning.Text = "";
                txt_phy_evening.Text = "";
                txt_phy_average.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update weight record!", "Error)", MessageBoxButtons.OK);
            }
        }

        private void menu_phy_delete_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (physiqueTabs.SelectedTab == weight_page)
                    {
                        // we build our query in the form page which has references to the its controls.
                        int id = Convert.ToInt32(data_weight.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                        string txtQuery = "DELETE FROM Weight WHERE ID = '" + id + "' ";

                        DialogResult dialogResult = MessageBox.Show("Delete Animal '" + txt_physique_animal_Id.Text + "' weight reading?", "Are you sure?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // we push the query to the AnimalControl class to process the query which links back to the con class
                            con.ExecuteQuery(txtQuery);
                            string query = "SELECT * FROM Weight";
                            con.LoadData(query, data_weight);
                        }
                    }

                    if (physiqueTabs.SelectedTab == waist_page)
                    {
                        // we build our query in the form page which has references to the its controls.
                        int id = Convert.ToInt32(data_waist.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                        string txtQuery = "DELETE FROM Waist WHERE ID = '" + id + "' ";

                        DialogResult dialogResult = MessageBox.Show("Delete Animal '" + txt_physique_animal_Id.Text + "' waist reading?", "Are you sure?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // we push the query to the AnimalControl class to process the query which links back to the con class
                            con.ExecuteQuery(txtQuery);
                            string query = "SELECT * FROM Waist";
                            con.LoadData(query, data_waist);
                            // MessageBox.Show("Weight Reading deleted!");
                        }
                    }

                }
                catch (Exception)
                {
                    // this happens when we have an error
                    MessageBox.Show("Empty row selected", "Error)", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error)", MessageBoxButtons.OK);
            }
        }
    }
}
