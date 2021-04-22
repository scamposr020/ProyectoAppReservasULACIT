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
    public partial class frmOrden : System.Web.UI.Page
    {
        IEnumerable<Models.Orden> ordenes = new ObservableCollection<Models.Orden>();
        OrdenManager ordenManager = new OrdenManager();

        IEnumerable<Models.Vehiculo> vehiculos = new ObservableCollection<Models.Vehiculo>();
        VehiculoManager vehiculoManager = new VehiculoManager();

        async protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                InicializarControles();

            }
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Models.Orden ordenIngresada = new Models.Orden();
                    Models.Orden orden = new Models.Orden()
                    {
                        ORD_FEC_SOLI = clFechSoli.SelectedDate,
                        ORD_DIAS_RENT = Convert.ToInt32(txtCantDias.Text),
                        ORD_MONTO_DIA = Convert.ToInt32(txtMontoDia.Text),
                        ORD_DETALLE = txtDetalle.Text,
                        VEH_CODIGO = Convert.ToInt32(ddVEH_CODIGO.SelectedItem.Value.ToString())
                    };

                    ordenIngresada = await ordenManager.Ingresar(orden, Session["TokenUsuario"].ToString());

                    if (ordenIngresada != null)
                    {
                        lblResultado.Text = "Orden ingresada correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear orden";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al ingresar la orden. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                ordenes = await ordenManager.ObtenerOrdenes(Session["TokenUsuario"].ToString());
                gvOrdenes.DataSource = ordenes.ToList();
                gvOrdenes.DataBind();

                vehiculos = await vehiculoManager.ObtenerVehiculos(Session["TokenUsuario"].ToString());
                ddVEH_CODIGO.DataTextField = "VEH_MODELO";
                ddVEH_CODIGO.DataValueField = "VEH_CODIGO";
                ddVEH_CODIGO.DataSource = vehiculos.ToList();
                ddVEH_CODIGO.DataBind();
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
            if (string.IsNullOrEmpty(txtCantDias.Text))
            {
                lblResultado.Text = "Debe ingresar la cantidad de días";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtMontoDia.Text))
            {
                lblResultado.Text = "Debe ingresar el monto diario";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtDetalle.Text))
            {
                lblResultado.Text = "Debe ingresar el detalle de la orden";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

       
          
            return true;
        }

        protected void gvOrdenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Código";
                e.Row.Cells[1].Text = "Fecha de orden solicitada";
                e.Row.Cells[2].Text = "Cantidad días";
                e.Row.Cells[3].Text = "Monto por día";
                e.Row.Cells[4].Text = "Detalle";
                e.Row.Cells[5].Text = "Código Vehículo";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Orden ordenModificada = new Models.Orden();
                Models.Orden orden = new Models.Orden()
                {
                    ORD_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    ORD_FEC_SOLI = clFechSoli.SelectedDate,
                    ORD_DIAS_RENT = Convert.ToInt32(txtCantDias.Text),
                    ORD_MONTO_DIA = Convert.ToInt32(txtMontoDia.Text),
                    ORD_DETALLE = txtDetalle.Text,
                    VEH_CODIGO = Convert.ToInt32(ddVEH_CODIGO.SelectedItem.Value.ToString())
                };

                ordenModificada = await ordenManager.Actualizar(orden, Session["TokenUsuario"].ToString());

                if (ordenModificada != null)
                {
                    lblResultado.Text = "Orden actualizada correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar orden";
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
                codigoEliminado = await ordenManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Orden eliminada con éxito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar la orden";
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
