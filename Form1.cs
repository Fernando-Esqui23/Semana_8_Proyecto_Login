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

namespace Semana_8_Proyecto_Login
{
   
    public partial class FormUsuarios : Form
    {
        // Crear la variable para la conexión
        public OleDbConnection miconexion;
        // Crear la variable para saber cuál actualizar
        public string usuario_modificar;
        public FormUsuarios()
        {
            //Crear cadena de conexion a la base
            miconexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\sistema.mdb");
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FormLogin fl = new FormLogin();
            fl.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaDataSet.usuarios' Puede moverla o quitarla según sea necesario.
            this.usuariosTableAdapter.Fill(this.sistemaDataSet.usuarios);

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtPassword.Text = "";
            lsNivel.Text = "Seleccione Nivel";
            txtUsuario.Focus();
            btnNuevo.Visible = false;
            btnGuardar.Visible = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            btnActualizar.Visible = true;

            txtUsuario.Enabled = true;
            txtPassword.Enabled = true;
            lsNivel.Enabled = true;
            txtUsuario.Focus();
            btnModificar.Visible = false;
            btnActualizar.Visible = true;
            usuario_modificar = txtUsuario.Text.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand guardar = new OleDbCommand();
                miconexion.Open();
                guardar.Connection = miconexion;
                guardar.CommandType = CommandType.Text;
                guardar.CommandText = "INSERT INTO usuarios ([usuario], [password],[level]) Values('" + txtUsuario.Text.ToString() + "','" + txtPassword.Text.ToString() + "'," + lsNivel.Text.ToString() + ")";
                guardar.ExecuteNonQuery();
                miconexion.Close();
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                //Deshabilitar campos, se activan al crear nuevo registro
                txtUsuario.Enabled = false;
                txtPassword.Enabled = false;
                lsNivel.Enabled = false;
                btnNuevo.Focus();
                //Mensaje que se guardó correctamente
                MessageBox.Show("Usuario agregado con éxito", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.usuariosTableAdapter.Fill(this.sistemaDataSet.usuarios);
                this.usuariosBindingSource.MoveLast();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

           
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand actualizar = new OleDbCommand();
                miconexion.Open();
                actualizar.Connection = miconexion;
                actualizar.CommandType = CommandType.Text;
                string nom = txtUsuario.Text.ToString();
                string cla = txtPassword.Text.ToString();
                string niv = lsNivel.Text;
                //Podemos actualizar todos los campos de una vez
                actualizar.CommandText = "UPDATE usuarios SET usuario = '" + nom + "', password = '" + cla + "',level = '" + niv + "' WHERE usuario = '" + usuario_modificar + "'";
                 actualizar.ExecuteNonQuery();
                miconexion.Close();
                btnModificar.Visible = true;
                btnActualizar.Visible = false;
                txtUsuario.Enabled = false;
                txtPassword.Enabled = false;
                lsNivel.Enabled = false;
                //Mensaje que se guardó correctamente
                MessageBox.Show("Usuario actualizado con éxito", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            this.usuariosTableAdapter.Fill(this.sistemaDataSet.usuarios);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            this.usuariosBindingSource.MoveFirst();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            this.usuariosBindingSource.MovePrevious();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            this.usuariosBindingSource.MoveNext();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            this.usuariosBindingSource.MoveLast();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand eliminar = new OleDbCommand();
                miconexion.Open();
                eliminar.Connection = miconexion;
                eliminar.CommandType = CommandType.Text;
                eliminar.CommandText = "DELETE FROM usuarios WHERE usuario = '" + txtUsuario.Text.ToString() + "'";
                eliminar.ExecuteNonQuery();
                this.usuariosBindingSource.MoveNext();
                miconexion.Close();
                //Mensaje que se guardó correctamente
                MessageBox.Show("Usuario eliminado con éxito", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.usuariosTableAdapter.Fill(this.sistemaDataSet.usuarios);
                this.usuariosBindingSource.MovePrevious();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
