using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace desempeño1
{
    public partial class consultaHistorial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void FiltrarHistorial(object sender, EventArgs e)
        {
            //Validamos que exista al menos un dato
            if (this.TextBoxIdAnimal.Text == "" && this.TextBoxNombreAnimal.Text == "")
            {
                this.LabelResultado.Text = "Por favor, ingrese el ID o el Nombre del animal a consultar.";
                this.GridViewHistorial.DataSource = null;
                this.GridViewHistorial.DataBind();
                return;
            }

            //Consulta INNER JOIN entre las tablas
            string consulta = "SELECT hc.id AS [Nro Registro], hc.fecha AS Fecha, a.nombre AS Animal, " +
                              "e.decripcion AS Evento, e.escala AS Escala, hc.comentario AS Comentario " +
                              "FROM HistoriaClinica hc " +
                              "INNER JOIN Animales a ON hc.idAnimal = a.id " +
                              "INNER JOIN Eventos e ON hc.idEvento = e.id ";

            //Se agrega la consulta faltante en base al dato que ingrese el usuario
            if (this.TextBoxIdAnimal.Text != "")
            {
                int idAnimal;
                if (!int.TryParse(this.TextBoxIdAnimal.Text, out idAnimal))
                {
                    this.LabelResultado.Text = "El ID del animal debe ser un número entero.";
                    this.GridViewHistorial.DataSource = null;
                    this.GridViewHistorial.DataBind();
                    return;
                }

                consulta += "WHERE a.id = " + idAnimal;
            }
            else
            {
                consulta += "WHERE a.nombre = '" + this.TextBoxNombreAnimal.Text + "'";
            }

            try
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    SqlDataReader registro = comando.ExecuteReader();

                    //Consulta si existe al menos un elemento
                    if (registro.HasRows)
                    {
                        //Agregamos a la tabla el resultado
                        this.GridViewHistorial.DataSource = registro;
                        this.GridViewHistorial.DataBind();
                        this.LabelResultado.Text = "Registros encontrados:";
                    }
                    else
                    {
                        //Vaciamos la tabla si no hay respuestas
                        this.GridViewHistorial.DataSource = null;
                        this.GridViewHistorial.DataBind();
                        this.LabelResultado.Text = "No se encontraron historias clínicas para el animal ingresado.";
                    }
                }
            }
            catch (SqlException)
            {
                this.LabelResultado.Text = "Error al consultar la base de datos.";
            }
        }
    }
}