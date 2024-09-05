using Mohib;
using System;
using System.Data.Linq;
using System.Linq;
using System.Windows.Forms;

namespace AbdullahProjectss
{
    public partial class PT : Form
    {
        public int currentTotal; 

        private MhddbmlDataContext db;

        public PT()
        {
            InitializeComponent();
            show_cb();
        }

        private void show_cb()
        {
            using (db = new MhddbmlDataContext())
            {
                var medicines = db.Meds.Select(m => m.Med_Name).ToList();
                comboBox1.Items.AddRange(medicines.ToArray());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (db = new MhddbmlDataContext())
            {
                try
                {
                    string medName = textBox11.Text;
                    int price = Convert.ToInt32(textBox8.Text);
                    int qnt = Convert.ToInt32(textBox9.Text);
                    int id = Convert.ToInt32(textBox10.Text);

                    Med newMed = new Med
                    {
                        Med_Name = medName,
                        price = price,
                        Qnt = qnt,
                        id = id
                    };

                    db.Meds.InsertOnSubmit(newMed);
                    db.SubmitChanges();

                    MessageBox.Show("Medicine record added successfully!");
                    comboBox1.Items.Add(newMed.Med_Name);

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
        }

        private void clear()
        {
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox8.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedMed = comboBox1.Text;
            if (!string.IsNullOrWhiteSpace(selectedMed) && !listBox1.Items.Contains(selectedMed))
            {
                listBox1.Items.Add(selectedMed);
            }

            using (db = new MhddbmlDataContext())
            {
                var selectedMedicine = db.Meds.FirstOrDefault(m => m.Med_Name == selectedMed);
                if (selectedMedicine != null)
                {
                    currentTotal = string.IsNullOrEmpty(textBox6.Text) ? 0 : Convert.ToInt32(textBox6.Text);

                    currentTotal += (int)selectedMedicine.price;
                    textBox6.Text = currentTotal.ToString();
                }
            }

            textBox1.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (db = new MhddbmlDataContext())
            {
                try
                {
                    long searchPhone = Convert.ToInt64(textBox7.Text);

                    var patient = db.Pts.FirstOrDefault(p => p.phone == searchPhone || p.Phone2nd == searchPhone);

                    if (patient != null)
                    {
                        textBox1.Text = patient.FName;
                        textBox2.Text = patient.phone.ToString();
                        textBox3.Text = patient.issue;
                        textBox4.Text = patient.Madicin;
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (db = new MhddbmlDataContext())
                {
                    long searchPhone = Convert.ToInt64(textBox2.Text);
                    var patient = db.Pts.FirstOrDefault(p => p.phone == searchPhone || p.Phone2nd == searchPhone);

                    if (patient != null)
                    {
                        Meds_user tal = new Meds_user
                        {
                            id = (int)patient.phone,
                            UName = patient.FName,
                            Med_Name = patient.Madicin,
                            price = currentTotal,
                            
                        };

                        db.Meds_users.InsertOnSubmit(tal);
                        db.SubmitChanges();

                        MessageBox.Show("Total record saved successfully.");
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
                MessageBox.Show("An error occurred while saving Total record: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
