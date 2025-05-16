using AtividadeEcommerce2.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace AtividadeEcommerce2.Repositorio
{  // Classe responsável por interagir com os dados de produtos no banco de dados
        public class ProdutoRepositorio
        {
            // Declara uma variável privada somente leitura para armazenar a string de conexão com o MySQL
            private readonly string _conexaoMySQL;

            // Construtor que recebe a configuração e obtém a string de conexão
            public ProdutoRepositorio(IConfiguration configuration)
            {
                // Obtém a string de conexão a partir do arquivo de configuração
                _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
            }

            // Método para cadastrar um novo produto no banco de dados
            public void Cadastrar(Produto produto)
            {
                // Bloco 'using' para garantir que a conexão será fechada automaticamente
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    // Abre a conexão com o banco de dados MySQL
                    conexao.Open();

                    // Cria um novo comando SQL para inserir dados na tabela 'produto'
                    MySqlCommand cmd = new MySqlCommand("insert into produto (CodProd, Nome, Quantidade, Descricao, Preco) values (@codProd, @nome, @quantidade, @descricao, @preco)", conexao);

                    // Adiciona parâmetros para os valores de produto a serem inseridos
                    cmd.Parameters.Add("@codProd", MySqlDbType.VarChar).Value = produto.CodProd;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.Add("@quantidade", MySqlDbType.VarChar).Value = produto.Quantidade;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                    cmd.Parameters.Add("@preco", MySqlDbType.Double).Value = produto.Preco;

                    // Executa o comando SQL de inserção
                    cmd.ExecuteNonQuery();

                    // Fecha a conexão com o banco de dados
                    conexao.Close();
                }
            }

            // Método para atualizar (editar) os dados de um produto existente no banco de dados
            public bool Atualizar(Produto produto)
            {
                try
                {
                    // Bloco 'using' para garantir que a conexão será fechada automaticamente
                    using (var conexao = new MySqlConnection(_conexaoMySQL))
                    {
                        // Abre a conexão com o banco de dados MySQL
                        conexao.Open();

                        // Cria um novo comando SQL para atualizar os dados do produto com base no código
                        MySqlCommand cmd = new MySqlCommand("Update produto set Nome=@nome, Quantidade=@quantidade, Descricao=@descricao, Preco=@preco where CodProd=@codigo", conexao);

                        // Adiciona os parâmetros para os valores de produto a serem atualizados
                        cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = produto.CodProd;
                        cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                        cmd.Parameters.Add("@quantidade", MySqlDbType.VarChar).Value = produto.Quantidade;
                        cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                        cmd.Parameters.Add("@preco", MySqlDbType.Double).Value = produto.Preco;

                        // Executa o comando SQL de atualização e retorna o número de linhas afetadas
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        // Retorna true se ao menos uma linha foi atualizada
                        return linhasAfetadas > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    // Caso haja um erro durante a atualização, exibe a mensagem no console
                    Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                    return false; // Retorna false em caso de erro
                }
            }

            // Método para listar todos os produtos do banco de dados
            public IEnumerable<Produto> TodosProdutos()
            {
                // Cria uma lista para armazenar os produtos
                List<Produto> productList = new List<Produto>();

                // Bloco 'using' para garantir que a conexão será fechada automaticamente
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    // Abre a conexão com o banco de dados MySQL
                    conexao.Open();

                    // Cria um comando SQL para selecionar todos os produtos
                    MySqlCommand cmd = new MySqlCommand("SELECT * from produto", conexao);

                    // Cria um adaptador de dados para preencher um DataTable com os resultados
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    // Cria um novo DataTable para armazenar os dados retornados
                    DataTable dt = new DataTable();

                    // Preenche o DataTable com os dados retornados pela consulta
                    da.Fill(dt);

                    // Fecha a conexão com o banco de dados
                    conexao.Close();

                    // Itera sobre cada linha do DataTable e cria um objeto Produto com os dados da linha
                    foreach (DataRow dr in dt.Rows)
                    {
                        productList.Add(new Produto
                        {
                            CodProd = Convert.ToInt32(dr["CodProd"]),
                            Nome = (string)dr["Nome"],
                            Quantidade = Convert.ToInt32(dr["Quantidade"]),
                            Descricao = (string)dr["Descricao"],
                            Preco = Convert.ToDouble(dr["Preco"])
                        });
                    }

                    // Retorna a lista de todos os produtos
                    return productList;
                }
            }

            // Método para buscar um produto específico pelo código
            public Produto ObterProduto(int Codigo)
            {
                // Bloco 'using' para garantir que a conexão será fechada automaticamente
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    // Abre a conexão com o banco de dados MySQL
                    conexao.Open();

                    // Cria um comando SQL para buscar um produto pelo código
                    MySqlCommand cmd = new MySqlCommand("SELECT * from produto where CodProd=@codigo", conexao);

                    // Adiciona o parâmetro do código para a consulta
                    cmd.Parameters.AddWithValue("@codigo", Codigo);

                    // Cria um leitor de dados para capturar o resultado da consulta
                    MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    // Cria um objeto Produto para armazenar o resultado
                    Produto produto = new Produto();

                    // Lê os dados do produto
                    while (dr.Read())
                    {
                        produto.CodProd = Convert.ToInt32(dr["CodProd"]);
                        produto.Nome = (string)(dr["Nome"]);
                        produto.Quantidade = Convert.ToInt32(dr["Quantidade"]);
                        produto.Descricao = (string)(dr["Descricao"]);
                        produto.Preco = Convert.ToDouble(dr["Preco"]);
                    }

                    // Retorna o produto encontrado (ou um produto com valores padrão se não encontrado)
                    return produto;
                }
            }

            // Método para excluir um produto do banco de dados pelo código
            public void Excluir(int Id)
            {
                // Bloco 'using' para garantir que a conexão será fechada automaticamente
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    // Abre a conexão com o banco de dados MySQL
                    conexao.Open();

                    // Cria um comando SQL para deletar um produto com base no código
                    MySqlCommand cmd = new MySqlCommand("delete from produto where CodProd=@codigo", conexao);

                    // Adiciona o parâmetro do código do produto a ser excluído
                    cmd.Parameters.AddWithValue("@codigo", Id);

                    // Executa o comando SQL de exclusão
                    cmd.ExecuteNonQuery();

                    // Fecha a conexão com o banco de dados
                    conexao.Close();
                }
            }
        }
    }