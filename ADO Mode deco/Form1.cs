﻿using System;
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


        DataSet ds = new DataSet();
        SqlDataAdapter sda;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sda = new SqlDataAdapter("select * from client",conn);
            sda.Fill(ds,"client");
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Nom";
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

            MessageBox.Show("Client Ajouté avec succes! ");
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            sda.Update(ds,"client");
            
        }
    }
}
