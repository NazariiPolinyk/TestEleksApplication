using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestEleksApplication.Presentation.Operations;
using TestEleksApplication.Presentation.Utility;

namespace TestEleksApplication.Presentation.Forms
{
    public partial class StudentsForm : Form
    {
        public StudentsForm()
        {
            InitializeComponent();
            try
            {
                var students = Task.Run(async () => await StudentsApiOperations.Get()).Result;
                foreach (var student in students)
                {
                    StudentsDataGridView.Rows.Add(student.Id, student.FirstName, student.LastName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddStudentBtn_Click(object sender, EventArgs e)
        {
            EditStudentForm editStudentForm = new EditStudentForm();
            editStudentForm.Owner = this;
            editStudentForm.Show();
        }

        private void UpdateStudentBtn_Click(object sender, EventArgs e)
        {
            if (StudentsDataGridView.CurrentCell == null)
            {
                MessageBox.Show("Choose row, that you want to update!");
            }
            else
            {
                int currentStudentId = Convert.ToInt32(StudentsDataGridView.Rows[StudentsDataGridView.CurrentCell.RowIndex].Cells[0].Value);
                EditStudentForm editStudentForm = new EditStudentForm(currentStudentId);
                editStudentForm.Owner = this;
                editStudentForm.Show();
            }
        }

        private async void DeleteStudentBtn_Click(object sender, EventArgs e)
        {
            if (StudentsDataGridView.CurrentCell == null)
            {
                MessageBox.Show("Choose row, that you want to delete!");
            }
            else
            {
                try
                {
                    int currentStudentId = Convert.ToInt32(StudentsDataGridView.Rows[StudentsDataGridView.CurrentCell.RowIndex].Cells[0].Value);
                    await StudentsApiOperations.Delete(currentStudentId);
                    await TableUpdater.UpdateTable(StudentsDataGridView);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
