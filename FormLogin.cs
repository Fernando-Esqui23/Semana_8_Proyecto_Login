using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Semana_8_Proyecto_Login
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnAccess_Click(object sender, EventArgs e)
        {
            try
            {
                //variables globales
                Variables cadenas = new Variables();
                //Creado la variable para la nueva conexion
                OleDbConnection conexion_access = new OleDbConnection();
                //Cadena de conexión para la base de datos
                //Se recomienda generar la cadena de conexion para evitar errores
                //conexion_access.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\sistema.accdb;Persist Security Info=False;";
                conexion_access.ConnectionString = @cadenas.conexionAcces;
                //Abriendo conexion
                conexion_access.Open();
                //Consulta a tabla de usuarios en la base de datos
                //Para encontrar fila que tiene los datos del usuario y clave ingresados
                OleDbDataAdapter consulta = new OleDbDataAdapter("SELECT * FROM usuarios", conexion_access);
                //OleDbDataReader reader = command.ExecuteReader();
                DataSet resultado = new DataSet();
                consulta.Fill(resultado);
                foreach (DataRow registro in resultado.Tables[0].Rows)
                {
                    if ((txtUsuario.Text == registro["usuario"].ToString()) && (txtPassword.Text == registro["password"].ToString()))
                    {
                        FormUsuarios fu = new FormUsuarios();
                        fu.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Error de usuario o clave de acceso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                } //Cierre de ciclo for
                conexion_access.Close();
            } //Cierre de Try
              //Si la conexion falla muestra mensaje de error
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                //en caso que usuario y clave sean incorrectos mostrar mensaje de error
                MessageBox.Show("Error de usuario o clave de acceso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Focus();
            }
            //Finalizando la conexión
        }

        private void btnMySQL_Click(object sender, EventArgs e)
        {
            //variables globales
            Variables cadenas = new Variables();
            string connStr = cadenas.conexionMySql;
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sql = "SELECT usuario, status FROM usuarios WHERE usuario='" + txtUsuario.Text.Trim() + "' and password='" + txtPassword.Text.Trim() + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                int contador = 0;
                while (rdr.Read())
                {
                    contador++;
                }
                rdr.Close();

                if (contador > 0)
                {
                    FormUsuarios fu = new FormUsuarios();
                    fu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error de usuario o clave de acceso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
