using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestEleksApplication.DataLayer.Models;
using TestEleksApplication.Presentation.Operations;
using TestEleksApplication.Presentation.Utility;

namespace TestEleksApplication.Presentation.Forms
{
    public partial class EditStudentForm : Form
    {
        private Student _student;
        public EditStudentForm()
        {
            InitializeComponent();
            _student = new Student();
        }
        public EditStudentForm(int id)
        {
            InitializeComponent();
            _student = new Student { Id = id };
            var student = Task.Run(async () => await StudentsApiOperations.Get(id)).Result;
            FirstNameTextBox.Text = student.FirstName;
            LastNameTextBox.Text = student.LastName;
            StudentIdLabel.Text = "Student id - " + student.Id;
        }

        private async void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (FirstNameTextBox.Text != string.Empty && LastNameTextBox.Text != string.Empty)
            {
                _student.FirstName = FirstNameTextBox.Text;
                _student.LastName = LastNameTextBox.Text;
                try
                {
                    if (_student.Id > 0)
                    {
                        await StudentsApiOperations.Put(_student);
                    }
                    else
                    {
                        await StudentsApiOperations.Post(_student);
                    }
                    var control = this.Owner.Controls.Find("StudentsDataGridView", true);
                    DataGridView dataGridView = (DataGridView)control[0];
                    await TableUpdater.UpdateTable(dataGridView);
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please, fill the form!");
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
