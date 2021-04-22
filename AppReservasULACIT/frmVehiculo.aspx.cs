using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Models;
using AppReservasULACIT.Controllers;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AppReservasULACIT
{
    public partial class frmVehiculo : System.Web.UI.Page
    {
        IEnumerable<Models.Vehiculo> vehiculos = new ObservableCollection<Models.Vehiculo>();
        VehiculoManager vehiculoManager = new VehiculoManager();

        async protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Models.Vehiculo vehiculoIngresado = new Models.Vehiculo();
                    Models.Vehiculo vehiculo = new Models.Vehiculo()
                    {
                        VEH_MARCA = txtVEH_MARCA.Text,
                        VEH_TIPO = txtVEH_TIPO.Text,
                        VEH_CANTI_PUERTAS = Convert.ToInt32(txtVEH_CANTI_PUERTAS.Text),
                        VEH_COMBUSTIBLE = txtVEH_COMBUSTIBLE.Text,
                        VEH_COLOR = txt_VEH_COLOR.Text,
                        VEH_MODELO = txtVEH_MODELO.Text,
                        VEH_ANO = Convert.ToInt32(txtVEH_ANO.Text),
                        SUC_CODIGO = Convert.ToInt32(txtSUC_CODIGO.Text)
                    };

                    vehiculoIngresado = await vehiculoManager.Ingresar(vehiculo, Session["TokenUsuario"].ToString());

                    if (vehiculoIngresado != null)
                    {
                        lblResultado.Text = "Vehiculo ingresado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear vehiculo";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al ingresar El vehiculo. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                vehiculos = await vehiculoManager.ObtenerVehiculos(Session["TokenUsuario"].ToString());
                gvVehiculos.DataSource = vehiculos.ToList();
                gvVehiculos.DataBind();
            }
            catch (Exception e)
            {
                lblResultado.Text = "Hubo un error al inicializar controles. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;

            }
        }

        private bool ValidarInsertar()
        {
            if (string.IsNullOrEmpty(txtVEH_MARCA.Text))
            {
                lblResultado.Text = "Debe ingresar la marca del vehiculo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtVEH_TIPO.Text))
            {
                lblResultado.Text = "Debe el tipo de Vehiculo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtVEH_CANTI_PUERTAS.Text))
            {
                lblResultado.Text = "Debe ingresar la cantidad de puertas";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtVEH_COMBUSTIBLE.Text))
            {
                lblResultado.Text = "Debe ingresar el tipo de combustible";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txt_VEH_COLOR.Text))
            {
                lblResultado.Text = "Debe ingresar el color del vehiculo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtVEH_MODELO.Text))
            {
                lblResultado.Text = "Debe ingresar el modelo del vehiculo.";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtVEH_ANO.Text))
            {
                lblResultado.Text = "Debe ingresar el año del vehiculo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtSUC_CODIGO.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo de sucursal";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void gvVehiculos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo Vehiculo";
                e.Row.Cells[1].Text = "Marca de Vehiculo";
                e.Row.Cells[2].Text = "Tipo de Vehiculo";
                e.Row.Cells[3].Text = "Cantidad de puertas";
                e.Row.Cells[4].Text = "Tipo de combustible";
                e.Row.Cells[5].Text = "Color";
                e.Row.Cells[6].Text = "Modelo";
                e.Row.Cells[7].Text = "Año";
                e.Row.Cells[8].Text = "Sucursal";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Vehiculo vehiculoModificado = new Models.Vehiculo();
                Models.Vehiculo vehiculo = new Models.Vehiculo()
                {
                    VEH_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    VEH_MARCA = txtVEH_MARCA.Text,
                    VEH_TIPO = txtVEH_TIPO.Text,
                    VEH_CANTI_PUERTAS = Convert.ToInt32(txtVEH_CANTI_PUERTAS.Text),
                    VEH_COMBUSTIBLE = txtVEH_COMBUSTIBLE.Text,
                    VEH_COLOR = txt_VEH_COLOR.Text,
                    VEH_MODELO = txtVEH_MODELO.Text,
                    VEH_ANO = Convert.ToInt32(txtVEH_ANO.Text),
                    SUC_CODIGO = Convert.ToInt32(txtSUC_CODIGO.Text)
                };

                vehiculoModificado = await vehiculoManager.Actualizar(vehiculo, Session["TokenUsuario"].ToString());

                if (vehiculoModificado != null)
                {
                    lblResultado.Text = "Vehiculo actualizado correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar vehiculo";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
            else
            {
                lblResultado.Text = "Debe ingresar todos los datos";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }


        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                string codigoEliminado = string.Empty;
                codigoEliminado = await vehiculoManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Vehiculo eliminado con exito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar el vehiculo";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
            else
            {
                lblResultado.Text = "Debe ingresar el código";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }
    }
}