using Microsoft.Data.SqlClient;
using SGB.Data;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace SGB
{
    public partial class FrmCadastroUsuario : Form
    {
        public FrmCadastroUsuario()
        {
            InitializeComponent();
        }
        private void FrmCadastroUsuario_Load(object sender, EventArgs e)
        {
            // 1. Barreira de segurança: impede o formulário de abrir se não for Admin
            if (!SessaoUsuario.PodeGerenciarUsuarios())
            {
                MessageBox.Show("Acesso negado! Apenas administradores podem gerenciar usuários.",
                                "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // 2. Preenche as opções dos ComboBoxes (Perfil e Tipo)
            ConfigurarCombos();

            // 3. Busca os dados no banco e preenche o grid
            CarregarUsuarios();


            btnConfirmar.Click -= btnConfirmar_Click;
            btnConfirmar.Click += btnConfirmar_Click;

            btnConfirmaDeletar.Click -= btnConfirmaDeletar_Click;
            btnConfirmaDeletar.Click += btnConfirmaDeletar_Click;

            gridUsuarios.CellClick -= gridUsuarios_CellClick;
            gridUsuarios.CellClick += gridUsuarios_CellClick;



            // 5. Inicia a tela apenas no modo de cadastro (ocultando grid, edição e botão deletar)
            ExibirModoCadastro();
        }

        private void ConfigurarCombos()
        {
            cmbPerfilCadastro.Items.Clear();
            cmbPerfilCadastro.Items.AddRange(new object[] { "Usuario", "Bibliotecario", "Administrador" });
            cmbPerfilCadastro.SelectedIndex = 0;

            cmbTipoUsuarioCadastro.Items.Clear();
            cmbTipoUsuarioCadastro.Items.AddRange(new object[] { "Aluno", "Professor", "Funcionario", "Externo" });
            cmbTipoUsuarioCadastro.SelectedIndex = 0;

            cmbPerfilEdicao.Items.Clear();
            cmbPerfilEdicao.Items.AddRange(new object[] { "Usuario", "Bibliotecario", "Administrador" });

            cmbBoxEdicao.Items.Clear();
            cmbBoxEdicao.Items.AddRange(new object[] { "Aluno", "Professor", "Funcionario", "Externo" });
        }

        // CONTROLE DE NAVEGAÇÃO E VISIBILIDADE DOS MENUS
        private void btnCadastro_Click(object sender, EventArgs e)
        {
            AlternarVisualizacao(cadastro: true, edicao: false, grid: false, botaoDeletar: false);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            AlternarVisualizacao(cadastro: false, edicao: false, grid: true, botaoDeletar: false);
            CarregarUsuarios();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            AlternarVisualizacao(cadastro: false, edicao: true, grid: true, botaoDeletar: false);
            CarregarUsuarios();
            PreencherCamposEdicaoComLinhaSelecionada();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            AlternarVisualizacao(cadastro: false, edicao: true, grid: true, botaoDeletar: true);
            CarregarUsuarios();
            PreencherCamposEdicaoComLinhaSelecionada();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExibirModoCadastro()
        {
            groupBoxCadastro.Visible = true;

            gridUsuarios.Visible = false;
            btnConfirmaDeletar.Visible = false; 

            if (txtNomeEdicao.Parent is GroupBox gbEdicao)
            {
                gbEdicao.Visible = false;
            }
        }

        private void ExibirModoListar()
        {
            groupBoxCadastro.Visible = false;
            DefinirVisibilidadeGrupoEdicao(false);
            gridUsuarios.Visible = true;
            CarregarUsuarios();
        }

        private void ExibirModoEditar()
        {
            groupBoxCadastro.Visible = false;
            DefinirVisibilidadeGrupoEdicao(true);
            gridUsuarios.Visible = true;
            CarregarUsuarios();
            PreencherCamposEdicaoComLinhaSelecionada();
        }

        private void ExibirModoDeletar()
        {
            groupBoxCadastro.Visible = false;
            DefinirVisibilidadeGrupoEdicao(true);
            gridUsuarios.Visible = true;
            CarregarUsuarios();
        }

        private void AlternarVisualizacao(bool cadastro, bool edicao, bool grid, bool botaoDeletar)
        {
            groupBoxCadastro.Visible = cadastro;

            // Se o seu GroupBox de edição tiver outro nome (ex: groupBoxEdicao ou groupBox1), ajuste aqui:
            if (txtNomeEdicao.Parent is GroupBox gbEdicao)
            {
                gbEdicao.Visible = edicao;
            }

            gridUsuarios.Visible = grid;
            btnConfirmaDeletar.Visible = botaoDeletar; // Esconde/mostra o botão DELETAR
        }

        private void DefinirVisibilidadeGrupoEdicao(bool visivel)
        {
            if (txtNomeEdicao.Parent != null && txtNomeEdicao.Parent != this)
            {
                txtNomeEdicao.Parent.Visible = visivel;
            }
        }

        // 1. MÓDULO DE CADASTRO
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (!ValidarCadastro(out string erro))
            {
                MessageBox.Show(erro, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string email = txtEmailCadastro.Text.Trim();
            if (EmailExiste(email))
            {
                MessageBox.Show("Este e-mail já está cadastrado no sistema.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                InserirUsuario();
                MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCamposCadastro();
                CarregarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCadastro(out string erro)
        {
            string nome = txtNomeCadastro.Text.Trim();
            string email = txtEmailCadastro.Text.Trim();
            string senha = txtSenhaCadastro.Text;

            if (string.IsNullOrWhiteSpace(nome))
            {
                erro = "Informe o nome.";
                return false;
            }

            if (nome.Any(char.IsDigit))
            {
                erro = "O nome não pode conter números.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                erro = "Informe um e-mail válido.";
                return false;
            }

            if (cmbPerfilCadastro.SelectedItem == null || cmbTipoUsuarioCadastro.SelectedItem == null)
            {
                erro = "Selecione o perfil e o tipo de usuário.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 4)
            {
                erro = "A senha é obrigatória e deve ter no mínimo 4 caracteres.";
                return false;
            }

            erro = string.Empty;
            return true;
        }

        private void InserirUsuario()
        {
            string sql = @"INSERT INTO dbo.Usuarios (Nome, Email, SenhaHash, Perfil, TipoUsuario, Ativo, DataCadastro)
                           VALUES (@nome, @email, @hash, @perfil, @tipo, 1, SYSDATETIME())";

            string hash = BCrypt.Net.BCrypt.HashPassword(txtSenhaCadastro.Text.Trim());

            ConexaoBanco.ExecutarComando(sql,
                new SqlParameter("@nome", txtNomeCadastro.Text.Trim()),
                new SqlParameter("@email", txtEmailCadastro.Text.Trim()),
                new SqlParameter("@hash", hash),
                new SqlParameter("@perfil", cmbPerfilCadastro.SelectedItem!.ToString()),
                new SqlParameter("@tipo", cmbTipoUsuarioCadastro.SelectedItem!.ToString()));
        }

        private void LimparCamposCadastro()
        {
            txtNomeCadastro.Clear();
            txtEmailCadastro.Clear();
            txtSenhaCadastro.Clear();
            cmbPerfilCadastro.SelectedIndex = 0;
            cmbTipoUsuarioCadastro.SelectedIndex = 0;
            txtNomeCadastro.Focus();
        }

        // 2. MÓDULO DE CONSULTA / LISTAGEM
        private void CarregarUsuarios()
        {
            try
            {
                string sql = @"SELECT Id, Nome, Email, Perfil, TipoUsuario, Ativo, DataCadastro 
                       FROM dbo.Usuarios 
                       ORDER BY Nome";

                DataTable dt = ConexaoBanco.ExecutarConsulta(sql);

                // 1. Limpa as colunas antigas/manuais do Designer para não duplicar
                gridUsuarios.DataSource = null;
                gridUsuarios.Columns.Clear();

                // 2. Vincula os dados vindos do banco
                gridUsuarios.AutoGenerateColumns = true;
                gridUsuarios.DataSource = dt;

                // 3. Formata os cabeçalhos
                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar usuários: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrid()
        {
            if (gridUsuarios.Columns.Count == 0) return;

            if (gridUsuarios.Columns["Id"] != null)
                gridUsuarios.Columns["Id"].Visible = false;

            if (gridUsuarios.Columns["Nome"] != null)
                gridUsuarios.Columns["Nome"].HeaderText = "Nome";

            if (gridUsuarios.Columns["Email"] != null)
                gridUsuarios.Columns["Email"].HeaderText = "E-mail";

            if (gridUsuarios.Columns["Perfil"] != null)
                gridUsuarios.Columns["Perfil"].HeaderText = "Perfil";

            if (gridUsuarios.Columns["TipoUsuario"] != null)
                gridUsuarios.Columns["TipoUsuario"].HeaderText = "Tipo de Usuário";

            if (gridUsuarios.Columns["Ativo"] != null)
                gridUsuarios.Columns["Ativo"].HeaderText = "Ativo";

            if (gridUsuarios.Columns["DataCadastro"] != null)
                gridUsuarios.Columns["DataCadastro"].HeaderText = "Data de Cadastro";

            gridUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridUsuarios.ReadOnly = true;
            gridUsuarios.AllowUserToAddRows = false;
            gridUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridUsuarios.MultiSelect = false;
        }

        private void gridUsuarios_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PreencherCamposEdicaoComLinhaSelecionada();
            }
        }

        private void PreencherCamposEdicaoComLinhaSelecionada()
        {
            if (gridUsuarios.CurrentRow == null || gridUsuarios.CurrentRow.Index < 0) return;

            var row = gridUsuarios.CurrentRow;
            txtNomeEdicao.Text = row.Cells["Nome"].Value?.ToString() ?? "";
            txtEmailEdicao.Text = row.Cells["Email"].Value?.ToString() ?? "";
            txtSenhaEdicao.Clear(); // Senha não é exibida por segurança

            string perfil = row.Cells["Perfil"].Value?.ToString() ?? "";
            string tipo = row.Cells["TipoUsuario"].Value?.ToString() ?? "";

            if (cmbPerfilEdicao.Items.Contains(perfil))
                cmbPerfilEdicao.SelectedItem = perfil;

            if (cmbBoxEdicao.Items.Contains(tipo))
                cmbBoxEdicao.SelectedItem = tipo;
        }

        // 3. MÓDULO DE EDIÇÃO
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            ExecutarEdicao();
        }

        private void btnConfirmarEdicao_Click(object sender, EventArgs e)
        {
            ExecutarEdicao();
        }

        private void ExecutarEdicao()
        {
            if (!_usuarioIdSelecionado.HasValue)
            {
                MessageBox.Show("Selecione um usuário na tabela para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarEdicao(out string erro))
            {
                MessageBox.Show(erro, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string email = txtEmailEdicao.Text.Trim();
            if (EmailExisteEmOutroUsuario(email, _usuarioIdSelecionado.Value))
            {
                MessageBox.Show("Este e-mail já está sendo utilizado por outro usuário.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool querAlterarSenha = !string.IsNullOrWhiteSpace(txtSenhaEdicao.Text);

                // 1. Se digitou senha nova, atualiza SenhaHash. Se deixou em branco, mantém a antiga.
                string sql = querAlterarSenha
                    ? @"UPDATE dbo.Usuarios 
                SET Nome = @nome, Email = @email, Perfil = @perfil, TipoUsuario = @tipo, SenhaHash = @hash 
                WHERE Id = @id"
                    : @"UPDATE dbo.Usuarios 
                SET Nome = @nome, Email = @email, Perfil = @perfil, TipoUsuario = @tipo 
                WHERE Id = @id";

                // 2. Parâmetros obrigatórios
                var parametros = new []
        {
            new SqlParameter("@nome", txtNomeEdicao.Text.Trim()),
            new SqlParameter("@email", email),
            new SqlParameter("@perfil", cmbPerfilEdicao.SelectedItem!.ToString()),
            new SqlParameter("@tipo", cmbBoxEdicao.SelectedItem!.ToString())
        };

                // 3. Adiciona o @hash APENAS se o comando SQL pedir a troca de senha
                if (querAlterarSenha)
                {
                    string novoHash = BCrypt.Net.BCrypt.HashPassword(txtSenhaEdicao.Text.Trim());
                    parametros.Append(new SqlParameter("@hash", novoHash));
                }

                // 4. Executa
                ConexaoBanco.ExecutarComando(sql, parametros.ToArray());

                MessageBox.Show("Usuário atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarUsuarios();
                LimparCamposEdicao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarEdicao(out string erro)
        {
            string nome = txtNomeEdicao.Text.Trim();
            string email = txtEmailEdicao.Text.Trim();
            string senha = txtSenhaEdicao.Text;

            if (string.IsNullOrWhiteSpace(nome))
            {
                erro = "Informe o nome.";
                return false;
            }

            if (nome.Any(char.IsDigit))
            {
                erro = "O nome não pode conter números.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                erro = "Informe um e-mail válido.";
                return false;
            }

            if (cmbPerfilEdicao.SelectedItem == null || cmbBoxEdicao.SelectedItem == null)
            {
                erro = "Selecione o perfil e o tipo de usuário.";
                return false;
            }

            // Na edição a senha é opcional: só valida tamanho se foi digitada
            if (!string.IsNullOrWhiteSpace(senha) && senha.Length < 4)
            {
                erro = "A nova senha deve ter no mínimo 4 caracteres.";
                return false;
            }

            erro = string.Empty;
            return true;
        }

        private void LimparCamposEdicao()
        {
            txtNomeEdicao.Clear();
            txtEmailEdicao.Clear();
            txtSenhaEdicao.Clear();
            cmbPerfilEdicao.SelectedIndex = -1;
            cmbBoxEdicao.SelectedIndex = -1;
        }

        // 4. MÓDULO DE INATIVAÇÃO (DELETAR / SOFT DELETE)
        private void btnConfirmaDeletar_Click(object sender, EventArgs e)
        {
            InativarUsuarioSelecionado();
        }

        private void btnConfirmarDeletar_Click(object sender, EventArgs e)
        {
            InativarUsuarioSelecionado();
        }

        private void InativarUsuarioSelecionado()
        {
            if (gridUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Selecione um usuário na tabela para inativar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(gridUsuarios.CurrentRow.Cells["Id"].Value);
            string nome = gridUsuarios.CurrentRow.Cells["Nome"].Value?.ToString() ?? "o usuário";

            // Regra de segurança crítica: não permite inativar a si mesmo
            if (id == SessaoUsuario.Id)
            {
                MessageBox.Show("Você não pode inativar a sua própria conta em sessão!", "Ação Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacao = MessageBox.Show(
                $"Deseja realmente inativar o usuário '{nome}'?\nO usuário perderá o acesso ao sistema.",
                "Confirmar Inativação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao != DialogResult.Yes) return;

            try
            {
                // Soft delete: apenas inativa, preservando a integridade referencial dos empréstimos
                string sql = "UPDATE dbo.Usuarios SET Ativo = 0 WHERE Id = @id";
                ConexaoBanco.ExecutarComando(sql, new SqlParameter("@id", id));

                MessageBox.Show("Usuário inativado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarUsuarios();
                LimparCamposEdicao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inativar usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MÉTODOS AUXILIARES DE BANCO (SQL)
        private bool EmailExiste(string email)
        {
            string sql = "SELECT COUNT(1) FROM dbo.Usuarios WHERE Email = @email";
            var dt = ConexaoBanco.ExecutarConsulta(sql, new SqlParameter("@email", email));
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        private bool EmailExisteEmOutroUsuario(string email, int idAtual)
        {
            string sql = "SELECT COUNT(1) FROM dbo.Usuarios WHERE Email = @email AND Id != @id";
            var dt = ConexaoBanco.ExecutarConsulta(sql,
                new SqlParameter("@email", email),
                new SqlParameter("@id", idAtual));
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }
    }
}