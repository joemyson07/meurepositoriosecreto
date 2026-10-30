using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using estetica_bemestar.Models;

namespace estetica_bemestar.Data;

public partial class EsteticaBemestarContext : DbContext
{
    public EsteticaBemestarContext()
    {
    }

    public EsteticaBemestarContext(DbContextOptions<EsteticaBemestarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Atendimento> Atendimentos { get; set; }

    public virtual DbSet<Atendimentoitem> Atendimentoitems { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<EntradaProduto> EntradaProdutos { get; set; }

    public virtual DbSet<Estoque> Estoques { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Pessoa> Pessoas { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<VwAtendimento> VwAtendimentos { get; set; }

    public virtual DbSet<VwFuncionarioEndereco> VwFuncionarioEnderecos { get; set; }

    public virtual DbSet<VwPessoaEndereco> VwPessoaEnderecos { get; set; }

    public virtual DbSet<VwProdutoCategorium> VwProdutoCategoria { get; set; }

    public virtual DbSet<VwServicoCategorium> VwServicoCategoria { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=estetica_bemestar;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.2.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Atendimento>(entity =>
        {
            entity.HasKey(e => e.Idatendimento).HasName("PRIMARY");

            entity.ToTable("atendimento");

            entity.HasIndex(e => e.Idpessoa, "idpessoa");

            entity.Property(e => e.Idatendimento).HasColumnName("idatendimento");
            entity.Property(e => e.DataAtendimento)
                .HasColumnType("datetime")
                .HasColumnName("data_atendimento");
            entity.Property(e => e.Idpessoa).HasColumnName("idpessoa");
            entity.Property(e => e.StatusAtendimento)
                .HasMaxLength(20)
                .HasColumnName("status_atendimento");
            entity.Property(e => e.ValorTotal)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("valor_total");

            entity.HasOne(d => d.IdpessoaNavigation).WithMany(p => p.Atendimentos)
                .HasForeignKey(d => d.Idpessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("atendimento_ibfk_1");
        });

        modelBuilder.Entity<Atendimentoitem>(entity =>
        {
            entity.HasKey(e => new { e.Idatendimento, e.Iditem })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("atendimentoitem");

            entity.HasIndex(e => e.Idfuncionario, "idfuncionario");

            entity.HasIndex(e => e.Iditem, "iditem");

            entity.Property(e => e.Idatendimento).HasColumnName("idatendimento");
            entity.Property(e => e.Iditem).HasColumnName("iditem");
            entity.Property(e => e.Idfuncionario).HasColumnName("idfuncionario");
            entity.Property(e => e.QuantidadeItens).HasColumnName("quantidade_itens");
            entity.Property(e => e.ValorUnitarioVenda)
                .HasPrecision(10, 2)
                .HasColumnName("valor_unitario_venda");

            entity.HasOne(d => d.IdatendimentoNavigation).WithMany(p => p.Atendimentoitems)
                .HasForeignKey(d => d.Idatendimento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("atendimentoitem_ibfk_1");

            entity.HasOne(d => d.IdfuncionarioNavigation).WithMany(p => p.Atendimentoitems)
                .HasForeignKey(d => d.Idfuncionario)
                .HasConstraintName("atendimentoitem_ibfk_3");

            entity.HasOne(d => d.IditemNavigation).WithMany(p => p.Atendimentoitems)
                .HasForeignKey(d => d.Iditem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("atendimentoitem_ibfk_2");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.HasIndex(e => e.Categoria, "categoria").IsUnique();

            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Categoria)
                .HasMaxLength(35)
                .HasColumnName("categoria");
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.Idendereco).HasName("PRIMARY");

            entity.ToTable("endereco");

            entity.HasIndex(e => e.Idpessoa, "idpessoa").IsUnique();

            entity.Property(e => e.Idendereco).HasColumnName("idendereco");
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("cep");
            entity.Property(e => e.Complemento)
                .HasMaxLength(150)
                .HasDefaultValueSql("''")
                .HasColumnName("complemento");
            entity.Property(e => e.Idpessoa).HasColumnName("idpessoa");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .HasColumnName("numero");

            entity.HasOne(d => d.IdpessoaNavigation).WithOne(p => p.Endereco)
                .HasForeignKey<Endereco>(d => d.Idpessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("endereco_ibfk_1");
        });

        modelBuilder.Entity<EntradaProduto>(entity =>
        {
            entity.HasKey(e => e.IdentradaProduto).HasName("PRIMARY");

            entity.ToTable("entrada_produto");

            entity.HasIndex(e => e.Iditem, "iditem");

            entity.Property(e => e.IdentradaProduto).HasColumnName("identrada_produto");
            entity.Property(e => e.DataEntradaProduto).HasColumnName("data_entrada_produto");
            entity.Property(e => e.Iditem).HasColumnName("iditem");
            entity.Property(e => e.QuantidadeEntrada).HasColumnName("quantidade_entrada");
            entity.Property(e => e.ValorUnitarioEntrada)
                .HasPrecision(10, 2)
                .HasColumnName("valor_unitario_entrada");

            entity.HasOne(d => d.IditemNavigation).WithMany(p => p.EntradaProdutos)
                .HasForeignKey(d => d.Iditem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrada_produto_ibfk_1");
        });

        modelBuilder.Entity<Estoque>(entity =>
        {
            entity.HasKey(e => e.Iditem).HasName("PRIMARY");

            entity.ToTable("estoque");

            entity.Property(e => e.Iditem)
                .ValueGeneratedNever()
                .HasColumnName("iditem");
            entity.Property(e => e.QuantidadeEstoque).HasColumnName("quantidade_estoque");

            entity.HasOne(d => d.IditemNavigation).WithOne(p => p.Estoque)
                .HasForeignKey<Estoque>(d => d.Iditem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estoque_ibfk_1");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.Idpessoa).HasName("PRIMARY");

            entity.ToTable("funcionario");

            entity.Property(e => e.Idpessoa)
                .ValueGeneratedNever()
                .HasColumnName("idpessoa");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .HasColumnName("cargo");
            entity.Property(e => e.DataAdmissao).HasColumnName("data_admissao");

            entity.HasOne(d => d.IdpessoaNavigation).WithOne(p => p.Funcionario)
                .HasForeignKey<Funcionario>(d => d.Idpessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("funcionario_ibfk_1");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Iditem).HasName("PRIMARY");

            entity.ToTable("item");

            entity.HasIndex(e => e.Idcategoria, "idcategoria");

            entity.Property(e => e.Iditem).HasColumnName("iditem");
            entity.Property(e => e.AtivoProdutoServico)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("ativo_produto_servico");
            entity.Property(e => e.DescricaoItem)
                .HasMaxLength(200)
                .HasColumnName("descricao_item");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Item1)
                .HasMaxLength(40)
                .HasColumnName("item");
            entity.Property(e => e.Preco)
                .HasPrecision(10, 2)
                .HasColumnName("preco");
            entity.Property(e => e.TipoItem)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("tipo_item");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.Idcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("item_ibfk_1");
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.Idpessoa).HasName("PRIMARY");

            entity.ToTable("pessoa");

            entity.HasIndex(e => e.Cpf, "cpf").IsUnique();

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Idpessoa).HasColumnName("idpessoa");
            entity.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("ativo");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpf");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("sexo");
            entity.Property(e => e.Telefone)
                .HasMaxLength(11)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Iditem).HasName("PRIMARY");

            entity.ToTable("produto");

            entity.Property(e => e.Iditem)
                .ValueGeneratedNever()
                .HasColumnName("iditem");
            entity.Property(e => e.CodigoPreco).HasColumnName("codigo_preco");
            entity.Property(e => e.PrecoVenda)
                .HasPrecision(10, 2)
                .HasColumnName("preco_venda");
            entity.Property(e => e.Unidade)
                .HasMaxLength(10)
                .HasColumnName("unidade");

            entity.HasOne(d => d.IditemNavigation).WithOne(p => p.Produto)
                .HasForeignKey<Produto>(d => d.Iditem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("produto_ibfk_1");
        });

        modelBuilder.Entity<VwAtendimento>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_atendimento");

            entity.Property(e => e.Cliente)
                .HasMaxLength(100)
                .HasColumnName("cliente");
            entity.Property(e => e.DataAtendimento)
                .HasColumnType("datetime")
                .HasColumnName("data_atendimento");
            entity.Property(e => e.Idatendimento).HasColumnName("idatendimento");
            entity.Property(e => e.Idpessoa).HasColumnName("idpessoa");
            entity.Property(e => e.StatusAtendimento)
                .HasMaxLength(20)
                .HasColumnName("status_atendimento");
        });

        modelBuilder.Entity<VwFuncionarioEndereco>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_funcionario_endereco");

            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .HasColumnName("cargo");
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("cep");
            entity.Property(e => e.Complemento)
                .HasMaxLength(150)
                .HasDefaultValueSql("''")
                .HasColumnName("complemento");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpf");
            entity.Property(e => e.DataAdmissao).HasColumnName("data_admissao");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Idpessoa).HasColumnName("idpessoa");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .HasColumnName("numero");
            entity.Property(e => e.Telefone)
                .HasMaxLength(11)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<VwPessoaEndereco>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_pessoa_endereco");

            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("cep");
            entity.Property(e => e.Complemento)
                .HasMaxLength(150)
                .HasDefaultValueSql("''")
                .HasColumnName("complemento");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpf");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Idpessoa).HasColumnName("idpessoa");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .HasColumnName("numero");
            entity.Property(e => e.Telefone)
                .HasMaxLength(11)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<VwProdutoCategorium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_produto_categoria");

            entity.Property(e => e.AtivoProdutoServico)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("ativo_produto_servico");
            entity.Property(e => e.Categoria)
                .HasMaxLength(35)
                .HasColumnName("categoria");
            entity.Property(e => e.DescricaoItem)
                .HasMaxLength(200)
                .HasColumnName("descricao_item");
            entity.Property(e => e.Iditem).HasColumnName("iditem");
            entity.Property(e => e.Item)
                .HasMaxLength(40)
                .HasColumnName("item");
            entity.Property(e => e.Preco)
                .HasPrecision(10, 2)
                .HasColumnName("preco");
            entity.Property(e => e.PrecoVenda)
                .HasPrecision(10, 2)
                .HasColumnName("preco_venda");
            entity.Property(e => e.QuantidadeEstoque).HasColumnName("quantidade_estoque");
            entity.Property(e => e.Unidade)
                .HasMaxLength(10)
                .HasColumnName("unidade");
        });

        modelBuilder.Entity<VwServicoCategorium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_servico_categoria");

            entity.Property(e => e.AtivoProdutoServico)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("ativo_produto_servico");
            entity.Property(e => e.Categoria)
                .HasMaxLength(35)
                .HasColumnName("categoria");
            entity.Property(e => e.DescricaoItem)
                .HasMaxLength(200)
                .HasColumnName("descricao_item");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Iditem).HasColumnName("iditem");
            entity.Property(e => e.Item)
                .HasMaxLength(40)
                .HasColumnName("item");
            entity.Property(e => e.Preco)
                .HasPrecision(10, 2)
                .HasColumnName("preco");
            entity.Property(e => e.TipoItem)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("tipo_item");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
