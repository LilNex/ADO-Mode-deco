using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ADO_Mode_deco
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-CLLLEH7\SQLEXPRESS;Initial Catalog=Tp_deco;Integrated Security=True");
        DataSet ds = new DataSet("GestionClient");
        SqlDataAdapter sda;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sda = new SqlDataAdapter("select * from client",conn);
            sda.Fill(ds,"client");

            // Affecte le dataset au datasource du combo box
            comboBox1.DataSource = ds.Tables[0];
            // Specifie l'attribut à afficher dans le combobox
            comboBox1.DisplayMember = "Nom";


            // 2eme methode
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox2.Items.Add(ds.Tables[0].Rows[i][0]);
            }


            lblCount.Text = ds.Tables[0].Rows.Count.ToString();
        }

        private void btnAfficher_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            DataRow dr = ds.Tables["client"].NewRow();
            dr[0] = txtId.Text;
            dr[1] = txtNom.Text;
            dr[2] = txtPrenom.Text;


            foreach (DataRow row in ds.Tables["client"].Rows)
            {
                if (row[0].ToString() == dr[0].ToString())
                {
                    MessageBox.Show("Id Existant");
                    return;
                }
            }

            ds.Tables["client"].Rows.Add(dr);

            MessageBox.Show("Client Ajouté avec HHHHHH! ");
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            sda.Update(ds,"client");
            
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            //DataRow dr = ds.Tables[0].Select("Id=" + txtId.Text).First();
            //dr[1] = txtNom.Text;
            //dr[2] = txtPrenom.Text;
            //txtNom.Text = dr[1].ToString();
            //txtPrenom.Text = dr[2].ToString();
            int pos = rechercher();
            if (pos != -1) {
                ds.Tables[0].Rows[pos][1] = txtNom.Text;
                ds.Tables[0].Rows[pos][2] = txtPrenom.Text;
            }

            

        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            //try
            //{
                //DataRow dr = ds.Tables[0].Select("Id=" + txtId.Text).First();
                int i = rechercher();
                if (i != -1)
                {
                    txtNom.Text = ds.Tables[0].Rows[i][1].ToString();
                    txtPrenom.Text = ds.Tables[0].Rows[i][2].ToString();
                    return;
                }
                else {
                    txtNom.Text = "";
                    txtPrenom.Text = "";
                    MessageBox.Show("Id inexistant");
                }
                


            //}
            //catch (InvalidOperationException ex)
            //{
            //    MessageBox.Show("Id inexistant");
            //}
            
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            //ds.Tables[0].Select("Id=" + txtId.Text)[0].Delete();
            //ds.Tables[0].Select("Id=" + txtId.Text).First().Delete();

            int pos = rechercher();
            if(pos != -1)
            {
                ds.Tables[0].Rows[pos].Delete();
                MessageBox.Show("Client HHHHHHHH");

            }
            else
            {
                MessageBox.Show("Id inexistant");
            }


        }

        public int rechercher()
        {
            int pos = -1;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                if (row[0].ToString() == txtId.Text)
                {
                    pos = i;
                }
            }
            return pos;
        }

        static int pos = 0;

        public void naviguer()
        {

            txtId.Text = ds.Tables[0].Rows[pos][0].ToString();
            txtNom.Text = ds.Tables[0].Rows[pos][1].ToString();
            txtPrenom.Text = ds.Tables[0].Rows[pos][2].ToString();

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            naviguer();
        }

        private void btnPrec_Click(object sender, EventArgs e)
        {
            try
            {
                pos--;
                naviguer();
            }
            catch (Exception)
            {
                pos = 0;
                naviguer();

            }
            

        }
        //x
        private void btnSuiv_Click(object sender, EventArgs e)
        {
            try
            {
                pos++;
                naviguer();
            }
            catch (Exception)
            {
                pos = ds.Tables[0].Rows.Count - 1;
                naviguer();

            }

        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            pos = ds.Tables[0].Rows.Count - 1;
            naviguer();

        }
    }
}
