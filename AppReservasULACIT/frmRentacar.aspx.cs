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
    public partial class frmRentacar : System.Web.UI.Page
    {
        IEnumerable<Models.Rentacar> rentacars = new ObservableCollection<Models.Rentacar>();
        RentacarManager rentacarManager = new RentacarManager();

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
                    Models.Rentacar rentacarIngresado = new Models.Rentacar();
                    Models.Rentacar rentacar = new Models.Rentacar()
                    {
                        RENTAC_NOMBRE = txtNombre.Text,
                        RENTAC_CANT_SUC = Convert.ToInt32(txtCantSuc.Text),
                        RENTAC_TIPO = txtTipo.Text,
                        RENTAC_WEBPAGE = txtWebPage.Text
                    };

                    rentacarIngresado = await rentacarManager.Ingresar(rentacar, Session["TokenUsuario"].ToString());

                    if (rentacarIngresado != null)
                    {
                        lblResultado.Text = "Rentacar ingresado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear rentacar";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al ingresar el rentacar. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                rentacars = await rentacarManager.ObtenerRentacars(Session["TokenUsuario"].ToString());
                gvRentacars.DataSource = rentacars.ToList();
                gvRentacars.DataBind();
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
                lblResultado.Text = "Debe ingresar el nombre del Rentacar";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCantSuc.Text))
            {
                lblResultado.Text = "Debe ingresar la cantidad de sucursales";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtTipo.Text))
            {
                lblResultado.Text = "Debe ingresar el tipo de rentacar";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtWebPage.Text))
            {
                lblResultado.Text = "Debe ingresar la pagina web del rentacar";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }


            return true;
        }

        protected void gvRentacars_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo Rentacar";
                e.Row.Cells[1].Text = "Nombre Rentacar";
                e.Row.Cells[2].Text = "Cantidad de sucursales";
                e.Row.Cells[3].Text = "Tipo";
                e.Row.Cells[4].Text = "Pagina Web";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Rentacar rentacarModificado = new Models.Rentacar();
                Models.Rentacar rentacar = new Models.Rentacar()
                {
                    RENTAC_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    RENTAC_NOMBRE = txtNombre.Text,
                    RENTAC_CANT_SUC = Convert.ToInt32(txtCantSuc.Text),
                    RENTAC_TIPO = txtTipo.Text,
                    RENTAC_WEBPAGE = txtWebPage.Text
                };

                rentacarModificado = await rentacarManager.Actualizar(rentacar, Session["TokenUsuario"].ToString());

                if (rentacarModificado != null)
                {
                    lblResultado.Text = "Rentacar actualizado correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar rentacar";
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
                codigoEliminado = await rentacarManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Rentacar eliminado con exito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar el rentacar";
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