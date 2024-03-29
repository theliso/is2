﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class si2Entities : DbContext
    {
        public si2Entities()
            : base("name=si2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Autor_Submissao> Autor_Submissao { get; set; }
        public virtual DbSet<Conferencia> Conferencia { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Ficheiro> Ficheiro { get; set; }
        public virtual DbSet<Instituicao> Instituicao { get; set; }
        public virtual DbSet<Registo> Registo { get; set; }
        public virtual DbSet<Revisor> Revisor { get; set; }
        public virtual DbSet<Revisor_Submissao> Revisor_Submissao { get; set; }
        public virtual DbSet<Submissao> Submissao { get; set; }
        public virtual DbSet<Utilizador> Utilizador { get; set; }
    
        [DbFunction("si2Entities", "listReviwers")]
        public virtual IQueryable<listReviwers_Result> listReviwers(Nullable<int> idSubmissao, Nullable<int> idConf)
        {
            var idSubmissaoParameter = idSubmissao.HasValue ?
                new ObjectParameter("idSubmissao", idSubmissao) :
                new ObjectParameter("idSubmissao", typeof(int));
    
            var idConfParameter = idConf.HasValue ?
                new ObjectParameter("idConf", idConf) :
                new ObjectParameter("idConf", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<listReviwers_Result>("[si2Entities].[listReviwers](@idSubmissao, @idConf)", idSubmissaoParameter, idConfParameter);
        }
    
        public virtual int assignReviewerToReview(Nullable<int> idRevisor, Nullable<int> idSubmissao)
        {
            var idRevisorParameter = idRevisor.HasValue ?
                new ObjectParameter("idRevisor", idRevisor) :
                new ObjectParameter("idRevisor", typeof(int));
    
            var idSubmissaoParameter = idSubmissao.HasValue ?
                new ObjectParameter("idSubmissao", idSubmissao) :
                new ObjectParameter("idSubmissao", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("assignReviewerToReview", idRevisorParameter, idSubmissaoParameter);
        }
    
        public virtual int deleteSubmission(Nullable<int> idSubmission, Nullable<int> idConf)
        {
            var idSubmissionParameter = idSubmission.HasValue ?
                new ObjectParameter("idSubmission", idSubmission) :
                new ObjectParameter("idSubmission", typeof(int));
    
            var idConfParameter = idConf.HasValue ?
                new ObjectParameter("idConf", idConf) :
                new ObjectParameter("idConf", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("deleteSubmission", idSubmissionParameter, idConfParameter);
        }
    
        public virtual int deleteUser(Nullable<int> idUser)
        {
            var idUserParameter = idUser.HasValue ?
                new ObjectParameter("idUser", idUser) :
                new ObjectParameter("idUser", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("deleteUser", idUserParameter);
        }
    
        public virtual int insertSubmission(string resumo, Nullable<int> idAuthor, Nullable<int> idSub, Nullable<int> idConf, ObjectParameter idSubmissao)
        {
            var resumoParameter = resumo != null ?
                new ObjectParameter("resumo", resumo) :
                new ObjectParameter("resumo", typeof(string));
    
            var idAuthorParameter = idAuthor.HasValue ?
                new ObjectParameter("idAuthor", idAuthor) :
                new ObjectParameter("idAuthor", typeof(int));
    
            var idSubParameter = idSub.HasValue ?
                new ObjectParameter("idSub", idSub) :
                new ObjectParameter("idSub", typeof(int));
    
            var idConfParameter = idConf.HasValue ?
                new ObjectParameter("idConf", idConf) :
                new ObjectParameter("idConf", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertSubmission", resumoParameter, idAuthorParameter, idSubParameter, idConfParameter, idSubmissao);
        }
    
        public virtual int insertUser(string mail, Nullable<int> idInstituicao, string nome, ObjectParameter idUtilizador)
        {
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            var idInstituicaoParameter = idInstituicao.HasValue ?
                new ObjectParameter("idInstituicao", idInstituicao) :
                new ObjectParameter("idInstituicao", typeof(int));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertUser", mailParameter, idInstituicaoParameter, nomeParameter, idUtilizador);
        }
    
        public virtual int registerReview(Nullable<int> idSubmissao, Nullable<decimal> nota, string texto)
        {
            var idSubmissaoParameter = idSubmissao.HasValue ?
                new ObjectParameter("idSubmissao", idSubmissao) :
                new ObjectParameter("idSubmissao", typeof(int));
    
            var notaParameter = nota.HasValue ?
                new ObjectParameter("nota", nota) :
                new ObjectParameter("nota", typeof(decimal));
    
            var textoParameter = texto != null ?
                new ObjectParameter("texto", texto) :
                new ObjectParameter("texto", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("registerReview", idSubmissaoParameter, notaParameter, textoParameter);
        }
    
        public virtual int updateConference(Nullable<int> idConferencia, string nome, Nullable<int> ano, string presidenteMail, Nullable<System.DateTime> dataLimite, Nullable<int> notaMinima)
        {
            var idConferenciaParameter = idConferencia.HasValue ?
                new ObjectParameter("idConferencia", idConferencia) :
                new ObjectParameter("idConferencia", typeof(int));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            var anoParameter = ano.HasValue ?
                new ObjectParameter("ano", ano) :
                new ObjectParameter("ano", typeof(int));
    
            var presidenteMailParameter = presidenteMail != null ?
                new ObjectParameter("PresidenteMail", presidenteMail) :
                new ObjectParameter("PresidenteMail", typeof(string));
    
            var dataLimiteParameter = dataLimite.HasValue ?
                new ObjectParameter("dataLimite", dataLimite) :
                new ObjectParameter("dataLimite", typeof(System.DateTime));
    
            var notaMinimaParameter = notaMinima.HasValue ?
                new ObjectParameter("notaMinima", notaMinima) :
                new ObjectParameter("notaMinima", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateConference", idConferenciaParameter, nomeParameter, anoParameter, presidenteMailParameter, dataLimiteParameter, notaMinimaParameter);
        }
    
        public virtual int updateSubmission(Nullable<int> idSubmission, string estado, string resumo)
        {
            var idSubmissionParameter = idSubmission.HasValue ?
                new ObjectParameter("idSubmission", idSubmission) :
                new ObjectParameter("idSubmission", typeof(int));
    
            var estadoParameter = estado != null ?
                new ObjectParameter("estado", estado) :
                new ObjectParameter("estado", typeof(string));
    
            var resumoParameter = resumo != null ?
                new ObjectParameter("resumo", resumo) :
                new ObjectParameter("resumo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateSubmission", idSubmissionParameter, estadoParameter, resumoParameter);
        }
    
        public virtual int updateSubmissionsState(Nullable<int> limiteCorte, Nullable<int> idConf)
        {
            var limiteCorteParameter = limiteCorte.HasValue ?
                new ObjectParameter("limiteCorte", limiteCorte) :
                new ObjectParameter("limiteCorte", typeof(int));
    
            var idConfParameter = idConf.HasValue ?
                new ObjectParameter("idConf", idConf) :
                new ObjectParameter("idConf", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateSubmissionsState", limiteCorteParameter, idConfParameter);
        }
    
        public virtual int updateUser(string mail, Nullable<int> idInst, string nome)
        {
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            var idInstParameter = idInst.HasValue ?
                new ObjectParameter("idInst", idInst) :
                new ObjectParameter("idInst", typeof(int));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateUser", mailParameter, idInstParameter, nomeParameter);
        }
    
        public virtual int updateUserRole(Nullable<int> idUser, Nullable<int> idConf)
        {
            var idUserParameter = idUser.HasValue ?
                new ObjectParameter("idUser", idUser) :
                new ObjectParameter("idUser", typeof(int));
    
            var idConfParameter = idConf.HasValue ?
                new ObjectParameter("idConf", idConf) :
                new ObjectParameter("idConf", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateUserRole", idUserParameter, idConfParameter);
        }
    }
}
