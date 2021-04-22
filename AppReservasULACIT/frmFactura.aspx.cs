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
    public partial class frmFactura : System.Web.UI.Page
    {
        IEnumerable<Models.Factura> facturas = new ObservableCollection<Models.Factura>();
        FacturaManager facturaManager = new FacturaManager();

        IEnumerable<Models.Empleado> empleados = new ObservableCollection<Models.Empleado>();
        EmpleadoManager empleadoManager = new EmpleadoManager();

        IEnumerable<Models.Sucursal> sucursales = new ObservableCollection<Models.Sucursal>();
        SucursalManager sucursalManager = new SucursalManager();

        IEnumerable<Models.Orden> ordenes = new ObservableCollection<Models.Orden>();
        OrdenManager ordenManager = new OrdenManager();
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
                    Models.Factura facturaIngresada = new Models.Factura();
                    Models.Factura factura = new Models.Factura()
                    {
                        FACT_FEC_RENT = clFechRenta.SelectedDate,
                        FACT_FEC_DEVOLU = clFechDevo.SelectedDate,
                        FACT_MONTO_TOT = Convert.ToInt32(txtMontoTotal.Text),
                        FACT_DETALLE = txtDetalle.Text,
                        EMP_CODIGO = Convert.ToInt32(ddEMP_CODIGO.SelectedItem.Value.ToString()),
                        SUC_CODIGO = Convert.ToInt32(ddSUC_CODIGO.SelectedItem.Value.ToString()),
                        USU_CODIGO = Convert.ToInt32(ddUSU_CODIGO.SelectedItem.Value.ToString()),
                        ORD_CODIGO = Convert.ToInt32(ddORD_CODIGO.SelectedItem.Value.ToString())
                    };

                   facturaIngresada = await facturaManager.Ingresar(factura, Session["TokenUsuario"].ToString());

                    if (facturaIngresada != null)
                    {
                        lblResultado.Text = "Factura ingresada correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear factura";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al ingresar la factura. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                facturas = await facturaManager.ObtenerFacturas(Session["TokenUsuario"].ToString());
                gvFacturas.DataSource = facturas.ToList();
                gvFacturas.DataBind();

                empleados = await empleadoManager.ObtenerEmpleados(Session["TokenUsuario"].ToString());
                ddEMP_CODIGO.DataTextField = "EMP_NOMBRE";
                ddEMP_CODIGO.DataValueField = "EMP_CODIGO";
                ddEMP_CODIGO.DataSource = empleados.ToList();
                ddEMP_CODIGO.DataBind();

                sucursales = await sucursalManager.ObtenerSucursales(Session["TokenUsuario"].ToString());
                ddSUC_CODIGO.DataTextField = "SUC_NOMBRE";
                ddSUC_CODIGO.DataValueField = "SUC_CODIGO";
                ddSUC_CODIGO.DataSource = sucursales.ToList();
                ddSUC_CODIGO.DataBind();

                string usu = Session["NombreUsuario"].ToString();
                var codUs = Session["CodUsuario"].ToString();
                ddUSU_CODIGO.Items.Add(new ListItem(usu,codUs));

                ordenes = await ordenManager.ObtenerOrdenes(Session["TokenUsuario"].ToString());
                ddORD_CODIGO.DataTextField = "ORD_DETALLE";
                ddORD_CODIGO.DataValueField = "ORD_CODIGO";
                ddORD_CODIGO.DataSource = ordenes.ToList();
                ddORD_CODIGO.DataBind();


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
            if (string.IsNullOrEmpty(txtMontoTotal.Text))
            {
                lblResultado.Text = "Debe ingresar el monto total";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }
             if (string.IsNullOrEmpty(txtDetalle.Text))
            {
                lblResultado.Text = "Debe ingresar el detalle de factura";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if(clFechRenta.SelectedDate > clFechDevo.SelectedDate)
            {
                lblResultado.Text = "Error. Fecha de renta es mayor a la Fecha de devolución";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }
            return true;
        }

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Código";
                e.Row.Cells[1].Text = "Fecha de renta";
                e.Row.Cells[2].Text = "Fecha de devolución";
                e.Row.Cells[3].Text = "Fecha monto total";
                e.Row.Cells[4].Text = "Detalle Factura";
                e.Row.Cells[5].Text = "Código Empleado";
                e.Row.Cells[6].Text = "Código Sucursal";
                e.Row.Cells[7].Text = "Código Usuario";
                e.Row.Cells[8].Text = "Código Orden";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Factura facturaModificado = new Models.Factura();
                Models.Factura factura = new Models.Factura()
                {
                    FACT_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    FACT_FEC_DEVOLU = clFechDevo.SelectedDate,
                    FACT_FEC_RENT = clFechRenta.SelectedDate,
                    FACT_MONTO_TOT = Convert.ToInt32(txtMontoTotal.Text),
                    FACT_DETALLE = txtDetalle.Text,
                    EMP_CODIGO = Convert.ToInt32(ddEMP_CODIGO.SelectedItem.Value.ToString()),
                    SUC_CODIGO = Convert.ToInt32(ddSUC_CODIGO.SelectedItem.Value.ToString()),
                    USU_CODIGO = Convert.ToInt32(ddUSU_CODIGO.SelectedItem.Value.ToString()),
                    ORD_CODIGO = Convert.ToInt32(ddORD_CODIGO.SelectedItem.Value.ToString())
                };

                facturaModificado = await facturaManager.Actualizar(factura, Session["TokenUsuario"].ToString());

                if (facturaModificado != null)
                {
                    lblResultado.Text = "Factura actualizada correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar factura";
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
                codigoEliminado = await facturaManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Factura eliminada con éxito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar la factura";
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