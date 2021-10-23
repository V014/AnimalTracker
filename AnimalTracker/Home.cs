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
            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            // style datagrind
            
        }

/* This section pulls data from the database and fills the datagrids when called */
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

        /* this section loads the data from the pull function on runtime */
        // load animal table when program starts
        private void Home_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Animal";
            LoadData(query, animalDataGrid);
            animalDataGrid.Columns[0].Visible = false; // hide ID column during runtime
        }

        /* this section fills and resets the fields after user interaction */
        // fill textboxes with data when user clicks a cell
        public void animalDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = animalDataGrid.Rows[e.RowIndex];
                name_txt.Text = row.Cells[1].Value.ToString();
                species_txt.Text = row.Cells[4].Value.ToString();
                gender_txt.Text = row.Cells[2].Value.ToString();
                age_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                name_txt.Text = " ";
                name_txt.Focus();
                gender_txt.Text = " ";
                age_txt.Value = 0;
                species_txt.Text = " ";
            }
        }

        // fill textboxes with data when user clicks a cell
        public void meallDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = mealDataGrid.Rows[e.RowIndex];
                meal_txt.Text = row.Cells[1].Value.ToString();
                calories_txt.Value = Convert.ToInt32(row.Cells[2].Value.ToString());
                portion_txt.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                AnimalId_txt.Value = Convert.ToInt32(row.Cells[5].Value.ToString());
            }
            catch (Exception) // reset textboxes
            {
                MessageBox.Show("Empty field");
                // refresh fields
                meal_txt.Text = " ";
                meal_txt.Focus();
                calories_txt.Value = 0;
                portion_txt.Value = 0;
                AnimalId_txt.Value = 0;
            }
        }

/* this section queries the database after user interaction */
        // updates animal list
        public void update_rec_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to its controls
                int id = Convert.ToInt32(animalDataGrid.CurrentRow.Cells[0].Value.ToString()); // collect id from selected row
                string txtQuery = "UPDATE Animal SET Name = '"+ @name_txt.Text +"', Age = '"+ @age_txt.Value +"', Gender = '"+ @gender_txt.Text +"', Species = '"+ @species_txt.Text +"' WHERE Id ='"+ id +"'";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Details updated!");
                string query = "SELECT * FROM Animal";
                LoadData(query, animalDataGrid);

                // refresh fields
                name_txt.Text = " ";
                name_txt.Focus();
                gender_txt.Text = " ";
                age_txt.Value = 0;
                species_txt.Text = " ";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // adds animal to database
        private void add_rec_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Animal (Name, Gender, Age, Species) VALUES ('"+ name_txt.Text +"','"+ gender_txt.Text +"','"+ age_txt.Text +"','"+ species_txt.
                Text +"')";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Animal added!");
                string query = "SELECT * FROM Animal";
                LoadData(query, animalDataGrid);

                // refresh fields
                name_txt.Text = " ";
                name_txt.Focus();
                gender_txt.Text = " ";
                age_txt.Value = 0;
                species_txt.Text = " ";
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to insert!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // deletes animal from database
        private void delete_rec_btn_Click(object sender, EventArgs e)
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

        // adds a meal to the database
        private void add_meal_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // we build our query in the form page which has references to the its controls.
                string txtQuery = "INSERT INTO Meal (Name, Calories, Portion, AnimalId) VALUES ('"+ meal_txt.Text +"','"+ calories_txt.Text +"','"+ portion_txt.Text +"','"+ AnimalId_txt.
                Text +"')";

                // we push the query to the AnimalControl class to process the query which links back to the connection class
                AnimalControls.Querydb(txtQuery);
                MessageBox.Show("Meal added!");
                string query = "SELECT * FROM Animal";
                LoadData(query, animalDataGrid);

                // refresh fields
                meal_txt.Text = " ";
                meal_txt.Focus();
                portion_txt.Value = 0;
                calories_txt.Value = 0;
                AnimalId_txt.Value = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to insert!", "Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this displays the data in the meals table when meals tab selected
            if(materialTabControl.SelectedTab == meal_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Meal";
                LoadData(query, mealDataGrid);
                mealDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }
            // this displays the data in the meals table when meals tab selected
            if (materialTabControl.SelectedTab == animal_page)
            {
                // Returns all data from the meals table
                string query = "SELECT * FROM Animal";
                LoadData(query, animalDataGrid);
                mealDataGrid.Columns[0].Visible = false; // hide ID column during runtime
            }

        }
    }
}
