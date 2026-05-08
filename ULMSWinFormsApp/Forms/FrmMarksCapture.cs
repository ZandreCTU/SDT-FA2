using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ULMSWinFormsApp.Models;

namespace ULMSWinFormsApp.Forms
{
    public partial class FrmMarksCapture : Form
    {
        // Static list to track marks records during the current session
        private static List<MarkRecord> capturedMarks = new List<MarkRecord>();

        public FrmMarksCapture()
        {
            InitializeComponent();
        }

        private void btnCalculateResults_Click(object sender, EventArgs e)
        {
            // Validate input
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                string studentId = txtMarkStudentId.Text.Trim();
                string studentName = txtMarkStudentName.Text.Trim();
                double subject1 = double.Parse(txtSubject1.Text.Trim());
                double subject2 = double.Parse(txtSubject2.Text.Trim());
                double subject3 = double.Parse(txtSubject3.Text.Trim());

                // Validate that marks are within valid range (0-100)
                if (!AreMarksValid(subject1, subject2, subject3))
                {
                    MessageBox.Show("All marks must be between 0 and 100.", "Invalid Marks", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if marks for this student already exist
                if (MarksAlreadyRecorded(studentId))
                {
                    MessageBox.Show("Marks for Student ID " + studentId + " have already been recorded.", "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calculate average correctly
                double average = (subject1 + subject2 + subject3) / 3;

                // Determine result status
                string resultStatus = average >= 50 ? "PASS" : "FAIL";

                MarkRecord record = new MarkRecord
                {
                    StudentId = studentId,
                    StudentName = studentName,
                    Subject1 = subject1,
                    Subject2 = subject2,
                    Subject3 = subject3,
                    Average = average,
                    ResultStatus = resultStatus
                };

                // Add to the marks list
                capturedMarks.Add(record);

                txtMarksOutput.Text =
                    "Marks processed successfully!" + Environment.NewLine +
                    "Student ID: " + record.StudentId + Environment.NewLine +
                    "Student Name: " + record.StudentName + Environment.NewLine +
                    "Subject 1: " + record.Subject1.ToString("F2") + Environment.NewLine +
                    "Subject 2: " + record.Subject2.ToString("F2") + Environment.NewLine +
                    "Subject 3: " + record.Subject3.ToString("F2") + Environment.NewLine +
                    "Average: " + record.Average.ToString("F2") + Environment.NewLine +
                    "Final Result: " + record.ResultStatus + Environment.NewLine +
                    Environment.NewLine +
                    "Total marks recorded: " + capturedMarks.Count;

                MessageBox.Show("Marks recorded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the form
                btnClearMarks_Click(null, null);
            }
            catch (FormatException)
            {
                MessageBox.Show("All marks must be valid numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// Check if marks for a student have already been recorded
        private bool MarksAlreadyRecorded(string studentId)
        {
            foreach (MarkRecord marks in capturedMarks)
            {
                if (marks.StudentId == studentId)
                {
                    return true;
                }
            }
            return false;
        }

        /// Validate that marks are within the valid range (0-100)
        private bool AreMarksValid(double subject1, double subject2, double subject3)
        {
            return subject1 >= 0 && subject1 <= 100 &&
                   subject2 >= 0 && subject2 <= 100 &&
                   subject3 >= 0 && subject3 <= 100;
        }

        /// Validate that all required fields are filled correctly
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMarkStudentId.Text))
            {
                MessageBox.Show("Please enter a Student ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMarkStudentId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMarkStudentName.Text))
            {
                MessageBox.Show("Please enter the student's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMarkStudentName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSubject1.Text))
            {
                MessageBox.Show("Please enter marks for Subject 1.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject1.Focus();
                return false;
            }

            if (!double.TryParse(txtSubject1.Text, out _))
            {
                MessageBox.Show("Subject 1 marks must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSubject2.Text))
            {
                MessageBox.Show("Please enter marks for Subject 2.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject2.Focus();
                return false;
            }

            if (!double.TryParse(txtSubject2.Text, out _))
            {
                MessageBox.Show("Subject 2 marks must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject2.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSubject3.Text))
            {
                MessageBox.Show("Please enter marks for Subject 3.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject3.Focus();
                return false;
            }

            if (!double.TryParse(txtSubject3.Text, out _))
            {
                MessageBox.Show("Subject 3 marks must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject3.Focus();
                return false;
            }

            return true;
        }

        private void btnClearMarks_Click(object sender, EventArgs e)
        {
            txtMarkStudentId.Clear();
            txtMarkStudentName.Clear();
            txtSubject1.Clear();
            txtSubject2.Clear();
            txtSubject3.Clear();
            txtMarksOutput.Clear();
            txtMarkStudentId.Focus();
        }

        private void btnBackMarks_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
