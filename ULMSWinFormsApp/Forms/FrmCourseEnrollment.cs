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
    public partial class FrmCourseEnrollment : Form
    {
        // Static list to track enrollments during the current session
        private static List<Enrollment> enrolledCourses = new List<Enrollment>();

        public FrmCourseEnrollment()
        {
            InitializeComponent();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            // Validate input
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                string studentId = txtEnrollStudentId.Text.Trim();
                string studentName = txtEnrollStudentName.Text.Trim();
                string courseName = cmbCourse.Text.Trim();
                string semester = cmbSemester.Text.Trim();

                // Check if student is already enrolled in the same course
                if (StudentAlreadyEnrolled(studentId, courseName, semester))
                {
                    MessageBox.Show("Student ID " + studentId + " is already enrolled in " + courseName + " for " + semester + ".", "Duplicate Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Enrollment enrollment = new Enrollment
                {
                    StudentId = studentId,
                    StudentName = studentName,
                    CourseName = courseName,
                    Semester = semester
                };

                // Add to the enrollments list
                enrolledCourses.Add(enrollment);

                txtEnrollmentOutput.Text =
                    "Enrollment completed successfully!" + Environment.NewLine +
                    "Student ID: " + enrollment.StudentId + Environment.NewLine +
                    "Student Name: " + enrollment.StudentName + Environment.NewLine +
                    "Course: " + enrollment.CourseName + Environment.NewLine +
                    "Semester: " + enrollment.Semester + Environment.NewLine +
                    Environment.NewLine +
                    "Total enrollments processed: " + enrolledCourses.Count;

                MessageBox.Show("Student enrolled successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the form
                btnClearEnrollment_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// Check if a student is already enrolled in the same course for the same semester
        private bool StudentAlreadyEnrolled(string studentId, string courseName, string semester)
        {
            foreach (Enrollment enrollment in enrolledCourses)
            {
                if (enrollment.StudentId == studentId && enrollment.CourseName == courseName && enrollment.Semester == semester)
                {
                    return true;
                }
            }
            return false;
        }

        /// Validate that all required fields are filled correctly
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtEnrollStudentId.Text))
            {
                MessageBox.Show("Please enter a Student ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnrollStudentId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEnrollStudentName.Text))
            {
                MessageBox.Show("Please enter the student's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnrollStudentName.Focus();
                return false;
            }

            if (cmbCourse.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cmbCourse.Text))
            {
                MessageBox.Show("Please select a course.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCourse.Focus();
                return false;
            }

            if (cmbSemester.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cmbSemester.Text))
            {
                MessageBox.Show("Please select a semester.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSemester.Focus();
                return false;
            }

            return true;
        }

        private void btnClearEnrollment_Click(object sender, EventArgs e)
        {
            txtEnrollStudentId.Clear();
            txtEnrollStudentName.Clear();
            cmbCourse.SelectedIndex = -1;
            cmbSemester.SelectedIndex = -1;
            txtEnrollmentOutput.Clear();
            txtEnrollStudentId.Focus();
        }

        private void btnBackEnrollment_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
