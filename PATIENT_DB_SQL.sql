CREATE TABLE [dbo].[Patients] (
	[Id]				   INT            IDENTITY (1, 1) NOT NULL,
    [PatientName]          NVARCHAR (50) NULL,
    [PatientSurname]       NVARCHAR (50) NULL,
	[PatientMail]		   NVARCHAR (50) NULL,
    [PatientPhone]		   INT            NULL,
	PRIMARY KEY (Id),
);

CREATE TABLE [dbo].[LoginPatient] (
    [Id]				  INT            IDENTITY (1, 1) NOT NULL,
    [PatientLogin]        NVARCHAR (40) NULL,
    [PatientPassword]     NVARCHAR (70) NULL,
	[PatientIndex]        INT,
	PRIMARY KEY (Id),
);

CREATE TABLE [dbo].[Visits] (
	[Id]				  INT            IDENTITY (1, 1) NOT NULL,
    [Specialization]	  NVARCHAR (50) NOT NULL,
    [VisitDate]           DATE NULL,
	[VisitTime]			  TIME NULL,
    [PrivateNFZ]		  NVARCHAR (10) NULL,
    [PatientId]			  INT NOT NULL,
	[DoctorId]			  INT NOT NULL,
	PRIMARY KEY (Id),
);

CREATE TABLE [dbo].[Doctors] (
	[Id]						   INT            IDENTITY (1, 1) NOT NULL,
    [DoctorName]				   NVARCHAR (50) NULL,
    [DoctorSurname]				   NVARCHAR (50) NULL,
	[DoctorSpecialization]		   NVARCHAR (50) NULL,
	PRIMARY KEY (Id),
);