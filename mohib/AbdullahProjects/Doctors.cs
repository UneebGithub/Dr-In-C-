using Mohib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbdullahProjectss
{
    public partial class Doctors : Form
    {
        public Doctors()
        {
            InitializeComponent();
            comboBox1.Items.Add("EARS");
            comboBox1.Items.Add("NOSE");
            comboBox1.Items.Add("THROAT");

          //  radioButton1.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
           // radioButton2.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);

            SetTextBox3Visibility();
        }

        MhddbmlDataContext db;

        private void button2_Click(object sender, EventArgs e)
        {
            db = new MhddbmlDataContext();

            try
            {
                string name = textBox2.Text;
                long pt_phone = Convert.ToInt64(textBox3.Text);
                string issue = comboBox1.SelectedItem.ToString();
                string med = textBox1.Text;
                int price = Convert.ToInt32(textBox6.Text);
                string dr = textBox4.Text;
                string date = textBox7.Text;
                long ph2 = Convert.ToInt64(textBox9.Text);

                Pt newPatient = new Pt
                {
                    FName = name,
                    phone = pt_phone,
                    issue = issue,
                    Madicin = med,
                    DR = dr,
                    price = price,
                    Date_PT = date,
                    Phone2nd = ph2
                };

                db.Pts.InsertOnSubmit(newPatient);

                
                Total newTotal = new Total
                {
                    Inc_Med = (int)pt_phone,
                    Dr_fees = price,
                    Datee = date
                };

                db.Totals.InsertOnSubmit(newTotal);

                db.SubmitChanges();

                MessageBox.Show("Patient record added successfully!");
                clear();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid input format: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            db = new MhddbmlDataContext();
            try
            {
                long searchPhone = Convert.ToInt64(textBox5.Text);

                var results = db.Pts.Where(p => p.phone == searchPhone || p.Phone2nd == searchPhone).ToList();

                if (results.Any())
                {
                    dataGridView1.DataSource = results;
                }
                else
                {
                    MessageBox.Show("No patient found with the given phone number.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid input format: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            db = new MhddbmlDataContext();
            try
            {
                var results = db.Meds_users.ToList();

                if (results.Any())
                {
                    dataGridView1.DataSource = results;
                }
                else
                {
                    MessageBox.Show("No income records found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving income records: " + ex.Message);
            }
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";
            textBox1.Focus();
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetTextBox3Visibility();
        }

        private void SetTextBox3Visibility()
        {/*
            if (radioButton1.Checked)
            {
                textBox3.Visible = false;
                textBox9.Visible = true;
            }
            else if (radioButton2.Checked)
            {
                textBox3.Visible = true;
                textBox9.Visible = false;
            }*/
        }

        private void Doctors_Load(object sender, EventArgs e)
        {
            SetTextBox3Visibility();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                long searchPhone = Convert.ToInt64(textBox5.Text);

                using (db = new MhddbmlDataContext())
                {
                    var patient = db.Pts.FirstOrDefault(p => p.phone == searchPhone || p.Phone2nd == searchPhone);

                    if (patient != null)
                    {
                      
                        patient.FName = textBox2.Text; 
                        patient.phone = Convert.ToInt64(textBox3.Text); 
                        patient.issue = comboBox1.SelectedItem.ToString(); 
                        patient.Madicin = textBox1.Text; 
                        patient.DR = textBox4.Text; 
                        patient.price = Convert.ToInt32(textBox6.Text); 
                        patient.Date_PT = textBox7.Text; 

                       
                       /* if (radioButton1.Checked)
                        {
                            patient.Phone2nd = Convert.ToInt64(textBox9.Text);
                        }
                        else if (radioButton2.Checked)
                        {
                            patient.Phone2nd = null; 
                        }*/

                        db.SubmitChanges();
                        MessageBox.Show("Patient record updated successfully!");
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("No patient found with the given phone number.");
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid input format: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating patient record: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                long searchPhone = Convert.ToInt64(textBox5.Text);

                using (db = new MhddbmlDataContext())
                {
                    var patient = db.Pts.FirstOrDefault(p => p.phone == searchPhone || p.Phone2nd == searchPhone);

                    if (patient != null)
                    {
                        db.Pts.DeleteOnSubmit(patient);
                        db.SubmitChanges();
                        MessageBox.Show("Patient record deleted successfully!");
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("No patient found with the given phone number.");
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid input format: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting patient record: " + ex.Message);
            }
        }

    }
}
