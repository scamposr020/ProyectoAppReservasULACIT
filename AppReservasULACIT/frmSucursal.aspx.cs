using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Models;
using AppReservasULACIT.Controllers;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace AppReservasULACIT
{
    public partial class frmSucursal : System.Web.UI.Page
    {
        IEnumerable<Models.Sucursal> sucursales = new ObservableCollection<Models.Sucursal>();
        SucursalManager sucursalManager = new SucursalManager();

        IEnumerable<Models.Rentacar> rentacars = new ObservableCollection<Models.Rentacar>();
        RentacarManager rentacarManager = new RentacarManager();

        IEnumerable<Models.Direccion> direcciones = new ObservableCollection<Models.Direccion>();
        DireccionManager direccionManager = new DireccionManager();

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
                    Models.Sucursal sucursalIngresada = new Models.Sucursal();
                    Models.Sucursal sucursal = new Models.Sucursal()
                    {
                        SUC_NOMBRE = txtNombre.Text,
                        SUC_TELEFONO = txtTelefono.Text,
                        SUC_EMAIL = txtCorreo.Text,
                        RENTAC_CODIGO = Convert.ToInt32(ddRENTAC_CODIGO.SelectedItem.Value.ToString()),
                        DIREC_CODIGO = Convert.ToInt32(ddDIREC_CODIGO.SelectedItem.Value.ToString())
                    };

                    sucursalIngresada = await sucursalManager.Ingresar(sucursal, Session["TokenUsuario"].ToString());

                    if (sucursalIngresada != null)
                    {
                        lblResultado.Text = "Sucursal ingresada correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear sucursal";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al ingresar la sucursal. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                sucursales = await sucursalManager.ObtenerSucursales(Session["TokenUsuario"].ToString());
                gvSucursales.DataSource = sucursales.ToList();
                gvSucursales.DataBind();

                rentacars = await rentacarManager.ObtenerRentacars(Session["TokenUsuario"].ToString());
                ddRENTAC_CODIGO.DataTextField = "RENTAC_NOMBRE";
                ddRENTAC_CODIGO.DataValueField = "RENTAC_CODIGO";
                ddRENTAC_CODIGO.DataSource = rentacars.ToList();
                ddRENTAC_CODIGO.DataBind();

                direcciones = await direccionManager.ObtenerDirecciones(Session["TokenUsuario"].ToString());
                ddDIREC_CODIGO.DataTextField = "DIREC_DETALLE";
                ddDIREC_CODIGO.DataValueField = "DIREC_CODIGO";
                ddDIREC_CODIGO.DataSource = direcciones.ToList();
                ddDIREC_CODIGO.DataBind();
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
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lblResultado.Text = "Debe ingresar el nombre";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                lblResultado.Text = "Debe ingresar el teléfono";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCorreo.Text))
            {
                lblResultado.Text = "Debe ingresar el correo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void gvSucursales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Código";
                e.Row.Cells[1].Text = "Nombre";
                e.Row.Cells[2].Text = "Teléfono";
                e.Row.Cells[3].Text = "Correo";
                e.Row.Cells[4].Text = "Código RentACar";
                e.Row.Cells[5].Text = "Código Dirección";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Sucursal sucursalModificada = new Models.Sucursal();
                Models.Sucursal sucursal = new Models.Sucursal()
                {
                    SUC_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    SUC_NOMBRE = txtNombre.Text,
                    SUC_TELEFONO = txtTelefono.Text,
                    SUC_EMAIL = txtCorreo.Text,
                    RENTAC_CODIGO = Convert.ToInt32(ddRENTAC_CODIGO.SelectedItem.Value.ToString()),
                    DIREC_CODIGO = Convert.ToInt32(ddDIREC_CODIGO.SelectedItem.Value.ToString())
                };

                sucursalModificada = await sucursalManager.Actualizar(sucursal, Session["TokenUsuario"].ToString());

                if (sucursalModificada != null)
                {
                    lblResultado.Text = "Sucursal actualizada correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar sucursal";
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
                codigoEliminado = await sucursalManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Sucursal eliminada con éxito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar la sucursal";
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