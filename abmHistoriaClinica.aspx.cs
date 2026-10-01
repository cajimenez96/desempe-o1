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
    public partial class abmHistoriaClinica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Registro en el archivo
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

        //Alta historial medico
        protected void AltaHistoria(object sender, EventArgs e)
        {
            if (this.TextBoxFecha.Text == "" || this.TextBoxIdAnimal.Text == "" || this.TextBoxIdEvento.Text == "" || this.TextBoxComentario.Text == "")
            {
                this.LabelResultado.Text = "Debe completar todos los campos.";
                return;
            }

            int idAnimal;
            if (!int.TryParse(this.TextBoxIdAnimal.Text, out idAnimal))
            {
                this.LabelResultado.Text = "El ID del animal debe ser un número.";
                return;
            }

            int idEvento;
            if (!int.TryParse(this.TextBoxIdEvento.Text, out idEvento))
            {
                this.LabelResultado.Text = "El ID del evento debe ser un número.";
                return;
            }

            try
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    string consulta = "INSERT INTO HistoriaClinica(fecha, idAnimal, idEvento, comentario) VALUES ('" +
                                      this.TextBoxFecha.Text + "', " +
                                      idAnimal + ", " +
                                      idEvento + ", '" +
                                      this.TextBoxComentario.Text + "')";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.ExecuteNonQuery();
                }

                RegistrarLog("ALTA", "HistoriaClinica", "Animal: " + this.TextBoxIdAnimal.Text + ", Evento: " + this.TextBoxIdEvento.Text + ", Fecha: " + this.TextBoxFecha.Text);

                this.LabelResultado.Text = "Registro clínico creado correctamente.";
                this.TextBoxFecha.Text = "";
                this.TextBoxIdAnimal.Text = "";
                this.TextBoxIdEvento.Text = "";
                this.TextBoxComentario.Text = "";
            }
            catch (SqlException)
            {
                this.LabelResultado.Text = "Error: Verifique que el ID de animal y el ID de evento existan.";
            }
        }

        //Busqueda de historial
        protected void BuscarHistoria(object sender, EventArgs e)
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

                string consulta = "SELECT id, fecha, idAnimal, idEvento, comentario FROM HistoriaClinica WHERE id=" + id;
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataReader registro = comando.ExecuteReader();

                if (registro.Read())
                {
                    DateTime fechaBd = Convert.ToDateTime(registro["fecha"]);
                    this.TextBoxFecha.Text = fechaBd.ToString("yyyy-MM-dd");

                    this.TextBoxIdAnimal.Text = registro["idAnimal"].ToString();
                    this.TextBoxIdEvento.Text = registro["idEvento"].ToString();
                    this.TextBoxComentario.Text = registro["comentario"].ToString();
                    this.LabelResultado.Text = "Registro encontrado.";
                }
                else
                {
                    this.LabelResultado.Text = "No existe un registro clínico con dicho ID.";
                    this.TextBoxFecha.Text = "";
                    this.TextBoxIdAnimal.Text = "";
                    this.TextBoxIdEvento.Text = "";
                    this.TextBoxComentario.Text = "";
                }
            }
        }

        //Eliminar historial
        protected void EliminarHistoria(object sender, EventArgs e)
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

                string consulta = "DELETE FROM HistoriaClinica WHERE id=" + id;
                SqlCommand comando = new SqlCommand(consulta, conexion);
                resultado = comando.ExecuteNonQuery();
            }

            if (resultado == 0)
            {
                this.LabelResultado.Text = "No se encontró el registro clínico a eliminar.";
            }
            else
            {
                RegistrarLog("BAJA", "HistoriaClinica", "ID eliminado: " + this.TextBoxBusquedaId.Text);
                this.LabelResultado.Text = "Registro clínico eliminado.";
                this.TextBoxBusquedaId.Text = "";
                this.TextBoxFecha.Text = "";
                this.TextBoxIdAnimal.Text = "";
                this.TextBoxIdEvento.Text = "";
                this.TextBoxComentario.Text = "";
            }
        }

        //Actualizar historial
        protected void ActualizarHistoria(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(this.TextBoxBusquedaId.Text, out id))
            {
                this.LabelResultado.Text = "Ingrese un ID numérico válido.";
                return;
            }

            if (this.TextBoxFecha.Text == "" || this.TextBoxIdAnimal.Text == "" || this.TextBoxIdEvento.Text == "" || this.TextBoxComentario.Text == "")
            {
                this.LabelResultado.Text = "Debe completar todos los campos.";
                return;
            }

            int idAnimal;
            if (!int.TryParse(this.TextBoxIdAnimal.Text, out idAnimal))
            {
                this.LabelResultado.Text = "El ID del animal debe ser un número.";
                return;
            }

            int idEvento;
            if (!int.TryParse(this.TextBoxIdEvento.Text, out idEvento))
            {
                this.LabelResultado.Text = "El ID del evento debe ser un número.";
                return;
            }

            try
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
                int resultado = 0;

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    string consulta = "UPDATE HistoriaClinica SET fecha='" + this.TextBoxFecha.Text +
                                      "', idAnimal=" + idAnimal +
                                      ", idEvento=" + idEvento +
                                      ", comentario='" + this.TextBoxComentario.Text +
                                      "' WHERE id=" + id;

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    resultado = comando.ExecuteNonQuery();
                }

                if (resultado == 0)
                {
                    this.LabelResultado.Text = "No se encontró el registro a modificar.";
                }
                else
                {
                    RegistrarLog("ACTUALIZACION", "HistoriaClinica", "ID modificado: " + this.TextBoxBusquedaId.Text + ", Animal: " + this.TextBoxIdAnimal.Text);
                    this.LabelResultado.Text = "Historia clínica modificada correctamente.";
                }
            }
            catch (SqlException)
            {
                this.LabelResultado.Text = "Error: Verifique que el ID de animal y el ID de evento existan.";
            }
        }
    }
}