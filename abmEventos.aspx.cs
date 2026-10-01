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
    public partial class abmEventos : System.Web.UI.Page
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

        //Alta evento
        protected void AltaEvento(object sender, EventArgs e)
        {
            if (this.TextBoxDescripcion.Text == "" || this.TextBoxEscala.Text == "")
            {
                this.LabelResultado.Text = "Debe completar todos los campos.";
                return;
            }

            int escala;
            if (!int.TryParse(this.TextBoxEscala.Text, out escala))
            {
                this.LabelResultado.Text = "La escala debe ser un número entero.";
                return;
            }

            string cadenaConexion = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "INSERT INTO Eventos(decripcion, escala) VALUES ('" + this.TextBoxDescripcion.Text + "', " + escala + ")";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
            }

            RegistrarLog("ALTA", "Evento", "Descripcion: " + this.TextBoxDescripcion.Text + ", Escala: " + this.TextBoxEscala.Text);

            this.LabelResultado.Text = "Evento registrado correctamente.";
            this.TextBoxDescripcion.Text = "";
            this.TextBoxEscala.Text = "";
        }

        //Buscar Evento
        protected void BuscarEvento(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(this.TextBoxBusquedaId.Text, out id))
            {
                this.LabelResultado.Text = "Ingrese un ID numérico válido.";
                return;
            }

            string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                conexion.Open();

                string consulta = "SELECT * FROM Eventos WHERE id=" + id;
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataReader resultado = comando.ExecuteReader();

                if (resultado.Read())
                {
                    this.LabelResultado.Text = "Evento encontrado!";
                    this.TextBoxDescripcion.Text = resultado["decripcion"].ToString();
                    this.TextBoxEscala.Text = resultado["escala"].ToString();
                }
                else
                {
                    this.LabelResultado.Text = "No existe un evento con dicho ID";
                    this.TextBoxDescripcion.Text = "";
                    this.TextBoxEscala.Text = "";
                }
            }
        }

        //Eliminar evento
        protected void EliminarEvento(object sender, EventArgs e)
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

                string consulta = "DELETE FROM Eventos where id=" + id;
                SqlCommand comando = new SqlCommand(consulta, conexion);
                resultado = comando.ExecuteNonQuery();
            }

            if (resultado == 0)
            {
                this.LabelResultado.Text = "No se encontró el evento a eliminar.";
            }
            else
            {
                RegistrarLog("BAJA", "Evento", "ID eliminado: " + this.TextBoxBusquedaId.Text);
                this.LabelResultado.Text = "Evento eliminado.";
                this.TextBoxBusquedaId.Text = "";
                this.TextBoxDescripcion.Text = "";
                this.TextBoxEscala.Text = "";
            }
        }

        //Actualizar evento
        protected void ActualizarEvento(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(this.TextBoxBusquedaId.Text, out id))
            {
                this.LabelResultado.Text = "Ingrese un ID numérico válido.";
                return;
            }

            if (this.TextBoxDescripcion.Text == "" || this.TextBoxEscala.Text == "")
            {
                this.LabelResultado.Text = "Debe completar todos los campos.";
                return;
            }

            int escala;
            if (!int.TryParse(this.TextBoxEscala.Text, out escala))
            {
                this.LabelResultado.Text = "La escala debe ser un número entero.";
                return;
            }

            string cadenaConexion = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
            int resultado = 0;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "UPDATE Eventos SET decripcion='" + this.TextBoxDescripcion.Text + "', escala=" + escala + " WHERE id=" + id;
                SqlCommand comando = new SqlCommand(consulta, conexion);
                resultado = comando.ExecuteNonQuery();
            }

            if (resultado == 0)
            {
                this.LabelResultado.Text = "No se encontró el evento a modificar.";
            }
            else
            {
                RegistrarLog("ACTUALIZACION", "Evento", "ID modificado: " + this.TextBoxBusquedaId.Text + ", Nueva Descripcion: " + this.TextBoxDescripcion.Text + ", Nueva Escala: " + this.TextBoxEscala.Text);
                this.LabelResultado.Text = "Datos modificados correctamente.";
            }
        }
    }
}