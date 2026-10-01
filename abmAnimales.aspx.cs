using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace desempeño1
{
    public partial class abmAnimales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Registro en el archivo
        private void RegistrarLog(string operacion, string tabla, string detalle)
        {
            try
            {
                using (StreamWriter arch = new StreamWriter(this.Server.MapPath(".") + "/log.txt", true))
                {
                    string linea = DateTime.Now.ToString() + " - Operacion: " + operacion + " - Tabla: " + tabla + " - Detalle: " + detalle;
                    arch.WriteLine(linea);
                }
            }
            catch (Exception ex)
            {
                this.LabelResultado.Text += " (Aviso: No se pudo escribir en el archivo log: " + ex.Message + ")";
            }
        }

        //Alta de Animal
        protected void AltaAnimal(object sender, EventArgs e)
        {
            //Validaciones
            if (this.TextBoxNombre.Text == "" || this.TextBoxEspecie.Text == "")
            {
                this.LabelResultado.Text = "Debe completar todos los campos.";
                return;
            }

            int especie;
            if (!int.TryParse(this.TextBoxEspecie.Text, out especie))
            {
                this.LabelResultado.Text = "La especie debe ser un número entero.";
                return;
            }

            string cadenaConexion = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "INSERT INTO Animales(nombre, especie) VALUES ('" + this.TextBoxNombre.Text + "', " + especie + ")";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
            }

            RegistrarLog("ALTA", "Animales", "Nombre: " + this.TextBoxNombre.Text + ", Especie: " + this.TextBoxEspecie.Text);

            this.LabelResultado.Text = "Animal registrado correctamente.";
            this.TextBoxNombre.Text = "";
            this.TextBoxEspecie.Text = "";
        }

        //Busqueda animal
        protected void BuscarAnimal(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(this.TextBoxBusquedaId.Text, out id))
            {
                this.LabelResultado.Text = "Ingrese un ID numérico válido.";
                return;
            }

            string cadenaConexion = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "SELECT * from Animales WHERE id=" + id;
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataReader registro = comando.ExecuteReader();

                if (registro.Read())
                {
                    this.TextBoxNombre.Text = registro["nombre"].ToString();
                    this.TextBoxEspecie.Text = registro["especie"].ToString();
                    this.LabelResultado.Text = "Animal encontrado.";
                }
                else
                {
                    this.LabelResultado.Text = "No existe un animal con dicho ID.";
                    this.TextBoxNombre.Text = "";
                    this.TextBoxEspecie.Text = "";
                }
            }
        }

        //Eliminar animal
        protected void EliminarAnimal(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(this.TextBoxBusquedaId.Text, out id))
            {
                this.LabelResultado.Text = "Ingrese un ID numérico válido.";
                return;
            }

            string cadenaConexion = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
            int resultado = 0;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "DELETE FROM Animales where id=" + id;
                SqlCommand comando = new SqlCommand(consulta, conexion);
                resultado = comando.ExecuteNonQuery();
            }

            if (resultado == 0)
            {
                this.LabelResultado.Text = "No se encontró el animal a eliminar.";
            }
            else
            {
                RegistrarLog("BAJA", "Animales", "ID eliminado: " + this.TextBoxBusquedaId.Text);
                this.LabelResultado.Text = "Animal eliminado.";
                this.TextBoxBusquedaId.Text = "";
                this.TextBoxNombre.Text = "";
                this.TextBoxEspecie.Text = "";
            }
        }

        //Actualizar animal
        protected void ActualizarAnimal(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(this.TextBoxBusquedaId.Text, out id))
            {
                this.LabelResultado.Text = "Ingrese un ID numérico válido.";
                return;
            }

            if (this.TextBoxNombre.Text == "" || this.TextBoxEspecie.Text == "")
            {
                this.LabelResultado.Text = "Debe completar todos los campos.";
                return;
            }

            int especie;
            if (!int.TryParse(this.TextBoxEspecie.Text, out especie))
            {
                this.LabelResultado.Text = "La especie debe ser un número entero.";
                return;
            }

            string cadenaConexion = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
            int resultado = 0;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "UPDATE Animales SET nombre='" + this.TextBoxNombre.Text + "', especie=" + especie + " WHERE id=" + id;
                SqlCommand comando = new SqlCommand(consulta, conexion);
                resultado = comando.ExecuteNonQuery();
            }

            if (resultado == 0)
            {
                this.LabelResultado.Text = "No se encontró el animal a modificar.";
            }
            else
            {
                RegistrarLog("ACTUALIZACION", "Animales", "ID modificado: " + this.TextBoxBusquedaId.Text + ", Nuevo Nombre: " + this.TextBoxNombre.Text + ", Nueva Especie: " + this.TextBoxEspecie.Text);
                this.LabelResultado.Text = "Datos modificados correctamente.";
            }
        }
    }
}