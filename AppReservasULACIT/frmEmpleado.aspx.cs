using AppReservasULACIT.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppReservasULACIT
{
    public partial class frmEmpleado : System.Web.UI.Page
    {
        IEnumerable<Models.Empleado> empleados = new ObservableCollection<Models.Empleado>();
        EmpleadoManager empleadoManager = new EmpleadoManager();

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
                    Models.Empleado empleadoIngresado = new Models.Empleado();
                    Models.Empleado empleado = new Models.Empleado()
                    {
                        EMP_CEDULA = txtCedula.Text,
                        EMP_NOMBRE = txtNombre.Text,
                        EMP_PUESTO = txtPuesto.Text,
                        EMP_TELEFONO = txtTelefono.Text,
                        EMP_EMAIL = txtEmail.Text,
                        SUC_CODIGO = txtSucursal.Text

                    };

                    empleadoIngresado = await empleadoManager.Ingresar(empleado, Session["TokenUsuario"].ToString());

                    if (empleadoIngresado != null)
                    {
                        lblResultado.Text = "Empleado ingresado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear empleado";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un errpr al ingresar el empleado. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                empleados = await empleadoManager.ObtenerEmpleados(Session["TokenUsuario"].ToString());
                gvEmpleados.DataSource = empleados.ToList();
                gvEmpleados.DataBind();
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

            if (string.IsNullOrEmpty(txtCedula.Text))
            {
                lblResultado.Text = "Debe ingresar la cedula";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lblResultado.Text = "Debe ingresar el nombre";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPuesto.Text))
            {
                lblResultado.Text = "Debe ingresar el puesto";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                lblResultado.Text = "Debe ingresar el telefono";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblResultado.Text = "Debe ingresar el email";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtSucursal.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo de la sucursal";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void gvEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo";
                e.Row.Cells[1].Text = "Cedula";
                e.Row.Cells[2].Text = "Nombre";
                e.Row.Cells[3].Text = "Puesto";
                e.Row.Cells[4].Text = "Telefono";
                e.Row.Cells[5].Text = "Email";
                e.Row.Cells[5].Text = "Sucursal";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Empleado empleadoModificado = new Models.Empleado();
                Models.Empleado empleado = new Models.Empleado()
                {
                    EMP_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    EMP_CEDULA = txtCedula.Text,
                    EMP_NOMBRE = txtNombre.Text,
                    EMP_PUESTO = txtPuesto.Text,
                    EMP_TELEFONO = txtTelefono.Text,
                    EMP_EMAIL = txtEmail.Text,
                    SUC_CODIGO = txtSucursal.Text
                };

                empleadoModificado = await empleadoManager.Actualizar(empleado, Session["TokenUsuario"].ToString());

                if (empleadoModificado != null)
                {
                    lblResultado.Text = "Empleado actualizado correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar empleado";
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
                codigoEliminado = await empleadoManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Empleado eliminado con exito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar el empleado";
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

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}