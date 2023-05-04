using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Pesaje_Inteligente
{
    public partial class FormCompanyList : Form
    {
        public FormCompanyList()
        {
            InitializeComponent();
        }

        private void FormCompanyList_Load(object sender, EventArgs e)
        {            
            Recargar();
        }

        #region Funcion de Eventos
        private void btnAñadir_Click(object sender, EventArgs e)
        {
            Pesaje_Inteligente.FormCompanyEditor formCompany = new Pesaje_Inteligente.FormCompanyEditor();
            formCompany.ShowDialog();
            Recargar();
        }    

        private void btmEditar_Click(object sender, EventArgs e)
        {
            int? idEmpresa = getEmpresaId();
            if (idEmpresa != null)
            {
                Pesaje_Inteligente.FormCompanyEditor formCompany = new Pesaje_Inteligente.FormCompanyEditor(idEmpresa);
                formCompany.ShowDialog();
                Recargar();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar las empresas seleccionadas?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (EmpresaContext bd = new EmpresaContext())
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int idEmpresa = Convert.ToInt32(row.Cells["EmpresaID"].Value);
                        TbEmpresa empresa = bd._Empresa.FirstOrDefault(x => x.EmpresaID == idEmpresa);
                        bd._Empresa.Remove(empresa);
                    }
                    bd.SaveChanges();
                    Recargar();
                }
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.Trim();
            using (EmpresaContext bd = new EmpresaContext())
            {
                var empresas = from j in bd._Empresa
                               where j.Nombre.Contains(filtro)
                               select j;

                dataGridView1.DataSource = empresas.ToList();
                dataGridView1.Columns["EmpresaID"].Visible = false;
            }
        }
        #endregion
        #region Funciones
        private void Recargar()
        {
            using (EmpresaContext bd = new EmpresaContext())
            {
                var empresas = from j in bd._Empresa
                               select j;

                dataGridView1.DataSource = empresas.ToList();
                dataGridView1.Columns["EmpresaID"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private int? getEmpresaId()
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int indiceFilaSeleccionada = dataGridView1.SelectedRows[0].Index;
                return int.Parse(dataGridView1.Rows[indiceFilaSeleccionada].Cells["EmpresaID"].Value.ToString());
            }
            else if (dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Solo puede seleccionar una fila para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila antes de continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }
        #endregion
    }
}
