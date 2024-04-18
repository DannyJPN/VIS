

create table [InsuranceCompany]
(
[ID] integer not null identity,
[Number] integer not null,
[Abbreviation] nvarchar(5) not null,
[Fullname] nvarchar(100) not null,
constraint inscomp_pk primary key ([ID])



)
go
create table [Parent]
(
[ID] integer not null identity,
[Name] nvarchar(40) null,
[Surname] nvarchar(50) not null,
[Phone] integer null,
[Email] nvarchar(60) null,
[Job] nvarchar (30) null,
[Gender] char(1) not null,
constraint parent_pk primary key ([ID])


)
go

create table [GroupLeader]
(
[ID] integer not null identity,
[Name] nvarchar(40) null,
[Surname] nvarchar(50) not null,
[Phone] integer null,
[Email] nvarchar(60) null,
constraint gleader_pk primary key (ID)


)
go
create table [HobbyGroup]
(
[ID] integer not null identity,
[Name] nvarchar(40) null,
[Day] nchar(9) null,
[Min]integer null,
[Max] integer null,
[MinAge]integer null,
[MaxAge] integer null,
[SeasonOfExistence] nchar(9) null,

[From] time null,
[To] time null,
constraint hbgroup_pk primary key (ID)


)
go

create table [GroupLeading]
(
[ID] integer not null identity,
[From] date null,
[To] date null,

[GroupID] integer not null,
[LeaderID] integer not null,
constraint leadingperson_fk FOREIGN KEY ([LeaderID])        REFERENCES [GroupLeader] ([ID]),
constraint leadedgroup_fk FOREIGN KEY ([GroupID])        REFERENCES [HobbyGroup]([ID]),
)
go


create table [City]
(
[ID] integer not null identity,
[Name] nvarchar(40) not null,
constraint city_pk primary key ([ID])


)
create table [CityPart]
(
[ID] integer not null identity,
[Name] nvarchar(40) not null,
[OriginCityID] integer null,
constraint citypart_pk primary key ([ID]),
constraint citypart_tocity_fk FOREIGN KEY ([OriginCityID])        REFERENCES City ([ID]),

)



create table [Address]
(
[ID] integer not null identity,
[Street] nvarchar(60) null,
 [OrientationNumber] integer null, [DescriptionNumber] integer null,
 [HomeCityID] integer null,
 [HomeCityPartID] integer null,
 [PostalCode] integer null,

constraint homeadd_pk primary key ([ID]),
constraint city_fk FOREIGN KEY ([HomeCityID])        REFERENCES [City] ([ID]),
constraint citypart_fk FOREIGN KEY ([HomeCityPartID])        REFERENCES [CityPart] ([ID])


)
go


create table [Child]
(
[ID] integer not null identity,
[RegistrationNumber] integer not null,
[Name] nvarchar(40) null,
[Surname] nvarchar(50) not null,
[Phone] integer null,
[Email] nvarchar(60) null,
[HealthState] nvarchar(60) null,
[Comments] nvarchar(200) null,
[Birthdate] date null,
[SchoolName] nvarchar(70) null,
[MotherID] integer null,
[FatherID] integer null,
[HomeAddressID] integer null,
[CompanyID] integer null,
constraint child_pk primary key ([ID]),  
constraint child_mother_fk FOREIGN KEY ([MotherID])        REFERENCES [Parent] ([ID]),
constraint child_father_fk FOREIGN KEY ([FatherID])        REFERENCES [Parent] ([ID]),
constraint address_fk FOREIGN KEY ([HomeAddressID])        REFERENCES [Address] ([ID]),
constraint insurance_fk FOREIGN KEY ([CompanyID])        REFERENCES [InsuranceCompany] ([ID]),


)
go

create table [GroupMembership]
(
[ID] integer not null identity,
[MemberID] integer not null,
[GroupID] integer not null,
[From] date null,
[To] date null,
constraint child_group_fk FOREIGN KEY ([MemberID])        REFERENCES Child ([ID]),
constraint visited_group_fk FOREIGN KEY ([GroupID])        REFERENCES HobbyGroup ([ID]),



)