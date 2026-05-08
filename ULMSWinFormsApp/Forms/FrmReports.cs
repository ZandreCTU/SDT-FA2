using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ULMSWinFormsApp.Forms
{
    public partial class FrmReports : Form
    {
        public FrmReports()
        {
            InitializeComponent();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            // Validate input
            if (!ValidateInput())
            {
                return;
            }

            string reportType = cmbReportType.Text;
            string studentId = txtReportStudentId.Text.Trim();

            StringBuilder report = new StringBuilder();

            report.AppendLine("===== ULMS REPORT =====");
            report.AppendLine("Report Type: " + reportType);
            report.AppendLine("Student ID Filter: " + studentId);
            report.AppendLine("Generated On: " + DateTime.Now);
            report.AppendLine();

            if (reportType == "Student Summary Report")
            {
                report.AppendLine("Student Name: John Doe");
                report.AppendLine("Programme: Software Engineering");
                report.AppendLine("Status: Active");
            }
            else if (reportType == "Marks Report")
            {
                double subject1 = 78;
                double subject2 = 85;
                double subject3 = 80;
                double average = (subject1 + subject2 + subject3) / 3;

                report.AppendLine("Subject 1: " + subject1);
                report.AppendLine("Subject 2: " + subject2);
                report.AppendLine("Subject 3: " + subject3);
                report.AppendLine("Average: " + average.ToString("F2"));
            }
            else if (reportType == "Enrollment Report")
            {
                report.AppendLine("Course 1: Programming 1");
                report.AppendLine("Course 2: Database Systems");
                report.AppendLine("Semester: Semester 1");
            }
            else
            {
                report.AppendLine("No report type selected.");
            }

            txtReportOutput.Text = report.ToString();
        }

        /// Validate that required fields are filled
        private bool ValidateInput()
        {
            if (cmbReportType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a report type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbReportType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtReportStudentId.Text))
            {
                MessageBox.Show("Please enter a Student ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReportStudentId.Focus();
                return false;
            }

            return true;
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            cmbReportType.SelectedIndex = -1;
            txtReportStudentId.Clear();
            txtReportOutput.Clear();
            txtReportStudentId.Focus();
        }

        private void btnBackReport_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
