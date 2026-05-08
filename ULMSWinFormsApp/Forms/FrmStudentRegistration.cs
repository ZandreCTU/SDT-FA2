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
    public partial class FrmStudentRegistration : Form
    {
        // Static list to track registered students during the current session
        private static List<Student> registeredStudents = new List<Student>();

        public FrmStudentRegistration()
        {
            InitializeComponent();
        }

        private void btnSaveStudent_Click(object sender, EventArgs e)
        {
            // Validate input
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                string studentId = txtStudentId.Text.Trim();
                string fullName = txtFullName.Text.Trim();
                string email = txtEmail.Text.Trim();
                int age = int.Parse(txtAge.Text.Trim());
                string programme = cmbProgramme.Text.Trim();

                // Check if student with same ID and Programme already exists
                if (StudentAlreadyRegistered(studentId, programme))
                {
                    MessageBox.Show("Student ID " + studentId + " is already registered for " + programme + ".", "Duplicate Registration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Student student = new Student
                {
                    StudentId = studentId,
                    FullName = fullName,
                    Email = email,
                    Age = age,
                    Programme = programme
                };

                // Add to the registered students list
                registeredStudents.Add(student);

                txtStudentOutput.Text =
                    "Student saved successfully!" + Environment.NewLine +
                    "Student ID: " + student.StudentId + Environment.NewLine +
                    "Full Name: " + student.FullName + Environment.NewLine +
                    "Email: " + student.Email + Environment.NewLine +
                    "Age: " + student.Age + Environment.NewLine +
                    "Programme: " + student.Programme + Environment.NewLine +
                    Environment.NewLine +
                    "Total students registered: " + registeredStudents.Count;

                MessageBox.Show("Student registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the form
                btnClearStudent_Click(null, null);
            }
            catch (FormatException)
            {
                MessageBox.Show("Age must be a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAge.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Check if a student is already registered for the same programme
        /// </summary>
        private bool StudentAlreadyRegistered(string studentId, string programme)
        {
            foreach (Student student in registeredStudents)
            {
                if (student.StudentId == studentId && student.Programme == programme)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validate that all required fields are filled correctly
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                MessageBox.Show("Please enter a Student ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter the student's full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter an email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Please enter the student's age.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age < 0 || age > 150)
            {
                MessageBox.Show("Please enter a valid age between 0 and 150.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            if (cmbProgramme.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a programme.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProgramme.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Simple email validation
        /// </summary>
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnClearStudent_Click(object sender, EventArgs e)
        {
            txtStudentId.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtAge.Clear();
            cmbProgramme.SelectedIndex = -1;
            txtStudentOutput.Clear();
            txtStudentId.Focus();
        }

        //Add Back button to return to dashboard
        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
