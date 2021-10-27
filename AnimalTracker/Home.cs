using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SQLite;
using System.Data;

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
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            // style datagrind
            
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
            string query = "SELECT * FROM Animal";
            LoadData(query, animalDataGrid);
            //animalDataGrid.Columns[0].Visible = false; // hide ID column during runtime
        }

        // fill textboxes with data when user clicks a cell
        public void animalDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = animalDataGrid.Rows[e.RowIndex];
                name_txt.Text = row.Cells[1].Value.ToString();
                if(row.Cells[2].Value.ToString() == "Female")
                {
                    female_radio_btn.Checked = true;
                } else
                {
                    male_radio_btn.Checked = true;
                }
                age_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                species_txt.Text = row.Cells[4].Value.ToString();
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                name_txt.Text = " ";
                name_txt.Focus();
                age_txt.Value = 0;
                species_txt.Text = " ";
            }
        }

        private void mealDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = mealDataGrid.Rows[e.RowIndex];
                meal_txt.Text = row.Cells[1].Value.ToString();
                calories_txt.Value = Convert.ToInt32(row.Cells[2].Value.ToString());
                portion_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                meal_txt.Text = " ";
                meal_txt.Focus();
                calories_txt.Value = 0;
                portion_txt.Value = 0;
            }
        }
        
        // Create a gender variable
        string gender = "Male";

        // react to user input when they select the female radio button
        private void female_radio_btn_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
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
                //mealDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }

            // this displays the data in the meals table when meals tab selected
            if (materialTabControl.SelectedTab == meal_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Meal";
                LoadData(query, mealDataGrid);
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
            if (materialTabControl.SelectedTab == weight_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Weight";
                LoadData(query, weightDataGrid);
                //weightDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }

            // this displays the data in the activity table after activity tab selected
            if (materialTabControl.SelectedTab == waist_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Waist";
                LoadData(query, waistDataGrid);
                //waistDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }
        }

        /* this section queries the database after user interaction */
        // adds animal to database
        private void add_ani_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Animal (Name, Gender, Age, Species, Registered) VALUES ('" + name_txt.Text + "','" + gender + "','" + age_txt.Text + "','" + species_txt.
                Text + "','" + DateTime.Now.ToString("s") + "')";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Animal Registered!");
                string query = "SELECT * FROM Animal";
                LoadData(query, animalDataGrid);

                // refresh fields
                name_txt.Text = " ";
                name_txt.Focus();
                gender = "Male";
                male_radio_btn.Checked = true;
                age_txt.Value = 0;
                species_txt.Text = " ";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to insert!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // updates animal list
        private void update_ani_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to its controls
                int id = Convert.ToInt32(animalDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                string txtQuery = "UPDATE Animal SET Name = '" + @name_txt.Text + "', Age = '" + @age_txt.Value + "', Gender = '" + @gender + "', Species = '" + @species_txt.Text + "' WHERE Id ='" + id + "'";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Details updated!");
                string query = "SELECT * FROM Animal";
                LoadData(query, animalDataGrid);

                // refresh fields
                name_txt.Text = " ";
                name_txt.Focus();
                age_txt.Value = 0;
                gender = "Male";
                male_radio_btn.Checked = true;
                species_txt.Text = " ";
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
                    // we build our query in the form page which has references to the its controls.
                    int id = Convert.ToInt32(animalDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "DELETE FROM Animal WHERE ID = '" + id + "' ";

                    // we push the query to the AnimalControl class to process the query which links back to the connection class
                    AnimalControls.Querydb(txtQuery);
                    string query = "SELECT * FROM Animal";
                    LoadData(query, animalDataGrid);
                    MessageBox.Show("Animal deleted!");
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

                // pull the meal name to compare it to the user input
                string queryMealName = "SELECT Name FROM Meal WHERE Name = '"+ meal_txt.Text +"'";
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
                    meal_txt.Text = " ";
                    meal_txt.Focus();
                    calories_txt.Value = 0;
                    portion_txt.Value = 0;
                    AnimalId_txt.Value = 0;
         
                } else
                {
                    // if the meal didn't exist, add the feeding and the meal details to the database
                    // we build our query in the form page which has references to the its controls.
                    string mealQuery = "INSERT INTO Meal (Name, Calories, Portion, Date) VALUES ('" + meal_txt.Text + "','" + calories_txt.Value + "','" + portion_txt.Value + "','" + DateTime.Now.ToString("s") + "')";
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

                    // refresh fields
                    meal_txt.Text = " ";
                    meal_txt.Focus();
                    calories_txt.Value = 0;
                    portion_txt.Value = 0;
                    AnimalId_txt.Value = 0;

                    MessageBox.Show("Record new meal!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record feeding!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // we build our query in the form page which has references to its controls
        private void update_meal_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to its controls
                int id = Convert.ToInt32(mealDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                string txtQuery = "UPDATE Meal SET Name = '" + @meal_txt.Text + "', Calories = '" + @calories_txt.Value + "', Portion = '" + @portion_txt.Value + "', AnimalId = '" + @AnimalId_txt.Value + "' WHERE Id ='" + id + "'";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Meal record updated!");
                string query = "SELECT * FROM Meal";
                LoadData(query, mealDataGrid);

                // refresh fields
                meal_txt.Text = " ";
                meal_txt.Focus();
                calories_txt.Value = 0;
                portion_txt.Value = 0;
                AnimalId_txt.Value = 0;
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
                    // we build our query in the form page which has references to the its controls.
                    int id = Convert.ToInt32(mealDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "DELETE FROM Meal WHERE ID = '" + id + "' ";

                    // we push the query to the AnimalControl class to process the query which links back to the connection class
                    AnimalControls.Querydb(txtQuery);
                    string query = "SELECT * FROM Meal";
                    LoadData(query, mealDataGrid);
                    MessageBox.Show("Meal deleted!");
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

        private void rec_exe_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Exercise (Name, Duration, CaloriesBurnt, AnimalId, Date) VALUES ('" + exercise_txt.Text + "','" + duration_txt.Value + "','" + calories_burnt.Value + "','" + exercise_ani_id.
                Value + "','" + DateTime.Now.ToString("s") + "')";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Exercise Recorded!");
                string query = "SELECT * FROM Exercise";
                LoadData(query, exerciseDataGrid);

                // refresh fields
                exercise_txt.Text = " ";
                exercise_txt.Focus();
                calories_burnt.Value = 0;
                duration_txt.Value = 0;
                exercise_ani_id.Value = 0;
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
                // we build our query in the form page which has references to its controls
                int id = Convert.ToInt32(exerciseDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                string txtQuery = "UPDATE Exercise SET Name = '" + @exercise_txt.Text + "', Duration = '" + @duration_txt.Value + "', CaloriesBurnt = '" + @calories_burnt.Value + "', AnimalId = '" + @AnimalId_txt.Value + "' WHERE Id ='" + id + "'";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Exercise record updated!");
                string query = "SELECT * FROM Exercise";
                LoadData(query, exerciseDataGrid);

                // refresh fields
                exercise_txt.Text = " ";
                exercise_txt.Focus();
                calories_burnt.Value = 0;
                duration_txt.Value = 0;
                exercise_ani_id.Value = 0;
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
                    int id = Convert.ToInt32(animalDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "DELETE FROM Exercise WHERE ID = '" + id + "' ";

                    // we push the query to the AnimalControl class to process the query which links back to the connection class
                    AnimalControls.Querydb(txtQuery);
                    string query = "SELECT * FROM Exercise";
                    LoadData(query, exerciseDataGrid);
                    MessageBox.Show("Session deleted!");
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
    }
}
