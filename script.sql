create database dbAtividadeEcommerce2;

use dbAtividadeEcommerce2;

create table tbUsuario(
Id int auto_increment primary key,
Nome varchar (100) not null,
Email varchar (50) not null,
senha varchar (50) not null

);

create table tbProduto(
Id int auto_increment primary key,
Nome varchar (100) not null,
Descricao varchar (200) not null,
Preco decimal (10,2) ,
Quantidade int 

);

select * from tbUsuario;
select * from tbProduto;

insert into tbUsuario (Nome,Email,Senha)values
('Hn', 'hn@gmail.com', '1234');
