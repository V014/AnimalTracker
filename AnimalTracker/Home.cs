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
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            // style datagrind
            
        }

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
                meal_txt.Text = row.Cells[1].Value.ToString();
                calories_lbl.Text = row.Cells[2].Value.ToString();
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
                weight_AnimalId.Value = Convert.ToInt32(row.Cells[1].Value.ToString());
                mor_weight_txt.Value = Convert.ToInt32(row.Cells[2].Value.ToString());
                eve_weight_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                avg_weight_lbl.Text = row.Cells[4].Value.ToString();
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                weight_AnimalId.Value = 0;
                mor_weight_txt.Value = 0;
                eve_weight_txt.Value = 0;
                avg_weight_lbl.Text = "Calculating...";
            }
        }

        private void waistDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = waistDataGrid.Rows[e.RowIndex];
                waist_AnimalId_txt.Value = Convert.ToInt32(row.Cells[1].Value.ToString());
                mor_waist_txt.Value = Convert.ToInt32(row.Cells[2].Value.ToString());
                eve_waist_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                avg_waist_txt.Text = row.Cells[4].Value.ToString();
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                waist_AnimalId_txt.Value = 0;
                mor_waist_txt.Value = 0;
                eve_waist_txt.Value = 0;
                avg_waist_txt.Text = "Calculating...";
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

        /* ========== this section queries the database after user interaction ======== */
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
                    string queryAnimal = "INSERT INTO Animal (AnimalId, Registered) VALUES ('" + AnimalId.ToString() +"', '" + DateTime.Now.ToString("s") + "')";
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
                name_txt.Text = "";
                name_txt.Focus();
                gender = "Male";
                male_radio_btn.Checked = true;
                age_txt.Value = 0;
                species_txt.Text = "";
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
                    meal_txt.Text = "";
                    meal_txt.Focus();
                    calories_lbl.Text = "Waiting on you";
                    portion_txt.Value = 0;
                    AnimalId_txt.Value = 0;
         
                } else
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

                    // refresh fields
                    meal_txt.Text = " ";
                    meal_txt.Focus();
                    calories_lbl.Text = "Waiting on you";
                    portion_txt.Value = 0;
                    AnimalId_txt.Value = 0;

                    MessageBox.Show("Recorded new meal!");
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

                // refresh fields
                meal_txt.Text = " ";
                meal_txt.Focus();
                calories_lbl.Text = "Waiting on you";
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

        private void update_feeding_txt_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to its controls
                int id = Convert.ToInt32(feedingDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                string txtQuery = "UPDATE Feeding SET AnimalId = '" + AnimalId_txt.Value + "' WHERE Id ='" + id + "'";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Feeding record updated!");
                string query = "SELECT * FROM Feeding";
                LoadData(query, feedingDataGrid);

                // refresh fields
                meal_txt.Text = " ";
                meal_txt.Focus();
                calories_lbl.Text = "Waiting on you";
                portion_txt.Value = 0;
                AnimalId_txt.Value = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update feeding record!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void del_feeding_txt_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    // we build our query in the form page which has references to the its controls.
                    int id = Convert.ToInt32(feedingDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "DELETE FROM Feeding WHERE ID = '" + id + "' ";

                    // we push the query to the AnimalControl class to process the query which links back to the connection class
                    AnimalControls.Querydb(txtQuery);
                    string query = "SELECT * FROM Feeding";
                    LoadData(query, feedingDataGrid);
                    MessageBox.Show("Feeding deleted!");
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
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Exercise (Name, Duration, CaloriesBurnt, AnimalId, Date) VALUES ('" + exercise_txt.Text + "','" + duration_txt.Value + "','" + calories_burnt_txt.Text + "','" + exercise_AnimalId_txt.
                Value + "','" + DateTime.Now.ToString("s") + "')";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Exercise Recorded!");
                string query = "SELECT * FROM Exercise";
                LoadData(query, exerciseDataGrid);

                // refresh fields
                exercise_txt.Text = "";
                exercise_txt.Focus();
                calories_burnt_txt.Text = "";
                duration_txt.Value = 0;
                exercise_AnimalId_txt.Value = 0;
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
                string txtQuery = "UPDATE Exercise SET Name = '" + @exercise_txt.Text + "', Duration = '" + @duration_txt.Value + "', CaloriesBurnt = '" + @calories_burnt_txt.Text + "', AnimalId = '" + @AnimalId_txt.Value + "' WHERE Id ='" + id + "'";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Exercise record updated!");
                string query = "SELECT * FROM Exercise";
                LoadData(query, exerciseDataGrid);

                // refresh fields
                exercise_txt.Text = "";
                exercise_txt.Focus();
                calories_burnt_txt.Text = "";
                duration_txt.Value = 0;
                exercise_AnimalId_txt.Value = 0;
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

        /* Weight tab */
        private void rec_weight_txt_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = (mor_weight_txt.Value + eve_weight_txt.Value) / 2;
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Weight (AnimalId, WeightMorning, WeightEvening, WeightAverage,  Date) VALUES ('" + weight_AnimalId.Value + "','" + mor_weight_txt.Value + "','" + eve_weight_txt.Value + "','" + average + "','" + DateTime.Now.ToString("s") + "')";
                AnimalControls.Querydb(txtQuery);

                string query = "SELECT * FROM Weight";
                LoadData(query, weightDataGrid);

                // refresh fields
                weight_AnimalId.Value = 0;
                mor_weight_txt.Value = 0;
                eve_weight_txt.Value = 0;
                avg_weight_lbl.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record Weight!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_weight_txt_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = (mor_weight_txt.Value + eve_weight_txt.Value) / 2;
                // we build our query in the form page which has references to its controls
                int id = Convert.ToInt32(weightDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                string txtQuery = "UPDATE Weight SET AnimalId = '" + @weight_AnimalId.Value + "', WeightMorning = '" + @mor_weight_txt.Value + "', WeightEvening = '" + @eve_weight_txt.Value + "', WeightAverage = '" + average + "' WHERE Id ='" + id + "'";
                AnimalControls.Querydb(txtQuery);
                string query = "SELECT * FROM Weight";
                LoadData(query, weightDataGrid);

                MessageBox.Show("Weight record updated!");

                // refresh fields
                weight_AnimalId.Value = 0;
                mor_weight_txt.Value = 0;
                eve_weight_txt.Value = 0;
                avg_weight_lbl.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update weight record!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void del_weight_txt_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    // we build our query in the form page which has references to the its controls.
                    int id = Convert.ToInt32(weightDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "DELETE FROM Weight WHERE ID = '" + id + "' ";

                    DialogResult dialogResult = MessageBox.Show("Delete Animal '" + @weight_AnimalId.Value + "' weight reading?", "Are you sure?", MessageBoxButtons.YesNo);
                    if(dialogResult == DialogResult.Yes)
                    {
                        // we push the query to the AnimalControl class to process the query which links back to the connection class
                        AnimalControls.Querydb(txtQuery);
                        string query = "SELECT * FROM Weight";
                        LoadData(query, weightDataGrid);
                        // MessageBox.Show("Weight Reading deleted!");
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

        /* Waist tab */
        private void rec_waist_txt_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = (mor_waist_txt.Value + eve_waist_txt.Value) / 2;
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Waist (AnimalId, WaistMorning, WaistEvening, WaistAverage,  Date) VALUES ('" + waist_AnimalId_txt.Value + "','" + mor_waist_txt.Value + "','" + eve_waist_txt.Value + "','" + average + "','" + DateTime.Now.ToString("s") + "')";
                AnimalControls.Querydb(txtQuery);

                string query = "SELECT * FROM Waist";
                LoadData(query, waistDataGrid);

                // refresh fields
                waist_AnimalId_txt.Value = 0;
                mor_waist_txt.Value = 0;
                eve_waist_txt.Value = 0;
                avg_waist_txt.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to record Waist!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_waist_txt_Click(object sender, EventArgs e)
        {
            try
            {
                // calculate average of weight before entry into the db
                var average = (mor_waist_txt.Value + eve_waist_txt.Value) / 2;
               
                DialogResult dialogResult = MessageBox.Show("Update Animal '" + @waist_AnimalId_txt.Value + "' waist reading?", "Are you sure?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // we build our query in the form page which has references to its controls
                    int id = Convert.ToInt32(waistDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "UPDATE Waist SET AnimalId = '" + @waist_AnimalId_txt.Value + "', WaistMorning = '" + @mor_waist_txt.Value + "', WaistEvening = '" + @eve_waist_txt.Value + "', WaistAverage = '" + average + "' WHERE Id ='" + id + "'";
                    AnimalControls.Querydb(txtQuery);
                    string query = "SELECT * FROM Waist";
                    LoadData(query, waistDataGrid);
                }

                // refresh fields
                waist_AnimalId_txt.Value = 0;
                mor_waist_txt.Value = 0;
                eve_waist_txt.Value = 0;
                avg_waist_txt.Text = "Waiting...";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update waist record!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void del_waist_txt_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    // we build our query in the form page which has references to the its controls.
                    int id = Convert.ToInt32(waistDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                    string txtQuery = "DELETE FROM Waist WHERE ID = '" + id + "' ";

                    DialogResult dialogResult = MessageBox.Show("Delete Animal '" + @waist_AnimalId_txt.Value + "' waist reading?", "Are you sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // we push the query to the AnimalControl class to process the query which links back to the connection class
                        AnimalControls.Querydb(txtQuery);
                        string query = "SELECT * FROM Waist";
                        LoadData(query, waistDataGrid);
                        // MessageBox.Show("Weight Reading deleted!");
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
    }
}
