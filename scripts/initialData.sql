use si2;
set NOCOUNT ON

--Instituicao
insert into dbo.Instituicao (nome, morada, pais) values ('ISEL', 'Rua Marvila 1', 'Portugal');
insert into dbo.Instituicao (nome, morada, pais) values ('Tecnico', 'Rua Alameda 1', 'Portugal');
insert into dbo.Instituicao (nome, morada, pais) values ('FCT Nova', 'Costa da Caparica', 'Portugal');

--Utilizador 
insert into dbo.Utilizador (mail, idInstituicao, nome) values ('user1@mail.com', 1, 'Dean Coff');
insert into dbo.Utilizador (mail, idInstituicao, nome) values ('user2@mail.com', 1, 'Thiago Hilder');
insert into dbo.Utilizador (mail, idInstituicao, nome) values ('user3@mail.com', 2, 'Adolfo Morais');
insert into dbo.Utilizador (mail, idInstituicao, nome) values ('user4@mail.com', 1, 'Kate Marshall');
insert into dbo.Utilizador (mail, idInstituicao, nome) values ('user5@mail.com', 3, 'John Snow');


--Revisor
insert into dbo.Revisor (idUtilizador) values(1);
insert into dbo.Revisor (idUtilizador) values(2);

--Conferencia
insert into dbo.Conferencia (idPresidente, acronimo, nome, ano, dataRealizacao) values (1, 'WSL', 'WebSummit Lisboa', 2018, '2018-09-10');
insert into dbo.Conferencia (idPresidente, acronimo, nome, ano, dataRealizacao) values (2, 'CCP', 'Comic Con Portugal', 2018, '2018-10-16');
insert into dbo.Conferencia (idPresidente, acronimo, nome, ano, dataRealizacao) values (1, 'EAC', 'Environment Aspect Conference', 2020, '2020-01-28');

--Registo
insert into dbo.Registo (idUtilizador, idConferencia, dataRegisto) values (1, 2, '2018-09-10');
insert into dbo.Registo (idUtilizador, idConferencia, dataRegisto) values (3, 2, '2018-09-14');
insert into dbo.Registo (idUtilizador, idConferencia, dataRegisto) values (2, 1, '2018-09-20');
insert into dbo.Registo (idUtilizador, idConferencia, dataRegisto) values (4, 1, '2018-04-20');

--Estado
insert into dbo.Estado (descricao) values ('Em Revisao');
insert into dbo.Estado (descricao) values ('Rejeitado');
insert into dbo.Estado (descricao) values ('Aceite');

--Submissao
insert into dbo.Submissao (idEstado, resumo, dataSubmissao) values (3, 'Então Allah desceu à Terra porque lhe apeteceu', '2018-10-02');
insert into dbo.Submissao (idEstado, resumo, dataSubmissao) values (1, 'Boots Boots Boots Boots', '2018-09-25');
insert into dbo.Submissao (idEstado, resumo, dataSubmissao) values (2, 'Laranjas e alperces', '2018-09-22');

--Autor
insert into dbo.Autor (idUtilizador) values (1); 
insert into dbo.Autor (idUtilizador) values (2); 
insert into dbo.Autor (idUtilizador) values (3); 

--Ficheiro
insert into dbo.Ficheiro (nome, extensao, idSubmissao) values ('Bananas', '.pdf', 1)
insert into dbo.Ficheiro (nome, extensao, idSubmissao) values ('Melao', '.pdf', 2)
insert into dbo.Ficheiro (nome, extensao, idSubmissao) values ('Ananas', '.pdf', 3)

--Submissao_Conferencia
insert into dbo.Submissao_Conferencia (idConferencia, idSubmissao) values (2,1)
insert into dbo.Submissao_Conferencia (idConferencia, idSubmissao) values (1,3)
insert into dbo.Submissao_Conferencia (idConferencia, idSubmissao) values (2,2)

--Revisor_Submissao
insert into dbo.Revisor_Submissao (idSubmissao, idRevisor, nota, texto, revisto) values (1, 1, 65, 'Muito good', 1);
insert into dbo.Revisor_Submissao (idSubmissao, idRevisor, revisto) values (2, 2, 0);
insert into dbo.Revisor_Submissao (idSubmissao, idRevisor, nota, texto, revisto) values (3, 2, 8, 'Very mau', 1);

--Autor_Submissao
insert into dbo.Autor_Submissao (idUtilizador, idSubmissao) values (1,2);
insert into dbo.Autor_Submissao (idUtilizador, idSubmissao) values (2,3);
insert into dbo.Autor_Submissao (idUtilizador, idSubmissao) values (3,1);