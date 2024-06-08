create table Admin_Profile
(
Admin_Id int primary key identity,
FullName varchar(50) not null,
Email varchar(50) not null unique check(Email like '@gmail.com'), 
Password varchar(50) not null,
Contact bigint  not null unique check(Contact >=6000000000 AND Contact <= 9999999999),
Address varchar(max),
DOB date not null,
Age as FLOOR(DATEDIFF(DAY,DOB,GETDATE())/365.25),
Gender varchar(10) check(Gender in ('male','female')),
AdminImage nvarchar(max),
CreatedAt  datetime default getdate(),
UpdatedAt datetime default getdate()
)


--************************************************************************************************************************************************
											--Doctor Entity
CREATE TABLE Doctors_Profile
(
    Doctor_Id INT PRIMARY KEY IDENTITY, 
    fullName VARCHAR(50) NOT NULL, 
    Email VARCHAR(50) NOT NULL unique,
    CONSTRAINT chk_Email CHECK (Email LIKE '%@gmail.com'), 
    Contact BIGINT CHECK (Contact >= 6000000000 AND Contact <= 9999999999),
    Address NVARCHAR(MAX),
    DOB DATETIME NOT NULL,
    Age AS (DATEDIFF(YEAR, DOB, GETDATE())),
    Gender VARCHAR(10) CHECK (Gender IN ('Male', 'Female')),
    Qualification NVARCHAR(MAX) NOT NULL,
    Specialization NVARCHAR(100) NOT NULL,
    CONSTRAINT chk_Specialization CHECK (Specialization IN ('Cardiologist', 'Dermatology', 'Psychiatry', 'Nephrologists', 'Surgery')),
    Experience INT NOT NULL,
    DoctorImage NVARCHAR(MAX),
    IsTrash BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

exec sp_rename 'Doctors_Profile.DOB','DateOfBirth','COLUMN';

select * from  Doctors_Profile
--*****************************************************************************************************
										--Register Doctor
alter proc usp_RegisterDoc
(
@fullName varchar(50),
@email varchar(50),
@contact bigint,
@address nvarchar(max),
@dob datetime,
@gender varchar(10),
@qualification nvarchar(max),
@specailization nvarchar(100),
@experience int,
@doctorImage nvarchar(max)
)
as
begin
insert into Doctors_Profile (fullName,Email,Contact,Address,DOB,Gender,Qualification,Specialization,Experience,DoctorImage)
values 
(@fullName,@email,@contact,@address,@dob,@gender,@qualification,@specailization,@experience,@doctorImage)
end


exec usp_RegisterDoc 'doc1','doc1@gmail.com','abcedef',9638574121,'viefv','2001-02-02','male','mbbs','Dermatology',5,'abc_image';


--***************************************************************************************
											--FetchDoctorbyId

alter proc usp_FetchById
(
@doctor_Id int
)
as
begin
select * from Doctors_Profile where Doctor_Id=@doctor_Id
end

exec usp_FetchById 2

--*******************************************************************************
												--FetchAllDoctors

alter proc usp_FetchAllDoctors
as
begin
select * from Doctors_Profile where IsTrash=0
end

--*************************************************************************************************************
												--UpdateDoctor

create proc usp_Edit
(
@doctorid int,
@fullName varchar(50) ,
@email varchar(50),
@contact bigint,
@address nvarchar(max),
@dob datetime,
@gender varchar(10),
@qualification nvarchar(max),
@specailization nvarchar(100),
@experience int,
@doctorImage nvarchar(max)
)
as
begin
update Doctors_profile set fullName=@fullName,Email=@email,Contact=@contact,Address=@address,DOB=@dob,Gender=@gender,Qualification=@qualification,
Specialization=@specailization,Experience=@experience,DoctorImage=@doctorImage,UpdatedAt=getdate() where Doctor_Id=@doctorid;
end

exec usp_Edit 2,'sandesh kumar','sandesh@gmail.com',9110894393,'bengaluru','12-06-2008','male','masters','Cardiologist',5,'asdgh'

select * from doctors_profile
--*************************************************************************************************************
										--Delete doctor profile
alter proc usp_DeleteDoctor
(
@doctorid int
)
as 
begin
update doctors_profile set IsTrash=1 where Doctor_Id=@doctorid
end

exec usp_DeleteDoctor 5
select * from doctors_profile

--*************************************************************************************************************
													--Login Doctor
create proc usp_LoginDoctor
(
@doctorid int,
@doctorName nvarchar(50)
)
as
begin 
begin try
select * from Doctors_Profile where Doctor_Id=@doctorid and fullName=@doctorName
--RAISERROR(N'Patient not found for id: %d', 16, 1, @patientid);
end try
begin catch
throw;
end catch
end;


exec usp_LoginDoctor 9, 'sandesh kumar ns'












--*************************************************************************************************************
	
												--Patient Entity

create table Patients_Profile(
	PatientId int primary key identity,
	FullName nvarchar(50) not null,
	Email nvarchar(50) not null unique check(Email like '%@gmail.com'),
	Contact bigint check(Contact >= 6000000000 AND Contact <= 9999999999),
	Address nvarchar(max),
	DOB datetime not null,
	Age as (DATEDIFF(YEAR,DOB,GETDATE())), -- Adding Computed column
	Gender varchar(10) check(Gender in ('Male', 'Female')),
	PatientImage nvarchar(max),
	IsTrash bit default 0,
	CreatedAt datetime DEFAULT GETDATE(),
	UpdatedAt datetime DEFAULT GETDATE()
);

drop table Patients_Profile
--****************************************************************************************************************
														--Register Patient
alter proc usp_RegisterPatient
(
@fullName nvarchar(50),
@email nvarchar(50),
@contact bigint,
@address nvarchar(max),
@dob datetime,
@gender varchar(10),
@patientImage nvarchar(max)
)
as
begin

insert into Patients_Profile (FullName,Email,Contact,Address,DOB,Gender,PatientImage)
values (@fullName,@email,@contact,@address,@dob,@gender,@patientImage);

end

--********************************************************************************************
													--Fetch All Patients


alter proc usp_FetchAllPatients
as
begin
select * from Patients_Profile where IsTrash=0;
end


select * from Patients_Profile
--********************************************************************************************
											-- Fetch by Patient Id

create proc usp_FetchByPatientId
(
@patientid int
)
as
begin
select * from Patients_Profile where PatientId=@patientid;
end

exec usp_FetchByPatientId 1
--*************************************************************************************************************
												--Update Patient

create proc usp_UpdatePatient
(
@patientid int,
@fullName nvarchar(50),
@email nvarchar(50),
@contact bigint,
@address nvarchar(max),
@dob datetime,
@gender varchar(10),
@patientImage nvarchar(max)
)
as
begin
update Patients_Profile set FullName=@fullName,Email=@email,Contact=@contact,Address=@address,DOB=@dob,Gender=@gender,PatientImage=@patientImage,UpdatedAt=getdate() where PatientId=@patientid
end



--*************************************************************************************************************
													--Delete Patient
alter proc usp_DeletePatient
(
@patientid int
)
as
begin
update Patients_Profile set IsTrash=1 where PatientId=@patientid
end


--*************************************************************************************************************
											--Login Patient by patient id ,patient name 
alter proc usp_LoginPatient
(
@patientid int,
@patientName nvarchar(50)
)
as
begin 
begin try
select * from Patients_Profile where PatientId=@patientid and FullName=@patientName
--RAISERROR(N'Patient not found for id: %d', 16, 1, @patientid);
end try
begin catch
throw;
end catch
end;
drop proc usp_LoginPatient
exec usp_LoginPatient 3, 'santhosh'

select * from Patients_Profile


--*************************************************************************************************************
													--Appointment Entity

create table Appointments
(
AppointmentId int primary key identity,
Doctor_Id int foreign key (Doctor_Id) references Doctors_Profile (Doctor_Id),
PatientId int foreign key (PatientId) references Patients_Profile(PatientId),
AppointmentDate datetime not null,
StartTime datetime not null,
EndTime datetime not null,
Concerns nvarchar(max) not null
)

--*************************************************************************************************************
													--Create Appointment 
ALTER PROCEDURE usp_CreateAppointment
(
    @doctorid INT,
    @patientid INT,
    @appointmentdate DATETIME,
    @starttime DATETIME,
    @endtime DATETIME,
    @concerns NVARCHAR(MAX)
)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Patients_Profile WHERE PatientId = @patientid)
    BEGIN
        IF EXISTS (SELECT 1 FROM Doctors_Profile WHERE Doctor_Id = @doctorid)
        BEGIN
            INSERT INTO Appointments 
            (
                Doctor_Id, 
                PatientId, 
                AppointmentDate, 
                StartTime, 
                EndTime, 
                Concerns
            ) 
            VALUES 
            (
                @doctorid, 
                @patientid, 
                @appointmentdate, 
                @starttime, 
                @endtime, 
                @concerns
            );
        END;
    END;
END;


exec usp_CreateAppointment 9,3,'30-05-2024','10:00','10:30','stomach pain';

select * from patients_profile


--*************************************************************************************************************
													-- Get All Appointments
create proc usp_GetAllAppointments
as
begin
select * from Appointments
end

exec usp_GetAllAppointments
--*************************************************************************************************************
alter proc GetDoctorAndPatientProfiles
as
begin
    select 
        d.fullName as DoctorName,
        d.DoctorImage,
        p.PatientId,
        p.FullName as PatientName,
        p.Email as PatientEmail,
        p.Contact as PatientContact,
        p.Address as PatientAddress,
        p.DOB as PatientDOB,
        p.Age as PatientAge,
        p.Gender as PatientGender,
        p.PatientImage,
        p.IsTrash as PatientIsTrash,
        p.CreatedAt as PatientCreatedAt,
        p.UpdatedAt as PatientUpdatedAt
    from 
        Appointments a 
    inner join 
        Doctors_Profile d ON a.Doctor_Id = d.Doctor_Id
    inner join 
        Patients_Profile p ON a.PatientId = p.PatientId
    where 
        d.IsTrash = 0 AND p.IsTrash = 0;
end

--*************************************************************************************************************























