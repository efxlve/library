using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Library_Project
{
    public partial class frmLibrary : Form
    {
        public frmLibrary()
        {
            InitializeComponent();
        }

        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= .\\Bookshelf.mdb");

        private void list()
        {
            DataTable dataTable = new DataTable();
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("SELECT * FROM Books", connection);
            oleDbDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void clear()
        {
            txtBookID.Text = "";
            txtBookName.Text = "";
            txtAuthor.Text = "";
            cmbType.Text = "";
            txtPages.Text = "";
            rbUsed.Checked = false;
            rbZero.Checked = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            list();        
        }

        string situation = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("INSERT INTO Books (BookName, Author, Type, Pages, Situation) VALUES (@p1, @p2, @p3, @p4, @p5)", connection);
                command.Parameters.AddWithValue("@p1", txtBookName.Text);
                command.Parameters.AddWithValue("@p2", txtAuthor.Text);
                command.Parameters.AddWithValue("@p3", cmbType.Text);
                command.Parameters.AddWithValue("@p4", txtPages.Text);
                command.Parameters.AddWithValue("@p5", situation);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Book added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                list();
                clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while adding book!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbUsed_CheckedChanged(object sender, EventArgs e)
        {
            situation = "0";
        }

        private void rbZero_CheckedChanged(object sender, EventArgs e)
        {
            situation = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int chosen = dataGridView1.SelectedCells[0].RowIndex;
            txtBookID.Text = dataGridView1.Rows[chosen].Cells[0].Value.ToString();
            txtBookName.Text = dataGridView1.Rows[chosen].Cells[1].Value.ToString();
            txtAuthor.Text = dataGridView1.Rows[chosen].Cells[2].Value.ToString();
            cmbType.Text = dataGridView1.Rows[chosen].Cells[3].Value.ToString();
            txtPages.Text = dataGridView1.Rows[chosen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[chosen].Cells[5].Value.ToString() == "True")
            {
                rbZero.Checked = true;
            }
            else
            {
                rbUsed.Checked = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("DELETE FROM Books WHERE BookID = @p1", connection);
                command.Parameters.AddWithValue("@p1", txtBookID.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Book deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                list();
                clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while deleting book!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("UPDATE Books SET BookName = @p1, Author = @p2, Type = @p3, Pages = @p4, Situation = @p5 WHERE BookID = @p6", connection);
                command.Parameters.AddWithValue("@p1", txtBookName.Text);
                command.Parameters.AddWithValue("@p2", txtAuthor.Text);
                command.Parameters.AddWithValue("@p3", cmbType.Text);
                command.Parameters.AddWithValue("@p4", txtPages.Text);
                command.Parameters.AddWithValue("@p5", situation);
                command.Parameters.AddWithValue("@p6", txtBookID.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Book updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                list();
                clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while updating book!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
