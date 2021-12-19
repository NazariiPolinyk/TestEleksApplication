using System;
using System.Threading;
using System.Windows.Forms;
using TestEleksApplication.Presentation.Operations;
using TestEleksApplication.Services.AuthenticationService;

namespace TestEleksApplication.Presentation.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            if(LoginTextBox.Text != string.Empty && PasswordTextBox.Text != string.Empty)
            {
                try
                {
                    await UsersApiOperations.Post(new AuthenticateModel { Login = LoginTextBox.Text, Password = PasswordTextBox.Text });
                    StudentsForm studentsForm = new StudentsForm();
                    studentsForm.Show();
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
