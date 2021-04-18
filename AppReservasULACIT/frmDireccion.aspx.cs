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
    public partial class frmDireccion : System.Web.UI.Page
    {
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
                    Models.Direccion direccionIngresada = new Models.Direccion();
                    Models.Direccion direccion = new Models.Direccion()
                    {
                        DIREC_PROVIN = txtProvincia.Text,
                        DIREC_DISTRI = txtDistrito.Text,
                        DIREC_CANTON =txtCanton.Text,
                        DIREC_DETALLE = txtDetalle.Text,
                        DIREC_COD_POSTAL = txtCodPostal.Text
                    };

                    direccionIngresada = await direccionManager.Ingresar(direccion, Session["TokenUsuario"].ToString());

                    if (direccionIngresada != null)
                    {
                        lblResultado.Text = "Dirección ingresada correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear dirección";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al ingresar la dirección. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                direcciones = await direccionManager.ObtenerDirecciones(Session["TokenUsuario"].ToString());
                gvDirecciones.DataSource = direcciones.ToList();
                gvDirecciones.DataBind();
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
            if (string.IsNullOrEmpty(txtProvincia.Text))
            {
                lblResultado.Text = "Debe ingresar la provincia";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtDistrito.Text))
            {
                lblResultado.Text = "Debe ingresar el distrito";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCanton.Text))
            {
                lblResultado.Text = "Debe ingresar el cantón";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtDetalle.Text))
            {
                lblResultado.Text = "Debe ingresar el detalle de la dirección";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }     if (string.IsNullOrEmpty(txtCodPostal.Text))
            {
                lblResultado.Text = "Debe ingresar el código postal";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }


            return true;
        }

        protected void gvDirecciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Código";
                e.Row.Cells[1].Text = "Provincia";
                e.Row.Cells[2].Text = "Distrito";
                e.Row.Cells[3].Text = "Cantón";
                e.Row.Cells[4].Text = "Detalle";
                e.Row.Cells[4].Text = "Código Postal";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Direccion direccionModificada = new Models.Direccion();
                Models.Direccion direccion = new Models.Direccion()
                {
                    DIREC_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    DIREC_PROVIN = txtProvincia.Text,
                    DIREC_DISTRI = txtDistrito.Text,
                    DIREC_CANTON = txtCanton.Text,
                    DIREC_DETALLE = txtDetalle.Text,
                    DIREC_COD_POSTAL = txtCodPostal.Text
                };

                direccionModificada = await direccionManager.Actualizar(direccion, Session["TokenUsuario"].ToString());

                if (direccionModificada != null)
                {
                    lblResultado.Text = "Dirección actualizada correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar dirección";
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
                codigoEliminado = await direccionManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Dirección eliminada con éxito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar la dirección";
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