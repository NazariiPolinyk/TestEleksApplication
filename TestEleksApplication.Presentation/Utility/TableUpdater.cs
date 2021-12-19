using System.Threading.Tasks;
using System.Windows.Forms;
using TestEleksApplication.Presentation.Operations;

namespace TestEleksApplication.Presentation.Utility
{
    public static class TableUpdater
    {
        public static async Task UpdateTable(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            var students = await StudentsApiOperations.Get();
            foreach (var student in students)
            {
                dataGridView.Rows.Add(student.Id, student.FirstName, student.LastName);
            }
        }
    }
}
