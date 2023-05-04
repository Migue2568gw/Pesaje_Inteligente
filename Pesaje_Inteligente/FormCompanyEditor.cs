using Data;
using Pesaje_Inteligente.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pesaje_Inteligente
{
    public partial class FormCompanyEditor : Form
    {
        public int? idEmpresa;
        bool bandera = false;
        TbEmpresa empresas = null;
        public FormCompanyEditor(int? _idEmpresa = null)
        {
            InitializeComponent();
            this.idEmpresa = _idEmpresa;
            if (_idEmpresa != null)
            {
                getEmpresaId();
                label8.Text = "Editando Empresa";
                bandera = true;
            }
            else
            {
                label8.Text = "Creando nueva Empresa";
            }
        }

        private void FormCompanyEditor_Load(object sender, EventArgs e)
        {
        }

        #region Funcion de Eventos
        private void getEmpresaId()
        {
            using (EmpresaContext bd = new EmpresaContext())
            {
                empresas = bd._Empresa.Find(idEmpresa);
                txtNombre.Text = empresas.Nombre;
                txtCodigo.Text = Convert.ToString(empresas.Codigo);
                txtDirec.Text = empresas.Direccion;
                txtTelefono.Text = empresas.Telefono;
                txtCiudad.Text = empresas.Ciudad;
                txtDepartamento.Text = empresas.Departamento;
                txtPais.Text = empresas.Pais;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (EmpresaContext bd = new EmpresaContext())
            {
                if (idEmpresa == null)
                    empresas = new TbEmpresa();

                empresas.Nombre = txtNombre.Text;
                empresas.Codigo = int.Parse(txtCodigo.Text);
                empresas.Direccion = txtDirec.Text;
                empresas.Telefono = txtTelefono.Text;
                empresas.Ciudad = txtCiudad.Text;
                empresas.Departamento = txtDepartamento.Text;
                empresas.Pais = txtPais.Text;
                empresas.FechaCreacion = DateTime.UtcNow;
                if (bandera)
                    empresas.FechaModificacion = DateTime.UtcNow;

                try
                {
                    if (idEmpresa == null)
                    {
                        bd._Empresa.Add(empresas);
                        MessageBox.Show("Empresa Guardada Satisfactoriamente");
                    }
                    else
                    {
                        bd.Entry(empresas).State = System.Data.Entity.EntityState.Modified;
                        MessageBox.Show("Empresa Modificada Satisfactoriamente");
                    }
                    bd.SaveChanges();
                    bandera = false;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar la empresa: " + ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region Validar Campos
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }
        private void txtCiudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }
        private void txtDepartamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }
        private void txtPais_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        #endregion
    }
}
