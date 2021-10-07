CREATE TABLE [dbo].[Adrese] (
    [id]     INT          NOT NULL,
    [broj]   VARCHAR (20) NULL,
    [ulica]  VARCHAR (30) NULL,
    [drzava] VARCHAR (20) NULL,
    [grad]   VARCHAR (20) NULL,
    [active] BIT          NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

insert into Adrese values(641,'2100','Radnicka 21','Srbija','Novi Sad',1);
insert into Adrese values(642,'2100','Radnicka 31','Srbija','Novi Sad',1);
insert into Adrese values(643,'1100','Ustanicka','Srbija','Beograd',0);
insert into Adrese values(847,'1300','Karadjordjeva 22','Srbija','Nis',1);

CREATE TABLE [dbo].[DomZdravlja] (
    [id]        INT          NOT NULL,
    [naziv]     VARCHAR (30) NULL,
    [adresa_id] INT          NULL,
    [active]    BIT          NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([adresa_id]) REFERENCES [dbo].[Adrese] ([id])
);

insert into domZdravlja values (390,'Dom Zdravlja Novi Sad',641,1);
insert into domZdravlja values (391,'Dom Zdravlja Beograd',643,1);
insert into domZdravlja values (392,'Dom Zdravlja Novi Sad2',642,1);
insert into domZdravlja values (393,'Dom Zdravlja ZR',643,1);


CREATE TABLE [dbo].[Pacijenti] (
    [Id]        INT          NOT NULL,
    [ime]       VARCHAR (20) NULL,
    [prezime]   VARCHAR (20) NULL,
    [lozinka]   VARCHAR (20) NULL,
    [email]     VARCHAR (20) NULL,
    [jmbg]      VARCHAR (13) NULL,
    [pol]       VARCHAR (20) NULL,
    [aktivan]   BIT          NULL,
    [adresa_id] INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([adresa_id]) REFERENCES [dbo].[Adrese] ([id])
);

insert into pacijenti values(100,'Slobodan','Termin','s','s','123',0,1,641);
insert into pacijenti values(101,'Jovan','Jovanovic','jovo123','jovo@gmail.com','111',0,1,642);
insert into pacijenti values(102,'Zoran','Jovanovic','zoki123','zoki@gmail.com','112',0,1,643);
insert into pacijenti values(103,'Dejan','Ivanovic','deki123','deki@gmail.com','113',0,1,642);


CREATE TABLE [dbo].[Doktori] (
    [Id]             INT          NOT NULL,
    [ime]            VARCHAR (20) NULL,
    [prezime]        VARCHAR (20) NULL,
    [lozinka]        VARCHAR (20) NULL,
    [email]          VARCHAR (20) NULL,
    [jmbg]           VARCHAR (13) NULL,
    [pol]            VARCHAR (20) NULL,
    [aktivan]        BIT          NULL,
    [adresa_id]      INT          NULL,
    [domZdravlja_id] INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([adresa_id]) REFERENCES [dbo].[Adrese] ([id]),
    FOREIGN KEY ([domZdravlja_id]) REFERENCES [dbo].[DomZdravlja] ([id])
);

insert into doktori values(768,'Dado','Dadovic','dado','dado@gmail.com','222',0,1,642,390);
insert into doktori values(760,'Pajo','Pajovic','pajo','pajo@gmail.com','333',0,1,643,391);
insert into doktori values(770,'Dragan','Jovanovic','dragan','drago@gmail.com','444',0,1,645,392);
insert into doktori values(769,'Neso','Nesovic','neso','neso@gmail.com','444',0,1,644,392);

CREATE TABLE [dbo].[Terapije] (
    [Id]          INT          NOT NULL,
    [opis]        VARCHAR (30) NULL,
    [lekar_id]    INT          NULL,
    [pacijent_id] INT          NULL,
    [aktivan]     BIT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([lekar_id]) REFERENCES [dbo].[Doktori] ([Id]),
    FOREIGN KEY ([pacijent_id]) REFERENCES [dbo].[Pacijenti] ([Id])
);

insert into terapije values(100,'Super sve',768,102,1);
insert into terapije values(102,'Super sve2',768,102,1);
insert into terapije values(101,'Okej sve',769,103,1);


CREATE TABLE [dbo].[Termini] (
    [id]          INT          NOT NULL,
    [lekar_id]    INT          NULL,
    [datum]       DATETIME     NULL,
    [status]      VARCHAR (20) NULL,
    [pacijent_id] INT          NULL,
    [active]      BIT          NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([lekar_id]) REFERENCES [dbo].[Doktori] ([Id]),
    FOREIGN KEY ([pacijent_id]) REFERENCES [dbo].[Pacijenti] ([Id])
);

insert into termini values (100,768,'31-1-2021 00:00:00',0,100,1);
insert into termini values (100,768,'01-2-2021 00:00:00',1,101,1);





